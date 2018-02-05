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
                sqlText = string.Format("SELECT * FROM Department WHERE (Id = {0})",id);
                DataTable dtFrom = DBContext.GetQueryTable(sqlText);
                if ((dtFrom != null) && (dtFrom.Rows.Count > 0))
                {
                    DataTable dt = DBContext.GetQueryTable("SELECT Max(Id) FROM Department");
                    DataRow dr = dtFrom.Rows[0];
                    int newId = (int)dt.Rows[0][0] + 1;

                    sqlText = string.Format("INSERT INTO Department (Id, Name) VALUES ({0}, '{1}')", newId, dr["Name"]);

                    if (DBContext.Execute(sqlText) > 0)
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(newId);
                    }
                    base.CopyToNewObject();
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
                    if (DBContext.DeleteEntityById(Department._dbTableName, id))
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

        public override void CreateNewObject()
        {
            DepartmentForm frm = new DepartmentForm();
            DialogResult result = frm.ShowDialog();
            if ((result == DialogResult.OK) && (frm.Department != null))
            {
                if (DBContext.InsertEntity(frm.Department))
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(frm.Department.Id);
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
                Department dep = new Department() {
                    Id = editId, Name = (string)dgvRow.Cells["Name"].Value
                };
                DepartmentForm frm = new DepartmentForm(dep);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (DBContext.UpdateEntity(frm.Department))
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(frm.Department.Id);
                    }
                }
                base.EditObject();
            }
        }

        public override void LoadDataToGrid()
        {
            _dataTable = DBContext.GetDepartments();
            base.LoadDataToGrid();

            // show Id
            _dataGrid.Columns[0].Visible = true;
            _dataGrid.Columns[0].FillWeight = 25;
        }
        #endregion


    }  // class
}
