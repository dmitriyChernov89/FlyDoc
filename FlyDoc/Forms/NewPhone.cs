﻿using System;
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
        private bool _isNewPhone;
        private PhoneModel _phone = null;

        public PhoneModel PhoneModel { get { return _phone; } }

        public NewPhone(PhoneModel phone)
        {
            InitializeComponent();

            FormsHelper.SetDepartmentsComboBox(cbxFormPhoneDepartment);

            FormsHelper.SetFocusEventHandlers(this, Color.Yellow, Color.White);

            _isNewPhone = (phone == null);
            if (_isNewPhone)
            {
                this.Text = "Створення нового комнтакта";
            }
            else
            {
                this.Text = "Редагування контакта";
                _phone = phone;
                tbxFio.Text = _phone.FIO;
                tbxPosition.Text = _phone.Positions;
                cbxFormPhoneDepartment.Text = _phone.Name;
                tbxDect.Text = _phone.Dect;
                tbxMobile.Text = _phone.Mobile;
                tbxPhone.Text = _phone.PhoneNumber;
                tbxFormPhoneMail.Text = _phone.eMail;
            }
           // 
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isValidInput())
            {
                if (_isNewPhone || isUpdate())
                {
                    if (_phone == null) _phone = new PhoneModel();
                    _phone.FIO = tbxFio.Text;
                    _phone.Positions = tbxPosition.Text;
                    _phone.Name = cbxFormPhoneDepartment.Text;
                    _phone.Dect = tbxDect.Text;
                    _phone.PhoneNumber = tbxPhone.Text;
                    _phone.Mobile = tbxMobile.Text;
                    _phone.eMail = tbxFormPhoneMail.Text;

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
                    || (cbxFormPhoneDepartment.SelectedValue.Equals(_phone.Name) == false)
                    || (tbxFormPhoneMail.Text.Equals(_phone.eMail) == false)
                    || (tbxDect.Text.Equals(_phone.Dect) == false)
                    || (tbxMobile.Text.Equals(_phone.Mobile) == false)
                    || (tbxPhone.Text.Equals(_phone.PhoneNumber) == false);

        }
    }
}
