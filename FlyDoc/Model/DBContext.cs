using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;
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

        public static List<string> GetColumnsNameList(DataTable dataTable)
        {
            if (dataTable == null) return null;

            List<string> retVal = new List<string>();
            foreach (DataColumn item in dataTable.Columns)
            {
                retVal.Add(item.ColumnName);
            }
            return retVal;
        }

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
        public static int GetDepartmenIdByName(string depName)
        {
            int retVal = -1;
            string sqlText = string.Format("SELECT Id FROM Department WHERE (Name = '{0}')", depName);
            using (DataTable dt = GetQueryTable(sqlText))
            {
                retVal = (dt == null) || (dt.Rows.Count == 0) ? -1 : (int)dt.Rows[0][0];
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
            string sqlText = $"INSERT INTO Access (PC, UserName, Department, Name, HeadNach, Notes, Schedule, Phone, Config, ApprAvtor, ApprDir, ApprComdir, ApprSBNach, ApprSB, ApprKasa, ApprNach, ApprFin, ApprDostavka, ApprEnerg, ApprSklad, ApprBuh, ApprASU, Mail) VALUES ('{user.PC}', '{user.UserName}', {user.DepartmentId}, '{user.Name}', '{user.HeadNach}', {((user.AllowNote) ? 1 : 0)}, {((user.AllowSchedule) ? 1 : 0)}, {((user.AllowPhonebook) ? 1 : 0)}, {((user.AllowConfig) ? 1 : 0)}, {((user.AllowApprAvtor) ? 1 : 0)}, {((user.AllowApproverDir) ? 1 : 0)}, {((user.AllowApprComdir) ? 1 : 0)}, {((user.AllowApprSBNach) ? 1 : 0)}, {((user.AllowApproverSB) ? 1 : 0)}, {((user.AllowApprKasa) ? 1 : 0)}, {((user.AllowApprovedNach) ? 1 : 0)}, {((user.AllowApprFin) ? 1 : 0)}, {((user.AllowApprDostavka) ? 1 : 0)}, {((user.AllowApprEnerg) ? 1 : 0)}, {((user.AllowApprSklad) ? 1 : 0)}, {((user.AllowApprBuh) ? 1 : 0)}, {((user.AllowApprASU) ? 1 : 0)},  '{user.enterMail}'); SELECT @@IDENTITY";
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            return (newId > 0);
        }
        public static bool UpdateUser(User user)
        {
            string sqlText = $"UPDATE Access SET PC = '{user.PC}', UserName = '{user.UserName}', Department = {user.DepartmentId}, Name = '{user.Name}', HeadNach = '{user.HeadNach}', Notes = {((user.AllowNote) ? 1 : 0)}, Schedule = {((user.AllowSchedule) ? 1 : 0)}, Phone = {((user.AllowPhonebook) ? 1 : 0)}, Config = {((user.AllowConfig) ? 1 : 0)}, ApprAvtor = {((user.AllowApprAvtor) ? 1 : 0)}, ApprDir = {((user.AllowApproverDir) ? 1 : 0)}, ApprComdir = {((user.AllowApprComdir) ? 1 : 0)}, ApprSBNach = {((user.AllowApprSBNach) ? 1 : 0)}, ApprSB = {((user.AllowApproverSB) ? 1 : 0)}, ApprKasa = {((user.AllowApprKasa) ? 1 : 0)}, ApprNach = {((user.AllowApprovedNach) ? 1 : 0)}, ApprFin = {((user.AllowApprFin) ? 1 : 0)}, ApprDostavka = {((user.AllowApprDostavka) ? 1 : 0)}, ApprEnerg = {((user.AllowApprEnerg) ? 1 : 0)}, ApprSklad = {((user.AllowApprSklad) ? 1 : 0)}, ApprBuh = {((user.AllowApprBuh) ? 1 : 0)}, ApprASU = {((user.AllowApprASU) ? 1 : 0)}, Mail = '{user.enterMail}' WHERE (Id = {user.Id})";
            return Execute(sqlText);
        }
        public static bool DeleteUser(int Id)
        {
            string sqlText = string.Format("DELETE FROM Access WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }
        #endregion

        #region Schedule
        public static DataTable GetSchInclude()
        {
            return GetQueryTable("SELECT * From vwSchInclude");
        }

        public static DataTable GetSchedule()
        {
            return GetQueryTable("SELECT * From vwSchedules");
        }
        public static bool InsertSchedule(ScheduleModel sched, out int newId)
        {
            string sqlText = $"INSERT INTO Schedules ([IdDepartment], [Data], [Approved]) VALUES ({sched.DepartmentId}, '{sched.Date}', {((sched.Approved) ? 1 : 0)}); SELECT @@IDENTITY";
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            return (newId > 0);
        }

        public static bool UpdateSchedule(ScheduleModel sched)
        {
            string sqlText = $"UPDATE Schedules SET [IdDepartment] = {sched.DepartmentId}, [Data] = '{sched.Date}', [Approved] = {((sched.Approved) ? 1 : 0)} WHERE (Id = {sched.Id})";
            return Execute(sqlText);
        }

        public static bool DeleteSchedule(int Id)
        {
            string sqlText = string.Format("DELETE FROM Schedules WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }
        #endregion

        #region Notes
        // получить сл.зап. для отображения в датагриде
        // сл.зап. отсортированы по убыванию Id, чтобы новые записи отображались вверху таблицы
        // (сортировать здесь, т.к. SQL-представление не хочет сохранять запрос с ORDER BY (!!!!????)
        public static DataTable GetNotes()
        {
            return GetQueryTable("SELECT * FROM vwNote");// ORDER BY Id DESC");
        }

        public static DataRow GetNote(int Id)
        {
            string sqlText = string.Format("SELECT * FROM Notes WHERE (Id = {0})", Id);
            DataTable dt = GetQueryTable(sqlText);
            return ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];
        }

        public static DataTable GetNoteTemplates()
        {
            return GetQueryTable("SELECT * FROM NoteTemplates");
        }

        // получить настройки шаблона
        public static DataRow GetNoteTemplatesConfig(int Id)
        {
            string sqlText = $"SELECT * FROM NoteTemplates WHERE (Id='{Id}')";
            DataTable dt = GetQueryTable(sqlText);
            return ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];
        }

        public static DataTable GetNoteIncludeByNoteId(int noteId)
        {
            string sqlText = string.Format("SELECT * FROM NoteIncludeTable WHERE (IdNotes = {0})", noteId);
            DataTable dt = GetQueryTable(sqlText);
            return dt;
        }
        
        public static bool InsertNotes(Note note, out int newId)
        {
            string sqlText = $"INSERT INTO Notes (Templates, IdDepartment, [Date], NameAvtor, BodyUp, BodyDown, HeadNach, HeadDir) VALUES ({note.NoteTemplateId}, {note.DepartmentId}, {note.Date.ToSQLExpr()}, '{note.NameAvtor}', '{note.BodyUp}', '{note.BodyDown}', '{note.HeadNach}', '{note.HeadDir}'); SELECT @@IDENTITY";
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            note.Id = newId;

            // note.Include
            if ((newId > 0) && (note.Include != null))
            {
                foreach (NoteInclude incl in note.Include)
                {
                    incl.IdNotes = newId;
                    sqlText = incl.GetSQLInsertText(note.IncludeFields) + "; SELECT @@IDENTITY";
                    dt = GetQueryTable(sqlText);
                    incl.Id = Convert.ToInt32(dt.Rows[0][0]);
                }
            }

            return (newId > 0);
        }

        public static bool UpdateNotes(Note note)
        {
            string sqlText = string.Format("UPDATE Notes SET {0} WHERE (Id = {1})", note.GetSQLUpdateString(), note.Id);
            bool result = false;
            try
            {
                result = Execute(sqlText);
            }
            catch (Exception ex)
            {
                showErrorBox("Notes", "обновления", ex.Message + ": " + sqlText);
            }

            // note.Include
            if ((result) && (note.Include != null) && (note.Include.Count > 0))
            {
                // то, что лежит в БД
                DataTable dtIncl = GetNoteIncludeByNoteId(note.Id);
                List<int> dbInclIds = new List<int>();
                foreach (DataRow item in dtIncl.Rows) dbInclIds.Add((int)item["Id"]);

                // удалить из БД строки, отсутствующие в таблице
                int[] delIds = dbInclIds.Except(note.Include.Select(i => i.Id)).ToArray();
                if (delIds.Length > 0)
                {
                    string sDelIds = string.Join(",", delIds.Select(j => j.ToString()).ToArray());
                    sqlText = string.Format("DELETE FROM NoteIncludeTable WHERE [Id] In ({0})", sDelIds);
                    try
                    {
                        Execute(sqlText);
                    }
                    catch (Exception ex)
                    {
                        showErrorBox("NoteIncludeTable", "удаления", ex.Message + ": " + sqlText);
                    }
                }
                // обновить или добавить
                foreach (NoteInclude incl in note.Include)
                {
                    updNoteIncl(incl, note.Id, note);
                }
            }

            return result;
        }

        private static void updNoteIncl(NoteInclude incl, int noteId, Note note)
        {
            string sqlText = "";

            // добавить
            if (incl.Id == 0)
            {
                incl.IdNotes = noteId;
                try
                {
                    sqlText = incl.GetSQLInsertText(note.IncludeFields) + "; SELECT @@IDENTITY";
                    using (DataTable dtTmp = GetQueryTable(sqlText))
                    {
                        incl.Id = Convert.ToInt32(dtTmp.Rows[0][0]);
                    }
                }
                catch (Exception ex)
                {
                    showErrorBox("NoteIncludeTable", "добавления", ex.Message + ": " + sqlText);
                }
            }
            else
            {
                try
                {
                    sqlText = incl.GetSQLUpdateText(note.IncludeFields);
                    Execute(sqlText);
                }
                catch (Exception ex)
                {
                    showErrorBox("NoteIncludeTable", "обновления", ex.Message + ": " + sqlText);
                }
            }
        }

        public static bool DeleteNotes(int Id)
        {
            string sqlText = string.Format("DELETE FROM Notes WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }

        #endregion

        #region Phone
        // получить телефонную книгу
        public static DataTable GetPhones()
        {
            return GetQueryTable("SELECT * From vwPhonebook");
        }
        public static bool InsertPhone(PhoneModel phone, out int newId)
        {
            string sqlText = $"INSERT INTO Phonebook ([Department], [Positions], [FIO], [Dect], [Phone], [Mobile], [Mail]) VALUES ({phone.DepartmentId}, '{phone.Positions}', '{phone.Name}', '{phone.Dect}', '{phone.PhoneNumber}', '{phone.Mobile}', '{phone.eMail}'); SELECT @@IDENTITY";
            DataTable dt = GetQueryTable(sqlText);
            newId = Convert.ToInt32(dt.Rows[0][0]);
            return (newId > 0);
        }

        public static bool UpdatePhone(PhoneModel phone)
        {
            string sqlText = $"UPDATE Phonebook SET [Department] = {phone.DepartmentId}, [Positions] = '{phone.Positions}', [FIO] = '{phone.Name}', [Dect] = '{phone.Dect}', [Phone] = '{phone.PhoneNumber}', [Mobile] = '{phone.Mobile}', [Mail] = '{phone.eMail}' WHERE (Id = {phone.Id})";
            return Execute(sqlText);
        }

        public static bool DeletePhone(int Id)
        {
            string sqlText = string.Format("DELETE FROM Phonebook WHERE (Id = {0})", Id);
            return Execute(sqlText);
        }

        #endregion

        private static void showErrorBox(string tableName, string actionName, string errText)
        {
            MessageBox.Show(string.Format("Ошибка {1} записи в {0}: {2}", actionName, tableName, errText), "Обновление " + tableName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }  // class DBContext
}