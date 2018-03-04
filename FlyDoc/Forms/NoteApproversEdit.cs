using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Forms
{
    public partial class NoteApproversEdit : Form
    {
        private static List<ApprItem> _apprList;
        static NoteApproversEdit()
        {
            _apprList = new List<ApprItem>()
            {
                new ApprItem("ApprDir", false, "Директор"),
                new ApprItem("ApprComdir", false, "Ком.директор"),
                new ApprItem("ApprSBNach", false, "Нач.СБ"),
                new ApprItem("ApprSB", false, "Iнспектор СБ"),
                new ApprItem("ApprKasa", false, "Касир"),
                new ApprItem("ApprNach", false, "Нач.торг."),
                new ApprItem("ApprFin", false, "Фiн.директор"),
                new ApprItem("ApprDostavka", false, "Доставка"),
                new ApprItem("ApprEnerg", false, "Енергетик"),
                new ApprItem("ApprSklad", false, "Склад"),
                new ApprItem("ApprBuh", false, "Бухгалтер"),
                new ApprItem("ApprASU", false, "Нач.АСУ"),
            };
        }

        public string GridTitle
        {
            get { return textBoxTitle.Text; }
            set { textBoxTitle.Text = value; }
        }

        public string ApproversText { get; set; }

        public NoteApproversEdit()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            fillCoordsList();

            // строка передана в форму для редактирвания (согл1;согл2;...)
            if (string.IsNullOrEmpty(ApproversText) == false)
            {
                string[] aStr = ApproversText.Split(';');
                DataGridViewRow row;
                foreach (string item in aStr)
                {
                    row = getDGVRowByKey(item);
                    if (row != null) row.Cells[1].Value = true;
                }
            }
        }

        private string getEnabledKeys()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dgvApprs.Rows)
            {
                bool bEnable = System.Convert.ToBoolean(row.Cells[1].Value);

                if (bEnable)
                {
                    string cKey = row.Cells[0].Value.ToString();
                    if (sb.Length > 0) sb.Append(";");
                    sb.Append(cKey);
                }
            }
            return sb.ToString();
        }

        // заполнить dataGridView
        private void fillCoordsList()
        {
            dgvApprs.Rows.Clear();
            dgvApprs.Rows.Add(_apprList.Count);
            ApprItem apprItem;
            for (int i = 0; i < _apprList.Count; i++)
            {
                apprItem = _apprList[i];
                DataGridViewRow row = dgvApprs.Rows[i];
                row.Cells[0].Value = apprItem.Key;   // column Name = "Key"
                row.Cells[1].Value = apprItem.Enable;
                row.Cells[2].Value = apprItem.Title;
            }
        }

        private DataGridViewRow getDGVRowByKey(string key)
        {
            foreach (DataGridViewRow item in dgvApprs.Rows)
            {
                if (item.Cells["Key"].Value.ToString() == key) return item;
            }
            return null;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0)) return;

            // для первой колонки сразу закоммитить изменения
            if (e.ColumnIndex == 1)
            {
                dgvApprs.EndEdit();
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0)) return;

            if (e.ColumnIndex == 2)
            {
                bool bValue = System.Convert.ToBoolean(dgvApprs.Rows[e.RowIndex].Cells[1].Value);
                bValue = !bValue;
                dgvApprs.Rows[e.RowIndex].Cells[1].Value = bValue;
                dgvApprs.EndEdit();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ApproversText = getEnabledKeys();
        }

    }  // class

    internal class ApprItem
    {
        public string Key { get; set; }
        public bool Enable { get; set; }
        public string Title { get; set; }

        public ApprItem()
        {

        }
        public ApprItem(string key, bool enable, string title)
        {
            Key = key; Enable = enable; Title = title;
        }
    }

}
