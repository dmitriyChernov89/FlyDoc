using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlyDoc.Model;
using FlyDoc.Lib;

namespace FlyDoc.Views
{
    public partial class AppSetup : Form
    {
        public AppSetup()
        {
            InitializeComponent();
        }

        private void AppSetup_Load(object sender, EventArgs e)
        {
            DataTable dtUsers = DBContext.GetUsers();
            //dtUsers.RowDeleting += DtUsers_RowDeleting;
            //dtUsers.RowChanging += DtUsers_RowChanging;

            dgvUsers.DataSource = dtUsers;

            DataGridViewComboBoxColumn dgCol = (DataGridViewComboBoxColumn)dgvUsers.Columns["Department"];
            dgCol.DataSource = DBContext.GetDepartments();
            dgCol.DisplayMember = "Name";
            dgCol.ValueMember = "Id";
        }

        private void DtUsers_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            MessageBox.Show("changing row");
        }

        #region delete row
        private void btnDel_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void dgvUsers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            deleteRow();
            e.Cancel = true;
        }

        private void stripMenuItemDelUser_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void deleteRow()
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Виділіть рядок та спробуйте знову.", "Видалення запису", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataGridViewRow row = dgvUsers.SelectedRows[0];

                DialogResult result = MessageBox.Show(string.Format("Видалити запис?\n\tPC\t = {0}\n\tUserName\t = {1}", row.Cells["PC"].Value, row.Cells["UserName"].Value), "Видалення запису", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                //if (result == DialogResult.No) e.Cancel = true;
                
            }
        }

        #endregion

        private void contextMenuStripUsers_Opening(object sender, CancelEventArgs e)
        {
            stripMenuItemDelUser.Enabled = (dgvUsers.SelectedRows.Count > 0);
        }
    }
}
