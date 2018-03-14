using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlyDoc;
using FlyDoc.Lib;
using FlyDoc.Model;
using System.Reflection;

namespace FlyDoc.Presenter
{
    public abstract class AppModelBase
    {
        #region fields
        private bool isExistIdColumn = false;

        protected Form _presenter;
        protected DataTable _dataTable;
        protected DataGridView _dataGrid;

        protected DataGridViewCellFormattingEventHandler OnCellFormattingHandler;
        #endregion

        public bool AllowEdit;

        public AppModelBase()
        {
            AllowEdit = false;
        }

        #region properties
        public DataGridView DataGrid
        {
            get { return _dataGrid; }
            set {
                _dataGrid = value;

                _dataGrid.CellDoubleClick += _dataGrid_CellDoubleClick;
                _dataGrid.KeyDown += _dataGrid_KeyDown;
                // форматирование ячейки
                if (OnCellFormattingHandler != null)
                {
                    _dataGrid.CellFormatting += OnCellFormattingHandler;
                }
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
            if (e.RowIndex > -1) EditObject();
        }

        public Form Presenter
        {
            get { return _presenter; }
            set { _presenter = value; }
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
            if ((_dataGrid != null) && (_dataGrid.Rows.Count > 0) && ((_dataGrid.SelectedRows.Count > 0)))
            {
                retVal = _dataGrid.SelectedRows[0];
            }
            return retVal;
        }

        public DataGridViewRow GetSelectedDataRow()
        {
            return getSelectedDataRow();
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

        #region сравнение наборов сущностей модели приложения
        protected virtual void onRemoveFunc(int id) { }
        protected virtual void onAddFunc(object addEntity) { }
        protected virtual void onUpdateFunc(object updateEntity) { }

        // поиск различия между двумя наборами элементов
        // keepItems - набор, хранящийся в модели
        // dbItems - набор, получаемый из БД по таймеру
        internal bool GetItemsListDifference<T>(List<T> keepItems, List<T> dbItems) where T : IDBInfo
        {
            bool retVal = false;
            // 1. удалить из keepItems записи, которых уже нет в БД
            //    собрать из dbItems все Id
            List<int> dbIds = new List<int>();
            foreach (T dbi in dbItems) { dbIds.Add(dbi.Id); }
            int[] notExistIds = keepItems.Select(e => e.Id).Except(dbIds).ToArray();
            if (notExistIds.Length > 0)
            {
                keepItems.RemoveAll(e =>
                {
                    if (notExistIds.Contains(e.Id))
                    {
                        onRemoveFunc(e.Id);
                        return true;
                    }
                    else
                        return false;
                });
                retVal = true;
            }

            // обновить набор keepItems
            T item; int id;
            foreach (T dbItem in dbItems)
            {
                id = dbItem.Id;
                item = keepItems.FirstOrDefault(e => e.Id == id);
                // добавить
                if (item == null)
                {
                    keepItems.Add(dbItem);
                    onAddFunc(item);
                    retVal = true;
                }
                // обновить
                else
                {
                    if (updateItemFromOtherOne(item, dbItem))
                    {
                        onUpdateFunc(item);
                        retVal = true;
                    }
                }
            }

            return retVal;
        }
        // сравнение двух сущностей по полям
        private bool updateItemFromOtherOne<T>(T itemTo, T itemFrom)
        {
            if ((itemTo == null) || (itemFrom == null) || !(itemTo.GetType().Equals(itemFrom.GetType()))) return false;

            bool retVal = false;
            List<string> updFields = new List<string>();
            Type tItem = typeof(T), tField;
            PropertyInfo[] pInfo = tItem.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in pInfo)
            {
                var v1 = pi.GetValue(itemTo, null);
                var v2 = pi.GetValue(itemFrom, null);
                tField = pi.PropertyType;
                if ((tField.IsValueType) || (tField.Name=="String"))
                {
                    if ((tField.Name == "String") && (v1 == null) && (v2 == null)) continue;

                    if (((v1 == null) && (v2 != null))
                        || ((v1 != null) && (v2 == null))
                        || !v1.Equals(v2))
                    {
                        updFields.Add(pi.Name);
                        pi.SetValue(itemTo, v2, null);
                        retVal = true;
                    }
                }
            }
            return retVal;
        }

        #endregion


        protected void notAllowEditAction()
        {
            AppFuncs.WriteLogTraceMessage(" - редагування заборонено !!");
            MessageBox.Show("Редагування заборонено.", "Редагування", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }
}
