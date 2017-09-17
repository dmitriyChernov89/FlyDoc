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

        private bool _isNew;


        public NewNote(Note note)
        {
            InitializeComponent();

            _isNew = (note == null);
            if (_isNew) _note = new Note(); else _note = note;
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

        private void setControlsForEdit()
        {
            this.Text = "Редагувати рядок";

            // настроить контролы для данного шаблона
            cbNoteTemplate.Enabled = false;
            DataRow drTemplate = DBContext.GetNoteTemplatesConfig(_note.NoteTemplateId);
            if (drTemplate != null) setControlForTemplate(drTemplate);

            this.tbNumber.Text = _note.Id.ToString();
            cbNoteTemplate.SelectedValue = _note.NoteTemplateId;
            cbDepartment.SelectedValue = _note.DepartmentId;
            dtpDateCreate.Value = _note.Date;

            this.tbAvtor.Text = _note.NameAvtor;

            //Имена утвердивших в журнале, тут хз куда лепить и надо ли

            // цветность кнопок утверждения
            if (_note.ApprAvtor) setBtnLime(btnApprAvtor);
            if (_note.ApprDir) setBtnLime(btnApprDir);
            if (_note.ApprComdir) setBtnLime(btnApprComdir);
            if (_note.ApprSBNach) setBtnLime(btnApprSBNach);
            if (_note.ApprSB) setBtnLime(btnApprSB);
            if (_note.ApprKasa) setBtnLime(btnApprKasa);
            if (_note.ApprNach) setBtnLime(btnApprNach);
            if (_note.ApprFin) setBtnLime(btnApprFin);
            if (_note.ApprDostavka) setBtnLime(btnApprDostavka);
            if (_note.ApprEnerg) setBtnLime(btnApprEnerg);
            if (_note.ApprSklad) setBtnLime(btnApprSklad);
            if (_note.ApprBuh) setBtnLime(btnApprBuh);
            if (_note.ApprASU) setBtnLime(btnApprASU);

            labelApprAll.Visible = _note.ApprAll;

            this.tbBodyUp.Text = _note.BodyUp;
            this.tbBodyDown.Text = _note.BodyDown;
            this.tbHeadDir.Text = _note.HeadDir;
            this.tbHeadNach.Text = _note.HeadNach;
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

        private void setBtnLime(Button btn)
        {
            btn.BackColor = Color.FromName("Lime");
        }

        // обработчик кнопки по умолчанию - Ок
        private void btnSaveToDB_Click(object sender, EventArgs e)
        {
            if (checkInput() == false)
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            // передать в вызывающий модуль значения полей в объекте Note
            if (_isNew)
            {
                _note.NoteTemplateId = (int)cbNoteTemplate.SelectedValue;
                _note.DepartmentId = (int)cbDepartment.SelectedValue;
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
            cbDepartment.SelectedValue = MainForm._currentDepId.ToString();

            int tplId = cbNoteTemplate.SelectedValue.ToInt();
            DataRow dr = DBContext.GetNoteTemplatesConfig(tplId);
            if (dr != null)
            {
                this.tbHeadDir.Text = dr["HeadDir"].ToStringNull();
                this.tbHeadNach.Text = dr["HeadNach"].ToStringNull() + MainForm.headNach;
                this.tbBodyUp.Text = dr["BodyUp"].ToStringNull();
                this.tbBodyDown.Text = dr["BodyDown"].ToStringNull();
                this.tbAvtor.Text = MainForm.name;

                setControlForTemplate(dr);
            }
        }

        // контролы, зависящие от шаблона
        // row - строка из NoteTemplates
        private void setControlForTemplate(DataRow dr)
        {
            this.Help = dr["Help"].ToStringNull();

            // дополнительные табличные данные
            if (dr["TableColums"].ToInt() == 0)
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
                _note.ResetIncludeFields(dr);
                // отобразить только те, которые есть в шаблоне
                foreach (string colName in _note.IncludeFields)
                {
                    dgvTable.Columns[colName].Visible = true;
                }
            }

            btnApprDir.Visible = dr["ApprDir"].ToBool();
            btnApprComdir.Visible = dr["ApprComdir"].ToBool();
            btnApprSBNach.Visible = dr["ApprSBNach"].ToBool();
            btnApprSB.Visible = dr["ApprSB"].ToBool();
            btnApprKasa.Visible = dr["ApprKasa"].ToBool();
            btnApprNach.Visible = dr["ApprNach"].ToBool();
            btnApprFin.Visible = dr["ApprFin"].ToBool();
            btnApprDostavka.Visible = dr["ApprDostavka"].ToBool();
            btnApprEnerg.Visible = dr["ApprEnerg"].ToBool();
            btnApprSklad.Visible = dr["ApprSklad"].ToBool();
            btnApprBuh.Visible = dr["ApprBuh"].ToBool();
            btnApprASU.Visible = dr["ApprASU"].ToBool();
        }

        #region btn_approved_and_notApproved
        private void btnApprDir_Click(object sender, EventArgs e)
        {
            if (btnApprDir.BackColor == Color.Lime)
            {
                DBContext.NoteApprovedDir(_note.Id, "[ApprDir]", true);
                btnApprDir.BackColor = Color.Magenta;
            }
            else
            {
                DBContext.NotNoteApprovedDir(_note.Id, "[ApprDir]", true);
                btnApprDir.BackColor = Color.Lime;
            }
        }

        private void btnApprComdir_Click(object sender, EventArgs e)
        {
            if (btnApprComdir.BackColor == Color.Lime)
            {
                DBContext.NoteApprovedCom(_note.Id, "[ApprDir]", true);
                btnApprComdir.BackColor = Color.Magenta;
            }
            else
            {
                DBContext.NotNoteApprovedCom(_note.Id, "[ApprDir]", true);
                btnApprComdir.BackColor = Color.Lime;
            }
        }

        #endregion

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Help, "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnApprAvtor_Click(object sender, EventArgs e)
        {
            DBContext.NoteApprovedAvtor(_note.Id, "[ApprAvtor]", true);
            btnApprAvtor.BackColor = Color.Lime;
            // MessageBox.Show(dtpDateCreate.Value.TimeOfDay.ToString());
        }
        //Application.StartupPath, 

        private void btnPrint_Click(object sender, EventArgs e)
            {
            string mydocu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = string.Format(@"{0}\doc_{1}.pdf", mydocu, _note.Id);
            string sText;

            Document document = new Document();
            try
            {
                BaseFont baseFont = BaseFont.CreateFont(@"C:\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
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
                        PropertyInfo propInfo;

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
                }
            }
            catch (DocumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            ProcessStartInfo pInfo = new ProcessStartInfo(fileName);
            pInfo.UseShellExecute = true;
            Process process = Process.Start(pInfo);
        }

        private void dgvTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка ввода: " + e.Exception.Message, "Проверка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


    }  // class
}
