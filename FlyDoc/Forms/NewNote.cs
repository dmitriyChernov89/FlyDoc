using FlyDoc.Lib;
using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FlyDoc.Forms
{
    // форма добавления сл.зап.
    public partial class NewNote : Form
    {
        private string Help;

        private Note _note;
        public Note Note { get { return _note; } }

        private NoteTemplate _template;
        private string[] _approversList;

        private PropertyInfo[] _propInfoNote;
        private PropertyInfo[] _propInfoUser;

        private bool _isNew;


        public NewNote(Note note)
        {
            InitializeComponent();

            _isNew = (note == null);
            if (_isNew) _note = new Note(); else _note = note;

            AppFuncs.openEditForm(this.GetType().Name, _isNew);

            // держать в поле PropertyInfo[] для быстрого доступа к значениям через рефлексию
            _propInfoNote = typeof(Note).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            _propInfoUser = typeof(User).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        private void NewNote_Load(object sender, EventArgs e)
        {
            // получить данные и настроить комбобокс шаблонов сл.зап.
            FormsHelper.SetNoteTemplatesComboBox(cbNoteTemplate);
            // получить данные и настроить комбобокс отделов
            FormsHelper.SetDepartmentsComboBox(cbDepartment);

            setNoteIncludeTable();

            // режим добавления
            if (_isNew)
            {
                this.Text = "Додати новий рядок";
                this.cbNoteTemplate.SelectedIndexChanged += new System.EventHandler(this.cbNoteTemplate_SelectedIndexChanged);
                cbNoteTemplate.Enabled = true;
                cbNoteTemplate_SelectedIndexChanged(null, null);
            }
            // режим редактирования
            else
            {
                setControlsForEdit();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            AppFuncs.closeEditForm(this.GetType().Name, e.CloseReason);
            base.OnFormClosed(e);
        }

        private void setNoteIncludeTable()
        {
            bool isNoRows = true;
            // заполнить таблицу реальными данными или фиктивной записью (для создания колонок таблицы)
            BindingList<NoteInclude> dgvSource = new BindingList<NoteInclude>();
            if (_isNew == false)
            {
                if (_note.Include != null)
                {
                    isNoRows = false;
                    _note.Include.ForEach(i => dgvSource.Add(i));
                }
                else
                    dgvSource.Add(new NoteInclude());
            }
            else
                dgvSource.Add(new NoteInclude());

            dgvTable.DataSource = dgvSource;

            // заголовки столбцов
            string sHeader;
            foreach (DataGridViewColumn col in dgvTable.Columns)
            {
                sHeader = NoteInclude.GetHeaderByName(col.Name);
                if (sHeader.IsNull() == false) col.HeaderText = sHeader;
            }

            if (isNoRows) dgvSource.Clear();
        }

        // настройка контролов для редактирования
        private void setControlsForEdit()
        {
            this.Text = "Редагувати рядок";

            // установить значения контролов
            cbNoteTemplate.SelectedValue = _note.Templates;
            cbDepartment.SelectedValue = _note.IdDepartment;
            dtpDateCreate.Value = _note.Date;
            this.tbHeadDir.Text = _note.HeadDir;
            this.tbHeadNach.Text = _note.HeadNach;
            this.tbNumber.Text = _note.Id.ToString();
            this.tbBodyUp.Text = _note.BodyUp;
            this.tbBodyDown.Text = _note.BodyDown;
            this.tbAvtor.Text = _note.NameAvtor;

            // закрыть для доступа все TextBox/ComboBox контролы
            bool isEnable = (Program.User.ApprAvtor && (Program.User.Department == _note.IdDepartment));
            // если это автор, проверить, утвердил ли он служебку
            if (isEnable) isEnable = !_note.ApprAvtor;
            foreach (Control ctl in this.Controls)
            {
                if ((ctl is TextBox) && (ctl is ComboBox)) ctl.Enabled = isEnable;
            }
            // текст служебки
            setBodyControlsEnable(isEnable);

            // группа кнопок согласователей
            this.grpApprove.Enabled = true;

            _template = _note.Template;
            setControlForTemplate();
        }

        private List<string> getDGVColNames()
        {
            List<string> names = new List<string>();
            foreach (DataGridViewColumn item in dgvTable.Columns)
            {
                if (item.Name.StartsWith("ColumName") == false) names.Add(item.Name);
            }
            return names;
        }

        // обработчик кнопки по умолчанию - Ок
        private void btnSaveToDB_Click(object sender, EventArgs e)
        {
            AppFuncs.WriteLogTraceMessage("   - press button 'Записати'");
            if (checkInput() == false)
            {
                AppFuncs.WriteLogTraceMessage("   - check input not passed, no action");

                this.DialogResult = DialogResult.None;
                return;
            }

            // передать в вызывающий модуль значения полей в объекте Note
            if (_isNew)
            {
                _note.Templates = (int)cbNoteTemplate.SelectedValue;
                _note.IdDepartment = (int)cbDepartment.SelectedValue;
                _note.Date = dtpDateCreate.Value;
                _note.HeadDir = this.tbHeadDir.Text;
                _note.HeadNach = this.tbHeadNach.Text;
            }
            _note.NameAvtor = this.tbAvtor.Text;
            _note.BodyUp = this.tbBodyUp.Text;
            _note.BodyDown = this.tbBodyDown.Text;

            if ((dgvTable.Visible == true) && ((dgvTable.Rows.Count-1) > 0))
            {
                if (_note.Include == null) _note.Include = new List<NoteInclude>();
                else _note.Include.Clear();

                IList<NoteInclude> inclList = (IList<NoteInclude>)dgvTable.DataSource;
                foreach (NoteInclude item in inclList) _note.Include.Add(item);

            }

        }  // method

        private bool checkInput()
        {
            // проверка номера служебки
            //if (tbNumber.Text.IsNull() == true)
            //{
            //    MessageBox.Show("Введите номер служебки", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    tbNumber.Focus();
            //    return false;
            //}
            // проверка даты служебки
            //DateTime dtCreate = DateTime.MinValue;
            //if (DateTime.TryParse(tbTimeCreate.Text, out dtCreate) == false)
            //{
            //    MessageBox.Show("Дата создания служебной записки должна иметь формат: dd.MM.yyyy HH:mm:ss", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    tbTimeCreate.Focus();
            //    return false;
            //}
            // проверка комбобокса отдела
            if (cbDepartment.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите отдел", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbDepartment.Focus();
                return false;
            }
            // проверка комбобокса шаблона сл.зап.
            if (cbNoteTemplate.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите шаблон служебной записки", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbNoteTemplate.Focus();
                this.DialogResult = DialogResult.None;
                return false;
            }

            // все Ок
            return true;
        }

        private void cbNoteTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpDateCreate.Value = DateTime.Now;
            cbDepartment.SelectedValue = Program.User.Department;

            _note.Templates = cbNoteTemplate.SelectedValue.ToInt();
            _note.Approvers = null;
            _template = new NoteTemplate(_note.Templates);

            this.tbHeadDir.Text = _template.HeadDir;
            this.tbHeadNach.Text = _template.HeadNach + Program.User.HeadNach;
            this.tbBodyUp.Text = _template.BodyUp;
            this.tbBodyDown.Text = _template.BodyDown;
            this.tbAvtor.Text = Program.User.Name;

            setControlForTemplate();
        }

        // контролы, зависящие от шаблона
        // row - строка из NoteTemplates
        private void setControlForTemplate()
        {
            this.Help = _template.Help;

            // дополнительные табличные данные
            if (_template.TableColums == 0)
            {
                dgvTable.Visible = false;
                tbBodyDown.Visible = false;
            }
            else
            {
                if (_isNew) dgvTable.Rows.Clear();

                dgvTable.Visible = true;
                tbBodyDown.Visible = true;

                // скрыть все столбцы
                foreach (DataGridViewColumn item in dgvTable.Columns) item.Visible = false;
                // заполнить коллекцию полей
                _note.ResetIncludeFields(_template);
                // отобразить только те, которые есть в шаблоне
                foreach (string colName in _note.IncludeFields)
                {
                    if (colName.IsNull()) continue;
                    dgvTable.Columns[colName].Visible = true;
                }
            }

            // если в служебке поле Approvers пустое, то взять согласователей из шаблона
            if (_note.Approvers.IsNull()) setApproversFromTemplate();
            // и обновить кнопки
            setApprButtonsVisible();
        }

        #region btn_approved_and_notApproved
        private void setApproversFromTemplate()
        {
            string sBuf = "";
            PropertyInfo[] pInfos = typeof(NoteTemplate).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo item in pInfos)
            {
                if ((item.Name.StartsWith("Appr")) && (Convert.ToBoolean(item.GetValue(_template, null))))
                {
                    if (sBuf.Length > 0) sBuf += ";";
                    sBuf += item.Name;
                }
            }
            _note.Approvers = sBuf;
        }

        private void setApprButtonsVisible()
        {
            // сначал скрыть все кнопки согласователей, кроме автора
            foreach (Control item in grpApprove.Controls)
            {
                if ((item is Button) && (item.Name.StartsWith("btnAppr"))) item.Visible = false;
            }

            // разобрать Approvers в массив
            _approversList = (_note.Approvers ?? "").Split(';');
            updateApprDependFields();

            string key;
            foreach (string item in _approversList)
            {
                key = "btn" + item;
                if (grpApprove.Controls.ContainsKey(key)) grpApprove.Controls[key].Visible = true;
            }

            // статус кнопок согласователей
            setApprButtonState();
        }

        private void setApprButtonsEnable(bool enable, params string[] exceptButtonNames)
        {
            foreach (string item in _approversList)
            {
                if ((exceptButtonNames != null) && (exceptButtonNames.Contains(item))) continue;

                string btnName = "btn" + item;
                PropertyInfo pi = _propInfoUser.FirstOrDefault(p => p.Name == item);
                bool itemValue = (bool)pi.GetValue(Program.User, null);
                grpApprove.Controls[btnName].Enabled = enable && itemValue;
            }

        }

        // клик по кнопке согласователя
        private void btnApprField_Click(object sender, EventArgs e)
        {
            setApprFieldByButton((Button)sender);
        }

        private void setApprFieldByButton(Button button)
        {
            string apprFieldName = button.Tag.ToString();
            bool apprValue = getNoteApprFieldValue(apprFieldName);
            apprValue = !apprValue;
            setNoteApprFieldValue(apprFieldName, apprValue);

            updateApprDependFields();

            // кнопки
            setApprButtonState();
            if (!apprValue)
            {
                button.BackColor = Color.Magenta;
            }
        }

        private void btnEditApprovers_Click(object sender, EventArgs e)
        {
            string preValue = _note.Approvers;

            NoteApproversEdit frm = new NoteApproversEdit();
            frm.ApproversText = _note.Approvers;

            frm.ShowDialog();

            // получить и распарсить новый список согласователей
            if (frm.ApproversText != _note.Approvers)
            {
                _note.Approvers = frm.ApproversText;
                // обновить кнопки
                setApprButtonsVisible();
            }
        }

        // обновить зависимые поля согласователей
        private void updateApprDependFields()
        {
            // поле ApprAll зависит от значения всех согласователей
            bool bVal = isAllApproved();
            if (_note.ApprAll != bVal) _note.ApprAll = bVal;
        }

        // основная процедура отображающая состояние кнопок согласования в следующем порядке:
        // Автор - Нач.АСУ - все остальные, кроме Директора - Директор
        // с учетом прав доступа из Access
        private void setApprButtonState()
        {
            // цветность кнопок согласователей
            foreach (string item in _approversList)
            {
                string btnName = "btn" + item;
                if ((grpApprove.Controls.ContainsKey(btnName)) && getNoteApprFieldValue(item))
                {
                    setBtnLime((Button)grpApprove.Controls[btnName]);
                }
            }
            // надпись, что служебка ВСЕМИ утверждена
            labelApprAll.Visible = _note.ApprAll;

            // проверка по нисходящей: Автор - Директор - Нач.АСУ
            // настройка контролов: 
            //      1. есть ли в согласователях; 
            //      2. имеет ли право на согласование (доступность кнопки); 
            //      3. доступность подчиненных согласователей

            // АВТОР
            // доступность кнопки
            if (Program.User.ApprAvtor)
            {
                btnApprAvtor.Enabled = isAllNotApproved();
            }
            else
            {
                btnApprAvtor.Enabled = false;
            }
            // контролы, зависимые от согласования автором - тело служебки, кнопки всех остальных согласователей (все они находятся в группе grpApprove)
            if (_note.ApprAvtor == false)
            {
                grpApprove.Enabled = false;
                setBodyControlsEnable(true);
                return;
            }
            else
            {
                grpApprove.Enabled = true;
                setBodyControlsEnable(false);
                setBtnLime(btnApprAvtor);
            }

            // ДИРЕКТОР
            if (_approversList.Contains("ApprDir"))
            {
                // доступность кнопки
                if (Program.User.ApprDir)
                {
                    btnApprDir.Enabled = isAllApproved("ApprDir");
                }
                else
                {
                    btnApprDir.Enabled = false;
                }
                // доступность зависимых согласователей
                bool dependApprEnable = !_note.ApprDir;
                setApprButtonsEnable(dependApprEnable, "ApprDir");
            }
                

            // НАЧ.АСУ
            if (_approversList.Contains("ApprASU"))
            {
                // доступность кнопки
                if (Program.User.ApprASU)
                {
                    btnApprASU.Enabled = isAllNotApproved("ApprASU");
                }
                else
                {
                    btnApprASU.Enabled = false;
                }
                // доступность зависимых согласователей
                bool dependApprEnable = _note.ApprASU;
                setApprButtonsEnable(dependApprEnable, "ApprASU", "ApprDir");
            }

            // есть в согласователях кто-то еще, кроме Дир и НачАСУ
            List<string> apprRest = _approversList.Except(new string[] { "ApprASU", "ApprDir" }).ToList();
            if (apprRest.Count > 0) 
            {
                foreach (string item in apprRest)
                {
                    string btnName = "btn" + item;
                    PropertyInfo pi = _propInfoUser.FirstOrDefault(p => p.Name == item);
                    bool itemValue = (bool)pi.GetValue(Program.User, null);
                    grpApprove.Controls[btnName].Enabled = itemValue;
                }
            }
        }

        // возвращает true, если ВСЕ согласовали (кроме exceptionApprName)
        private bool isAllApproved(string exceptionApprName = null)
        {
            bool bVal = true;
            foreach (string item in _approversList)
            {
                if ((exceptionApprName.IsNull() == false) && item.Equals(exceptionApprName)) continue;
                bVal &= getNoteApprFieldValue(item);
            }

            return bVal;
        }
        // возвращает true, если никто еще не согласовал (кроме exceptionApprName)
        private bool isAllNotApproved(string exceptionApprName = null)
        {
            bool bVal = false;
            foreach (string item in _approversList)
            {
                if ((exceptionApprName.IsNull() == false) && item.Equals(exceptionApprName)) continue;
                bVal |= getNoteApprFieldValue(item);
            }

            return (bVal == false);
        }

        // получить статус согласователя по имени поля из _note
        private bool getNoteApprFieldValue(string apprFieldsName)
        {
            PropertyInfo pi = _propInfoNote.FirstOrDefault(p => p.Name == apprFieldsName);
            if (pi != null)
                return System.Convert.ToBoolean(pi.GetValue(_note, null));
            else
                return false;
        }

        // установить значение поля согласователя в _note по названию поля
        private void setNoteApprFieldValue(string apprFieldName, bool value)
        {
            PropertyInfo pi = _propInfoNote.FirstOrDefault(p => p.Name == apprFieldName);
            if (pi != null) pi.SetValue(_note, value, null);
        }

        private bool getAccessApprValue(string apprFieldsName)
        {
            PropertyInfo pi = _propInfoUser.FirstOrDefault(p => p.Name == apprFieldsName);
            if (pi != null)
                return System.Convert.ToBoolean(pi.GetValue(Program.User, null));
            else
                return false;
        }

        private void setBtnLime(Button btn)
        {
            btn.BackColor = Color.FromName("Lime");
        }

        private void setBodyControlsEnable(bool isEnable)  
        {
            tbBodyUp.Enabled = isEnable;
            dgvTable.Enabled = isEnable;
            tbBodyDown.Enabled = isEnable;
        }

        #endregion

        private void btnHelp_Click(object sender, EventArgs e)
        {
            AppFuncs.WriteLogTraceMessage(" - press button 'Довiдка'");
            MessageBox.Show(Help, "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        //Application.StartupPath, 

        private void btnPrint_Click(object sender, EventArgs e)
            {
            AppFuncs.WriteLogTraceMessage(" - press button 'Друк'");

            string mydocu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = string.Format(@"{0}\doc_{1}.pdf", mydocu, _note.Id);
            string sText;

            Document document = new Document();
            try
            {
                //@"C:\arial.ttf"
                BaseFont baseFont = BaseFont.CreateFont(@"\\192.168.46.206\FlyDoc\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font14 = new iTextSharp.text.Font(baseFont, 14f, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font font14Bold = new iTextSharp.text.Font(baseFont, 14f, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font12 = new iTextSharp.text.Font(baseFont, 12f, iTextSharp.text.Font.NORMAL);

                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // шапка - директор
                    sText = _note.HeadDir; // _note.HeadNach;
                    Paragraph p = new Paragraph(sText, font14Bold);
                    p.IndentationLeft = 300f;
                    document.Add(p);
                    // шапка - нач.отд.
                    sText = _note.HeadNach;
                    p = new Paragraph(sText, font14Bold);
                    p.IndentationLeft = 300f;
                    p.SpacingBefore = 6f;
                    document.Add(p);

                    // номер служебки
                    sText = "Службова записка №: " + _note.Id;
                    p = new Paragraph(sText, font14Bold);
                    p.SpacingBefore = 20f; p.SpacingAfter = 10f;
                    p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    document.Add(p);

                    // текст 1
                    if (_note.BodyUp != null)
                    {
                        sText = _note.BodyUp;
                        p = new Paragraph(sText, font14);
                        p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        p.FirstLineIndent = 20f;
                        document.Add(p);
                    }
                    
                    // TODO доделать вывод табилицы
                    // таблицу взять из DataGridView
                    if (dgvTable.Visible == true)
                    {
                        List<PropertyInfo> propInfos = typeof(NoteInclude).GetProperties().ToList();

                        int colVisCount = 0;
                        foreach (DataGridViewColumn col in dgvTable.Columns) if (col.Visible) colVisCount++;

                        PdfPTable table = new PdfPTable(colVisCount);

                        table.SpacingBefore = 20f;
                        table.SpacingAfter = 20f;
                        table.WidthPercentage = 100;
                        table.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        //table.WidthPercentage
                        foreach (DataGridViewColumn col in dgvTable.Columns)
                        {
                            if (col.Visible) table.AddCell(new Paragraph(col.HeaderText, font12));
                        }

                        foreach (DataGridViewRow row in dgvTable.Rows)
                            foreach (DataGridViewColumn col in dgvTable.Columns)
                                if (col.Visible)
                                {
                                    object oValue = row.Cells[col.Name].Value;
                                    string cellValue = oValue.ToStringNull();

                                    table.AddCell(new Paragraph(cellValue, font12));
                                }


                        document.Add(table);

                        //PdfPCell cell = new PdfPCell(new Phrase("Simple table",
                        //  new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16,
                        //  iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                        //cell.BackgroundColor = new BaseColor(Color.Wheat);
                        //cell.Padding = 5;
                        //cell.Colspan = 3;
                        //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //table.AddCell(cell);

                        //table.AddCell("Col 1 Row 1");
                        //table.AddCell("Col 2 Row 1");
                        //table.AddCell("Col 3 Row 1");
                        //table.AddCell("Col 1 Row 2");
                        //table.AddCell("Col 2 Row 2");
                        //table.AddCell("Col 3 Row 2");


                        //foreach (NoteInclude incl in _note.Include)
                        //{
                        //    foreach (string fldName in _note.IncludeFields)
                        //    {
                        //        propInfo = propInfos.FirstOrDefault(prp => prp.Name == fldName);
                        //        object obj = propInfo.GetValue(this, null);
                        //    }

                        //}
                    }

                    // текст 2
                    if (_note.BodyDown != null)
                    {
                        sText = _note.BodyDown;
                        p = new Paragraph(sText, font14);
                        p.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        p.FirstLineIndent = 20f;
                        document.Add(p);
                    }

                    document.Close();
                    AppFuncs.WriteLogTraceMessage($"file '{fileName}' created success");
                }
            }
            catch (DocumentException ex)
            {
                AppFuncs.WriteLogErrorMessage(ex.ToString());
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                AppFuncs.WriteLogErrorMessage(ex.ToString());
                Console.WriteLine(ex.Message);
            }
           
            ProcessStartInfo pInfo = new ProcessStartInfo(fileName);
            pInfo.UseShellExecute = true;
            Process process = Process.Start(pInfo);
        }

        private void dgvTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            AppFuncs.WriteLogErrorMessage("Ошибка ввода: " + e.Exception.Message);

            MessageBox.Show("Ошибка ввода: " + e.Exception.Message, "Проверка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }  // class
}
