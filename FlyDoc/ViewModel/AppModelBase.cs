using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlyDoc;


namespace FlyDoc.ViewModel
{
    public abstract class AppModelBase 
    {
        #region fields
        private bool isExistIdColumn = false;

        protected Form _viewForm;
        protected DataTable _dataTable;
        protected DataGridView _dataGrid;
        #endregion

        #region properties
        public DataGridView DataGrid
        {
            get { return _dataGrid; }
            set {
                _dataGrid = value;
                _dataGrid.CellDoubleClick += _dataGrid_CellDoubleClick;
                _dataGrid.KeyDown += _dataGrid_KeyDown;
            }
        }

        private void _dataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F2) && (e.Modifiers == 0))
            {
                EditObject();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                if (e.Control) CopyToNewObject();
                else if (e.Modifiers == 0) CreateNewObject();
            }
            else if ((e.KeyCode == Keys.Delete) && (e.Modifiers == 0))
            {
                DeleteObject();
            }
        }

        private void _dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditObject();
        }

        public Form ViewForm
        {
            get { return _viewForm; }
            set { _viewForm = value; }
        }
        #endregion

        // получить Id из выделенной строки
        protected int getSelectedId()
        {
            int retVal = -1;
            if ((_dataGrid != null) && ((_dataGrid.SelectedRows.Count > 0)) && isExistIdColumn)
            {
                retVal = (int)_dataGrid.SelectedRows[0].Cells["Id"].Value;
            }
            return retVal;
        }

        // получить выделенную строку
        protected DataGridViewRow getSelectedDataRow()
        {
            DataGridViewRow retVal = null;
            if ((_dataGrid != null) && ((_dataGrid.SelectedRows.Count > 0)))
            {
                retVal = _dataGrid.SelectedRows[0];
            }
            return retVal;
        }

        protected void selectGridRowById(int Id)
        {
            if ((_dataGrid != null) && (isExistIdColumn))
            {
                if (_dataGrid.SelectedRows.Count > 0) _dataGrid.SelectedRows[0].Selected = false;

                foreach (DataGridViewRow dr in _dataGrid.Rows)
                {
                    if ((int)dr.Cells["Id"].Value == Id)
                    {
                        dr.Selected = true;
                        _dataGrid.FirstDisplayedScrollingRowIndex = dr.Index;
                        break;
                    }
                }
            }
        }

        protected void deleteGridRowById(int Id)
        {
            if ((_dataGrid != null) && (isExistIdColumn))
            {
                foreach (DataGridViewRow dr in _dataGrid.Rows)
                {
                    if ((int)dr.Cells["Id"].Value == Id)
                    {
                        _dataGrid.Rows.Remove(dr);
                        break;
                    }
                }
            }
        }


        #region Public methods
        public virtual void CopyToNewObject()
        {
        }

        public virtual void CreateNewObject()
        {
        }

        public virtual void DeleteObject()
        {
            if (_dataGrid != null)
            {
                int iDel;
                foreach (DataGridViewRow dr in _dataGrid.SelectedRows)
                {
                    iDel = _dataGrid.Rows.IndexOf(dr);
                    _dataGrid.Rows.Remove(dr);

                    if (iDel >= _dataGrid.Rows.Count) iDel = _dataGrid.Rows.Count - 1;
                    _dataGrid.Rows[iDel].Selected = true;
                }
            }
        }

        public virtual void EditObject()
        {
        }

        public virtual void LoadDataToGrid()
        {
            if (_dataTable != null)
            {
                isExistIdColumn = _dataTable.Columns.Contains("Id");
                _dataGrid.DataSource = _dataTable;

                // скрыть колонку Id
                if (isExistIdColumn) _dataGrid.Columns["Id"].Visible = false;
            }
        }

        #endregion

    }
}
