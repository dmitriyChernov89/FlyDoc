using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FlyDoc.Model;
using FlyDoc.Views;
using System.Net.Mail;
using System.Net;
using FlyDoc.Forms;
using FlyDoc.Lib;

namespace FlyDoc
{
    public partial class Schedule : Form
    {
        private bool _isNew;
        private ScheduleModel _schedule;
        private bool _isChanged;

        public ScheduleModel ScheduleModel { get { return _schedule; } }

        public Schedule(ScheduleModel sched)
        {
            _isNew = (sched == null);
            AppFuncs.openEditForm(this.GetType().Name, _isNew);

            InitializeComponent();

            _schedule = sched;
            if (_schedule == null) _schedule = new ScheduleModel() { Date = dateTimePicker.MinDate};

            this.FormClosing += Schedule_FormClosing;

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            AppFuncs.closeEditForm(this.GetType().Name, e.CloseReason);
            base.OnFormClosed(e);
        }

        private void Schedule_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isChanged = isChangedControls();
            if (_isChanged)
            {
                DialogResult result = MessageBox.Show("Для збереження даних нажміть Да, щоб закрити без збереження, нажміть Нет. Не закривати вікно нажміть Отмена", "Збереження даних", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
                if (result == DialogResult.Cancel) e.Cancel = true;
                else if (result == DialogResult.Yes)
                {
                    _schedule.DepartmentId = (int)cbDepartment.SelectedValue;
                    _schedule.Date = dateTimePicker.Value;
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.None;
            }
        }

        private bool isChangedControls()
        {
            int selDepId = (int)(cbDepartment.SelectedValue??-1);

            return ((selDepId > -1) && (selDepId != _schedule.DepartmentId))
                || (dateTimePicker.Value != _schedule.Date);
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            //Заполняем комбобокс отделы
            FormsHelper.SetDepartmentsComboBox(cbDepartment, true);
            //DataTable dtDeps = DBContext.GetDepartments();
            //if (dtDeps != null)
            //{
            //    cbDepartment.DataSource = dtDeps;
            //    cbDepartment.DisplayMember = "Name";
            //    cbDepartment.ValueMember = "Id";
                
            //}
            //Настраиваем комбобокс
            //this.cbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbDepartment.SelectedValue = _departmentModel.ToString();
            ////Если есть доступ директора, то окрываем все отделы
            //if (FlyDoc.EnableApprovedDir)
            //{
            //}
            //else
            //{
            //    cbDepartment.Enabled = false;
            //}

            LoadData();

            //Настраиваем выбор даты
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd MMMM yyyy";
            // По дефолту первое число следующего месяца
            dateTimePicker.Value = DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - (DateTime.Now.Day) + 1);

            if (_schedule != null)
            {
                cbDepartment.SelectedValue = _schedule.DepartmentId;
                dateTimePicker.Value = _schedule.Date;
            }

        }
        //Выгребаем графики 
        public void LoadData()
        {
            DataTable dtSchInclude = DBContext.GetSchInclude();  // чтение данных о сл.зап.
            if (dtSchInclude != null)
            {
                dgvSchedule.DataSource = dtSchInclude;
                dgvSchedule.Columns[0].Visible = false;
                dgvSchedule.Columns[1].Visible = false;
                dgvSchedule.Columns[3].Visible = false;
              //  dgvSchedule.ReadOnly = true;
                dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            AppFuncs.WriteLogTraceMessage(" - press button 'Зберегти'");

            DateTime dt = dateTimePicker.Value.Date;
            
            MessageBox.Show(dt.ToString());
            
        }

        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
