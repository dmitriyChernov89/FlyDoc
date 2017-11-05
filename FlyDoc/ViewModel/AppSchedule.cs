using FlyDoc.Model;
using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.ViewModel
{
    public class AppSchedule : AppModelBase
    {

        public AppSchedule()
        {
        }


        #region override methods
        public override void EditObject()
        {
            DataGridViewRow dgvRow = base.getSelectedDataRow();
            if (dgvRow != null)
            {
                int editId = (int)dgvRow.Cells["Id"].Value;
                ScheduleModel sched = new ScheduleModel()
                {
                    Id = editId,
                    DepartmentName  = Convert.ToString(dgvRow.Cells["Відділ"].Value),
                    DepartmentId  = Convert.ToInt32(dgvRow.Cells["DepartmentId"].Value),
                    Approved =  Convert.ToBoolean(dgvRow.Cells["Затверджений"].Value),
                    Date = Convert.ToDateTime(dgvRow.Cells["Дата"].Value),
                    Dir = Convert.ToString(dgvRow.Cells["Ким"].Value)
                };
                Schedule frm = new Schedule(sched);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (DBContext.UpdateSchedule(sched))
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
            Schedule frm = new Schedule(null);
            DialogResult result = frm.ShowDialog();
            if ((result == DialogResult.OK) && (frm.ScheduleModel != null))
            {
                int newId = 0;
                if (DBContext.InsertSchedule(frm.ScheduleModel, out newId))
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
                DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити поточний графік?", "Видалення графіка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    if (DBContext.DeleteSchedule(id))
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
            _dataTable = DBContext.GetSchedule();  // чтение данных о сл.зап.
            base.LoadDataToGrid();

           // _dataGrid.Columns[5].Visible = false;
            // отн.ширина колонок
            //            _dataGrid.Columns[1].FillWeight = 50;
            //            _dataGrid.Columns[2].FillWeight = 50;
        }
        #endregion


    }  // class
}
