using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class NoteInclude
    {
        public int Id { get; set; }
        public int IdNotes { get; set; }
        public int Order { get; set; }
        public DateTime Date { get; set; }
        public string NumberDoc { get; set; }
        public int Artikul { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string NumberDoc2 { get; set; }
        public DateTime DateDoc { get; set; }
        public string Code { get; set; }
        public decimal Sum { get; set; }
        public string Label { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }

        private static List<ColumnNameHeader> _nameHeaders;
        static NoteInclude()
        {
            _nameHeaders = new List<ColumnNameHeader>()
            {
                new ColumnNameHeader("Order", "№ п/п"),
                new ColumnNameHeader("Date", "Date"),
                new ColumnNameHeader("NumberDoc", "№ документа"),
                new ColumnNameHeader("Artikul", "Артикул"),
                new ColumnNameHeader("Amount", "Кількість"),
                new ColumnNameHeader("Description", "Назва товару"),
                new ColumnNameHeader("NumberDoc2", "NumberDoc2"),
                new ColumnNameHeader("DateDoc", "DateDoc"),
                new ColumnNameHeader("Code", "Code"),
                new ColumnNameHeader("Sum", "Сума, в грн"),
                new ColumnNameHeader("Label", "Label"),
                new ColumnNameHeader("Price", "Ціна, грн."),
                new ColumnNameHeader("Unit", "Од. вим.")
            };
        }
        // получить имя поля таблицы NoteIncludeTable (столбца доп.таблицы) по заголовку поля (столбца доп.таблицы)
        internal static string GetNameByHeader(string header)
        {
            string retVal = null;
            ColumnNameHeader cn = NoteInclude._nameHeaders.FirstOrDefault(c => c.Header == header);
            if (cn != null) retVal = cn.Name;

            return retVal;
        }
        // получить заголовок Header столбца доп.таблицы по имени столбца доп.таблицы
        internal static string GetHeaderByName(string name)
        {
            string retVal = null;
            ColumnNameHeader cn = NoteInclude._nameHeaders.FirstOrDefault(c => c.Name == name);
            if (cn != null) retVal = cn.Header;

            return retVal;
        }

        // inner class Имя + Заголовок
        private class ColumnNameHeader
        {
            public string Name { get; set; }
            public string Header { get; set; }

            public ColumnNameHeader()
            {
            }

            public ColumnNameHeader(string name, string header)
            {
                this.Name = name; this.Header = header;
            }
        }

        internal string GetSQLInsertText()
        {
            List<string> flds = new List<string>();
            List<string> vals = new List<string>();

            flds.Add("IdNotes"); vals.Add(this.IdNotes.ToString());

            if (this.Order != 0) { flds.Add("[Order]"); vals.Add(this.Order.ToString()); }

            if (this.Date != DateTime.MinValue) { flds.Add("[Date]"); vals.Add(this.Date.ToSQLExpr()); }

            if (this.NumberDoc.IsNull() == false) { flds.Add("NumberDoc"); vals.Add("'" + this.NumberDoc + "'"); }

            if (this.Artikul != 0) { flds.Add("Artikul"); vals.Add(this.Artikul.ToString()); }

            if (this.Amount != 0) { flds.Add("Amount"); vals.Add(this.Amount.ToString(CultureInfo.InvariantCulture)); }

            if (this.Description.IsNull() == false) { flds.Add("Description"); vals.Add("'" + this.Description + "'"); }

            if (this.NumberDoc2.IsNull() == false) { flds.Add("NumberDoc2"); vals.Add("'" + this.NumberDoc2 + "'"); }

            if (this.DateDoc != DateTime.MinValue) { flds.Add("DateDoc"); vals.Add(this.DateDoc.ToSQLExpr()); }

            if (this.Code.IsNull() == false) { flds.Add("Code"); vals.Add("'" + this.Code + "'"); }

            if (this.Sum != 0) { flds.Add("[Sum]"); vals.Add(this.Sum.ToString(CultureInfo.InvariantCulture)); }

            if (this.Label.IsNull() == false) { flds.Add("Label"); vals.Add("'" + this.Label + "'"); }

            if (this.Price != 0) { flds.Add("Price"); vals.Add(this.Price.ToString(CultureInfo.InvariantCulture)); }

            if (this.Unit.IsNull() == false) { flds.Add("Unit"); vals.Add("'" + this.Unit + "'"); }

            string sFlds = string.Join(", ", flds.ToArray());
            string sVals = string.Join(", ", vals.ToArray());

            string retVal = string.Format("INSERT INTO NoteIncludeTable ({0}) VALUES ({1})", sFlds, sVals);
            return retVal;
        }

        internal string GetSQLUpdateText()
        {
            List<string> sets = new List<string>();

            if (this.Order != 0) sets.Add("[Order] = " + this.Order.ToString());

            if (this.Date != DateTime.MinValue) sets.Add("[Date] = " + this.Date.ToSQLExpr());

            if (this.NumberDoc.IsNull() == false) sets.Add("NumberDoc = '" + this.NumberDoc + "'");

            if (this.Artikul != 0) sets.Add("Artikul = " + this.Artikul.ToString());

            if (this.Amount != 0) sets.Add("Amount = " + this.Amount.ToString(CultureInfo.InvariantCulture));

            if (this.Description.IsNull() == false) sets.Add("Description = '" + this.Description + "'");

            if (this.NumberDoc2.IsNull() == false) sets.Add("NumberDoc2 = '" + this.NumberDoc2 + "'");

            if (this.DateDoc != DateTime.MinValue) sets.Add("DateDoc = " + this.DateDoc.ToSQLExpr());

            if (this.Code.IsNull() == false) sets.Add("Code = '" + this.Code + "'");

            if (this.Sum != 0) sets.Add("[Sum] = " + this.Sum.ToString(CultureInfo.InvariantCulture));

            if (this.Label.IsNull() == false) sets.Add("Label = '" + this.Label + "'");

            if (this.Price != 0) sets.Add("Price = " + this.Price.ToString(CultureInfo.InvariantCulture));

            if (this.Unit.IsNull() == false) sets.Add("Unit = '" + this.Unit + "'");

            string sSets = string.Join(", ", sets.ToArray());
            string retVal = string.Format("UPDATE NoteIncludeTable SET {0} WHERE ({1})", sSets, this.Id);

            return retVal;
        }


    }  // class

}
