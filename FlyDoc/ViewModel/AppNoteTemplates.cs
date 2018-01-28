using FlyDoc.Forms;
using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.ViewModel
{
    public class AppNoteTemplates : AppModelBase
    {
        private static Dictionary<string, DGVColDescr> dgvColDescr;
        static AppNoteTemplates()
        {
            // key - db-field name
            dgvColDescr = new Dictionary<string, DGVColDescr>()
            {
                { "Id", new DGVColDescr() { Visible = false } },
                { "Name", new DGVColDescr() { Header="шаблон", FillWeight=30 } },
                { "HeadDir", new DGVColDescr() { Visible = false } },
                { "HeadNach", new DGVColDescr() { Visible = false } },
                { "BodyUp", new DGVColDescr() { Header = "текст службової", FillWeight=30 } },
                { "TableColums", new DGVColDescr() { Header="стовпців таблиці", FillWeight=10 } },
                { "ColumName1", new DGVColDescr() { Visible = false } },
                { "ColumName2", new DGVColDescr() { Visible = false } },
                { "ColumName3", new DGVColDescr() { Visible = false } },
                { "ColumName4", new DGVColDescr() { Visible = false } },
                { "ColumName5", new DGVColDescr() { Visible = false } },
                { "ColumName6", new DGVColDescr() { Visible = false } },
                { "ColumName7", new DGVColDescr() { Visible = false } },
                { "ColumName8", new DGVColDescr() { Visible = false } },
                { "ColumName9", new DGVColDescr() { Visible = false } },
                { "ColumName10", new DGVColDescr() { Visible = false } },
                { "BodyDown", new DGVColDescr() { Visible = false } },
                { "ApprASU", new DGVColDescr() { Header="АСУ", FillWeight=4 } },
                { "ApprBuh", new DGVColDescr() { Header="Бух", FillWeight=4 } },
                { "ApprComdir", new DGVColDescr() { Header="КомДир", FillWeight=4 } },
                { "ApprDir", new DGVColDescr() { Header="Директор", FillWeight=4 } },
                { "ApprDostavka", new DGVColDescr() { Header="Доставка", FillWeight=4 } },
                { "ApprEnerg", new DGVColDescr() { Header="Енерг", FillWeight=4 } },
                { "ApprFin", new DGVColDescr() { Header="Фiн", FillWeight=4 } },
                { "ApprKasa", new DGVColDescr() { Header="Каса", FillWeight=4 } },
                { "ApprNach", new DGVColDescr() { Header="Нач", FillWeight=4 } },
                { "ApprSB", new DGVColDescr() { Header="СБ", FillWeight=4 } },
                { "ApprSBNach", new DGVColDescr() { Header="СБНач", FillWeight=4 } },
                { "ApprSklad", new DGVColDescr() { Header="Склад", FillWeight=4 } },
                { "Help", new DGVColDescr() { Visible = false } }
            };
        }

        public AppNoteTemplates()
        {
        }

        #region override methods
        public override void CopyToNewObject()
        {
            base.CopyToNewObject();
        }

        public override void CreateNewObject()
        {
            NoteTemplateForm frm = new NoteTemplateForm(null);
            DialogResult result = frm.ShowDialog();
            if ((result == DialogResult.OK) && (frm.NoteTemplate != null))
            {
                if (DBContext.InsertNoteTemplate(frm.NoteTemplate))
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(frm.NoteTemplate.Id);
                    MessageBox.Show("Створена нова службова за № " + frm.NoteTemplate.Id.ToString(), "Строверення службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            frm.Dispose();

            base.CreateNewObject();
        }

        public override void EditObject()
        {
            DataGridViewRow dgvRow = base.getSelectedDataRow();
            if (dgvRow != null)
            {
                int editId = (int)dgvRow.Cells["Id"].Value;
                NoteTemplate note = new NoteTemplate(editId);

                NoteTemplateForm frm = new NoteTemplateForm(note);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (DBContext.UpdateNoteTemplate(note))
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                        MessageBox.Show("Шаблон службової оновлен", "Оновлення шаблону службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                base.EditObject();
            }
        }

        public override void DeleteObject()
        {
            int id = getSelectedId();
            if (id != -1)
            {
                DataGridViewRow dgvRow = base.getSelectedDataRow();
                string nameTpl = dgvRow.Cells["Name"].ToString();

                DialogResult result = MessageBox.Show($"Ви впевнені що хочете видалити шаблон службової '{nameTpl}' ?", "Видалення шаблону службової", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    if (DBContext.DeleteNotes(id))
                    {
                        base.DeleteObject();
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть рядок для видалення");
            }
        }


        public override void LoadDataToGrid()
        {
            _dataTable = DBContext.GetNoteTemplates();  // чтение данных о шаблонах сл.зап.
            base.LoadDataToGrid();
        }
        #endregion

    }  // class
}
