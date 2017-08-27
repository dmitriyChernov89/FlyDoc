using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using FlyDoc.Model;
using FlyDoc.Lib;
using FlyDoc.ViewModel;
//using FlyDoc.Decor;
using static System.Net.Mime.MediaTypeNames;

namespace FlyDoc.Forms
{
    public partial class MainForm : Form
    {
        // business layer
        private AppNotes _noteModel;
        private AppNoteTemplates _noteTemplateModel;
        private AppUsers _userModel;
        private AppSchedule _scheduleModel;
        private AppDepartments _departmentModel;
        private AppPhone _phoneModel;
        private AppModelBase _currentModel;
        public static int _currentDepId = 0;
        public string _currentDepName;
        // view layer
        private const string AppModeClosingMessage = "Доступ до цієї опціЇ закритий! Зверніться в відділ АСУ";
        private bool _isShowButtonTip;
       // private DecorForm _decorForm;

        public string pc = System.Environment.MachineName;
        public string userName = System.Environment.UserName;
        public static bool enableNotes = false;      //Доступ к служебкам
        public bool enableSchedule = false;   //Доступ к графику работы
        public bool enablePhone = false;      //Доступ на редактирование телефонного справочника
        public bool enableConfig = false;     //Доступ в настройки
        public static string headNach = ""; //Шапка служебки
        public static string name = "";//имя автора или того кто утверждает документ
        public bool enableApprAvtor = false;   //Право утверждения документов как автор
        public bool enableApprDir = false;   //Право утверждения документов как директор
        public bool enableApprComdir = false;   //Право утверждения документов как ком.директор
        public bool enableSBNach = false;   //Право утверждения документов как нач СБ
        public bool enableApprSB = false;   //Право утверждения документов как инспектор СБ
        public bool enableApprKasa = false;   //Право утверждения документов как ст кассир
        public bool enableApprNach = false;   //Право утверждения документов как нач торг
        public bool enableApprFin = false;   //Право утверждения документов как финик
        public bool enableApprDostavka = false;   //Право утверждения документов как доставка
        public bool enableApprEnerg = false;   //Право утверждения документов как енергетик
        public bool enableApprSklad = false;   //Право утверждения документов как склад
        public bool enableApprBuh = false;   //Право утверждения документов как бух
        public bool enableApprASU = false;   //Право утверждения документов как АСУ
        public string Mail = ""; // Почта для писем при запросе на утвердение документа
        public static int WhichSection = 0;  //Определяем в каком мы разделе(1-служебки,2-графики,3-кто на работе,4-телефонная книга)
        public static int WhichBtnClick = 0; //Определяем параметры запуска новой формы 1-New, 2-Copy, 3-Edit,4 -Dell
        // ctor
        public MainForm()
        {
            InitializeComponent();

            _noteModel = new AppNotes() { ViewForm = this, DataGrid = this.dgvNotes };
           

            _noteTemplateModel = new AppNoteTemplates() { ViewForm = this, DataGrid = this.dgvNoteTemplates };
         

            _userModel = new AppUsers() { ViewForm = this, DataGrid = this.dgvUsers };
            _userModel.LoadDataToGrid();

            _scheduleModel = new AppSchedule() { ViewForm = this, DataGrid = this.dgvSchedule };
            

            _departmentModel = new AppDepartments() { ViewForm = this, DataGrid = this.dgvDepartments };
            _departmentModel.LoadDataToGrid();

            _phoneModel = new AppPhone() { ViewForm = this, DataGrid = this.dgvPhonebook };
            _phoneModel.LoadDataToGrid();

        }

        public void FlyDoc_Load(object sender, EventArgs e)
        {
            // заголовок окна
            String substring = pc.Substring(3, 3);
            _currentDepId = substring.ToInt();
            this.Text = string.Format("FlyDoc (користувач - {0}, ПК - {1}, відділ - {2})", userName, pc, _currentDepId);

            DataRow dr = _userModel.GetConfigRow(pc, userName);
            // если нашли в таблице Access данного пользователя, т.е. вернули строку
            if (dr != null)
            {
                _currentDepId = (int)dr["Department"];
                _currentDepName = DBContext.GetDepartmentName(_currentDepId);
                this.Text = string.Format("FlyDoc (користувач - {0}, ПК - {1}, відділ - {2})", userName, pc, _currentDepId);

                enableNotes = (bool)dr["Notes"];
                enableSchedule = (bool)dr["Schedule"];
                enablePhone = (bool)dr["Phone"];
                enableConfig = (bool)dr["Config"];
                if (dr["Name"] is System.DBNull)
                { name = ""; }
                else { name = (string)dr["Name"]; }
                if (dr["HeadNach"] is System.DBNull)
                { headNach = ""; }
                else { headNach = (string)dr["HeadNach"]; }
                enableApprAvtor = (bool)dr["ApprAvtor"];
                enableApprDir = (bool)dr["ApprDir"];
                enableApprComdir = (bool)dr["ApprComdir"];
                enableSBNach = (bool)dr["ApprSBNach"];
                enableApprSB = (bool)dr["ApprSB"];
                enableApprKasa = (bool)dr["ApprKasa"];
                enableApprNach = (bool)dr["ApprNach"];
                enableApprFin = (bool)dr["ApprFin"];
                enableApprDostavka = (bool)dr["ApprDostavka"];
                enableApprEnerg = (bool)dr["ApprEnerg"];
                enableApprSklad = (bool)dr["ApprSklad"];
                enableApprBuh = (bool)dr["ApprBuh"];
                enableApprASU = (bool)dr["ApprASU"];
                Mail = (string)dr["Mail"];
            }
            // доступность кнопок режима работы
            setAppModeButtonEnable(btnotes, enableNotes);
            setAppModeButtonEnable(btschedule, enableSchedule); // графики доступны всем на чтение
            setAppModeButtonEnable(btwork, enableSchedule); // доступно всем
            //  setAppModeButtonEnable(btphone, enablePhone);  //телефонная книга доступна всем на чтение
            setAppModeButtonEnable(btconfig, enableConfig);
            // отобразить правую панель для первого доступного режима
            tabControlMain.SelectedIndex = -1;
            if (enableNotes) btnotes_Click(null, null);
            else if (enableSchedule) btschedule_Click(null, null);
            else if (enablePhone) btphone_Click(null, null);
            else if (enableConfig) btconfig_Click(null, null);
            else { tabControlMain.Visible = false;}
            //отключаем панель
            panelFindNotes.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            //правка
            btnotes.FlatAppearance.BorderSize = 0;
            btnotes.FlatStyle = FlatStyle.Flat;
            btschedule.FlatAppearance.BorderSize = 0;
            btschedule.FlatStyle = FlatStyle.Flat;
            btwork.FlatAppearance.BorderSize = 0;
            btwork.FlatStyle = FlatStyle.Flat;
            btphone.FlatAppearance.BorderSize = 0;
            btphone.FlatStyle = FlatStyle.Flat;
            btconfig.FlatAppearance.BorderSize = 0;
            btconfig.FlatStyle = FlatStyle.Flat;
            btexit.FlatAppearance.BorderSize = 0;
            btexit.FlatStyle = FlatStyle.Flat;


            // заполнить комбобоксы отделов
           // FormsHelper.SetDepartmentsComboBox(cbxFilterDepsOnNotePage, true);   // с пустой первой строкой
          //  FormsHelper.SetDepartmentsComboBox(cbxFilterDepsOnUsers, true);   // с пустой первой строкой
            FormsHelper.SetDepartmentsComboBox(cbDepartmentFilter, true);
            cbDepartmentFilter.SelectedIndexChanged += new System.EventHandler(cbDepartmentFilter_SelectedIndexChanged);

            // начальные значения элементов управления
            tbxFindDocNumber_TextChanged(null,null);

            // убрать ярлычки у tabControl-a
            tabControlMain.Appearance = TabAppearance.FlatButtons;
            tabControlMain.ItemSize = new Size(0, 1);
            tabControlMain.SizeMode = TabSizeMode.Fixed;

            //Ставим даты в фильтре на период текущего месяца
            datePickerStart.Value = DateTime.Now.AddDays(-(DateTime.Now.Day - 1));
            datePickerEnd.Value = DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - (DateTime.Now.Day));

            // Стартуем с телефонной книги
            btphone_Click(null, null);
        }



        //private void setAppModeButtonEnableSchedule(Button btschedule, bool enableSchedule)
        //{
        //    btschedule.Enabled = enableSchedule;
        //    toolTip1.SetToolTip(btschedule, btschedule.Text);
        //}


        #region кнопки режимов работы
        private void btnotes_Click(object sender, EventArgs e) 
        {
            _noteModel.LoadDataToGrid();
            _noteTemplateModel.LoadDataToGrid();
            tabControlMain.SelectTab("tpgNotes");
            WhichSection = 1;
            btnOnOff(WhichSection);
        }

        private void btschedule_Click(object sender, EventArgs e)
        {
            _scheduleModel.LoadDataToGrid();
            tabControlMain.SelectTab("tpgSchedule");
            WhichSection = 2;
            btnOnOff(WhichSection);
        }
        
        private void btwork_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ця функція ще в процесі розробки");
            //WhichSection = 3;
            //btnOnOff(WhichSection);

        }

        public  void btphone_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectTab("tpgPhoneBook");
            WhichSection = 4;
            btnOnOff(WhichSection);
        }

        private void btconfig_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectTab("tpgConfig");
            WhichSection = 5;
            btnOnOff(WhichSection);
        }

        //Выход
        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region IAppMainForm implementation
        // метод, устанавливающий доступность кнопок редактирования грида (Create/Update/Delete)
        public void SetEnableGridEditButtons()
        {
            bool isEnable = false;
            DataGridView dgv = (_currentModel == null)? null : _currentModel.DataGrid;

            if (dgv == null)
            {
                btnNew.Enabled = false;
                cmiGridCreate.Enabled = false;
                isEnable = false;

                cbDepartmentFilter.Enabled = false;
                btnDeleteDepartmentFilter.Enabled = false;
            }
            else
            {
                btnNew.Enabled = true;
                cmiGridCreate.Enabled = true;
                isEnable = (dgv.Rows.Count > 0);

                if (dgv.Columns.Contains("DepartmentId"))
                {
                    cbDepartmentFilter.Enabled = true;
                    btnDeleteDepartmentFilter.Enabled = true;
                }
                else
                {
                    cbDepartmentFilter.Enabled = false;
                    btnDeleteDepartmentFilter.Enabled = false;
                }

            }
            // tool strip
            btnCopy.Enabled = isEnable;
            btnEdit.Enabled = isEnable;
            btnDelete.Enabled = isEnable;
            // context strip
            cmiGridCopy.Enabled = isEnable;
            cmiGridEdit.Enabled = isEnable;
            cmiGridDelete.Enabled = isEnable;
        }

        #endregion

        #region toolTip show on disabled buttons
        private void panelControl_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = panelControl.GetChildAtPoint(e.Location);

            if ((control == null) && (_isShowButtonTip == true))
            {
                toolTip1.Hide((Control)toolTip1.Tag);
                toolTip1.Tag = null;
                _isShowButtonTip = false;
                return;
            }
            if (control == null) return;

            if ((control is Button) && (control.Enabled == false) && (_isShowButtonTip == false))
            {
                //this.mFormTips.ShowAlways = true;
                toolTip1.Show(AppModeClosingMessage, control, control.Width / 2, control.Height / 2);
                toolTip1.Tag = control;
                _isShowButtonTip = true;
            }
        }

        #endregion

        #region Public methods
        #endregion

        #region Send mail
        /// <param name="mailto">Адрес получателя</param>
        /// <param name="caption">Тема письма</param>
        /// <param name="message">Сообщение</param>
        /// <param name="attachFile">Присоединенный файл</param>
        public static void SendMail(string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(@"flydoc@kc.epicentrk.com");
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = "FlyDoc: " + caption;
                mail.Body = message + "\n\n\nСистема електронного документообміну\nFlyDoc";
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = "192.168.46.46";
                client.Port = 25;
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential(@"flydoc@kc.epicentrk.com", "DgecnbVtyt");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new Exception("Mail.Send: " + e.Message);
            }
        }
        #endregion

        #region display messages
        public void displayInfoMsg(string text)
        {
            MessageBox.Show(text, "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void displayExclamationMsg(string text)
        {
            MessageBox.Show(text, "Застереження", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void displayWarningMsg(string text)
        {
            MessageBox.Show(text, "УВАГА", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

        #region select app mode
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab != null) setAppMode(tabControlMain.SelectedTab.Name);
        }
        private void tabControlConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab != null) setAppMode(tabControlConfig.SelectedTab.Name);
        }
        private void setAppMode(string tabName)
        {
            _currentModel = null;
            if (tabName.Contains("Notes"))
            {
                _currentModel = _noteModel;
            }
            else if (tabName.Contains("Schedule"))
            {
                _currentModel = _scheduleModel;
            }
            else if (tabName.Contains("PhoneBook"))
            {
                _currentModel = _phoneModel;
            }
            else if (tabName.Contains("Config"))
            {
                tabControlConfig.SelectTab("tpgUsers");
                setAppMode("Users");
                return;
            }
            else if (tabName.Contains("Users"))
            {
                _currentModel = _userModel;
            }
            else if (tabName.Contains("NoteTemplates"))
            {
                _currentModel = _noteTemplateModel;
            }
            else if (tabName.Contains("Departments"))
            {
                _currentModel = _departmentModel;
            }


            if (_currentModel == null)
                tabControlMain.TabPages[tabName].Hide();
            else
            {
                if (tabControlMain.Visible == false) tabControlMain.Visible = true;
            }
            SetEnableGridEditButtons();
        }
        #endregion

        #region edit grid buttons
        private void btnNew_Click(object sender, EventArgs e)
        {
            WhichBtnClick = 1;
            gridCreate();
             }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            WhichBtnClick = 2;
            gridCopy();
            
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            WhichBtnClick = 3;
            gridEdit();
           
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            WhichBtnClick = 4;
            gridDelete();
            
        }
        private void cmiGridCreate_Click(object sender, EventArgs e)
        {
            gridCreate();
        }

        private void cmiGridCopy_Click(object sender, EventArgs e)
        {
            gridCopy();
        }

        private void cmiGridEdit_Click(object sender, EventArgs e)
        {
            gridEdit();
        }

        private void cmiGridDelete_Click(object sender, EventArgs e)
        {
            gridDelete();
        }

        private void gridCreate()
        {
            if (_currentModel == null) return;
            _currentModel.CreateNewObject();
        }
        private void gridCopy()
        {
            if (_currentModel == null) return;
            _currentModel.CopyToNewObject();
        }
        private void gridEdit()
        {
            if (_currentModel == null) return;
            _currentModel.EditObject();
        }
        private void gridDelete()
        {
            if (_currentModel == null) return;
            _currentModel.DeleteObject();
        }
        #endregion

        private void tbxFindDocNumber_TextChanged(object sender, EventArgs e)
        {
            bool enabled = !tbxFindDocNumber.Text.IsNull();
            if (btnFindDocByNumber.Enabled != enabled) btnFindDocByNumber.Enabled = enabled;
        }

        private void btnFindDocByNumber_Click(object sender, EventArgs e)
        {
            string sDocNumFind = tbxFindDocNumber.Text;
            if (sDocNumFind.IsNull()) return;

            foreach (DataGridViewRow row in dgvNotes.Rows)
            {
                if (row.Cells["Номер"].Value.ToString() == sDocNumFind)
                {
                    row.Selected = true; break;
                }
            }
        }

        //private void btnSetFilter_Click(object sender, EventArgs e)
        //{
        //    string sFilter = "";
        //    if ((int)cbxFilterDepsOnNotePage.SelectedValue > -1)
        //    {
        //        sFilter = string.Format("(Відділ = {0})", cbxFilterDepsOnNotePage.SelectedValue);
        //    }

        //    if (chkCEO.CheckState == CheckState.Checked)
        //        sFilter += string.Format("{0}(AgreedCEO = 1)", (sFilter.IsNull()?"":" AND "));
        //    else if (chkCEO.CheckState == CheckState.Unchecked)
        //        sFilter += string.Format("{0}(AgreedCEO = 0)", (sFilter.IsNull() ? "" : " AND "));

        //    if (chkCFO.CheckState == CheckState.Checked)
        //        sFilter += string.Format("{0}(AgreedCFO = 1)", (sFilter.IsNull() ? "" : " AND "));
        //    else if (chkCFO.CheckState == CheckState.Unchecked)
        //        sFilter += string.Format("{0}(AgreedCFO = 0)", (sFilter.IsNull() ? "" : " AND "));

        //    if (!sFilter.IsNull())
        //    {
        //        (dgvNotes.DataSource as DataTable).DefaultView.RowFilter = sFilter;
        //    }
        //}

        private void btnDeleteDepartmentFilter_Click(object sender, EventArgs e)
        {
            cbDepartmentFilter.SelectedIndex = 0;
        }

        private void btnSetFilterUsers_Click(object sender, EventArgs e)
        {
        }

        private void btnDeleteFilterUsers_Click(object sender, EventArgs e)
        {
            //removeFilter(dgvUsers);
        }

        private void btnFindPC_Click(object sender, EventArgs e)
        {
            string sFind = tbxFindPC.Text;
            if (sFind.IsNull()) return;

            int iFrom = 0;
            // со следующей строки
            if (dgvUsers.SelectedRows.Count > 0) iFrom = dgvUsers.Rows.IndexOf(dgvUsers.SelectedRows[0]) + 1;

            bool isFind = false;
            // ищем со строки, следующей после текущей
            for (int i = iFrom; i < dgvUsers.Rows.Count; i++)
            {
                // вхождение строки в PC
                if (dgvUsers.Rows[i].Cells["PC"].Value.ToString().Contains(sFind))
                {
                    isFind = true;
                    dgvUsers.Rows[i].Selected = true; break;
                }
            }
            // если не нашли, то ищем с начала до текущей строки
            if (isFind == false)
            {
                for (int i = 0; i < iFrom; i++)
                {
                    if (dgvUsers.Rows[i].Cells["PC"].Value.ToString().Contains(sFind))
                    {
                        dgvUsers.Rows[i].Selected = true; break;
                    }
                }
            }
        }
        

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            datePickerEnd.MinDate = datePickerStart.Value;
        }

        private void chkCEO_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbDepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentModel != null)
            {
                string sFilter = "";
                if (cbDepartmentFilter.SelectedIndex > 0)
                {
                    sFilter = string.Format("(DepartmentId = {0})", cbDepartmentFilter.SelectedValue);
                    if (_currentModel.DataGrid.Columns.Contains("DepartmentId"))
                        (_currentModel.DataGrid.DataSource as DataTable).DefaultView.RowFilter = sFilter;
                }
                else
                {
                    removeFilter();
                }
            }
        }

        private void removeFilter()
        {
            cbDepartmentFilter.SelectedValue = -1;
            if (_currentModel != null)
            {
                (_currentModel.DataGrid.DataSource as DataTable).DefaultView.RowFilter = null;
            }
        }

        #region DecorForm
        private void btnOnOff(int WhichSection)
        {
            switch (WhichSection)
            {
                case 1:
                    setAppModeButtonEnable(btnNew, enableNotes);
                    setAppModeButtonEnable(btnCopy, enableNotes);
                    setAppModeButtonEnable(btnEdit, enableNotes);
                    setAppModeButtonEnable(btnDelete, enableNotes);
                    datePickerStart.Enabled = true;
                    datePickerEnd.Enabled = true;
                    tbxFindDocNumber.Enabled = true;
                    chkCEO.Enabled = true;
                    // если дир то видим все отделы, или принудительный фильтр по отделу
                    if (enableApprDir) { }
                    else
                    {
                        cbDepartmentFilter.SelectedValue = _currentDepId.ToString();// придумать как вытянуть номер отдела без таблицы конфиг!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        cbDepartmentFilter.Enabled = false;
                        setAppModeButtonEnable(btnDeleteDepartmentFilter, false);
                    }
                    datePickerStart.Enabled = true;
                    datePickerEnd.Enabled = true;
                    break;
                case 2:
                    setAppModeButtonEnable(btnNew, enableSchedule);
                    setAppModeButtonEnable(btnCopy, enableSchedule);
                    setAppModeButtonEnable(btnEdit, enableSchedule);
                    setAppModeButtonEnable(btnDelete, enableSchedule);
                    datePickerStart.Enabled = true;
                    datePickerEnd.Enabled = true;
                    tbxFindDocNumber.Enabled = false;
                    chkCEO.Enabled = true;
                    // если дир то видим все отделы, или принудительный фильтр по отделу
                    if (enableApprDir) { }
                    else
                    {
                        cbDepartmentFilter.SelectedValue = _currentDepId.ToString();// придумать как вытянуть номер отдела без таблицы конфиг!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        cbDepartmentFilter.Enabled = false;
                        setAppModeButtonEnable(btnDeleteDepartmentFilter, false);
                    }
                    datePickerStart.Enabled = true;
                    datePickerEnd.Enabled = true;
                    break;
                case 3:
                    //Выключаем лишние кнопки
                    //setAppModeButtonEnable(btnNew, false);
                    //setAppModeButtonEnable(btnCopy, false);
                    //setAppModeButtonEnable(btnEdit, false);
                    //setAppModeButtonEnable(btnDelete, false);
                    //datePickerStart.Enabled = false;
                    //datePickerEnd.Enabled = false;
                    //tbxFindDocNumber.Enabled = false;
                    //cbDepartmentFilter.SelectedIndex = 0;
                    //setAppModeButtonEnable(btnDeleteDepartmentFilter, true);
                    //chkCEO.Enabled = false;
                    break;
                case 4:
                    setAppModeButtonEnable(btnNew, enablePhone);
                    setAppModeButtonEnable(btnCopy, enablePhone);
                    setAppModeButtonEnable(btnEdit, enablePhone);
                    setAppModeButtonEnable(btnDelete, enablePhone);
                    datePickerStart.Enabled = false;
                    datePickerEnd.Enabled = false;
                    tbxFindDocNumber.Enabled = false;
                    setAppModeButtonEnable(btnDeleteDepartmentFilter, true);
                    chkCEO.Enabled = false;
                    cbDepartmentFilter.SelectedIndex = 0;
                    break;
                case 5:
                    setAppModeButtonEnable(btnNew, enableConfig);
                    setAppModeButtonEnable(btnCopy, enableConfig);
                    setAppModeButtonEnable(btnEdit, enableConfig);
                    setAppModeButtonEnable(btnDelete, enableConfig);
                    datePickerStart.Enabled = false;
                    datePickerEnd.Enabled = false;
                    tbxFindDocNumber.Enabled = true;
                    setAppModeButtonEnable(btnDeleteDepartmentFilter, true);
                    chkCEO.Enabled = false;
                    cbDepartmentFilter.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }
        private void setAppModeButtonEnable(Button appModeButton, bool enable)
        {
            appModeButton.Enabled = enable;
            toolTip1.SetToolTip(appModeButton, appModeButton.Text);

        }


        #endregion

        private void dgvNotes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }  // class
}
