using FlyDoc.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Lib
{
    public static class AppFuncs
    {
        public static void SetDGVColumnsFromDescr(DataGridView dgv, Dictionary<string, DGVColDescr> colDescr)
        {
            if ((dgv == null) || (colDescr == null)) return;
            DGVColDescr curDescr;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (colDescr.ContainsKey(col.Name))
                {
                    curDescr = colDescr[col.Name];
                    col.HeaderText = (curDescr.Header.IsNull()) ? col.Name : curDescr.Header;

                    col.Visible = curDescr.Visible;

                    if (col.Visible && (col.FillWeight > 0)) col.FillWeight = curDescr.FillWeight;

                    col.DefaultCellStyle.Alignment = curDescr.Alignment;

                    col.HeaderCell.Style.Alignment = (curDescr.HeaderAlignment == DataGridViewContentAlignment.NotSet) 
                        ? curDescr.Alignment
                        : curDescr.HeaderAlignment;


                    if ((col is DataGridViewCheckBoxColumn) && curDescr.ThreeStates)
                        ((DataGridViewCheckBoxColumn)col).ThreeState = true;
                }
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        // выделить строку в гриде по Id
        public static void SelectGridRowById(DataGridView dataGrid, int id)
        {
            if (!dataGrid.Columns.Contains("Id")) return;

            foreach (DataGridViewRow item in dataGrid.SelectedRows) item.Selected = false;

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if ((int)row.Cells["Id"].Value == id)
                {
                    row.Selected = true;
                    dataGrid.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }


    }  // class
}
