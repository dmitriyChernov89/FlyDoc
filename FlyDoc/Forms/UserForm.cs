using FlyDoc.Lib;
using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Forms
{
    public partial class UserForm : Form
    {
        private bool _isNew;
        private User _currentUser = null;
        public User User { get { return _currentUser; } }

        public UserForm(User pUser)
        {
            InitializeComponent();

            // получить данные и настроить комбобокс отделов
            FormsHelper.SetDepartmentsComboBox(cbxDepartment);

            // подписаться на события фокуса
            FormsHelper.SetFocusEventHandlers(this, Color.Yellow, Color.White);

            _isNew = (pUser == null);
            if (_isNew)
            {
                this.Text = "Створення нового користувача";
            }
            else
            {
                this.Text = "Редагування користувача";
                _currentUser = pUser;
                tbxPC.Text = _currentUser.PC;
                tbxUserName.Text = _currentUser.UserName;
                cbxDepartment.SelectedValue = _currentUser.Department;
                checkBoxNote.Checked = _currentUser.Notes;
                checkBoxSchedule.Checked = _currentUser.Schedule;
                checkBoxPhonebook.Checked = _currentUser.Phone;
                checkBoxConfig.Checked = _currentUser.Config;
                checkBoxApprovedN.Checked = _currentUser.ApprNach;
                checkBoxApprovedSB.Checked = _currentUser.ApprSB;
                checkBoxApprovedDir.Checked = _currentUser.ApprDir;
                checkBoxApprAvtor.Checked = _currentUser.ApprAvtor;
                checkBoxApprComdir.Checked = _currentUser.ApprComdir;
                checkBoxApprSBN.Checked = _currentUser.ApprSBNach;
                checkBoxApprKasa.Checked = _currentUser.ApprKasa;
                checkBoxApprFin.Checked = _currentUser.ApprFin;
                checkBoxApprDastavka.Checked = _currentUser.ApprDostavka;
                checkBoxApprEnerg.Checked = _currentUser.ApprEnerg;
                checkBoxApprSklad.Checked = _currentUser.ApprSklad;
                checkBoxApprBun.Checked = _currentUser.ApprBuh;
                checkBoxApprASU.Checked = _currentUser.ApprASU;
                tbxName.Text = _currentUser.Name;
                tbxHeadNach.Text = _currentUser.HeadNach;
                tbxMail.Text = _currentUser.Mail;
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isValidInput())
            {
                if (_isNew || isUpdate())
                {
                    if (_currentUser == null) _currentUser = new User();
                    _currentUser.PC = tbxPC.Text;
                    _currentUser.UserName = tbxUserName.Text;
                    _currentUser.Department = (int)cbxDepartment.SelectedValue;
                    _currentUser.Notes = checkBoxNote.Checked;
                    _currentUser.Schedule = checkBoxSchedule.Checked;
                    _currentUser.Phone = checkBoxPhonebook.Checked;
                    _currentUser.Config = checkBoxConfig.Checked;
                    _currentUser.ApprNach = checkBoxApprovedN.Checked;
                    _currentUser.ApprSB = checkBoxApprovedSB.Checked;
                    _currentUser.ApprDir = checkBoxApprovedDir.Checked;
                    _currentUser.Mail = tbxMail.Text;
                    _currentUser.ApprAvtor = checkBoxApprAvtor.Checked;
                    _currentUser.ApprComdir = checkBoxApprComdir.Checked;
                    _currentUser.ApprSBNach = checkBoxApprSBN.Checked;
                    _currentUser.ApprKasa = checkBoxApprKasa.Checked;
                    _currentUser.ApprFin = checkBoxApprFin.Checked;
                    _currentUser.ApprDostavka = checkBoxApprDastavka.Checked;
                    _currentUser.ApprEnerg = checkBoxApprEnerg.Checked;
                    _currentUser.ApprSklad = checkBoxApprSklad.Checked;
                    _currentUser.ApprBuh = checkBoxApprBun.Checked;
                    _currentUser.ApprASU = checkBoxApprASU.Checked;
                    _currentUser.Name = tbxName.Text;
                    _currentUser.HeadNach = tbxHeadNach.Text;

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
                _currentUser = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region validate input
        // проверка правильности ввода
        private bool isValidInput()
        {
            if (tbxPC.Text.IsNull())
            {
                MessageBox.Show("Введіть найменування компьютера", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxPC.Focus();
                return false;
            }
            if (tbxUserName.Text.IsNull())
            {
                MessageBox.Show("Введіть найменування користувача", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxUserName.Focus();
                return false;
            }
            if (tbxMail.Text.IsNull())
            {
                MessageBox.Show("Введіть електроний адрес", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxMail.Focus();
                return false;
            }
            if (tbxName.Text.IsNull())
            {
                MessageBox.Show("Введіть Ф.І.О", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxName.Focus();
                return false;
            }
            if (tbxHeadNach.Text.IsNull())
            {
                MessageBox.Show("Введіть посаду", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxHeadNach.Focus();
                return false;
            }
            return true;
        }

        private bool isUpdate()
        {
            if (_currentUser == null)
                return true;
            else
                return (tbxPC.Text.Equals(_currentUser.PC) == false)
                    || (tbxUserName.Text.Equals(_currentUser.UserName) == false)
                    || (cbxDepartment.SelectedValue.Equals(_currentUser.Department) == false)
                    || (tbxMail.Text.Equals(_currentUser.Mail) == false)
                    || (checkBoxNote.Checked != _currentUser.Notes)
                    || (checkBoxSchedule.Checked != _currentUser.Schedule)
                    || (checkBoxPhonebook.Checked != _currentUser.Phone)
                    || (checkBoxConfig.Checked != _currentUser.Config)
                    || (checkBoxApprovedN.Checked != _currentUser.ApprNach)
                    || (checkBoxApprovedSB.Checked != _currentUser.ApprSB)
                    || (checkBoxApprovedDir.Checked != _currentUser.ApprDir)
                    || (checkBoxApprAvtor.Checked != _currentUser.ApprAvtor)
                    || (checkBoxApprComdir.Checked != _currentUser.ApprComdir)
                    || (checkBoxApprSBN.Checked != _currentUser.ApprSBNach)
                    || (checkBoxApprKasa.Checked != _currentUser.ApprKasa)
                    || (checkBoxApprFin.Checked != _currentUser.ApprFin)
                    || (checkBoxApprDastavka.Checked != _currentUser.ApprDostavka)
                    || (checkBoxApprEnerg.Checked != _currentUser.ApprEnerg)
                    || (checkBoxApprSklad.Checked != _currentUser.ApprSklad)
                    || (checkBoxApprBun.Checked != _currentUser.ApprBuh)
                    || (checkBoxApprASU.Checked != _currentUser.ApprASU)
                    || (tbxName.Text.Equals(_currentUser.Name) == false)
                    || (tbxHeadNach.Text.Equals(_currentUser.HeadNach) == false);
        }
        #endregion


    }  // class
}
