using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class Department : IDBInfo
    {
        public static string _dbTableName = "Department";
        private static List<DBTableColumn> _dbColumns;
        static Department()
        {
            _dbColumns = DBContext.GetTableColumns(_dbTableName);
        }

        #region public fields
        public int Id { get; set; }
        public string Name { get; set; }
        #endregion

        #region IDBInfo
        public string DBTableName { get { return _dbTableName; } }
        public List<DBTableColumn> DBColumns { get { return _dbColumns; } }
        #endregion

        // CTORs
        public Department()
        {
        }
        public Department(int id): this()
        {
            DBContext.PopulateEntityById(this, id);
        }

    }
}
