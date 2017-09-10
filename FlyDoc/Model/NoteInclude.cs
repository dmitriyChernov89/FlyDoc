using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FlyDoc.Model
{
    public class NoteInclude
    {
        public int Id { get; set; }
        public int IdNotes { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string NumberDoc { get; set; }
        public Nullable<int> Artikul { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
        public string NumberDoc2 { get; set; }
        public Nullable<DateTime> DateDoc { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> Sum { get; set; }
        public string Label { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Unit { get; set; }

        private static List<ColumnNameHeader> _headers;
        static NoteInclude()
        {
            _headers = new List<ColumnNameHeader>()
            {
                new ColumnNameHeader("Order", "№ п/п", true, null),
                new ColumnNameHeader("Date", "Date", true, (o)=>Convert.ToDateTime(o).ToSQLExpr()),
                new ColumnNameHeader("NumberDoc", "№ документа", true, (o)=>"'" + o.ToString() + "'"),
                new ColumnNameHeader("Artikul", "Артикул", true, null),
                new ColumnNameHeader("Amount", "Кількість", true, (o)=>Convert.ToDecimal(o).ToString(CultureInfo.InvariantCulture)),
                new ColumnNameHeader("Description", "Назва товару", true, (o)=>"'" + o.ToString() + "'"),
                new ColumnNameHeader("NumberDoc2", "NumberDoc2", true, (o)=>"'" + o.ToString() + "'"),
                new ColumnNameHeader("DateDoc", "DateDoc", true, (o)=>Convert.ToDateTime(o).ToSQLExpr()),
                new ColumnNameHeader("Code", "Code", true, (o)=>"'" + o.ToString() + "'"),
                new ColumnNameHeader("Sum", "Сума", true, (o)=>Convert.ToDecimal(o).ToString(CultureInfo.InvariantCulture)),
                new ColumnNameHeader("Label", "Label", true, (o)=>"'" + o.ToString() + "'"),
                new ColumnNameHeader("Price", "Ціна", true, (o)=>Convert.ToDecimal(o).ToString(CultureInfo.InvariantCulture)),
                new ColumnNameHeader("Unit", "Од. вим.", true, (o)=>"'" + o.ToString() + "'")
            };
        }
        // получить имя поля таблицы NoteIncludeTable (столбца доп.таблицы) по заголовку поля (столбца доп.таблицы)
        internal static string GetNameByHeader(string header)
        {
            string retVal = null;
            ColumnNameHeader cn = NoteInclude._headers.FirstOrDefault(c => (c.Header == header) || (header.StartsWith(c.Header)));
            if (cn != null) retVal = cn.Name;

            return retVal;
        }
        // получить заголовок Header столбца доп.таблицы по имени столбца доп.таблицы
        internal static string GetHeaderByName(string name)
        {
            string retVal = null;
            ColumnNameHeader cn = NoteInclude._headers.FirstOrDefault(c => c.Name == name);
            if (cn != null) retVal = cn.Header;

            return retVal;
        }

        // inner class Имя + Заголовок
        private class ColumnNameHeader
        {
            public string Name { get; set; }
            public string Header { get; set; }
            public bool IsDBNull { get; set; }
            private Func<object, string> _toSQLStringFunc { get; set; }

            public ColumnNameHeader()
            {
            }

            public ColumnNameHeader(string name, string header, bool isDBNull, Func<object, string> toSQLStrFunc)
            {
                this.Name = name; this.Header = header;
                this.IsDBNull = isDBNull;
                this._toSQLStringFunc = toSQLStrFunc;
            }

            public string GetSQLStringValue(object objValue)
            {
                if (objValue == null)
                    return "Null";
                else
                {
                    if (_toSQLStringFunc == null)
                        return objValue.ToString();
                    else
                        return _toSQLStringFunc(objValue);
                }
            }

        }


        internal string GetSQLInsertText(List<string> tplFields)
        {
            List<string> flds = new List<string>();
            List<string> vals = new List<string>();

            flds.Add("IdNotes"); vals.Add(this.IdNotes.ToString());

            ColumnNameHeader colDescr;
            // коллекция свойств объекта типа NoteInclude
            List<PropertyInfo> pInfos = typeof(NoteInclude).GetProperties().ToList();
            PropertyInfo pInfo;
            foreach (string fldName in tplFields)
            {
                flds.Add("[" + fldName + "]");
                colDescr = _headers.FirstOrDefault(c => c.Name == fldName);
                if (colDescr != null)
                {
                    pInfo = pInfos.FirstOrDefault(p => p.Name == fldName);
                    string sqlStrVal = colDescr.GetSQLStringValue(pInfo.GetValue(this, null));
                    vals.Add(sqlStrVal);
                }
            }

            string sFlds = string.Join(", ", flds.ToArray());
            string sVals = string.Join(", ", vals.ToArray());

            string retVal = string.Format("INSERT INTO NoteIncludeTable ({0}) VALUES ({1})", sFlds, sVals);
            return retVal;
        }

        internal string GetSQLUpdateText(List<string> tplFields)
        {
            List<string> sets = new List<string>();

            ColumnNameHeader colDescr;
            // коллекция свойств объекта типа NoteInclude
            List<PropertyInfo> pInfos = typeof(NoteInclude).GetProperties().ToList();
            PropertyInfo pInfo;
            foreach (string fldName in tplFields)
            {
                colDescr = _headers.FirstOrDefault(c => c.Name == fldName);
                if (colDescr != null)
                {
                    pInfo = pInfos.FirstOrDefault(p => p.Name == fldName);
                    object obj = pInfo.GetValue(this, null);
                    string sqlStrVal = colDescr.GetSQLStringValue(obj);

                    sets.Add("[" + fldName + "] = " + sqlStrVal);
                }
            }

            string sSets = string.Join(", ", sets.ToArray());
            string retVal = string.Format("UPDATE NoteIncludeTable SET {0} WHERE (Id = {1})", sSets, this.Id);

            return retVal;
        }


    }  // class

}
