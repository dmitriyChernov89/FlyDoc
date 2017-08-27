﻿using FlyDoc.Forms;
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
                User user = new User()
                {
                    Id = editId,PC = (string)dgvRow.Cells["ПК"].Value,
                    UserName = (string)dgvRow.Cells["Користувач"].Value,
                    DepartmentId = (int)dgvRow.Cells["Відділ"].Value,
                    AllowNote = (bool)dgvRow.Cells["Службові"].Value,
                    AllowSchedule = (bool)dgvRow.Cells["Графіки"].Value,
                    AllowPhonebook = (bool)dgvRow.Cells["Тел.книга"].Value,
                    AllowConfig = (bool)dgvRow.Cells["Налаштування"].Value,
                    AllowApprovedNach = (bool)dgvRow.Cells["Затв.нач"].Value,
                //    AllowNach = (string)dgvRow.Cells["Ім'я нач"].Value,
                    AllowApproverSB = (bool)dgvRow.Cells["Затв.СБ"].Value,
                //    AllowSB = (string)dgvRow.Cells["Ім'я СБ"].Value,
                    AllowApproverDir = (bool)dgvRow.Cells["Затв.дир"].Value,
                //    AllowDir = (string)dgvRow.Cells["Ім'я дир"].Value,
                    enterMail = (string)dgvRow.Cells["Mail"].Value,
                };
                UserForm frm = new UserForm(user);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (DBContext.UpdateUser(user))
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                    }
                }
                base.EditObject();
            }
        }

        public override void LoadDataToGrid()
        {
            _dataTable = DBContext.GetUsers();
            base.LoadDataToGrid();

            if (_dataGrid.Columns.Contains("DepartmentId")) _dataGrid.Columns["DepartmentId"].Visible = false;

            // установить отн.ширину колонок
            //_dataGrid.Columns[4].FillWeight = 50;
            //_dataGrid.Columns[5].FillWeight = 50;
            //_dataGrid.Columns[6].FillWeight = 50;
            //_dataGrid.Columns[7].FillWeight = 50;
        }

        public override void CreateNewObject()
        {
            UserForm frm = new UserForm(null);
            DialogResult result = frm.ShowDialog();
            if ((result == DialogResult.OK) && (frm.User != null))
            {
                int newId = 0;
                if (DBContext.InsertUser(frm.User, out newId))
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
                DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити поточного користувача?", "Видалення користувача", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    if (DBContext.DeleteUser(id))
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
        #endregion

        #region Public methods
        public DataRow GetConfigRow(string PC, string UserName)
        {
            return DBContext.GetUserConfig(PC, UserName);
        }

        #endregion

    }
}