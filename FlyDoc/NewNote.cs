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

namespace FlyDoc
{
    // форма добавления сл.зап.
    public partial class NewNote : Form
    {
        public NewNote()
        {
            InitializeComponent();

            // значения по умолчанию для новой сл.зап.
            tbDateCreate.Text = DateTime.Now.ToString();
        }

        private void NewNote_Load(object sender, EventArgs e)
        {
            // получить данные и настроить комбобокс отделов
            DataTable dtDeps = DBContext.GetDepartments();
            if (dtDeps != null)
            {
                cbDepartment.DataSource = dtDeps;
                cbDepartment.DisplayMember = "Name";
                cbDepartment.ValueMember = "Id";
            }

            // получить данные и настроить комбобокс шаблонов сл.зап.
            DataTable dtNoteTemplates = DBContext.GetNoteTemplates();
            if (dtNoteTemplates != null)
            {
                cbNoteTemplate.DataSource = dtNoteTemplates;
                cbNoteTemplate.DisplayMember = "Name";
                cbNoteTemplate.ValueMember = "Id";
            }
        }

        // обработчик кнопки по умолчанию - Ок
        private void btnOk_Click(object sender, EventArgs e)
        {
            #region проверка введенных данных
            // проверка номера служебки
            if (tbNumber.Text.IsNullOrEmpty() == true)
            {
                MessageBox.Show("Введите номер служебки", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbNumber.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
            // проверка даты служебки
            DateTime dtCreate = DateTime.MinValue;
            if (DateTime.TryParse(tbDateCreate.Text, out dtCreate) == false)
            {
                MessageBox.Show("Дата создания служебной записки должна иметь формат: dd.MM.yyyy HH:mm:ss", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDateCreate.Focus();
                this.DialogResult = DialogResult.None;
                return;
            }
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
            string sqlText = string.Format("INSERT INTO Note (Number, DateCreate, DepartmentId) VALUES ('{0}', CONVERT(datetime, '{1}', 20), {2})", tbNumber.Text, dtCreate.ToString("yyyy-MM-dd HH:mm:ss"), cbDepartment.SelectedValue.ToString());

            if (DBContext.Execute(sqlText) == true)
            {
                MessageBox.Show("Запись добавлена успешно","Добавление служебки",MessageBoxButtons.OK);
                Form f = Application.OpenForms[0];
                if (f is FlyDoc)
                {
                    FlyDoc mainForm = (f as FlyDoc);
                    mainForm.ReloadData();
                }
               
            }

        }  // method
    }  // class
}
