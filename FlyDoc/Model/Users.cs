using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public static class Users
    {
        public static DataTable GetUsers()
        {
            return DBContext.GetUsers();
        }
    }
}
