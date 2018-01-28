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
        private static Dictionary<string, DGVColDescr> dgvColDescr;

        static AppNotes()
        {
            dgvColDescr = new Dictionary<string, DGVColDescr>();
            setColDescr("Тип", fillWeight: 800);
            setColDescr("Відділ", fillWeight: 200);
            setColDescr("Дата", fillWeight: 250);
            setColDescr("ApprAvtor", "Автор", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprDir", "Директор", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprComdir", "КомДир", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprSBNach", "СБНач", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprSB", "СБ", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprKasa", "Каса", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprNach", "Нач", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprFin", "Фiн", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprDostavka", "Доставка", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprEnerg", "Енерг", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprSklad", "Склад", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprBuh", "Бух", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprASU", "АСУ", DataGridViewContentAlignment.MiddleCenter, 80, true);
            setColDescr("ApprAll", "Всi", DataGridViewContentAlignment.MiddleCenter, 80, true);
        }
        private static void setColDescr(string name, 
            string header=null, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.NotSet, int fillWeight = 100, bool threeStates=false)
        {
            if (header == null) header = name;
            DGVColDescr col = new DGVColDescr() { Name = name, Header = header, Alignment=alignment, FillWeight=fillWeight, ThreeStates=threeStates};

            dgvColDescr.Add(name, col);
        }

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
                        MessageBox.Show("Службова оновлена", "Оновлення службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Створена нова службова за № " + newId.ToString(), "Строверення службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DialogResult result = MessageBox.Show($"Ви впевнені що хочете видалити службову № {id.ToString()} ?", "Видалення службової", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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

            if (_dataGrid.Columns.Contains("DepartmentId")) _dataGrid.Columns["DepartmentId"].Visible = false;

            AppFuncs.SetDGVColumnsFromDescr(_dataGrid, AppNotes.dgvColDescr);
        }
        #endregion

    }  // class
}
