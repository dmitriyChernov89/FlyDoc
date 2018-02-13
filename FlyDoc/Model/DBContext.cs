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
using System.Reflection;
using System.Text;

namespace FlyDoc.Model
{
    // служебный класс для CRUD-методов к данным (Create-Read-Update-Delete)
    public static class DBContext
    {
        private static readonly string _configConStringName = "FlyDoc";
        private static readonly List<string> _items = new List<string>();

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
            lock (new object())
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
        }


        // метод, который выполняет SQL-запрос, не возвращающий данные, напр. вставка или удаление строк
        public static int Execute(string sqlText)
        {
            SqlConnection conn = getConnection();
            if (conn == null) return 0;

            int retVal = 0;
            if (openDB(conn))
            {
                SqlCommand sc = conn.CreateCommand();
                sc.CommandText = sqlText;
                try
                {
                    retVal = sc.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    showMsg("Ошибка выполнения команды в MS SQL Server: " + ex.Message);
                    retVal = 0;
                }
                finally
                {
                    closeDB(conn);
                }
            }

            return retVal;
        }

        // получить поля таблицы из схемы
        public static List<DBTableColumn> GetTableColumns(string tableName)
        {
            SqlConnection conn = getConnection();
            if (conn == null) return null;

            List<DBTableColumn> retVal = new List<DBTableColumn>();
            if (openDB(conn))
            {
                try
                {
                    // For the array, 0-member represents Catalog; 1-member represents Schema; 
                    // 2-member represents Table Name; 3-member represents Column Name. 
                    DataTable dt = conn.GetSchema("Columns", new string[4] { null, null, tableName, null });
                    //printSchemaColumns(dt);
                    if (dt != null)
                    {
                        DBTableColumn col;
                        foreach (DataRow row in dt.Rows)
                        {
                            col = new DBTableColumn()
                            {
                                Name = row["COLUMN_NAME"].ToString(),
                                IsNullable = row["IS_NULLABLE"].ToBool(),
                                TypeName = row["DATA_TYPE"].ToString(),
                                MaxLenght = row["CHARACTER_MAXIMUM_LENGTH"].ToInt()
                            };
                            retVal.Add(col);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errMsg = string.Format("Ошибка получения метаданных для таблицы [{0}]: {1}", tableName, ex.Message);
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

        #region schema
        private static void printSchemaColumns(DataTable dt)
        {
            Debug.Print("index\tname\ttype");
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                Debug.Print("{0}\t{1}\t{2}", i++, col.ColumnName, col.DataType.Name);
            }
        }

        /*
TABLE COLUMNS
    // For the array, 0-member represents Catalog; 1-member represents Schema; 
    // 2-member represents Table Name; 3-member represents Column Name. 
    DataTable dt = conn.GetSchema("Columns", new string[4] {null, null, tableName, null });
index name              type
0	TABLE_CATALOG       String
1	TABLE_SCHEMA        String
2	TABLE_NAME          String
3	COLUMN_NAME         String
4	ORDINAL_POSITION    Int32
5	COLUMN_DEFAULT      String
6	IS_NULLABLE         String
7	DATA_TYPE           String
8	CHARACTER_MAXIMUM_LENGTH Int32
9	CHARACTER_OCTET_LENGTH  Int32
10	NUMERIC_PRECISION       Byte
11	NUMERIC_PRECISION_RADIX Int16
12	NUMERIC_SCALE           Int32
13	DATETIME_PRECISION      Int16
14	CHARACTER_SET_CATALOG   String
15	CHARACTER_SET_SCHEMA    String
16	CHARACTER_SET_NAME      String
17	COLLATION_CATALOG       String
18	IS_SPARSE               Boolean
19	IS_COLUMN_SET           Boolean
20	IS_FILESTREAM           Boolean
*/
        #endregion
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

        public static List<IdNameTuple> GetDepartmentNamesList()
        {
            return getIdNameList("SELECT [Id], [Name] FROM [Department] ORDER BY [Name]");
        }

        public static Dictionary<int, string> GetDepartmentNamesDict()
        {
            return getIdNameDict("SELECT [Id], [Name] FROM [Department] ORDER BY [Name]");
        }
        #endregion

        #region User
        // получить всех пользователей
        public static DataTable GetUsers()
        {
            string sqlText = "SELECT * FROM vwUsers";
            return GetQueryTable(sqlText);
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
            return (Execute(sqlText) > 0);
        }

        public static bool DeleteSchedule(int Id)
        {
            string sqlText = string.Format("DELETE FROM Schedules WHERE (Id = {0})", Id);
            return (Execute(sqlText) > 0);
        }
        #endregion

        #region Notes
        // получить сл.зап. для отображения в датагриде
        // сл.зап. отсортированы по убыванию Id, чтобы новые записи отображались вверху таблицы
        // (сортировать здесь, т.к. SQL-представление не хочет сохранять запрос с ORDER BY (!!!!????)
        public static DataTable GetNotes()
        {
            return GetQueryTable("SELECT * FROM Notes");  // ORDER BY Id DESC");
        }

        public static List<Note> GetNotesModelList()
        {
            DataTable dt = GetQueryTable("SELECT * FROM Notes");
            if (dt != null)
            {
                List<Note> retVal = new List<Note>();
                foreach (DataRow row in dt.Rows)
                {
                    Note note = getEntityFromDataRow<Note>(row);
                    if (note != null) retVal.Add(note);
                }
                return retVal;
            }
            else
                return null;
        }

        public static DataRow GetNote(int Id)
        {
            string sqlText = string.Format("SELECT * FROM Notes WHERE (Id = {0})", Id);
            DataTable dt = GetQueryTable(sqlText);
            return ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];
        }

        public static DataTable GetNoteIncludeByNoteId(int noteId)
        {
            string sqlText = string.Format("SELECT * FROM NoteIncludeTable WHERE (IdNotes = {0})", noteId);
            DataTable dt = GetQueryTable(sqlText);
            return dt;
        }
        
        public static bool InsertNotes(Note note)
        {
            if (InsertEntity(note))
            {
                // note.Include
                if ((note.Id > 0) && (note.Include != null))
                {
                    DataTable dt;
                    string sqlText;
                    foreach (NoteInclude incl in note.Include)
                    {
                        incl.IdNotes = note.Id;
                        sqlText = incl.GetSQLInsertText(note.IncludeFields) + "; SELECT @@IDENTITY";
                        dt = GetQueryTable(sqlText);
                        incl.Id = Convert.ToInt32(dt.Rows[0][0]);
                    }
                }
            }

            return (note.Id > 0);
        }

        public static bool UpdateNotes(Note note)
        {
            if (UpdateEntity(note))
            {
                // note.Include
                if ((note.Include != null) && (note.Include.Count > 0))
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
                        string sqlText = string.Format("DELETE FROM [NoteIncludeTable] WHERE [Id] In ({0})", sDelIds);
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
                return true;
            }
            else
                return false;
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

        public static bool DeleteNotes(int id)
        {
            // удалить строки из NoteIncludeTable
            string sqlText = string.Format("DELETE FROM [NoteIncludeTable] WHERE ([IdNotes] = {0})", id);
            Execute(sqlText);

            return (DBContext.DeleteEntityById(Note._dbTableName, id));
        }

        #endregion

        #region Phone
        // получить телефонную книгу
        public static DataTable GetPhones()
        {
            return GetQueryTable("SELECT * From vwPhonebook");
        }

        #endregion

        #region шаблон службової
        public static DataTable GetNoteTemplates()
        {
            return GetQueryTable("SELECT * FROM NoteTemplates");
        }

        // получить настройки шаблона
        public static DataRow GetNoteTemplateById(int Id)
        {
            string sqlText = $"SELECT * FROM [NoteTemplates] WHERE ([Id] = '{Id}')";
            DataTable dt = GetQueryTable(sqlText);
            return ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];
        }

        public static List<IdNameTuple> GetNoteTemplateNamesList()
        {
            return getIdNameList("SELECT [Id], [Name] FROM [NoteTemplates] ORDER BY [Name]");
        }

        public static Dictionary<int, string> GetNoteTemplateNamesDict()
        {
            return getIdNameDict("SELECT [Id], [Name] FROM [NoteTemplates] ORDER BY [Name]");
        }
        #endregion

        #region entity funcs

        public static bool InsertEntity<T>(T entity) where T: IDBInfo
        {
            string insText = DBContext.GetSQLInsertText(entity);

            string sqlText = insText + "; SELECT @@IDENTITY";
            DataTable dt = GetQueryTable(sqlText);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                int newId = Convert.ToInt32(dt.Rows[0][0]);
                entity.Id = newId;
                return (newId > 0);
            }
            else
                return false;
        }

        public static bool DeleteEntityById(string tableName, int id)
        {
            string sqlText = $"DELETE FROM [{tableName}] WHERE (Id = {id.ToString()})";
            return (Execute(sqlText) > 0);
        }

        public static bool UpdateEntity<T>(T entity) where T: IDBInfo
        {
            string sqlText = DBContext.GetSQLUpdateText(entity);
            return (Execute(sqlText) > 0);
        }

        public static string toSQLString(object value)
        {
            if ((value == null) || (value.GetType().Equals(typeof(System.DBNull))))
                return "NULL";
            else if (value is string)
                return string.Format("'{0}'", value.ToString());
            else if (value is bool)
                return string.Format("{0}", ((bool)value ? "1" : "0"));
            else if (value is DateTime)
                return ((DateTime)value).ToSQLExpr();
            else if (value is float)
                return ((float)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else if (value is double)
                return ((double)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else if (value is decimal)
                return ((decimal)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else
                return value.ToString();
        }


        public static void PopulateEntityById<T>(T entity, int id) where T: IDBInfo
        {
            string sWhere = $"[Id] = '{id}'";

            PopulateEntityByWhere(entity, sWhere);
        }

        public static void PopulateEntityByWhere<T>(T entity, string sWhere) where T : IDBInfo
        {
            string sqlText = $"SELECT * FROM [{entity.DBTableName}] WHERE ({sWhere})";

            DataTable dt = GetQueryTable(sqlText);
            // если записть не найдена, то в entity Id=0
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                entity.Id = 0;
                return;
            }

            DataRow dr = dt.Rows[0];
            Type t = typeof(T);
            PropertyInfo[] pInfo = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in pInfo)
            {
                if (dr.Table.Columns.Contains(pi.Name) && !dr.IsNull(pi.Name))
                {
                    pi.SetValue(entity, dr[pi.Name], null);
                }
            }
        }

        private static T getEntityFromDataRow<T>(DataRow row) where T: new()
        {
            T retVal = new T();
            PropertyInfo[] pInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in pInfo)
            {
                if (row.Table.Columns.Contains(pi.Name) && !row.IsNull(pi.Name))
                {
                    pi.SetValue(retVal, row[pi.Name], null);
                }
            }
            return retVal;
        }

        public static string GetSQLInsertText<T>(T instance) where T: IDBInfo
        {
            string retVal = null;

            PropertyInfo[] pInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            DBTableColumn col;
            StringBuilder sbFields = new StringBuilder(), sbValues = new StringBuilder();

            foreach (PropertyInfo pi in pInfo)
            {
                // поле Id пропускаем
                if (pi.Name.ToLower() == "id") continue;

                List<DBTableColumn> dbColumns = (instance as IDBInfo).DBColumns;
                col = dbColumns.FirstOrDefault(c => c.Name.Equals(pi.Name, StringComparison.OrdinalIgnoreCase));
                if (col != null)
                {
                    if (sbFields.Length > 0) sbFields.Append(", ");
                    sbFields.Append(string.Format("[{0}]", col.Name));

                    if (sbValues.Length > 0) sbValues.Append(", ");
                    object value = pi.GetValue(instance, null);
                    if (col.TypeName.EndsWith("char"))
                    {
                        if (value == null)
                            sbValues.Append(col.IsNullable ? "Null" : "");
                        else
                        {
                            string sVal = value.ToString();
                            if ((col.MaxLenght > 0) && (sVal.Length > col.MaxLenght)) sVal = sVal.Substring(0, col.MaxLenght);
                            sbValues.Append(string.Format("'{0}'", sVal));
                        }
                    }
                    else
                    {
                        sbValues.Append(DBContext.toSQLString(value));
                    }
                }
            }
            string fields = sbFields.ToString(), values = sbValues.ToString();

            if (!string.IsNullOrEmpty(fields) && !string.IsNullOrEmpty(values))
            {
                string tableName = (instance as IDBInfo).DBTableName;
                retVal = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", tableName, fields, values);
            }

            return retVal;
        }

        public static string GetSQLUpdateText<T>(T instance) where T: IDBInfo
        {
            string retVal = null;
            int id = 0;

            PropertyInfo[] pInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            DBTableColumn col;
            StringBuilder sbFields = new StringBuilder();

            foreach (PropertyInfo pi in pInfo)
            {
                // поле Id пропускаем
                if (pi.Name.ToLower() == "id")
                {
                    id = (int)pi.GetValue(instance, null);
                    continue;
                }

                List<DBTableColumn> dbColumns = (instance as IDBInfo).DBColumns;
                col = dbColumns.FirstOrDefault(c => c.Name.Equals(pi.Name, StringComparison.OrdinalIgnoreCase));
                if (col != null)
                {
                    object value = pi.GetValue(instance, null);
                    string sVal = null;
                    if (col.TypeName.EndsWith("char"))
                    {
                        if (value == null)
                            sVal = (col.IsNullable ? "Null" : "");
                        else
                        {
                            sVal = value.ToString();
                            if ((col.MaxLenght > 0) && (sVal.Length > col.MaxLenght)) sVal = sVal.Substring(0, col.MaxLenght);
                            sVal = string.Format("'{0}'", sVal);
                        }
                    }
                    else
                    {
                        sVal = DBContext.toSQLString(value);
                    }

                    if (sVal != null)
                    {
                        if (sbFields.Length > 0) sbFields.Append(", ");
                        sbFields.Append(string.Format("[{0}] = {1}", col.Name, sVal));
                    }
                }
            }
            string fields = sbFields.ToString();

            if ((id > 0) && !string.IsNullOrEmpty(fields))
            {
                string tableName = (instance as IDBInfo).DBTableName;
                retVal = string.Format("UPDATE [{0}] SET {1} WHERE ([Id] = {2})", tableName, fields, id);
            }

            return retVal;
        }
        #endregion

        // получить список пар Id, Name из справочника
        private static List<IdNameTuple> getIdNameList(string sqlText)
        {
            DataTable dt = GetQueryTable(sqlText);
            if (dt != null)
            {
                List<IdNameTuple> retVal = new List<IdNameTuple>();
                foreach (DataRow row in dt.Rows)
                {
                    retVal.Add(new IdNameTuple(Convert.ToInt32(row[0]), Convert.ToString(row[1])));
                }
                return retVal;
            }
            else
                return null;
        }

        private static Dictionary<int, string> getIdNameDict(string sqlText)
        {
            DataTable dt = GetQueryTable(sqlText);
            if (dt != null)
            {
                Dictionary<int, string> retVal = new Dictionary<int, string>();
                foreach (DataRow row in dt.Rows)
                {
                    retVal.Add(Convert.ToInt32(row[0]), (row.IsNull(1) ? null : Convert.ToString(row[1])));
                }
                return retVal;
            }
            else
                return null;
        }

        private static void showErrorBox(string tableName, string actionName, string errText)
        {
            MessageBox.Show(string.Format("Ошибка {1} записи в {0}: {2}", actionName, tableName, errText), "Обновление " + tableName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }  // class DBContext

    public class DBTableColumn
    {
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public string TypeName { get; set; }
        public int MaxLenght { get; set; }
    }

    public class IdNameTuple
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IdNameTuple(int id, string name)
        {
            Id = id; Name = name;
        }

    }

}