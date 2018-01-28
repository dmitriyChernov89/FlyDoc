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
    public partial class NoteTemplateForm : Form
    {
        private bool _isNew;
        private NoteTemplate _currentTemplate = null;
        public NoteTemplate NoteTemplate { get { return _currentTemplate; } }

        public NoteTemplateForm(NoteTemplate pTemplate)
        {
            InitializeComponent();

            // подписаться на события фокуса
            //FormsHelper.SetFocusEventHandlers(this, Color.Yellow, Color.White);

            _isNew = (pTemplate == null);
            if (_isNew)
            {
                this.Text = "Створення нового шаблону службової записки";
            }
            else
            {
                this.Text = "Редагування існуючого шаблону";
                _currentTemplate = pTemplate;
                #region заполнение полей
                tbxTplName.Text = _currentTemplate.Name;
                tbxHelp.Text = _currentTemplate.Help;
                tbxHeadDir.Text = _currentTemplate.HeadDir;
                tbxBodyUp.Text = _currentTemplate.BodyUp;
                tbxHeadNach.Text = _currentTemplate.HeadNach;
                tbxTableColums.Text = _currentTemplate.TableColums.ToString();
                tbxColumnName1.Text = _currentTemplate.ColumName1;
                tbxColumnName2.Text = _currentTemplate.ColumName2;
                tbxColumnName3.Text = _currentTemplate.ColumName3;
                tbxColumnName4.Text = _currentTemplate.ColumName4;
                tbxColumnName5.Text = _currentTemplate.ColumName5;
                tbxColumnName6.Text = _currentTemplate.ColumName6;
                tbxColumnName7.Text = _currentTemplate.ColumName7;
                tbxColumnName8.Text = _currentTemplate.ColumName8;
                tbxColumnName9.Text = _currentTemplate.ColumName9;
                tbxColumnName10.Text = _currentTemplate.ColumName10;
                tbxBodyDown.Text = _currentTemplate.BodyDown;
                cbxApprASU.Checked = _currentTemplate.ApprASU;
                cbxApprBuh.Checked = _currentTemplate.ApprBuh;
                cbxApprComdir.Checked = _currentTemplate.ApprComdir;
                cbxApprDir.Checked = _currentTemplate.ApprDir;
                cbxApprDostavka.Checked = _currentTemplate.ApprDostavka;
                cbxApprEnerg.Checked = _currentTemplate.ApprEnerg;
                cbxApprFin.Checked = _currentTemplate.ApprFin;
                cbxApprKasa.Checked = _currentTemplate.ApprKasa;
                cbxApprNach.Checked = _currentTemplate.ApprNach;
                cbxApprSB.Checked = _currentTemplate.ApprSB;
                cbxApprSBNach.Checked = _currentTemplate.ApprSBNach;
                cbxApprSklad.Checked = _currentTemplate.ApprSklad;
                #endregion
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isValidInput())
            {
                if (_isNew || isUpdate())
                {
                    if (_currentTemplate == null) _currentTemplate = new NoteTemplate();

                    _currentTemplate.Name = tbxTplName.Text;
                    _currentTemplate.Help = tbxHelp.Text;
                    _currentTemplate.HeadDir = tbxHeadDir.Text;
                    _currentTemplate.HeadNach = tbxTableColums.Text;
                    _currentTemplate.BodyUp = tbxBodyUp.Text;
                    _currentTemplate.BodyDown = tbxBodyDown.Text;

                    _currentTemplate.TableColums = tbxTableColums.Text.ToInt();
                    _currentTemplate.ColumName1 = tbxColumnName1.Text;
                    _currentTemplate.ColumName2 = tbxColumnName2.Text;
                    _currentTemplate.ColumName3 = tbxColumnName3.Text;
                    _currentTemplate.ColumName4 = tbxColumnName4.Text;
                    _currentTemplate.ColumName5 = tbxColumnName5.Text;
                    _currentTemplate.ColumName6 = tbxColumnName6.Text;
                    _currentTemplate.ColumName7 = tbxColumnName7.Text;
                    _currentTemplate.ColumName8 = tbxColumnName8.Text;
                    _currentTemplate.ColumName9 = tbxColumnName9.Text;
                    _currentTemplate.ColumName10 = tbxColumnName10.Text;
                    _currentTemplate.ApprASU = cbxApprASU.Checked;
                    _currentTemplate.ApprBuh = cbxApprBuh.Checked;
                    _currentTemplate.ApprComdir = cbxApprComdir.Checked;
                    _currentTemplate.ApprDir = cbxApprDir.Checked;
                    _currentTemplate.ApprDostavka = cbxApprDostavka.Checked;
                    _currentTemplate.ApprEnerg = cbxApprEnerg.Checked;
                    _currentTemplate.ApprFin = cbxApprFin.Checked;
                    _currentTemplate.ApprKasa = cbxApprKasa.Checked;
                    _currentTemplate.ApprNach = cbxApprNach.Checked;
                    _currentTemplate.ApprSB = cbxApprSB.Checked;
                    _currentTemplate.ApprSBNach = cbxApprSBNach.Checked;
                    _currentTemplate.ApprSklad = cbxApprSklad.Checked;

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
                _currentTemplate = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region validate input
        // проверка правильности ввода
        private bool isValidInput()
        {
            if (_isNullTextBox(tbxTplName, "Введіть найменування шаблону")
                || _isNullTextBox(tbxHeadDir, "Введіть заголовок Директора")
                || _isNullTextBox(tbxHeadNach, "Введіть заголовок нач.відділу")
                || _isNullTextBox(tbxBodyUp, "Введіть шаблон тексту службової")
                )
                return false;
            else
                return true;
        }
        private bool _isNullTextBox(TextBox textBox, string caption)
        {
            if (textBox.Text.IsNull())
            {
                MessageBox.Show(caption, "Перевірка вводу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox.Focus();
                return true;
            }
            else
                return false;
        }

        private bool isUpdate()
        {
            if (_currentTemplate == null)
                return true;
            else
                return (tbxTplName.Text.Equals(_currentTemplate.Name) == false)
                    || (tbxHeadDir.Text.Equals(_currentTemplate.HeadDir) == false)
                    || (tbxHeadNach.Text.Equals(_currentTemplate.HeadNach) == false)
                    || (tbxHelp.Text.Equals(_currentTemplate.Help) == false)
                    || (tbxBodyUp.Text.Equals(_currentTemplate.BodyUp) == false)
                    || (tbxBodyDown.Text.Equals(_currentTemplate.BodyDown) == false)
                    || (tbxTableColums.Text.Equals(_currentTemplate.TableColums.ToString()) == false)
                    || (tbxColumnName1.Text.Equals(_currentTemplate.ColumName1) == false)
                    || (tbxColumnName2.Text.Equals(_currentTemplate.ColumName2) == false)
                    || (tbxColumnName3.Text.Equals(_currentTemplate.ColumName3) == false)
                    || (tbxColumnName4.Text.Equals(_currentTemplate.ColumName4) == false)
                    || (tbxColumnName5.Text.Equals(_currentTemplate.ColumName5) == false)
                    || (tbxColumnName6.Text.Equals(_currentTemplate.ColumName6) == false)
                    || (tbxColumnName7.Text.Equals(_currentTemplate.ColumName7) == false)
                    || (tbxColumnName8.Text.Equals(_currentTemplate.ColumName8) == false)
                    || (tbxColumnName9.Text.Equals(_currentTemplate.ColumName9) == false)
                    || (tbxColumnName10.Text.Equals(_currentTemplate.ColumName10) == false)
                    || (cbxApprASU.Checked != _currentTemplate.ApprASU)
                    || (cbxApprBuh.Checked != _currentTemplate.ApprBuh)
                    || (cbxApprComdir.Checked != _currentTemplate.ApprComdir)
                    || (cbxApprDir.Checked != _currentTemplate.ApprDir)
                    || (cbxApprDostavka.Checked != _currentTemplate.ApprDostavka)
                    || (cbxApprEnerg.Checked != _currentTemplate.ApprEnerg)
                    || (cbxApprFin.Checked != _currentTemplate.ApprFin)
                    || (cbxApprKasa.Checked != _currentTemplate.ApprKasa)
                    || (cbxApprNach.Checked != _currentTemplate.ApprNach)
                    || (cbxApprSB.Checked != _currentTemplate.ApprSB)
                    || (cbxApprSBNach.Checked != _currentTemplate.ApprSBNach)
                    || (cbxApprSklad.Checked != _currentTemplate.ApprSklad)
                ;
        }

        #endregion

    }  // class
}
