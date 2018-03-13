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
using FlyDoc.Presenter;
//using FlyDoc.Decor;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Reflection;

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
        private static int _currentDepId = 0;
        private string _currentDepName;
        // view layer
        private const string AppModeClosingMessage = "Доступ до цієї опціЇ закритий! Зверніться в відділ АСУ";
        private bool _isShowButtonTip;
        // private DecorForm _decorForm;

        private bool enableNotes = false;      //Доступ к служебкам
        private bool enableSchedule = false;   //Доступ к графику работы
        private bool enablePhone = false;      //Доступ на редактирование телефонного справочника
        private bool enableConfig = false;     //Доступ в настройки

        public int WhichSection = 0;  //Определяем в каком мы разделе(1-служебки,2-графики,3-кто на работе,4-телефонная книга)

        private bool _dgvNoteRowChangedEnable;
        private PropertyInfo[] _propInfoUser;



        // ctor
        public MainForm()
        {
            InitializeComponent();

            _propInfoUser = typeof(User).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // режимы работы
            _noteModel = new AppNotes() { ViewForm = this, DataGrid = this.dgvNotes };
            _scheduleModel = new AppSchedule() { ViewForm = this, DataGrid = this.dgvSchedule };
            _phoneModel = new AppPhone() { ViewForm = this, DataGrid = this.dgvPhonebook };
            _phoneModel.LoadDataToGrid();

            // настройка
            _noteTemplateModel = new AppNoteTemplates() { ViewForm = this, DataGrid = this.dgvNoteTemplates };
            _userModel = new AppUsers() { ViewForm = this, DataGrid = this.dgvUsers };
            _departmentModel = new AppDepartments() { ViewForm = this, DataGrid = this.dgvDepartments };
        }

        public void FlyDoc_Load(object sender, EventArgs e)
        {
            // заголовок окна
            _currentDepId = Program.User.Department;
            _currentDepName = DBContext.GetDepartmentName(_currentDepId);
            string ver = AppFuncs.GetVersion();
            this.Text = $"FlyDoc (користувач - {Program.UserName}, ПК - {Program.MachineName}, відділ - {_currentDepId}), ver.{ver}";

            enableNotes = Program.User.Notes;
            enableSchedule = Program.User.Schedule;
            //телефонная книга доступна всем на чтение
            enablePhone = true; // Program.User.Phone;
            enableConfig = Program.User.Config;

            // доступность кнопок режима работы
            setAppModeButtonEnable(btnotes, enableNotes);
            setAppModeButtonEnable(btschedule, enableSchedule); // графики доступны всем на чтение
            setAppModeButtonEnable(btwork, enableSchedule); // доступно всем
            setAppModeButtonEnable(btphone, enablePhone);
            setAppModeButtonEnable(btconfig, enableConfig);

            // заполнить комбобоксы отделов
            // FormsHelper.SetDepartmentsComboBox(cbxFilterDepsOnNotePage, true);   // с пустой первой строкой
            //  FormsHelper.SetDepartmentsComboBox(cbxFilterDepsOnUsers, true);   // с пустой первой строкой
            FormsHelper.SetDepartmentsComboBox(cbDepartmentFilter, true);
            cbDepartmentFilter.SelectedValueChanged += new System.EventHandler(cbDepartmentFilter_SelectedIndexChanged);

            //tabControlMain.SelectedIndex = -1;
            //if (enableNotes) btnotes_Click(null, null);
            //else if (enableSchedule) btschedule_Click(null, null);
            //else if (enablePhone) btphone_Click(null, null);
            //else if (enableConfig) btconfig_Click(null, null);
            //else { tabControlMain.Visible = false;}
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
            clickPhoneButton(false);
        }

        protected override void OnHelpButtonClicked(CancelEventArgs e)
        {
            (new AboutForm()).ShowDialog();
            e.Cancel = true;
        }


        //private void setAppModeButtonEnableSchedule(Button btschedule, bool enableSchedule)
        //{
        //    btschedule.Enabled = enableSchedule;
        //    toolTip1.SetToolTip(btschedule, btschedule.Text);
        //}


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

        #region выбор режима работы
        #region выбор режима работы кликом по кнопке слева (эмулируется клик по вкладке)
        private void btnotes_Click(object sender, EventArgs e)
        {
            clickNoteButton();
        }

        private void btschedule_Click(object sender, EventArgs e)
        {
            clickScheduleButton();
        }

        private void btwork_Click(object sender, EventArgs e)
        {
            clickWorkButton();
        }

        public void btphone_Click(object sender, EventArgs e)
        {
            clickPhoneButton(false);
        }
        private void btconfig_Click(object sender, EventArgs e)
        {
            clickConfigButton();
        }

        //Выход
        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        #region обработки нажатий кнопок режима работы
        private void clickNoteButton()
        {
            AppFuncs.WriteLogInfoMessage($"Нажата кнопка СЛУЖБОВI");

            _dgvNoteRowChangedEnable = false;
            _noteModel.LoadDataToGrid();
            _dgvNoteRowChangedEnable = true;

            tabControlMain.SelectTab("tpgNotes");
        }

        private void clickWorkButton()
        {
            AppFuncs.WriteLogInfoMessage($"Нажата кнопка РОБОТА");
            MessageBox.Show("Ця функція ще в процесі розробки");
        }

        private void clickPhoneButton(bool isLog = true)
        {
            if (isLog) AppFuncs.WriteLogInfoMessage($"Нажата кнопка ТЕЛЕФОНИ");

            tabControlMain.SelectTab("tpgPhoneBook");
        }

        private void clickScheduleButton()
        {
            AppFuncs.WriteLogInfoMessage($"Нажата кнопка ГРАФИКИ");
            _scheduleModel.LoadDataToGrid();
            tabControlMain.SelectTab("tpgSchedule");
        }

        private void clickConfigButton()
        {
            AppFuncs.WriteLogInfoMessage($"Нажата кнопка КОНФIГУРАЦIЯ");

            _userModel.LoadDataToGrid();
            _noteTemplateModel.LoadDataToGrid();
            _departmentModel.LoadDataToGrid();

            tabControlMain.SelectTab("tpgConfig");
        }
        #endregion


        #region выбор режима работы кликом по вкладке
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab != null) setAppMode(tabControlMain.SelectedTab.Name);
        }
        private void tabControlConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab != null) setAppMode(tabControlConfig.SelectedTab.Name);
        }
        #endregion

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

            setEnableControls();
        }

        // метод, устанавливающий доступность кнопок редактирования 
        private void setEnableControls(bool btnEditOnly = false)
        {

            #region кнопки редактирования грида (Create/Update/Delete) на основе прав доступа
            //  - получить выделенную строку
            DataGridViewRow row = _currentModel.GetSelectedDataRow();
            //    null, если в гриде нет строк, или нет выделенной строки
            // если находимся в служебках

            if (_currentModel is AppNotes)
            {
                int depId = (row==null) ? 0 : (int)row.Cells["DepartmentId"].Value;
                // для юзеров, которые имеют право автора и отдел клиента совпадает с отделом служебки
                if ((Program.User.ApprAvtor) && (Program.User.Department == depId))
                {
                    setEditButtonEnable(true, false, true, true);
                    _currentModel.AllowEdit = true;
                }
                // текущий юзер из отдела 500 или 114
                else if ((Program.User.Department == 500) || (Program.User.Department == 114))
                {
                    setEditButtonEnable(true, false, true, false);
                    _currentModel.AllowEdit = true;
                }
                // согласователь
                else
                {
                    bool isApprExist = false;
                    foreach (PropertyInfo pi in _propInfoUser)
                    {
                        if (pi.Name.StartsWith("Appr"))
                        {
                            bool isAppr = (bool)pi.GetValue(Program.User, null);
                            string cellValue = (string)row.Cells[pi.Name].Value;
                            if (isAppr && (cellValue != AppConst.DGVNotesApprNoneText))
                            {
                                isApprExist = true;
                                break;
                            }
                        }
                    }
                    if (isApprExist)
                    {
                        setEditButtonEnable(false, false, true, false);
                        _currentModel.AllowEdit = true;
                    }
                    else
                    {
                        setEditButtonEnable(false, false, false, false);
                        _currentModel.AllowEdit = false;
                    }
                }
            }

            // если находимся в тел.справочнике
            else if (_currentModel is AppPhone)
            {
                // редактирование разрешено только след.пользователю
                if ((Program.User.PC == "KC-114-06") && (Program.User.UserName == "kc-asistdir"))
                {
                    setEditButtonEnable(true, false, true, true);
                }
                else if (Program.User.Department == 500)
                {
                    setEditButtonEnable(false, false, true, false);
                }
            }

            // настройки пользователей
            else if (_currentModel is AppUsers)
            {
                if (Program.User.Department == 500)
                {
                    setEditButtonEnable(true, false, true, true);
                }
            }

            #endregion

            if (btnEditOnly) return;

            #region поиск документа по дате и номеру док-та (пользователи, шаблоны служебок и отделы)
            if ((_currentModel is AppNotes) || (_currentModel is AppUsers)
                || (_currentModel is AppNoteTemplates) || (_currentModel is AppDepartments))
            {
                datePickerStart.Enabled = true;
                datePickerEnd.Enabled = true;
                tbxFindDocNumber.Enabled = true;
                btnFindDocByNumber.Enabled = true;
            }
            else
            {
                datePickerStart.Enabled = false;
                datePickerEnd.Enabled = false;
                tbxFindDocNumber.Enabled = false;
                btnFindDocByNumber.Enabled = false;
            }
            #endregion

            // фильтр отдела только для гридов, у который есть колонка DepartmentId
            if (_currentModel.DataGrid.Columns.Contains("DepartmentId"))
            {
                cbDepartmentFilter.Enabled = true;
                btnDeleteDepartmentFilter.Enabled = true;

                // если дир то видим все отделы, или принудительный фильтр по отделу
                if (Program.User.ApprDir)
                {
                }
                else
                {
                    _dgvNoteRowChangedEnable = false;

                    if ((int)cbDepartmentFilter.SelectedValue == _currentDepId)
                        cbDepartmentFilter_SelectedIndexChanged(null, null);
                    else
                        cbDepartmentFilter.SelectedValue = _currentDepId.ToString();

                    _dgvNoteRowChangedEnable = true;
                }
            }
            else
            {
                cbDepartmentFilter.SelectedIndex = 0;
                cbDepartmentFilter.Enabled = false;
                btnDeleteDepartmentFilter.Enabled = false;
            }

//            chkCEO.Enabled = true;
//            chkCFO.Enabled = true;
        }

        private void setEditButtonEnable(bool createBtn, bool copyBtn, bool editBtn, bool delBtn)
        {
            btnNew.Enabled = createBtn;
            cmiGridCreate.Enabled = createBtn;

            btnCopy.Enabled = copyBtn;
            cmiGridCopy.Enabled = copyBtn;

            btnEdit.Enabled = editBtn;
            cmiGridEdit.Enabled = editBtn;

            btnDelete.Enabled = delBtn;
            cmiGridDelete.Enabled = delBtn;
        }

        #endregion

        #region edit grid buttons
        private void btnNew_Click(object sender, EventArgs e)
        {
            gridCreate();
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            gridCopy();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            gridEdit();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
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
        #endregion

        #region действия по редактированию данных
        private void gridCreate()
        {
            if (_currentModel == null) return;

            AppFuncs.WriteLogTraceMessage($" - {_currentModel.GetType().Name}.CreateNew()");
            _currentModel.CreateNewObject();
        }
        private void gridCopy()
        {
            if (_currentModel == null) return;

            AppFuncs.WriteLogTraceMessage($" - {_currentModel.GetType().Name}.Copy()");
            _currentModel.CopyToNewObject();
        }
        private void gridEdit()
        {
            if (_currentModel == null) return;

            AppFuncs.WriteLogTraceMessage($" - {_currentModel.GetType().Name}.Edit()");
            _currentModel.EditObject();
        }
        private void gridDelete()
        {
            if (_currentModel == null) return;

            AppFuncs.WriteLogTraceMessage($" - {_currentModel.GetType().Name}.Delete()");
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
            if ((_currentModel != null) && (_currentModel.DataGrid.DataSource != null))
            {
                (_currentModel.DataGrid.DataSource as DataTable).DefaultView.RowFilter = "";
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
                    if (Program.User.ApprDir) { }
                    else
                    {
                        cbDepartmentFilter.SelectedValue = _currentDepId.ToString();// придумать как вытянуть номер отдела без таблицы конфиг!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        cbDepartmentFilter.Enabled = false;
                        setAppModeButtonEnable(btnDeleteDepartmentFilter, false);
                    }
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
                    if (Program.User.ApprDir) { }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }

        private void dgvPhonebook_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.F) && e.Control)
            {
                findPIBStartWith();
            }

        }

        private void dgvNotes_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgvNoteRowChangedEnable) setEnableControls(true);
        }


        private void findPIBStartWith()
        {
            string value = "";
inputFindText:
            if (AppFuncs.InputBox("Пошук", "Введiть текст", ref value) == DialogResult.OK)
            {
                if (value.IsNull())
                {
                    MessageBox.Show("Введiть прiзвище.", "Пошук", MessageBoxButtons.OK);
                    goto inputFindText;
                }

                bool isFind = false;
                for (int i = 0; i < dgvPhonebook.Rows.Count; i++)
                {
                    if (dgvPhonebook.Rows[i].Cells["П.І.Б."].Value.ToString().ToUpper().StartsWith(value.ToUpper()))
                    {
                        dgvPhonebook.Rows[i].Selected = true;
                        dgvPhonebook.CurrentCell = dgvPhonebook.Rows[i].Cells["П.І.Б."];
                        dgvPhonebook.FirstDisplayedScrollingRowIndex = i;
                        isFind = true;
                        break;
                    }
                }
                if (!isFind)
                {
                    MessageBox.Show($"Прiзвище '{value}' не знайдено. Спробуйте ще раз.", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    goto inputFindText;
                }
            }
        }

        private void refreshDBTimer_Tick(object sender, EventArgs e)
        {

        }

        //private void refreshDBTimer_Tick(object sender, EventArgs e)
        //{
        //    Timer refresh = new Timer();
        //    refresh.Enabled = true;
        //    refresh.Start();
        //}
    }  // class
}
