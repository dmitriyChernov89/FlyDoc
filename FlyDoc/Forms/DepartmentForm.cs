using FlyDoc.Model;
using FlyDoc.Lib;
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
    public partial class DepartmentForm : Form
    {
        private bool _isNew;
        private Color _backColor;
        private Department _currentDep = null;
        public Department Department { get { return _currentDep; } }


        public DepartmentForm(Department dep = null)
        {
            _isNew = (dep == null);
            AppFuncs.openEditForm(this.GetType().Name, _isNew);

            InitializeComponent();

            _backColor = getBackColor();
            lblErrMsg.Visible = false;
            // подписаться на события фокуса
            FormsHelper.SetFocusEventHandlers(this, Color.Yellow, _backColor);

            if (_isNew)
            {
                this.Text = "Створення нового відділу";
            }
            else
            {
                this.Text = "Редагування відділу";
                _currentDep = dep;
                tbxId.Text = dep.Id.ToString();
                tbxName.Text = dep.Name;
            }

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
                    if (_currentDep == null) _currentDep = new Department();
                    _currentDep.Id = int.Parse(tbxId.Text);
                    _currentDep.Name = tbxName.Text;
                    this.DialogResult = DialogResult.OK;
                }
                else 
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
                _currentDep = null;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region validate input
        // проверка правильности ввода
        private bool isValidInput()
        {
            if (tbxId.Text.IsNull())
            {
                lblErrMsg.Show();
                return false;
            }
            return true;
        }

        private bool isUpdate()
        {
            if (_currentDep == null)
                return true;
            else
                return (tbxId.Text.Equals(_currentDep.Id.ToString()) == false) 
                    || (tbxName.Text.Equals(_currentDep.Name) == false);
        }

        private void tbxId_TextChanged(object sender, EventArgs e)
        {
            lblErrMsg.Visible = tbxId.Text.IsNull();
        }

        private void tbxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (isNumericKey(e.KeyCode) == false) e.SuppressKeyPress = true;
        }

        private bool isNumericKey(Keys key)
        {
            return ((key >= Keys.D0) && (key <= Keys.D9))
                || ((key >= Keys.NumPad0) && (key <= Keys.NumPad9))
                || (key == Keys.Left) || (key == Keys.Right)
                || (key == Keys.Delete) || (key == Keys.Back)
                || (key == Keys.Return);
        }
        #endregion

        private Color getBackColor()
        {
            foreach (Control item in this.Controls)
                if (item is TextBox) return item.BackColor;

            return Color.Empty;
        }

    }  // class
}
