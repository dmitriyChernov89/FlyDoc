using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public interface IDBInfo
    {
        int Id { get; set; }
        string DBTableName { get; }
        List<DBTableColumn> DBColumns { get; }
    }
}
