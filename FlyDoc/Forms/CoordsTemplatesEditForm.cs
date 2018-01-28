using FlyDoc.Lib;
using FlyDoc.Model;
using FlyDoc.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlyDoc.Forms
{
    public partial class CoordsTemplatesEditForm : Form
    {
        #region static members
        private static Dictionary<string, DGVColDescr> _dgvColDescr;
        static CoordsTemplatesEditForm()
        {
            _dgvColDescr = new Dictionary<string, DGVColDescr>()
            {
                { "Id", new DGVColDescr() { Alignment = DataGridViewContentAlignment.MiddleCenter, FillWeight=10 } },
                { "TemplateName", new DGVColDescr() { FillWeight=45 } },
                { "CoordsList", new DGVColDescr() { FillWeight = 45 } }
            };
        }
        #endregion

        public CoordsTemplatesEditForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            reloadData();
            AppFuncs.SetDGVColumnsFromDescr(dataGridView1, _dgvColDescr);

            base.OnLoad(e); 
        }

        #region data handlers
        private void reloadData()
        {
            DataTable dt = DBContext.GetCoordsTemplates();
            dataGridView1.DataSource = dt;
        }

        private void editEnableCoords()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                int iTplId = System.Convert.ToInt32(row.Cells[0].Value);
                string sTplName = row.Cells[1].Value.ToString();
                string sPreEnableKeys = row.Cells[2].Value.ToString();

                // распарсить строку Согласователей в набор объектов Coordinator
                List<Coordinator> tplCoords = DBContext.GetCoordinatorsDescr();
                Coordinator coord;
                if (!sPreEnableKeys.IsNull())
                {
                    string[] aKeys = sPreEnableKeys.Split(';');
                    foreach (string key in aKeys)
                    {
                        coord = tplCoords.FirstOrDefault(c => c.Key == key);
                        if (coord != null) coord.Enable = true;
                    }
                }
                // и передать этот набор в форму редактирования списка Согласователей
                CoordSeqEditForm csFrm = new CoordSeqEditForm()
                {
                    Coordinators = tplCoords, GridTitle = string.Format("шаблона Согласователей '{0}'", sTplName)
                };
                if (csFrm.ShowDialog() == DialogResult.OK)
                {
                    string sNewEnableKeys = csFrm.GetEnableCoordKeys();
                    csFrm.Dispose();

                    // если что-то изменили в наборе Согласователей, то сохранить новую строку в БД
                    if (sPreEnableKeys != sNewEnableKeys)
                    {
                        if (DBContext.UpdateCoordsTemplate(iTplId, sTplName, sNewEnableKeys))
                        {
                            row.Cells[2].Value = sNewEnableKeys;
                        }
                    }
                }
            }
        }

        private void editTemplateName()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int iTplId = System.Convert.ToInt32(row.Cells[0].Value);
                string sTplName = row.Cells[1].Value.ToString();
                string sPreName = sTplName;

                if ((AppFuncs.InputBox("Редактирование строки", "Введите новое наименование шаблона", ref sTplName) == DialogResult.OK) && (sTplName != sPreName))
                {
                    if (DBContext.UpdateCoordsTemplate(iTplId, sTplName))
                    {
                        row.Cells[1].Value = sTplName;
                    }
                }
            }

        }

        private void addNewTemplateCoords()
        {
            string value = "";
            if (AppFuncs.InputBox("Ввод строки", "Введите наименование шаблона Согласователей:", ref value) == DialogResult.OK)
            {
                CoordSeqEditForm csFrm = new CoordSeqEditForm()
                {
                    GridTitle = string.Format("нового шаблона Согласователей '{0}'", value)
                };
                if (csFrm.ShowDialog() == DialogResult.OK)
                {
                    string sNewEnableKeys = csFrm.GetEnableCoordKeys();
                    csFrm.Dispose();

                    int newId = DBContext.InsertCoordsTemplate(value, sNewEnableKeys);
                    if (newId > 0)
                    {
                        reloadData();
                        AppFuncs.SelectGridRowById(this.dataGridView1, newId);
                    }
                }
            }
        }

        private void deleteTemplate()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string sTplName = row.Cells[1].Value.ToString();
                if (MessageBox.Show(string.Format("Видалит шаблон '{0}'", sTplName), "Видалення шаблону", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;

                int rowIndex = dataGridView1.Rows.IndexOf(row);
                int id = System.Convert.ToInt32(row.Cells[0].Value);

                if (DBContext.DeleteCoordsTemplate(id))
                {
                    reloadData();
                    if (rowIndex >= dataGridView1.Rows.Count) rowIndex = dataGridView1.Rows.Count - 1;
                    if (rowIndex >= 0)
                    {
                        row = dataGridView1.Rows[rowIndex];
                        row.Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = rowIndex;
                    }
                }
            }
        }
        #endregion

            #region event handlers
        private void btnEdit_Click(object sender, EventArgs e)
        {
            editEnableCoords();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addNewTemplateCoords();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            deleteTemplate();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;

            // если двойной клик в колонке наим.шаблоно, то изменить наименование шаблона
            if (e.ColumnIndex == 1)
                editTemplateName();
            else
                editEnableCoords();
        }
        #endregion

    }  // class
}
