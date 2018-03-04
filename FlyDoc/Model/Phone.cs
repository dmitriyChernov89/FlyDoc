using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class PhoneModel: IDBInfo
    {
        public static string _dbTableName = "Phonebook";
        private static List<DBTableColumn> _dbColumns;
        static PhoneModel()
        {
            using (DBContext db = new DBContext())
            {
                _dbColumns = db.GetTableColumns(_dbTableName);
            }
        }

        #region public fields
        public int Id { get; set; }
        public int Department { get; set; }
        public string DepName { get; set; }
        public string Positions { get; set; }
        public string FIO { get; set; }
        public string Dect { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Mail { get; set; }
        #endregion

        #region IDBInfo
        public string DBTableName { get { return _dbTableName; } }
        public List<DBTableColumn> DBColumns { get { return _dbColumns; } }
        #endregion

        // CTORs
        public PhoneModel()
        {
        }
        public PhoneModel(int id): this()
        {
            DBContext.PopulateEntityById(this, id);
        }

    }  // class
}
