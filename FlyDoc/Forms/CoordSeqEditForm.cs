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
    public partial class CoordSeqEditForm : Form
    {
        public List<Coordinator> Coordinators;

        public string GridTitle
        {
            get { return textBoxTitle.Text; }
            set { textBoxTitle.Text = value; }
        }

        public CoordSeqEditForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            fillCoordsList();

            base.OnLoad(e);
        }

        public string GetEnableCoordKeys()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dataGridView1.Rows)
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

        private void fillCoordsList()
        {
            // получить список согласователей из БД (из полей табл. Notes)
            List<Coordinator> coordDB = DBContext.GetCoordinatorsDescr();

            Coordinator curCoord;
            // если Coordinators не пусто, то заполнить из него поле Enable - признак согласования
            if (Coordinators != null)
            {
                // цикл по Coordinators, у которых Enable = true
                foreach (var item in Coordinators.Where(c => c.Enable))
                {
                    curCoord = coordDB.FirstOrDefault(c => c.Key == item.Key);
                    if (curCoord != null) curCoord.Enable = true;
                }
            }

            // заполнить dataGridView
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(coordDB.Count);
            for (int i = 0; i < coordDB.Count; i++)
            {
                curCoord = coordDB[i];
                DataGridViewRow row = dataGridView1.Rows[i];
                row.Cells[0].Value = curCoord.Key;
                row.Cells[1].Value = curCoord.Enable;
                row.Cells[2].Value = curCoord.Title;
                row.Cells[3].Value = curCoord.SeqNumber;
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0)) return;

            // для первой колонки сразу закоммитить изменения
            if (e.ColumnIndex == 1)
            {
                dataGridView1.EndEdit();
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0)) return;

            if (e.ColumnIndex == 2)
            {
                bool bValue = System.Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                bValue = !bValue;
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = bValue;
                dataGridView1.EndEdit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Coordinator coord;

            // создать новый список из datGrid
            if ((this.Coordinators == null) || (this.Coordinators.Count == 0))
            {
                this.Coordinators = new List<Coordinator>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string cKey = row.Cells[0].Value.ToString();
                    this.Coordinators.Add(new Coordinator() { Key = cKey });
                }
            }

            // переопределить Enable и SeqNumber
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string cKey = row.Cells[0].Value.ToString();
                coord = this.Coordinators.FirstOrDefault(c => c.Key == cKey);
                if (coord != null)
                {
                    coord.Enable = System.Convert.ToBoolean(row.Cells[1].Value);
                    coord.SeqNumber = System.Convert.ToInt32(row.Cells[3].Value);
                }
            }
        }

    }  // class
}
