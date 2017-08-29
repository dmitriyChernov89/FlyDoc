using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class NoteInclude
    {
        public int Id { get; set; }
        public int IdNotes { get; set; }
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
                new ColumnNameHeader("IdNotes", "№ п/п"),
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

    }  // class

}
