using FlyDoc.Model;
using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlyDoc.Forms;

namespace FlyDoc.ViewModel
{
    public class AppNotes : AppModelBase
    {
        #region fields
        #endregion

        #region properties
        #endregion

        public AppNotes()
        {
        }
        public override void CopyToNewObject()
        {
            base.CopyToNewObject();
        }

        public override void EditObject()
        {
            DataGridViewRow dgvRow = base.getSelectedDataRow();
            if (dgvRow != null)
            {
                int editId = (int)dgvRow.Cells["Id"].Value;
                Note note = new Note(editId);

                NewNote frm = new NewNote(note);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (DBContext.UpdateNotes(note))
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                    }
                }
                base.EditObject();
            }
        }
        public override void CreateNewObject()
        {
            NewNote frm = new NewNote(null);
            DialogResult result = frm.ShowDialog();
            if ((result == DialogResult.OK) && (frm.Note != null))
            {
                int newId = 0;
                if (DBContext.InsertNotes(frm.Note, out newId))
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(newId);
                }
            }
            frm.Dispose();

            base.CreateNewObject();
        }
        public override void DeleteObject()
        {
            int id = getSelectedId();
            if (id != -1)
            {
                DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити поточний відділ?", "Видалення відділу", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
        #region override methods
        public override void LoadDataToGrid()
        {
            _dataTable = DBContext.GetNotes();  // чтение данных о сл.зап.
       
            base.LoadDataToGrid();

            DataGridViewColumn column1 = _dataGrid.Columns[1];
            column1.Name = "Відділ";
            column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewColumn column2 = _dataGrid.Columns[2];
            column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DataGridViewColumn column3 = _dataGrid.Columns[3];
            column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DataGridViewColumn column4 = _dataGrid.Columns[4];
            column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DataGridViewColumn column5 = _dataGrid.Columns[5];
            column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DataGridViewColumn column6 = _dataGrid.Columns[6];
            column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        #endregion

    }  // class
}
