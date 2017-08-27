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

namespace FlyDoc
{
    public partial class Schedule : Form
    {
        public Schedule()
        {
            InitializeComponent();
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            //Заполняем комбобокс отделы
            DataTable dtDeps = DBContext.GetDepartments();
            if (dtDeps != null)
            {
                cbDepartment.DataSource = dtDeps;
                cbDepartment.DisplayMember = "Name";
                cbDepartment.ValueMember = "Id";
                
            }
            //Настраиваем комбобокс
            this.cbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDepartment.SelectedValue = FlyDoc.DepartmentId.ToString();
            //Если есть доступ директора, то окрываем все отделы
            if (FlyDoc.EnableApprovedDir)
            {
            }
            else
            {
                cbDepartment.Enabled = false;
            }

            LoadData();

            //Настраиваем выбор даты
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "MMMM yyyy";
            // По дефолту первое число следующего месяца
            dateTimePicker.Value = DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - (DateTime.Now.Day) + 1);
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
            DateTime dt = dateTimePicker.Value.Date;
            
            MessageBox.Show(dt.ToString());
            
        }
    }
}
