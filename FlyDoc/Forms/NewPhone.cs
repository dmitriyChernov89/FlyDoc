using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlyDoc.Model;
using FlyDoc.Lib;

namespace FlyDoc.Forms
{
    public partial class NewPhone : Form
    {
        private bool _isNew;
        private PhoneModel _phone = null;

        public PhoneModel PhoneModel { get { return _phone; } }

        public NewPhone(PhoneModel phone)
        {
            _isNew = (phone == null);
            AppFuncs.openEditForm(this.GetType().Name, _isNew);

            InitializeComponent();

            FormsHelper.SetDepartmentsComboBox(cbxFormPhoneDepartment);

            FormsHelper.SetFocusEventHandlers(this, Color.Yellow, Color.White);

            if (_isNew)
            {
                this.Text = "Створення нового комнтакта";
            }
            else
            {
                this.Text = "Редагування контакта";
                _phone = phone;
                tbxFio.Text = _phone.FIO;
                tbxPosition.Text = _phone.Positions;
                cbxFormPhoneDepartment.SelectedValue = _phone.Department;
                tbxDect.Text = _phone.Dect;
                tbxMobile.Text = _phone.Mobile;
                tbxPhone.Text = _phone.Phone;
                tbxFormPhoneMail.Text = _phone.Mail;

            }
           // 
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            AppFuncs.closeEditForm(this.GetType().Name, e.CloseReason);
            base.OnFormClosed(e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            AppFuncs.WriteLogTraceMessage(" - press button 'Зберегти'");

            if (isValidInput())
            {
                if (_isNew || isUpdate())
                {
                    if (_phone == null) _phone = new PhoneModel();
                    _phone.Department = Convert.ToInt32(cbxFormPhoneDepartment.SelectedValue);
                    _phone.FIO = tbxFio.Text;
                    _phone.Positions = tbxPosition.Text;
                    _phone.DepName = cbxFormPhoneDepartment.Text;
                    _phone.Dect = tbxDect.Text;
                    _phone.Phone = tbxPhone.Text;
                    _phone.Mobile = tbxMobile.Text;
                    _phone.Mail = tbxFormPhoneMail.Text;

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }

            }
            else
                _phone = null;
        }

        private void btnCandel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool isValidInput()
        {
            if (tbxFio.Text.IsNull())
            {
                MessageBox.Show("Введіть Ф.І.О", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxFio.Focus();
                return false;
            }
            if (tbxPosition.Text.IsNull())
            {
                MessageBox.Show("Введіть посаду", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxPosition.Focus();
                return false;
            }
            if (tbxFormPhoneMail.Text.IsNull())
            {
                MessageBox.Show("Введіть електроний адрес", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxFormPhoneMail.Focus();
                return false;
            }
            //if (tbxDect.Text.IsNull())
            //{
            //    MessageBox.Show("Введіть номер трубк", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    tbxDect.Focus();
            //    return false;
            //}
            if (tbxPhone.Text.IsNull())
            {
                MessageBox.Show("Введіть номер внутрішнього телефону", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxPhone.Focus();
                return false;
            }
            if (tbxMobile.Text.IsNull())
            {
                MessageBox.Show("Введіть номер мобільного телефону", "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbxMobile.Focus();
                return false;
            }
            return true;
        }

        private bool isUpdate()
        {
            if (_phone == null)
                return true;
            else
                return (tbxFio.Text.Equals(_phone.FIO) == false)
                    || (tbxPosition.Text.Equals(_phone.Positions) == false)
                    || (cbxFormPhoneDepartment.SelectedValue.Equals(_phone.Department) == false)
                    || (tbxFormPhoneMail.Text.Equals(_phone.Mail) == false)
                    || (tbxDect.Text.Equals(_phone.Dect) == false)
                    || (tbxMobile.Text.Equals(_phone.Mobile) == false)
                    || (tbxPhone.Text.Equals(_phone.Phone) == false);

        }
    }
}
