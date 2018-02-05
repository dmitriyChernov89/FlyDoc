using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlyDoc.Model
{
    public class Note : IDBInfo
    {
        public static string _dbTableName = "Notes";
        private static List<DBTableColumn> _dbColumns;
        static Note()
        {
            _dbColumns = DBContext.GetTableColumns(_dbTableName);
        }

        #region public DB fields
        public int Id { get; set; }
        public int Templates { get; set; }
        public int IdDepartment { get; set; }
        public DateTime Date { get; set; }
        public string NameAvtor { get; set; }
        public string NameDir { get; set; }
        public string NameComdir { get; set; }
        public string NameSBNach { get; set; }
        public string NameSB { get; set; }
        public string NameKasa { get; set; }
        public string NameNach { get; set; }
        public string NameFin { get; set; }
        public string NameDostavka { get; set; }
        public string NameEnerg { get; set; }
        public string NameSklad { get; set; }
        public string NameBuh { get; set; }
        public string NameASU { get; set; }
        public bool ApprAvtor { get; set; }
        public bool ApprDir { get; set; }
        public bool ApprComdir { get; set; }
        public bool ApprSBNach { get; set; }
        public bool ApprSB { get; set; }
        public bool ApprKasa { get; set; }
        public bool ApprNach { get; set; }
        public bool ApprFin { get; set; }
        public bool ApprDostavka { get; set; }
        public bool ApprEnerg { get; set; }
        public bool ApprSklad { get; set; }
        public bool ApprBuh { get; set; }
        public bool ApprASU { get; set; }
        public bool ApprAll { get; set; }
        public string BodyUp { get; set; }
        public string BodyDown { get; set; }
        public string HeadNach { get; set; }
        public string HeadDir { get; set; }
        public int NoteType { get; set; }
        public string Approvers { get; set; }
        #endregion

        #region add props
        public List<NoteInclude> Include { get; set; }
        private List<string> _inclFields;
        public List<string> IncludeFields { get { return _inclFields; } }

        private NoteTemplate _template;
        public NoteTemplate Template { get { return _template; } }

        private Department _dep;
        public Department Department { get { return _dep; } }

        #endregion

        #region IDBInfo
        public string DBTableName { get { return _dbTableName; } }
        public List<DBTableColumn> DBColumns { get { return _dbColumns; } }
        #endregion

        // CTORs
        public Note()
        {
            _inclFields = new List<string>();
        }

        // объект, заполненный данными из БД
        public Note(int noteId) : this()
        {
            DBContext.PopulateEntityById(this, noteId);
            if (this.Id == 0) return;

            // внутренняя таблица NoteInclude
            DataTable dt = DBContext.GetNoteIncludeByNoteId(noteId);
            if (dt != null)
            {
                this.Include = new List<NoteInclude>();
                NoteInclude noteIncl;
                // коллекция имен полей таблицы
                List<string> colNames = DBContext.GetColumnsNameList(dt);
                // коллекция свойств объекта типа NoteInclude
                List<PropertyInfo> pInfo = typeof(NoteInclude).GetProperties().ToList();
                // для каждой строки таблицы создаем объект NoteInclude, заполняем его через рефлексию
                foreach (DataRow row in dt.Rows)
                {
                    noteIncl = new NoteInclude();
                    foreach (PropertyInfo item in pInfo)
                    {
                        if (colNames.Contains(item.Name))
                        {
                            item.SetValue(noteIncl, (row.IsNull(item.Name) ? null : row[item.Name]), null);
                        }
                    }
                    // и добавляем в коллекцию Include
                    this.Include.Add(noteIncl);
                }
            }

            // шаблон
            _template = new NoteTemplate(Templates);

            // отдел
            _dep = new Department(IdDepartment);
        }


        #region include table
        public void ResetIncludeFields(DataRow drTemplate = null)
        {
            if (drTemplate == null) drTemplate = DBContext.GetNoteTemplateById(this.Templates);

            if (drTemplate["TableColums"].ToInt() == 0)
            {
                if (_inclFields.Count > 0) _inclFields.Clear();
            }
            else
            {
                _inclFields.Clear();
                if (drTemplate != null)
                    foreach (DataColumn col in drTemplate.Table.Columns)
                    {
                        if (col.ColumnName.StartsWith("ColumName") && (!drTemplate.IsNull(col)))
                            _inclFields.Add(NoteInclude.GetNameByHeader((string)drTemplate[col]));
                    }
            }
        }

        public void ResetIncludeFields(NoteTemplate template)
        {
            if (template == null) { _inclFields.Clear(); return; }

            _inclFields.Clear();
            if (template.TableColums >= 0)
            {
                PropertyInfo[] pInfos = template.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo pInfo in pInfos)
                {
                    if (pInfo.Name.StartsWith("ColumName"))
                    {
                        string colName = Convert.ToString(pInfo.GetValue(template));
                        if (string.IsNullOrEmpty(colName) == false)
                        {
                            string sRet = NoteInclude.GetNameByHeader(colName);
                            if (string.IsNullOrEmpty(sRet) == false) _inclFields.Add(sRet);
                        }
                    }
                }
            }
        }
        #endregion

    }  // class Note
}
