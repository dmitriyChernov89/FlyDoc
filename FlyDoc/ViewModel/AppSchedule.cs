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
        #region static members
        private static Dictionary<string, DGVColDescr> dgvColDescr;
        static AppSchedule()
        {
            // запрос vwSchedules
            /*SELECT dbo.Schedules.Id, dbo.Schedules.IdDepartment AS DepartmentId, dbo.Department.Name AS Відділ, dbo.Schedules.Data AS Дата, dbo.Schedules.Approved AS Затверджений, dbo.Schedules.Manager AS Ким
                FROM  dbo.Schedules INNER JOIN dbo.Department ON dbo.Schedules.IdDepartment = dbo.Department.Id*/
            dgvColDescr = new Dictionary<string, DGVColDescr>()
            {
                { "Id", new DGVColDescr() { Header="Id", Visible = false } },
                { "DepartmentId", new DGVColDescr() { Visible = false} },
                { "Відділ", new DGVColDescr() { Header = "Відділ", FillWeight=50} },
                { "Дата", new DGVColDescr() { Header = "Дата", FillWeight=20, Alignment= DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleCenter} },
                { "Затверджений", new DGVColDescr() {FillWeight=15, Alignment= DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleCenter} },
                { "Ким", new DGVColDescr() { FillWeight=15, Alignment= DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleCenter} }
            };
        }
        #endregion

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
                AppFuncs.dialogCloseResult(frm.GetType().Name, result);
                if (result == DialogResult.OK)
                {
                    bool dbResult = DBContext.UpdateSchedule(sched);
                    if (dbResult)
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                    }
                    AppFuncs.saveToDBResult(dbResult);
                }
                base.EditObject();
            }
        }

        public override void CreateNewObject()
        {
            Schedule frm = new Schedule(null);
            DialogResult result = frm.ShowDialog();
            AppFuncs.dialogCloseResult(frm.GetType().Name, result);
            if ((result == DialogResult.OK) && (frm.ScheduleModel != null))
            {
                int newId = 0;
                bool dbResult = DBContext.InsertSchedule(frm.ScheduleModel, out newId);
                if (dbResult)
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(newId);
                }
                AppFuncs.saveToDBResult(dbResult);
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
                    string logMsg = $"Видалення поточний графіку, id {id}";
                    AppFuncs.WriteLogTraceMessage(logMsg + "...");

                    bool dbResult = DBContext.DeleteSchedule(id);
                    if (dbResult)
                    {
                        base.DeleteObject();
                    }
                    AppFuncs.deleteFromDBResult(logMsg, dbResult);
                }
            }
            else
            {
                MessageBox.Show("Виберіть рядок для видалення");
            }
        }
        public override void LoadDataToGrid()
        {
            AppFuncs.WriteLogTraceMessage($" - {this.GetType().Name}.LoadDataToGrid()");

            _dataTable = DBContext.GetSchedule();  // чтение данных о сл.зап.

            base.LoadDataToGrid();

            AppFuncs.SetDGVColumnsFromDescr(_dataGrid, AppSchedule.dgvColDescr);
        }
        #endregion


    }  // class
}
