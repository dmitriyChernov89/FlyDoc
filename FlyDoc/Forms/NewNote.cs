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

namespace FlyDoc.Forms
{
    // форма добавления сл.зап.
    public partial class NewNote : Form
    {
        private Note _note;
        private string Help;
        public Note Note { get { return _note; } }


        public NewNote(Note note)
        {
            InitializeComponent();
            _note = note;
        }

        private void NewNote_Load(object sender, EventArgs e)
        {
            // получить данные и настроить комбобокс отделов
            FormsHelper.SetNoteTemplatesComboBox(cbNoteTemplate);
            FormsHelper.SetDepartmentsComboBox(cbDepartment);

            // получить данные и настроить комбобокс шаблонов сл.зап.
            //if (MainForm.WhichBtnClick == 1)
            //{
            //}

            if (_note != null)
            {
                this.Text = "Редагувати рядок";

                cbNoteTemplate.Enabled = false;
                DataRow drTemplate = DBContext.GetNoteTemplatesConfig(_note.NoteTemplateId);
                if (drTemplate != null) setControlForTemplate(drTemplate);

                this.tbNumber.Text = _note.Id.ToString();
                cbNoteTemplate.SelectedValue = _note.NoteTemplateId;
                cbDepartment.SelectedValue = _note.DepartmentId;
                dtpDateCreate.Value = _note.Date;

                this.tbAvtor.Text = _note.NameAvtor;

                //Имена утвердивших в журнале, тут хз куда лепить и надо ли
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
            // Добавити новий рядок
            else
            {
                this.Text = "Добавити новий рядок";
                this.cbNoteTemplate.SelectedIndexChanged += new System.EventHandler(this.cbNoteTemplate_SelectedIndexChanged);
                cbNoteTemplate.Enabled = true;
                cbNoteTemplate_SelectedIndexChanged(null, null);
            }
        }

        private void setBtnLime(Button btn)
        {
            btn.BackColor = Color.FromName("Lime");
        }

        // обработчик кнопки по умолчанию - Ок
        private void btnOk_Click(object sender, EventArgs e)
        {
            #region проверка введенных данных
            // проверка номера служебки
            //if (tbNumber.Text.IsNull() == true)
            //{
            //    MessageBox.Show("Введите номер служебки", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    tbNumber.Focus();
            //    this.DialogResult = DialogResult.None;
            //    return;
            //}
            // проверка даты служебки
            //DateTime dtCreate = DateTime.MinValue;
            //if (DateTime.TryParse(tbTimeCreate.Text, out dtCreate) == false)
            //{
            //    MessageBox.Show("Дата создания служебной записки должна иметь формат: dd.MM.yyyy HH:mm:ss", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    tbTimeCreate.Focus();
            //    this.DialogResult = DialogResult.None;
            //    return;
            //}
            // проверка комбобокса отдела
            if (cbDepartment.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите отдел", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbDepartment.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            // проверка комбобокса шаблона сл.зап.
            if (cbNoteTemplate.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите шаблон служебной записки", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbNoteTemplate.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            #endregion

            // собрать строку для добавления новой записи в сл.зап.
            string sqlText = string.Format("INSERT INTO Notes (Templates, IdDepartment, Date, NameAvtor, BodyUp, BodyDown, HeadNach, HeadDir) VALUES ('{0}', '{1}',CONVERT(datetime, '{2}', 20),'{3}','{4}','{5}','{6}','{7}')", cbNoteTemplate.SelectedValue, cbDepartment.SelectedValue, dtpDateCreate.Value.ToString("yyyy-MM-dd HH:mm:ss"), tbAvtor.Text, tbBodyUp.Text, tbBodyDown.Text, tbHeadNach.Text, tbHeadDir.Text);

            if (DBContext.Execute(sqlText) == true)
            {
                MessageBox.Show("Запись добавлена успешно","Добавление служебки",MessageBoxButtons.OK);
                Form f = Application.OpenForms[0];
                //if (f is MainForm)
                //{
                //    MainForm mainForm = (f as MainForm);
                //    mainForm.ReloadData();
                //}
            }

        }  // method

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

            if (dr["TableColums"].ToInt() == 0)
            {
                dgvTable.Visible = false;
                tbBodyDown.Visible = false;
            }
            else
            {
                dgvTable.Visible = true;
                tbBodyDown.Visible = true;
                dgvTable.ColumnCount = dr["TableColums"].ToInt();
                dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                for (int i = 0; i < (int)dr["TableColums"]; i++)
                {
                    dgvTable.Columns[i].Name = (string)dr["ColumName" + (i + 1).ToString()];
                    //DataGridViewColumn dgvColumn = dgvTable.Columns[i];
                    //dgvColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }

                //dataGridView1.Columns[0].Visible = false;
                //dataGridView1.ReadOnly = true;
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

        private void btnApprDir_Click(object sender, EventArgs e)
        {
            DBContext.NoteApproved(_note.Id, "[ApprDir]", true);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Help, "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnApprAvtor_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dtpDateCreate.Value.TimeOfDay.ToString());
        }

        private void btnPrint_Click(object sender, EventArgs e)
            {
            string fileName = string.Format(@"{0}\doc_{1}.pdf", Application.StartupPath, _note.Id);
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
                    
                    // TODO таблиця

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


        }  // class
}
