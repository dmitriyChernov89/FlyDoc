using FlyDoc.Forms;
using FlyDoc.Lib;
using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.ViewModel
{
    public class AppUsers : AppModelBase
    {
        public AppUsers()
        {
        }

        #region override methods

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
                User user = new User(editId);

                UserForm frm = new UserForm(user);
                DialogResult result = frm.ShowDialog();
                AppFuncs.dialogCloseResult(frm.GetType().Name, result);
                if (result == DialogResult.OK)
                {
                    bool dbResult = DBContext.UpdateEntity(user);
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
            UserForm frm = new UserForm(null);
            DialogResult result = frm.ShowDialog();
            AppFuncs.dialogCloseResult(frm.GetType().Name, result);
            if ((result == DialogResult.OK) && (frm.User != null))
            {
                bool dbResult = DBContext.InsertEntity(frm.User);
                if (dbResult)
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(frm.User.Id);
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
                DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити поточного користувача?", "Видалення користувача", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    string logMsg = $"Видалення користувача, id {id}";
                    AppFuncs.WriteLogTraceMessage(logMsg + "...");
                    bool dbResult = DBContext.DeleteEntityById(User._dbTableName, id);
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

            _dataTable = DBContext.GetUsers();
            base.LoadDataToGrid();

            if (_dataGrid.Columns.Contains("DepartmentId")) _dataGrid.Columns["DepartmentId"].Visible = false;


            // установить отн.ширину колонок
            //_dataGrid.Columns[4].FillWeight = 50;
            //_dataGrid.Columns[5].FillWeight = 50;
            //_dataGrid.Columns[6].FillWeight = 50;
            //_dataGrid.Columns[7].FillWeight = 50;
        }

        #endregion

    }
}
