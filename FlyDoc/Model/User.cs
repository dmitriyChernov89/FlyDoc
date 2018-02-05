using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class User : IDBInfo
    {
        public static string _dbTableName = "Access";
        private static List<DBTableColumn> _dbColumns;
        static User()
        {
            _dbColumns = DBContext.GetTableColumns(_dbTableName);
        }

        #region public fields
        public int Id { get; set; }
        public string PC { get; set; }
        public string UserName { get; set; }
        public int Department { get; set; }
        public bool Notes { get; set; }
        public bool Schedule { get; set; }
        public bool Phone { get; set; }
        public bool Config { get; set; }
        public string HeadNach { get; set; }
        public string Name { get; set; }

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
        public string Mail { get; set; }
        #endregion

        #region IDBInfo
        public string DBTableName { get { return _dbTableName; } }
        public List<DBTableColumn> DBColumns { get { return _dbColumns; } }
        #endregion


        // CTORs
        public User()
        {
        }
        public User(int id) : this()
        {
            DBContext.PopulateEntityById(this, id);
        }
        public User(string machineName, string userName) : this()
        {
            string sWhere = $"([PC] = '{machineName}') AND ([UserName] = '{userName}')";

            DBContext.PopulateEntityByWhere(this, sWhere);
        }

    }  // class
}
