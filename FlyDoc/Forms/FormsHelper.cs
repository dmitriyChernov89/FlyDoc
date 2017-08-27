using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Forms
{
    public static class FormsHelper
    {
        // для текстовых полей устанавливаются фон поля с фокусом и без фокуса
        public static void SetFocusEventHandlers(Form sourceForm, Color focusBackColor, Color notFocusBackColor)
        {
            TextBox tb;
            foreach (Control item in sourceForm.Controls)
            {
                if (item is TextBox)
                {
                    tb = (item as TextBox);
                    tb.Enter += (object sender, EventArgs e) => (sender as TextBox).BackColor = focusBackColor;
                    tb.Leave += (object sender, EventArgs e) => (sender as Control).BackColor = notFocusBackColor;
                }
            }
        }

        public static void SetDepartmentsComboBox(ComboBox cbDepartment, bool isAddEmptyRow = false)
        {
            // получить данные и настроить комбобокс отделов
            DataTable dtDeps = DBContext.GetDepartments();
            if (dtDeps != null)
            {
                if (isAddEmptyRow)
                {
                    DataRow emptyRow = dtDeps.NewRow();
                    emptyRow.ItemArray = new object[] { -1, "" };
                    dtDeps.Rows.InsertAt(emptyRow, 0);
                }

                cbDepartment.DataSource = dtDeps;
                cbDepartment.DisplayMember = "Name";
                cbDepartment.ValueMember = "Id";
            }
        }

        public static void SetNoteTemplatesComboBox(ComboBox cbNoteTemplate)
        {
            DataTable dtNoteTemplates = DBContext.GetNoteTemplates();
            if (dtNoteTemplates != null)
            {
                cbNoteTemplate.DataSource = dtNoteTemplates;
                cbNoteTemplate.DisplayMember = "Name";
                cbNoteTemplate.ValueMember = "Id";
            }

        }

    }  // class
}
