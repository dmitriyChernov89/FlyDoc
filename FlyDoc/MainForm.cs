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
    public partial class FlyDoc : Form
    {
        public FlyDoc()
        {
            InitializeComponent();
        }
        String PC, UserName;
        int WhichSection = 0;  //Определяем в каком мы разделе(1-служебки,2-графики,3-кто на работе,4-телефонная книга)
        public static int DepartmentId = 0;  //Id Отделa
        bool EnableNotes;      //Доступ к служебкам
        bool EnableSchedule;   //Доступ к графику работы
        bool EnablePhone;      //Доступ на редактирование телефонного справочника
        bool EnableConfig;     //Доступ в настройки
        public static bool EnableApprovedNach;   //Право утверждения документов как начальник
        string Nach; // Имя того кто утврждает документ
        public static bool EnableApprovedSB;   //Право утверждения документов как СБ
        string SB; // Имя того кто утврждает документ
        public static bool EnableApprovedDir;   //Право утверждения документов как директор
        string Dir; // Имя того кто утврждает документ
        string Mail; // Почта для писем при запросе на утвердение документа
        private static SqlConnection conn = new SqlConnection(@"Data Source=KC-500-00;Initial Catalog=FlyDoc;Integrated Security=True");

        private void FlyDoc_Load(object sender, EventArgs e)
        {
            //Заполняем комбобокс отделы
            DataTable dtDeps = DBContext.GetDepartments();
            if (dtDeps != null)
            {
                cbDepartment.DataSource = dtDeps;
                cbDepartment.DisplayMember = "Name";
                cbDepartment.ValueMember = "Id";
            }


            //Проверяем куда давать доступ
            PC = System.Environment.MachineName;
            UserName = System.Environment.UserName;
            // заголовок окна
            this.Text = string.Format("FlyDoc (користувач - {0}, ПК - {1})", UserName, PC);

            DataTable dtConfig = DBContext.GetUserConfig(PC, UserName);
            // если нашли в таблице Access данного пользователя, т.е. вернули строку
            if ((dtConfig != null) && (dtConfig.Rows.Count > 0))
            {
                DataRow row = dtConfig.Rows[0];
                DepartmentId = (int)row["Department"];
                EnableNotes = (bool)row["Notes"];
                EnableSchedule = (bool)row["Schedule"];
                EnablePhone = (bool)row["Phone"];
                EnableConfig = (bool)row["Config"];
                EnableApprovedNach = (bool)row["ApprovedNach"];
                if (row["Nach"] is System.DBNull)
                { Nach = ""; }
                else { Nach = (string)row["Nach"]; }
                EnableApprovedSB = (bool)row["ApprovedSB"];
                if (row["SB"] is System.DBNull)
                { SB = ""; }
                else { SB = (string)row["SB"]; }
                EnableApprovedDir = (bool)row["ApprovedDir"];
                if (row["Dir"] is System.DBNull)
                { Dir = ""; }
                else{ Dir = (string)row["Dir"];}
                Mail = (string)row["Mail"];
            }

            //Выключаем кнопку настроек
            btconfig.Enabled = EnableConfig;
            btnotes.Enabled = EnableNotes;
          //btphone.Enabled = EnablePhone;
          //btschedule.Enabled = EnableSchedule;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // cbDepartment. = true;

            //Ставим даты в фильтре на период текущего месяца
            datePickerStart.Value = DateTime.Now.AddDays(-(DateTime.Now.Day-1));
            datePickerEnd.Value = DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month))- (DateTime.Now.Day));

            //Настраиваем комбобокс - только выбор - ставим значение по дефолту
            this.cbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            
            
            // Стартуем с телефонной книги
            btphone_Click_1(sender, e);
        }
        
        private void btnotes_Click(object sender, EventArgs e)
        {
            if (EnableNotes)
            {
                //Выключаем лишние кнопки
                btnNew.Enabled = true;
                btnEdit.Enabled = true;
                btnView.Enabled = true;
                datePickerStart.Enabled = true;
                datePickerEnd.Enabled = true;
                cbApproed.Enabled = true;
                ReloadData();
                WhichSection = 1;

                //Если есть доступ директора, то окрываем все отделы
                if (EnableApprovedDir)
                {

                }
                else
                {
                    cbDepartment.SelectedValue = DepartmentId.ToString();
                    cbDepartment.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Доступ до цієї опціЇ закритий! Зверніться в відділ АСУ","FlyDoc");
            }
        }

        /// <summary>
        /// Метод, который получает из БД список сл.записок, обновляет источник данных датагрида и скрывает первый столбец - идентификатор сл.зап. из БД (нужен для последующего изменения сл.зап.)
        /// </summary>
        public void ReloadData()
        {
            DataTable dtNotes = DBContext.GetNotes();  // чтение данных о сл.зап.
            if (dtNotes != null)
            {
                dataGridView1.DataSource = dtNotes;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void btschedule_Click(object sender, EventArgs e)
        {
            //Выключаем лишние кнопки
            if (EnableSchedule)
            {
                btnNew.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnNew.Enabled = false;
                btnEdit.Enabled = false;
            }
            //Если есть доступ директора, то окрываем все отделы
            if (EnableApprovedDir)
            {
            }
            else
            {
                cbDepartment.SelectedValue = DepartmentId.ToString();
                cbDepartment.Enabled = false;
            }
            btnView.Enabled = true;
            datePickerStart.Enabled = true;
            datePickerEnd.Enabled = true;
            cbApproed.Enabled = true;
            WhichSection = 2;

            //Выгребаем графики 
            DataTable dtSchedule = DBContext.GetSchedule();  // чтение данных о сл.зап.
            if (dtSchedule != null)
            {
                dataGridView1.DataSource = dtSchedule;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void btwork_Click(object sender, EventArgs e)
        {
            /*      //Выключаем лишние кнопки
                  btnNew.Enabled = false;
                  btnEdit.Enabled = false;
                  btnView.Enabled = false;
                  datePickerStart.Enabled = false;
                  datePickerEnd.Enabled = false;
                  cbApproed.Enabled = false;
                  cbDepartment.SelectedIndex = 0;
                  cbDepartment.Enabled = true;
                  WhichSection = 3;
                  */
            MessageBox.Show("Ця функція ще в процесі розробки");
        //    MessageBox.Show(Mail);
        }

        private void btphone_Click_1(object sender, EventArgs e)
        {
            //Выключаем лишние кнопки
            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnView.Enabled = false;
            datePickerStart.Enabled = false;
            datePickerEnd.Enabled = false;
            cbApproed.Enabled = false;
            cbDepartment.SelectedIndex = 0;
            cbDepartment.Enabled = true;
            WhichSection = 4;

            DataTable dtPhonebook = DBContext.GetPhonebook();  // чтение данных о сл.зап.
            if (dtPhonebook != null)
            {
                    dataGridView1.DataSource = dtPhonebook;
                    dataGridView1.Columns[0].Visible = false;
            }
            /*
            try
            {
                conn.Open();
                string sqlText = "SELECT * FROM vwPhonebook";
                SqlDataAdapter da = new SqlDataAdapter(sqlText, conn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds, "vwPhonebook");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                conn.Close();
            }
            catch (Exception ex)
            {
                display(ex.Message);
            } */
        }

        //Ловим ошибку
        private void display(string text)
        {
            MessageBox.Show(text, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Выключено на период разработки        SendMail(@"asu@kc.epicentrk.com", "Error!", "Упс, помилка!\nНа комп'ютері: "+PC+ " З користувачем: " +UserName+ " сталася наступна помилка:\n\n" + text);
        }

        //Выход
        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btconfig_Click(object sender, EventArgs e)
        {
            AppSetup setupForm = new AppSetup();
            setupForm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbApproed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (WhichSection)
            {
                case 1:
                    // экземпляр формы добавления сл.зап.
                    NewNote frmNewNote = new NewNote();
                    frmNewNote.ShowDialog();
                    break;
                case 2:
                    // экземпляр формы добавления график
                    Schedule frmSchedule = new Schedule();
                    frmSchedule.ShowDialog();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            //Защита от дурака
            datePickerEnd.MinDate = datePickerStart.Value;
        }


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

    }
}
