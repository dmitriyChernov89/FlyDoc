using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyDoc.Model;
using FlyDoc.Forms;
using FlyDoc.Lib;
using System.Windows.Forms;

namespace FlyDoc.ViewModel
{
    public class AppPhone : AppModelBase
    {
        public AppPhone()
        {
        }
        public override void EditObject()
        {
            DataGridViewRow dgvRow = base.getSelectedDataRow();
            if (dgvRow != null)
            {
                int editId = (int)dgvRow.Cells["Id"].Value;
                PhoneModel phone = new PhoneModel()
                {
                    Id = editId,
                    Name = dgvRow.Cells["Відділ"].Value.ToStringNull(),
                    Positions = dgvRow.Cells["Посада"].Value.ToString(),
                    FIO = dgvRow.Cells["П.І.Б."].Value.ToString(),
                    Dect = dgvRow.Cells["Трубка"].Value.ToString(),
                    PhoneNumber = dgvRow.Cells["Телефон"].Value.ToString(),
                    Mobile = dgvRow.Cells["Мобільний"].Value.ToString(),
                    eMail = dgvRow.Cells["Пошта"].Value.ToString()
                };
                NewPhone frm = new NewPhone(phone);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (DBContext.UpdatePhone(phone))
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                    }
                }
                base.EditObject();
            }
        }//Edit
        public override void CreateNewObject()
        {
            NewPhone frm = new NewPhone(null);
            DialogResult result = frm.ShowDialog();
            if ((result == DialogResult.OK) && (frm.PhoneModel != null))
            {
                int newId = 0;
                if (DBContext.InsertPhone(frm.PhoneModel, out newId))
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
                DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити поточний телефон?", "Видалення телефона", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    if (DBContext.DeletePhone(id))
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
            _dataTable = DBContext.GetPhones();  // чтение данных о сл.зап.

            base.LoadDataToGrid();

            if (_dataGrid.Columns.Contains("DepartmentId")) _dataGrid.Columns["DepartmentId"].Visible = false;

            DataGridViewColumn column1 = _dataGrid.Columns[1];
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

    }
}
