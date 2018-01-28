using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyDoc.Model
{
    public class NoteTemplate
    {
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

        public NoteTemplate()
        {
        }

        public NoteTemplate(int noteTplId)
        {
            DataRow dr = DBContext.GetNoteTemplateById(noteTplId);
            if (dr != null)
            {
                Id = noteTplId;
                if (!dr.IsNull("Name")) Name = dr["Name"].ToString();
                if (!dr.IsNull("HeadDir")) HeadDir = dr["HeadDir"].ToString();
                if (!dr.IsNull("HeadNach")) HeadNach = dr["HeadNach"].ToString();
                if (!dr.IsNull("BodyUp")) BodyUp = dr["BodyUp"].ToString();
                if (!dr.IsNull("TableColums")) TableColums = Convert.ToInt32(dr["TableColums"]);
                if (!dr.IsNull("ColumName1")) ColumName1 = dr["ColumName1"].ToString();
                if (!dr.IsNull("ColumName2")) ColumName2 = dr["ColumName2"].ToString();
                if (!dr.IsNull("ColumName3")) ColumName3 = dr["ColumName3"].ToString();
                if (!dr.IsNull("ColumName4")) ColumName4 = dr["ColumName4"].ToString();
                if (!dr.IsNull("ColumName5")) ColumName5 = dr["ColumName5"].ToString();
                if (!dr.IsNull("ColumName6")) ColumName6 = dr["ColumName6"].ToString();
                if (!dr.IsNull("ColumName7")) ColumName7 = dr["ColumName7"].ToString();
                if (!dr.IsNull("ColumName8")) ColumName8 = dr["ColumName8"].ToString();
                if (!dr.IsNull("ColumName9")) ColumName9 = dr["ColumName9"].ToString();
                if (!dr.IsNull("ColumName10")) ColumName10 = dr["ColumName10"].ToString();
                if (!dr.IsNull("BodyDown")) BodyDown = dr["BodyDown"].ToString();

                if (!dr.IsNull("ApprASU")) ApprASU = Convert.ToBoolean(dr["ApprASU"]);
                if (!dr.IsNull("ApprBuh")) ApprBuh = Convert.ToBoolean(dr["ApprBuh"]);
                if (!dr.IsNull("ApprComdir")) ApprComdir = Convert.ToBoolean(dr["ApprComdir"]);
                if (!dr.IsNull("ApprDir")) ApprDir = Convert.ToBoolean(dr["ApprDir"]);
                if (!dr.IsNull("ApprDostavka")) ApprDostavka = Convert.ToBoolean(dr["ApprDostavka"]);
                if (!dr.IsNull("ApprEnerg")) ApprEnerg = Convert.ToBoolean(dr["ApprEnerg"]);
                if (!dr.IsNull("ApprFin")) ApprFin = Convert.ToBoolean(dr["ApprFin"]);
                if (!dr.IsNull("ApprKasa")) ApprKasa = Convert.ToBoolean(dr["ApprKasa"]);
                if (!dr.IsNull("ApprNach")) ApprNach = Convert.ToBoolean(dr["ApprNach"]);
                if (!dr.IsNull("ApprSB")) ApprSB = Convert.ToBoolean(dr["ApprSB"]);
                if (!dr.IsNull("ApprSBNach")) ApprSBNach = Convert.ToBoolean(dr["ApprSBNach"]);
                if (!dr.IsNull("ApprSklad")) ApprSklad = Convert.ToBoolean(dr["ApprSklad"]);

                if (!dr.IsNull("Help")) Help = dr["Help"].ToString();
            }
        }

        internal string GetSQLInsertString()
        {
            string retVal = $"INSERT INTO [NoteTemplates] ([Name], [HeadDir], [HeadNach], [BodyUp], [TableColums], [ColumName1], [ColumName2], [ColumName3], [ColumName4], [ColumName5], [ColumName6], [ColumName7], [ColumName8], [ColumName9], [ColumName10], [BodyDown], [ApprDir], [ApprComdir], [ApprSBNach], [ApprSB], [ApprKasa], [ApprNach], [ApprFin], [ApprDostavka], [ApprEnerg], [ApprSklad], [ApprBuh], [ApprASU], [Help]) VALUES ('{this.Name}', '{this.HeadDir}', '{this.HeadNach}', '{this.BodyUp}', {this.TableColums.ToString()}, '{this.ColumName1}', '{this.ColumName2}', '{this.ColumName3}', '{this.ColumName4}', '{this.ColumName5}', '{this.ColumName6}', '{this.ColumName7}', '{this.ColumName8}', '{this.ColumName9}', '{this.ColumName10}', '{this.BodyDown}', '{this.ApprDir.ToString()}', '{this.ApprComdir.ToString()}', '{this.ApprSBNach.ToString()}', '{this.ApprSB.ToString()}', '{this.ApprKasa.ToString()}', '{this.ApprNach.ToString()}', '{this.ApprFin.ToString()}', '{this.ApprDostavka.ToString()}', '{this.ApprEnerg.ToString()}', '{this.ApprSklad.ToString()}', '{this.ApprBuh.ToString()}', '{this.ApprASU.ToString()}', '{this.Help}')";
            return retVal;
        }

        internal string GetSQLUpdateString()
        {
            string retVal = $"[Name] = '{this.Name}', [HeadDir] = '{this.HeadDir}', " +
                "[HeadNach] = '{this.HeadNach}', " +
                "[BodyUp] = '{this.BodyUp}', " + 
                "[TableColums] = {this.TableColums.ToString()}, " + 
                "[ColumName1] = '{this.ColumName1}', [ColumName2] = '{this.ColumName2}', [ColumName3] = '{this.ColumName3}', [ColumName4] = '{this.ColumName4}', [ColumName5] = '{this.ColumName5}', [ColumName6] = '{this.ColumName6}', [ColumName7] = '{this.ColumName7}', [ColumName8] = '{this.ColumName8}', [ColumName9] = '{this.ColumName9}', [ColumName10] = '{this.ColumName10}', " + 
                "[BodyDown] = '{this.BodyDown}', " + 
                "[ApprDir] = '{this.ApprDir.ToString()}', [ApprComdir] = '{this.ApprComdir.ToString()}', [ApprSBNach] = '{this.ApprSBNach.ToString()}', [ApprSB] = '{this.ApprSB.ToString()}', [ApprKasa] = '{this.ApprKasa.ToString()}', [ApprNach] = '{this.ApprNach.ToString()}', [ApprFin] = '{this.ApprFin.ToString()}', [ApprDostavka] = '{this.ApprDostavka.ToString()}', [ApprEnerg] = '{this.ApprEnerg.ToString()}', [ApprSklad] = '{this.ApprSklad.ToString()}', [ApprBuh] = '{this.ApprBuh.ToString()}', [ApprASU] = '{this.ApprASU.ToString()}', " + 
                "[Help] = '{this.Help}')";

            return retVal;
        }

    }  // class
}
