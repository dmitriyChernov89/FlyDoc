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
                cbxDepartment.SelectedValue = _currentUser.DepartmentId;
                checkBoxNote.Checked = _currentUser.AllowNote;
                checkBoxSchedule.Checked = _currentUser.AllowSchedule;
                checkBoxPhonebook.Checked = _currentUser.AllowPhonebook;
                checkBoxConfig.Checked = _currentUser.AllowConfig;
                checkBoxApprovedN.Checked = _currentUser.AllowApprovedNach;
                checkBoxApprovedSB.Checked = _currentUser.AllowApproverSB;
                checkBoxApprovedDir.Checked = _currentUser.AllowApproverDir;
                checkBoxApprAvtor.Checked = _currentUser.AllowApprAvtor;
                checkBoxApprComdir.Checked = _currentUser.AllowApprComdir;
                checkBoxApprSBN.Checked = _currentUser.AllowApprSBNach;
                checkBoxApprKasa.Checked = _currentUser.AllowApprKasa;
                checkBoxApprFin.Checked = _currentUser.AllowApprFin;
                checkBoxApprDastavka.Checked = _currentUser.AllowApprDostavka;
                checkBoxApprEnerg.Checked = _currentUser.AllowApprEnerg;
                checkBoxApprSklad.Checked = _currentUser.AllowApprSklad;
                checkBoxApprBun.Checked = _currentUser.AllowApprBuh;
                checkBoxApprASU.Checked = _currentUser.AllowApprASU;
                tbxName.Text = _currentUser.Name;
                tbxHeadNach.Text = _currentUser.HeadNach;
                tbxMail.Text = _currentUser.enterMail;
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
                    _currentUser.DepartmentId = (int)cbxDepartment.SelectedValue;
                    _currentUser.AllowNote = checkBoxNote.Checked;
                    _currentUser.AllowSchedule = checkBoxSchedule.Checked;
                    _currentUser.AllowPhonebook = checkBoxPhonebook.Checked;
                    _currentUser.AllowConfig = checkBoxConfig.Checked;
                    _currentUser.AllowApprovedNach = checkBoxApprovedN.Checked;
                    _currentUser.AllowApproverSB = checkBoxApprovedSB.Checked;
                    _currentUser.AllowApproverDir = checkBoxApprovedDir.Checked;
                    _currentUser.enterMail = tbxMail.Text;
                    _currentUser.AllowApprAvtor = checkBoxApprAvtor.Checked;
                    _currentUser.AllowApprComdir = checkBoxApprComdir.Checked;
                    _currentUser.AllowApprSBNach = checkBoxApprSBN.Checked;
                    _currentUser.AllowApprKasa = checkBoxApprKasa.Checked;
                    _currentUser.AllowApprFin = checkBoxApprFin.Checked;
                    _currentUser.AllowApprDostavka = checkBoxApprDastavka.Checked;
                    _currentUser.AllowApprEnerg = checkBoxApprEnerg.Checked;
                    _currentUser.AllowApprSklad = checkBoxApprSklad.Checked;
                    _currentUser.AllowApprBuh = checkBoxApprBun.Checked;
                    _currentUser.AllowApprASU = checkBoxApprASU.Checked;
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
                    || (cbxDepartment.SelectedValue.Equals(_currentUser.DepartmentId) == false)
                    || (tbxMail.Text.Equals(_currentUser.enterMail) == false)
                    || (checkBoxNote.Checked != _currentUser.AllowNote)
                    || (checkBoxSchedule.Checked != _currentUser.AllowSchedule)
                    || (checkBoxPhonebook.Checked != _currentUser.AllowPhonebook)
                    || (checkBoxConfig.Checked != _currentUser.AllowConfig)
                    || (checkBoxApprovedN.Checked != _currentUser.AllowApprovedNach)
                    || (checkBoxApprovedSB.Checked != _currentUser.AllowApproverSB)
                    || (checkBoxApprovedDir.Checked != _currentUser.AllowApproverDir)
                    || (checkBoxApprAvtor.Checked != _currentUser.AllowApprAvtor)
                    || (checkBoxApprComdir.Checked != _currentUser.AllowApprComdir)
                    || (checkBoxApprSBN.Checked != _currentUser.AllowApprSBNach)
                    || (checkBoxApprKasa.Checked != _currentUser.AllowApprKasa)
                    || (checkBoxApprFin.Checked != _currentUser.AllowApprFin)
                    || (checkBoxApprDastavka.Checked != _currentUser.AllowApprDostavka)
                    || (checkBoxApprEnerg.Checked != _currentUser.AllowApprEnerg)
                    || (checkBoxApprSklad.Checked != _currentUser.AllowApprSklad)
                    || (checkBoxApprBun.Checked != _currentUser.AllowApprBuh)
                    || (checkBoxApprASU.Checked != _currentUser.AllowApprASU)
                    || (tbxName.Text.Equals(_currentUser.Name) == false)
                    || (tbxHeadNach.Text.Equals(_currentUser.HeadNach) == false);
        }
        #endregion


    }  // class
}
