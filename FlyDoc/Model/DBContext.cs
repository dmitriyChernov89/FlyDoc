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
    public class DBContext : IDisposable
    {
        #region static members
        private static string _configConStringName = "FlyDoc";

        public static Action<string> BeforeDBCallAction;
        public static Action<string> DBErrorAction;

        public static string ConfigConnectionStringName
        {
            get { return _configConStringName; }
            set { _configConStringName = value; }
        }

        static DBContext()
        {
        }

        #endregion


        private string _connString;
        public string ConnectionString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        private string _errMsg;
        public string ErrMsg { get { return _errMsg; } }

        private SqlConnection _conn;

        public DBContext()
        {
            setConnection();
        }

        #region base funcs
        private void setConnection()
        {
            try
            {
                _connString = ConfigurationManager.ConnectionStrings[_configConStringName].ConnectionString;
                // получить Connection
                try
                {
                    _conn = new SqlConnection(_connString);
                }
                catch (Exception e)
                {
                    errorAction("Ошибка создания подключения к БД: " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorAction("Ошибка получения строки подключения к БД из config-файла: " + e.Message);
            }
        }

        // открыть подключение к БД
        public bool Open()
        {
            if (_conn == null) return false;
            try
            {
                if (_conn.State == ConnectionState.Broken) _conn.Close();
                if (_conn.State == ConnectionState.Closed) _conn.Open();
                return true;
            }
            catch (Exception e)
            {
                errorAction("Ошибка открытия подключения к БД: " + e.Message);
                return false;
            }
        }
        public bool Close()
        {
            if (_conn == null) return false;
            try
            {
                if (_conn.State != ConnectionState.Closed) _conn.Close();
                return true;
            }
            catch (Exception e)
            {
                errorAction("Ошибка закрытия подключения к БД: " + e.Message);
                return false;
            }
        }

        // получить DataTable из SELECT-запроса
        public DataTable GetQueryTable(string sqlText)
        {
            BeforeDBCallAction?.Invoke(sqlText);

            DataTable retVal = null;
            if (Open())
            {
                SqlDataAdapter da = new SqlDataAdapter(sqlText, _conn);
                retVal = new DataTable();
                try
                {
                    da.Fill(retVal);
                }
                catch (Exception ex)
                {
                    string errMsg = $"Ошибка выполнения запроса к MS SQL Server-у: ошибка - {ex.Message}\nзапрос: {sqlText}, ";
                    errorAction(errMsg);
                    retVal = null;
                }
                finally
                {
                    da.Dispose();
                }
            }

            return retVal;
        }

        // метод, который выполняет SQL-запрос, не возвращающий данные, напр. вставка или удаление строк
        public bool ExecuteCommand(string sqlText)
        {
            BeforeDBCallAction?.Invoke(sqlText);

            bool retVal = true;
            if (Open())
            {
                SqlCommand sc = _conn.CreateCommand();
                sc.CommandType = CommandType.Text;
                sc.CommandText = sqlText;
                try
                {
                    sc.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    errorAction($"Ошибка выполнения команды в MS SQL Server: {ex.Message}\nкоманда: {sqlText}");
                    retVal = false;
                }
                finally
                {
                    sc.Dispose();
                }
            }
            return retVal;
        }

        public object ExecuteScalar(string sqlText)
        {
            BeforeDBCallAction?.Invoke(sqlText);

            object retVal = null;
            if (Open())
            {
                SqlCommand sc = _conn.CreateCommand();
                sc.CommandType = CommandType.Text;
                sc.CommandText = sqlText;
                try
                {
                    retVal = sc.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    errorAction($"Ошибка выполнения команды в MS SQL Server: {ex.Message}\nкоманда: {sqlText}");
                    retVal = false;
                }
                finally
                {
                    sc.Dispose();
                }
            }

            return retVal;
        }

        // получить поля таблицы из схемы
        public List<DBTableColumn> GetTableColumns(string tableName)
        {
            BeforeDBCallAction?.Invoke($"Get schema for table '{tableName}'");

            List<DBTableColumn> retVal = new List<DBTableColumn>();
            if (Open())
            {
                try
                {
                    // For the array, 0-member represents Catalog; 1-member represents Schema; 
                    // 2-member represents Table Name; 3-member represents Column Name. 
                    DataTable dt = _conn.GetSchema("Columns", new string[4] { null, null, tableName, null });
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
                    errorAction(errMsg);
                    retVal = null;
                }
                finally
                {
                }
            }

            return retVal;
        }


        private void errorAction(string msg)
        {
            _errMsg = msg;
            DBErrorAction?.Invoke(msg);
        }

        public void ClearErrMsg()
        {
            _errMsg = null;
        }

        public void Dispose()
        {
            Close();
            _conn.Dispose();
        }

        #endregion

        #region public static methods
        // проверка доступа к БД
        public static bool CheckDBConnection(string getCountTableName, out string outMsg)
        {
            outMsg = null;
            bool retVal = false;
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    if (getCountTableName != null)
                    {
                        string sqlText = $"SELECT Count(*) FROM [{getCountTableName}]";
                        int cnt = (int)db.ExecuteScalar(sqlText);
                        outMsg = $"table [{getCountTableName}] has {cnt.ToString()} records.";
                    }
                    else
                    {
                        outMsg = "DB open was successful.";
                    }
                    retVal = true;
                    db.Close();
                }
                else
                {
                    outMsg = db.ErrMsg;
                }
            }

            return retVal;
        }

        // возвращает кол-во записей из таблицы tableName
        public static int GetRowsCount(string tableName)
        {
            int retVal = 0;
            string sqlText = $"SELECT Count(*) FROM [{tableName}]";
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    retVal = (int)db.ExecuteScalar(sqlText);
                }
            }
            return retVal;
        }

        public static int GetLastInsertedId()
        {
            int retVal = 0;
            string sqlText = "SELECT @@IDENTITY";
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    retVal = (int)db.ExecuteScalar(sqlText);
                }
            }
            return retVal;
        }

        public static int GetLastAffectedRowCount()
        {
            int retVal = 0;
            string sqlText = "SELECT @@ROWCOUNT";
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    retVal = (int)db.ExecuteScalar(sqlText);
                }
            }
            return retVal;
        }

        // получить список пар Id, Name из справочника
        public static List<IdNameTuple> GetPairIdNameList(string sqlText)
        {
            List<IdNameTuple> retVal = new List<IdNameTuple>();
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    using (DataTable dt = db.GetQueryTable(sqlText))
                    {
                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                retVal.Add(new IdNameTuple(Convert.ToInt32(row[0]), Convert.ToString(row[1])));
                            }
                        }
                    }
                }
            }
            return ((retVal.Count == 0) ? null : retVal);
        }

        private static Dictionary<int, string> GetPairIdNameDict(string sqlText)
        {
            Dictionary<int, string> retVal = new Dictionary<int, string>();
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    using (DataTable dt = db.GetQueryTable(sqlText))
                    {
                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                retVal.Add(Convert.ToInt32(row[0]), (row.IsNull(1) ? null : Convert.ToString(row[1])));
                            }
                        }
                    }
                }
            }
            return ((retVal.Count == 0) ? null : retVal);
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

        public static int ExecuteDMLAndGetAffectedRowCount(string dmlText)
        {
            int retVal = 0;
            string sqlText = dmlText + "; SELECT @@ROWCOUNT";
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    retVal = (int)db.ExecuteScalar(sqlText);
                }
            }
            return retVal;
        }

        public static int InsertRecordAndReturnNewId(string dmlText)
        {
            int retVal = 0;
            string sqlText = dmlText + "; SELECT @@IDENTITY";
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    retVal = (int)db.ExecuteScalar(sqlText);
                }
            }
            return retVal;
        }

        // ищет запись в таблице tableName по условию where и, если запись найдена, то возвращает значение поля Id из таблицы, иначе возвращает 0
        public static int FindEntityByWhere(string tableName, string where)
        {
            int retVal = 0;

            string sqlText = $"SELECT Id FROM [{tableName}] WHERE ({where})";
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    object oValue = db.ExecuteScalar(sqlText);
                    if (oValue != null) retVal = Convert.ToInt32(oValue);
                }
            }

            return retVal;
        }

        public static DataRow GetEntityRow(string tableName, int id)
        {
            DataRow retVal = null;
            string sqlText = $"SELECT * FROM [{tableName}] WHERE ([Id]={id.ToString()})";
            using (DBContext db = new DBContext())
            {
                if (db.Open())
                {
                    using (DataTable dt = db.GetQueryTable(sqlText))
                    {
                        if ((dt != null) && (dt.Rows.Count > 0)) retVal = dt.Rows[0];
                    }
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

        #region entity funcs

        public static bool InsertEntity<T>(T entity) where T : IDBInfo
        {
            string insText = GetSQLInsertText(entity);

            string sqlText = insText + "; SELECT @@IDENTITY";
            DataTable dt = null;
            using (DBContext db = new DBContext())
            {
                dt = db.GetQueryTable(sqlText);
            }
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
            string sqlText = $"DELETE FROM [{tableName}] WHERE (Id = {id.ToString()}); SELECT @@ROWCOUNT";
            int iAffected = 0;
            using (DBContext db = new DBContext())
            {
                iAffected = (int)db.ExecuteScalar(sqlText);
            }
            return (iAffected > 0);
        }

        public static bool UpdateEntity<T>(T entity) where T : IDBInfo
        {
            string sqlText = GetSQLUpdateText(entity) + "; SELECT @@ROWCOUNT";
            int iAffected = 0;
            using (DBContext db = new DBContext())
            {
                iAffected = (int)db.ExecuteScalar(sqlText);
            }
            return (iAffected > 0);
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
                //преобразование числа с точкой
                return ((float)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else if (value is double)
                return ((double)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else if (value is decimal)
                return ((decimal)value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            else
                return value.ToString();
        }


        public static void PopulateEntityById<T>(T entity, int id) where T : IDBInfo
        {
            string sWhere = $"[Id] = '{id}'";

            PopulateEntityByWhere(entity, sWhere);
        }

        public static void PopulateEntityByWhere<T>(T entity, string sWhere) where T : IDBInfo
        {
            string sqlText = $"SELECT * FROM [{entity.DBTableName}] WHERE ({sWhere})";

            DataTable dt = null; 
            using (DBContext db = new DBContext())
            {
                dt = db.GetQueryTable(sqlText);
            }

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

        private static T getEntityFromDataRow<T>(DataRow row) where T : new()
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

        public static string GetSQLInsertText<T>(T instance) where T : IDBInfo
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
                        sbValues.Append(toSQLString(value));
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

        public static string GetSQLUpdateText<T>(T instance) where T : IDBInfo
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
                        sVal = toSQLString(value);
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


        //**********************************

        #region  Department
        // получить отделы из SQL-таблицы
        public static DataTable GetDepartments()
        {
            DataTable retVal = null;
            using (DBContext db = new DBContext())
            {
                retVal = db.GetQueryTable("SELECT * FROM Department ORDER BY Name");
            }
            return retVal;
        }
        public static string GetDepartmentName(int Id)
        {
            string retVal = null;
            string sqlText = string.Format("SELECT Name FROM Department WHERE (Id = {0})", Id);
            using (DBContext db = new DBContext())
            {
                retVal = (string)db.ExecuteScalar(sqlText);
            }
            return retVal;
        }
        public static int GetDepartmenIdByName(string depName)
        {
            int retVal = -1;
            string sqlText = string.Format("SELECT Id FROM Department WHERE (Name = '{0}')", depName);
            using (DBContext db = new DBContext())
            {
                retVal = (int)db.ExecuteScalar(sqlText);
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
            DataTable retVal = null;
            string sqlText = "SELECT * FROM vwUsers";

            using (DBContext db = new DBContext())
            {
                retVal = db.GetQueryTable(sqlText);
            }

            return retVal;
        }

        #endregion

        #region Schedule
        public static DataTable GetSchInclude()
        {
            DataTable retVal = null;
            using (DBContext db = new DBContext())
            {
                retVal = db.GetQueryTable("SELECT * From vwSchInclude");
            }
            return retVal;
        }

        public static DataTable GetSchedule()
        {
            DataTable retVal = null;
            using (DBContext db = new DBContext())
            {
                retVal = db.GetQueryTable("SELECT * From vwSchedules");
            }
            return retVal;
        }
        public static bool InsertSchedule(ScheduleModel sched, out int newId)
        {
            string sqlText = $"INSERT INTO Schedules ([IdDepartment], [Data], [Approved]) VALUES ({sched.DepartmentId}, '{sched.Date}', {((sched.Approved) ? 1 : 0)}); SELECT @@IDENTITY";

            using (DBContext db = new DBContext())
            {
                newId = (int)db.ExecuteScalar(sqlText);
            }

            return (newId > 0);
        }

        public static bool UpdateSchedule(ScheduleModel sched)
        {
            string sqlText = $"UPDATE Schedules SET [IdDepartment] = {sched.DepartmentId}, [Data] = '{sched.Date}', [Approved] = {((sched.Approved) ? 1 : 0)} WHERE (Id = {sched.Id})";

            int iAffected = ExecuteDMLAndGetAffectedRowCount(sqlText);
            return (iAffected > 0);
        }

        public static bool DeleteSchedule(int Id)
        {
            string sqlText = string.Format("DELETE FROM Schedules WHERE (Id = {0})", Id);

            int iAffected = ExecuteDMLAndGetAffectedRowCount(sqlText);
            return (iAffected > 0);
        }
        #endregion

        #region Notes
        // получить сл.зап. для отображения в датагриде
        // сл.зап. отсортированы по убыванию Id, чтобы новые записи отображались вверху таблицы
        // (сортировать здесь, т.к. SQL-представление не хочет сохранять запрос с ORDER BY (!!!!????)
        public static DataTable GetNotes()
        {
            string sqlText = "SELECT * FROM Notes"; // ORDER BY Id DESC"); 

            DataTable retVal = null;
            using (DBContext db = new DBContext())
            {
                retVal = db.GetQueryTable(sqlText);
            }
            return retVal;
        }

        public static List<Note> GetNotesModelList()
        {
            DataTable dt = GetNotes();
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

            DataRow retVal = null;
            using (DBContext db = new DBContext())
            {
                DataTable dt = db.GetQueryTable(sqlText);
                retVal = ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];

            }
            return retVal;
        }

        public static DataTable GetNoteIncludeByNoteId(int noteId)
        {
            string sqlText = string.Format("SELECT * FROM NoteIncludeTable WHERE (IdNotes = {0})", noteId);
            DataTable dt = null; 
            using (DBContext db = new DBContext())
            {
                dt = db.GetQueryTable(sqlText);
            }
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
                    using (DBContext db = new DBContext())
                    {
                        foreach (NoteInclude incl in note.Include)
                        {
                            incl.IdNotes = note.Id;
                            sqlText = incl.GetSQLInsertText(note.IncludeFields) + "; SELECT @@IDENTITY";
                            dt = db.GetQueryTable(sqlText);
                            incl.Id = Convert.ToInt32(dt.Rows[0][0]);
                        }
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
                            using (DBContext db = new DBContext())
                            {
                                db.ExecuteCommand(sqlText);
                            }
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

                    using (DBContext db = new DBContext())
                    {
                        DataTable dtTmp = db.GetQueryTable(sqlText);
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
                    using (DBContext db = new DBContext())
                    {
                        db.ExecuteCommand(sqlText);
                    }
                }
                catch (Exception ex)
                {
                    showErrorBox("NoteIncludeTable", "обновления", ex.Message + ": " + sqlText);
                }
            }
        }

        public static bool DeleteNotes(int id)
        {
            int iAffected = 0;
            using (DBContext db = new DBContext())
            {
                // удалить строки из NoteIncludeTable
                string sqlText = string.Format("DELETE FROM [NoteIncludeTable] WHERE ([IdNotes] = {0})", id);
                db.ExecuteCommand(sqlText);

                sqlText = $"DELETE FROM [{Note._dbTableName}] WHERE (Id = {id.ToString()}); SELECT @@ROWCOUNT";
                iAffected = (int)db.ExecuteScalar(sqlText);
            }
            return (iAffected > 0);
        }

        #endregion

        #region Phone
        // получить телефонную книгу
        public static DataTable GetPhones()
        {
            DataTable retVal = null;
            using (DBContext db = new DBContext())
            {
                retVal = db.GetQueryTable("SELECT * From vwPhonebook");
            }
            return retVal;
        }

        #endregion

        #region шаблон службової
        public static DataTable GetNoteTemplates()
        {
            DataTable retVal = null;
            using (DBContext db = new DBContext())
            {
                retVal = db.GetQueryTable("SELECT * From NoteTemplates");
            }
            return retVal;
        }

        // получить настройки шаблона
        public static DataRow GetNoteTemplateById(int Id)
        {
            string sqlText = $"SELECT * FROM [NoteTemplates] WHERE ([Id] = '{Id}')";

            DataRow retVal = null;
            using (DBContext db = new DBContext())
            {
                DataTable dt = db.GetQueryTable(sqlText);
                retVal = ((dt == null) || (dt.Rows.Count == 0)) ? null : dt.Rows[0];

            }
            return retVal;
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

        // получить список пар Id, Name из справочника
        private static List<IdNameTuple> getIdNameList(string sqlText)
        {
            DataTable dt = null;
            using (DBContext db = new DBContext())
            {
                dt = db.GetQueryTable(sqlText);
            }
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
            DataTable dt = null;
            using (DBContext db = new DBContext())
            {
                dt = db.GetQueryTable(sqlText);
            }
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