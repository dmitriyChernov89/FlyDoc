using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlyDoc.Model
{
    public class Note
    {
        public int Id { get; set; }
        public int NoteTemplateId { get; set; }
        public int DepartmentId { get; set; }
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

        public Note()
        {

        }

        public Note(int dbId)
        {
            DataRow dr = DBContext.GetNoteInclude(dbId);
            if (dr != null)
            {
                Id = dbId;
                DepartmentId = Convert.ToInt32(dr["IdDepartment"]);
                NoteTemplateId = Convert.ToInt32(dr["Templates"]);
                Date = (DateTime)(dr["Date"] ?? DateTime.MinValue);
                NameAvtor = dr["NameAvtor"].ToString();
                NameDir = dr["NameDir"].ToString();
                NameComdir = dr["NameComdir"].ToString();
                NameSBNach = dr["NameSBNach"].ToString();
                NameSB = dr["NameSB"].ToString();
                NameKasa = dr["NameKasa"].ToString();
                NameNach = dr["NameNach"].ToString();
                NameFin = dr["NameFin"].ToString();
                NameDostavka = dr["NameDostavka"].ToString();
                NameEnerg = dr["NameEnerg"].ToString();
                NameSklad = dr["NameSklad"].ToString();
                NameBuh = dr["NameBuh"].ToString();
                NameASU = dr["NameASU"].ToString();
                ApprAvtor = dr["ApprAvtor"].ToBool();
                ApprDir = dr["ApprDir"].ToBool();
                ApprComdir = dr["ApprComdir"].ToBool();
                ApprSBNach = dr["ApprSBNach"].ToBool();
                ApprSB = dr["ApprSB"].ToBool();
                ApprKasa = dr["ApprKasa"].ToBool();
                ApprNach = dr["ApprNach"].ToBool();
                ApprFin = dr["ApprFin"].ToBool();
                ApprDostavka = dr["ApprDostavka"].ToBool();
                ApprEnerg = dr["ApprEnerg"].ToBool();
                ApprSklad = dr["ApprSklad"].ToBool();
                ApprBuh = dr["ApprBuh"].ToBool();
                ApprASU = dr["ApprASU"].ToBool();
                ApprAll = dr["ApprAll"].ToBool();
                BodyUp = dr["BodyUp"].ToString();
                BodyDown = dr["BodyDown"].ToString();
                HeadNach = dr["HeadNach"].ToString();
                HeadDir = dr["HeadDir"].ToString();
            }
        }

        public string GetSQLUpdateString()
        {
            string retVal = "", sVal, sName;
            PropertyInfo[] fields = (typeof(Note)).GetProperties();
            foreach (PropertyInfo item in fields)
            {
                if (item.Name == "Id") continue;
                if (item.Name.StartsWith("Name") && (item.Name != "NameAvtor")) continue;

                if (retVal.Length > 0) retVal += ", ";

                sName = item.Name;
                if (sName == "DepartmentId") sName = "IdDepartment";
                if (sName == "NoteTemplateId") sName = "Templates";

                sVal = item.GetValue(this, null).ToString();
                if (item.PropertyType.Name == "DateTime")
                    sVal = string.Format("CONVERT(datetime, '{0}', 20)", Convert.ToDateTime(item.GetValue(this, null)).ToString("yyyy-MM-dd HH:mm:ss"));
                else if (item.PropertyType.Name == "String")
                    sVal = "'" + sVal + "'";
                else if (item.PropertyType.Name == "Boolean")
                    sVal = (sVal.ToBool() ? "1" : "0");

                retVal += "[" + sName + "] = " + sVal;
            }

            return retVal;
        }

    }  // class
}
