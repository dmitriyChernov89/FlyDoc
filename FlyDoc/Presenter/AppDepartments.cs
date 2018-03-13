using FlyDoc.Forms;
using FlyDoc.Lib;
using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Presenter
{
    public class AppDepartments: AppModelBase
    {
        public AppDepartments()
        {
        }

        #region override methods
        
        // у каждой модели могут быть свои процедуры копирования
        public override void CopyToNewObject()
        {
            string sqlText;
            int id = getSelectedId();
            if (id != -1)
            {
                using (DBContext db = new DBContext())
                {
                    sqlText = string.Format("SELECT * FROM Department WHERE (Id = {0})", id);
                    DataTable dtFrom = db.GetQueryTable(sqlText);
                    if ((dtFrom != null) && (dtFrom.Rows.Count > 0))
                    {
                        DataTable dt = db.GetQueryTable("SELECT Max(Id) FROM Department");
                        DataRow dr = dtFrom.Rows[0];
                        int newId = (int)dt.Rows[0][0] + 1;

                        sqlText = $"INSERT INTO Department (Id, Name) VALUES ({newId}, '{dr["Name"]}'); SELECT @@IDENTITY";

                        if ((int)db.ExecuteScalar(sqlText) > 0)
                        {
                            this.LoadDataToGrid();
                            base.selectGridRowById(newId);
                        }
                        base.CopyToNewObject();
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть рядок для копіювання");
            }
        }

        public override void DeleteObject()
        {
            int id = getSelectedId();
            if (id != -1)
            {
                DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити поточний відділ?", "Видалення відділу", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    DataGridViewRow dgvRow = base.getSelectedDataRow();
                    string name = dgvRow.Cells["Name"].ToString();
                    string logMsg = $"Видалення вiддiлу {id}";
                    AppFuncs.WriteLogTraceMessage(logMsg + "...");

                    bool dbResult = DBContext.DeleteEntityById(Department._dbTableName, id);
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

        public override void CreateNewObject()
        {
            DepartmentForm frm = new DepartmentForm();
            DialogResult result = frm.ShowDialog();
            AppFuncs.dialogCloseResult(frm.GetType().Name, result);
            if ((result == DialogResult.OK) && (frm.Department != null))
            {
                bool dbResult = DBContext.InsertEntity(frm.Department);
                if (dbResult)
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(frm.Department.Id);
                }
                AppFuncs.saveToDBResult(dbResult);
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
                Department dep = new Department() {
                    Id = editId, Name = (string)dgvRow.Cells["Name"].Value
                };
                DepartmentForm frm = new DepartmentForm(dep);
                DialogResult result = frm.ShowDialog();
                AppFuncs.dialogCloseResult(frm.GetType().Name, result);
                if (result == DialogResult.OK)
                {
                    bool dbResult = DBContext.UpdateEntity(frm.Department);
                    if (dbResult)
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(frm.Department.Id);
                    }
                    AppFuncs.saveToDBResult(dbResult);
                }
                base.EditObject();
            }
        }

        public override void LoadDataToGrid()
        {
            AppFuncs.WriteLogTraceMessage($" - {this.GetType().Name}.LoadDataToGrid()");

            _dataTable = DBContext.GetDepartments();
            base.LoadDataToGrid();

            // show Id
            _dataGrid.Columns[0].Visible = true;
            _dataGrid.Columns[0].FillWeight = 25;
        }
        #endregion


    }  // class
}
