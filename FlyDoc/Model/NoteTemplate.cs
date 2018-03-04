using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;


namespace FlyDoc.Model
{
    public class NoteTemplate: IDBInfo
    {
        public static string _dbTableName = "NoteTemplates";
        private static List<DBTableColumn> _dbColumns;
        static NoteTemplate()
        {
            using (DBContext db = new DBContext())
            {
                _dbColumns = db.GetTableColumns(_dbTableName);
            }
        }

        #region public fields
        public int Id { get; set; }
        public string Name { get; set; }
        public string HeadDir { get; set; }
        public string HeadNach { get; set; }
        public string BodyUp { get; set; }
        public int TableColums { get; set; }
        public string ColumName1 { get; set; }
        public string ColumName2 { get; set; }
        public string ColumName3 { get; set; }
        public string ColumName4 { get; set; }
        public string ColumName5 { get; set; }
        public string ColumName6 { get; set; }
        public string ColumName7 { get; set; }
        public string ColumName8 { get; set; }
        public string ColumName9 { get; set; }
        public string ColumName10 { get; set; }
        public string BodyDown { get; set; }
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
        public string Help { get; set; }
        #endregion

        #region IDBInfo
        public string DBTableName { get { return _dbTableName; } }
        public List<DBTableColumn> DBColumns { get { return _dbColumns; } }
        #endregion

        public NoteTemplate()
        {
        }

        public NoteTemplate(int id) : this()
        {
            DBContext.PopulateEntityById(this, id);
        }

    }  // class
}
