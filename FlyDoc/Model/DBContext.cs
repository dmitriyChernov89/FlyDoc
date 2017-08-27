﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace FlyDoc.Model
{
    // служебный класс для CRUD-методов к данным (Create-Read-Update-Delete)

    public static class DBContext
    {
        private static readonly string _configConStringName = "FlyDoc";


        #region private methods

        private static string getConnString()
        {
            string retVal = null;
            try
            {
                retVal = ConfigurationManager.ConnectionStrings[_configConStringName].ConnectionString;
            }
            catch (Exception e)
            {
                showMsg("Ошибка получения строки подключения к БД из config-файла: " + e.Message);
            }
            return retVal;
        }
        private static SqlConnection getConnection()
        {
            string connString = getConnString();
            if (connString == null) return null;

            SqlConnection retVal = null;
            try
            {
                retVal = new SqlConnection(connString);
            }
            catch (Exception e)
            {
                showMsg("Ошибка создания подключения к БД: " + e.Message);
            }
            return retVal;
        }


        // открыть подключение к БД
        private static bool openDB(SqlConnection conn)
        {
            if (conn == null) return false;
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception e)
            {
                showMsg("Ошибка открытия подключения к БД: " + e.Message);
                return false;
            }
        }
        // обертка для закрыть подключение
        private static void closeDB(SqlConnection conn)
        {
            if ((conn == null) || (conn.State == ConnectionState.Closed)) return;

            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                showMsg("Ошибка закрытия подключения к БД: " + e.Message);
            }
        }

        private static void showMsg(string msg)
        {
            MessageBox.Show(msg, "Ошибка доступа к данным", MessageBoxButtons.OK, MessageBoxIcon.Error);
            FlyDoc.Forms.MainForm.SendMail(@"asu@kc.epicentrk.com", "Error!", "Упс, помилка!\nНа комп'ютері: " + System.Environment.MachineName + " З користувачем: " + System.Environment.UserName + " сталася наступна помилка:\n\n" + msg);
        }

        #endregion

        #region Public methods

        // получить DataTable из SELECT-запроса
        public static DataTable GetQueryTable(string queryString)
        {
            SqlConnection conn = getConnection();
            if (conn == null) return null;

            DataTable retVal = null;
            if (openDB(conn))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
                    retVal = new DataTable();
                    da.Fill(retVal);
                }
                catch (Exception ex)
                {
                    string errMsg = string.Format("Ошибка выполнения запроса MS SQL Server-у: запрос - {0}, ошибка - {1}", queryString, ex.Message);
                    showMsg(errMsg);
                    retVal = null;
                }
                finally
                {
                    closeDB(conn);
                }
            }

            return retVal;
        }

        // метод, который выполняет SQL-запрос, не возвращающий данные, напр. вставка или удаление строк
        public static bool Execute(string sqlText)
        {
            SqlConnection conn = getConnection();
            if (conn == null) return false;

            bool retVal = true;
            if (openDB(conn))
            {
                SqlCommand sc = conn.CreateCommand();
                sc.CommandText = sqlText;
                try
                {
                    sc.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    showMsg("Ошибка выполнения команды в MS SQL Server: " + ex.Message);
                    retVal = false;
                }
                finally
                {
                    closeDB(conn);
                }
            }

            return retVal;
        }
        #endregion

        public static int GetLastInsertedId()
        {
            DataTable dt = GetQueryTable("SELECT @@IDENTITY");
            var retVal = dt.Rows[0][0];
            return (retVal == null) ? 0 : (int)retVal;
        }

        // статические методы для получения данных 
        // здесь можно получить как из таблиц, так и из представлений MS SQL, выбирать нужные поля, фильтровать и сортировать записи, короче, все, что можно сделать в SELECT-операторе
        #region get data

        public static DataTable GetSchInclude()
        {
            return GetQueryTable("SELECT * From vwSchInclude");
        }

        // получить телефонную книгу
        public static DataTable GetPhonebook()
        {
            return GetQueryTable("SELECT * FROM vwPhoneBook");
        }

        // получить сл.зап. для отображения в датагриде
        // сл.зап. отсортированы по убыванию Id, чтобы новые записи отображались вверху таблицы
        // (сортировать здесь, т.к. SQL-представление не хочет сохранять запрос с ORDER BY (!!!!????)
        public static DataTable GetNotes()
        {
            return GetQueryTable("SELECT * FROM vwNote");// ORDER BY Id DESC");
        }

        public static DataTable GetNoteTemplates()
        {
            return GetQueryTable("SELECT * FROM NoteTemplates");
        }

        #endregion

        #region  Department
        // получить отделы из SQL-таблицы
        public static DataTable GetDepartments()
        {
            return GetQueryTable("SELECT * FROM Department ORDER BY Name");
        }
        public static string GetDepartmentName(int Id)
        {
            string retVal = null;
            string sqlText = string.Format("SELECT Name FROM Department WHERE (Id = {0})", Id);
            using (DataTable dt = GetQueryTable(sqlText))
            {
                retVal = (dt == null) || (dt.Rows.Count == 0) ? null : (string)dt.Rows[0][0];
            }
            return retVal;
        }
        public static bool InsertDepartment(Department dep)
        {
            string sqlText = string.Format("INSERT INTO Department (Id, Name) VALUES ({0}, '{1}')", dep.Id, dep.Name);
            return Execute(sqlText);
        }
        public static bool UpdateDepartment(Department dep, int Id)
        {
            string sqlText = string.Format("UPDATE Department SET Id = {0}, Name = '{1}' WHERE (Id = {2})", dep.Id, dep.Name, Id);
            return Execute(sqlText);
        }
        public static bool DeleteDepartment(int Id)
        {
            string sqlText = string.Format("DELETE FROM Department WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }
        #endregion

        #region User
        // получить всех пользователей
        public static DataTable GetUsers()
        {
            string sqlText = "SELECT * FROM vwUsers";
            return GetQueryTable(sqlText);
        }

        // получить настройки пользователя
        public static DataRow GetUserConfig(string PC, string UserName)
        {
            string sqlText = string.Format("SELECT * FROM Access WHERE (PC='{0}') AND (UserName='{1}')", PC, UserName);
            DataTable dt = GetQueryTable(sqlText);
            return ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];
        }

        public static bool InsertUser(User user, out int newId)
        {
            string sqlText = string.Format("INSERT INTO Access (PC, UserName, Department, Notes, Schedule, Phone, Config, ApprovedNach, ApprovedSB, ApprovedDir, Mail) VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, '{10}'); SELECT @@IDENTITY",
                user.PC, user.UserName, user.DepartmentId,
                (user.AllowNote) ? 1 : 0, (user.AllowSchedule) ? 1 : 0, (user.AllowPhonebook) ? 1 : 0, (user.AllowConfig) ? 1 : 0, (user.AllowApprovedNach) ? 1 : 0,
                (user.AllowApproverSB) ? 1 : 0, (user.AllowApproverDir) ? 1 : 0, user.enterMail);
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            return (newId > 0);
        }
        public static bool UpdateUser(User user)
        {
            string sqlText = string.Format("UPDATE Access SET PC = '{1}', UserName = '{2}', Department = {3}, Notes = {4}, Schedule = {5}, Phone = {6}, Config = {7}, ApprovedNach = {8}, ApprovedSB = {9}, ApprovedDir = {10}, Mail = '{11}' WHERE (Id = {0})",
                user.Id, user.PC, user.UserName, user.DepartmentId,
                (user.AllowNote) ? 1 : 0, (user.AllowSchedule) ? 1 : 0, (user.AllowPhonebook) ? 1 : 0, (user.AllowConfig) ? 1 : 0, (user.AllowApprovedNach) ? 1 : 0,
                (user.AllowApproverSB) ? 1 : 0, (user.AllowApproverDir) ? 1 : 0, user.enterMail);
            return Execute(sqlText);
        }
        public static bool DeleteUser(int Id)
        {
            string sqlText = string.Format("DELETE FROM Access WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }
        #endregion

        #region Schedule
        public static DataTable GetSchedule()
        {
            return GetQueryTable("SELECT * From vwSchedules");
        }
        public static bool InsertSchedule(ScheduleModel sched, out int newId)
        {
            string sqlText = string.Format("INSERT INTO Schedules ([IdDepartment], [Data], [Approved]) VALUES ({0}, '{1}', {2}); SELECT @@IDENTITY",
                sched.DepartmentId, sched.Date, (sched.Approved) ? 1 : 0);
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            return (newId > 0);
        }

        public static bool UpdateSchedule(ScheduleModel sched)
        {
            string sqlText = string.Format("UPDATE Schedules SET [IdDepartment] = {1}, [Data] = '{2}', [Approved] = {3} WHERE (Id = {0})", sched.Id, sched.DepartmentId, sched.Date, (sched.Approved) ? 1 : 0);
            return Execute(sqlText);
        }

        public static bool DeleteSchedule(int Id)
        {
            string sqlText = string.Format("DELETE FROM Schedules WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }
        #endregion

        #region Notes
        public static DataTable GetNote()
        {
            return GetQueryTable("SELECT * From vwNote");
        }
        public static DataRow GetNoteInclude(int Id)
        {
            string sqlText = string.Format("SELECT * FROM Notes WHERE (Id='{0}')", Id);
            DataTable dt = GetQueryTable(sqlText);
            return ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];
        }
        public static bool NoteApproved(int Id, string ApprColumn, bool Appr)
        {
            string sqlText = string.Format("UPDATE Notes SET [ApprDir] = NULL WHERE (Id = {0})", Id, Appr);
            //MessageBox.Show(sqlText);
            return Execute(sqlText);
        }
        public static bool InsertNotes(Note note, out int newId)
        {
            string sqlText = string.Format("INSERT INTO Note ([Name], [Id], [Number], [DataCreate], [DepartmentId], [NoteTemplateId]) VALUES ('{0}', {1}, '{2}', '{3}', {4}, {5}); SELECT @@IDENTITY",
                 note.Id, note.Date, note.DepartmentId, note.NoteTemplateId);
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            return (newId > 0);
        }

        public static bool UpdateNotes(Note note)
        {
            string sqlText = string.Format("UPDATE Notes SET [Name] = {0}, [Number] = '{2}', [Datecreate] = {3}, [DepartmentId] = {4}, [NoteTemplateId] = {5} WHERE (Id = {1})", note.Id, note.Date, note.DepartmentId, note.NoteTemplateId);
            return Execute(sqlText);
        }

        public static bool DeleteNotes(int Id)
        {
            string sqlText = string.Format("DELETE FROM Notes WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }

        #endregion

        #region Phone
        public static DataTable GetPhone()
        {
            return GetQueryTable("SELECT * From vwPhonebook");
        }
        public static bool InsertPhone(PhoneModel phone, out int newId)
        {
            string sqlText = string.Format("INSERT INTO Phonebook ([Name], [Positions], [Name], [Dect], [Phone], [Mobile], [Mail]) VALUES ('{0}', {1}, '{2}', '{3}', {4}, {5}, {6}); SELECT @@IDENTITY",
                phone.Name, phone.Positions, phone.FIO, phone.Dect, phone.PhoneNumber, phone.Mobile, phone.eMail);
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            return (newId > 0);
        }

        public static bool UpdatePhone(PhoneModel phone)
        {
            string sqlText = string.Format("UPDATE Phonebook SET [Name] = {1}, [Positions] = '{2}', [Name] = {3}, [Dect] = {4}, [Phone] = {5}, [Mobile] = {6}, [Mail] = {7} WHERE (Id = {0})", phone.Id, phone.Name, phone.Positions, phone.FIO, phone.Dect, phone.PhoneNumber, phone.Mobile, phone.eMail);
            return Execute(sqlText);
        }

        public static bool DeletePhone(int Id)
        {
            string sqlText = string.Format("DELETE FROM Phonebook WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }

        #endregion

        // получить настройки шаблона
        public static DataRow GetNoteTemplatesConfig(int Id)
        {
            string sqlText = string.Format("SELECT * FROM NoteTemplates WHERE (Id='{0}')", Id);
            DataTable dt = GetQueryTable(sqlText);
            return ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];
        }
    }  // class DBContext
}