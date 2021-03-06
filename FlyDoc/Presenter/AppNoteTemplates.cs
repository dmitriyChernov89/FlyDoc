﻿using FlyDoc.Forms;
using FlyDoc.Lib;
using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Presenter
{
    public class AppNoteTemplates : AppModelBase
    {
        private static Dictionary<string, DGVColDescr> dgvColDescr;
        static AppNoteTemplates()
        {
            // key - db-field name
            dgvColDescr = new Dictionary<string, DGVColDescr>()
            {
                { "Id", new DGVColDescr() { Visible = false } },
                { "Name", new DGVColDescr() { Header="шаблон", FillWeight=30 } },
                { "HeadDir", new DGVColDescr() { Visible = false } },
                { "HeadNach", new DGVColDescr() { Visible = false } },
                { "BodyUp", new DGVColDescr() { Header = "текст службової", FillWeight=30 } },
                { "TableColums", new DGVColDescr() { Header="стовпців таблиці", FillWeight=5 } },
                { "ColumName1", new DGVColDescr() { Visible = false } },
                { "ColumName2", new DGVColDescr() { Visible = false } },
                { "ColumName3", new DGVColDescr() { Visible = false } },
                { "ColumName4", new DGVColDescr() { Visible = false } },
                { "ColumName5", new DGVColDescr() { Visible = false } },
                { "ColumName6", new DGVColDescr() { Visible = false } },
                { "ColumName7", new DGVColDescr() { Visible = false } },
                { "ColumName8", new DGVColDescr() { Visible = false } },
                { "ColumName9", new DGVColDescr() { Visible = false } },
                { "ColumName10", new DGVColDescr() { Visible = false } },
                { "BodyDown", new DGVColDescr() { Visible = false } },
                { "ApprASU", new DGVColDescr() { Header="АСУ", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprBuh", new DGVColDescr() { Header="Бух", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprComdir", new DGVColDescr() { Header="КомДир", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprDir", new DGVColDescr() { Header="Директор", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprDostavka", new DGVColDescr() { Header="Доставка", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprEnerg", new DGVColDescr() { Header="Енерг", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprFin", new DGVColDescr() { Header="Фiн", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprKasa", new DGVColDescr() { Header="Каса", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprNach", new DGVColDescr() { Header="Нач", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprSB", new DGVColDescr() { Header="СБ", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprSBNach", new DGVColDescr() { Header="СБНач", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "ApprSklad", new DGVColDescr() { Header="Склад", FillWeight=4, Alignment = DataGridViewContentAlignment.MiddleCenter, HeaderAlignment = DataGridViewContentAlignment.MiddleLeft } },
                { "Help", new DGVColDescr() { Visible = false } }
            };
        }

        public AppNoteTemplates()
        {
        }

        #region override methods
        public override void CopyToNewObject()
        {
            base.CopyToNewObject();
        }

        public override void CreateNewObject()
        {
            NoteTemplateForm frm = new NoteTemplateForm(null);
            DialogResult result = frm.ShowDialog();
            AppFuncs.dialogCloseResult(frm.GetType().Name, result);
            if ((result == DialogResult.OK) && (frm.NoteTemplate != null))
            {
                bool dbResult = DBContext.InsertEntity(frm.NoteTemplate);
                if (dbResult)
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(frm.NoteTemplate.Id);
                    MessageBox.Show("Створена нова службова за № " + frm.NoteTemplate.Id.ToString(), "Строверення службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                AppFuncs.saveToDBResult(dbResult);
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
                NoteTemplate note = new NoteTemplate(editId);

                NoteTemplateForm frm = new NoteTemplateForm(note);
                DialogResult result = frm.ShowDialog();
                AppFuncs.dialogCloseResult(frm.GetType().Name, result);
                if (result == DialogResult.OK)
                {
                    bool dbResult = DBContext.UpdateEntity(note);
                    if (dbResult)
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                        MessageBox.Show("Шаблон службової оновлен", "Оновлення шаблону службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    AppFuncs.saveToDBResult(dbResult);
                }
                base.EditObject();
            }
        }

        public override void DeleteObject()
        {
            int id = getSelectedId();
            if (id != -1)
            {
                DataGridViewRow dgvRow = base.getSelectedDataRow();
                string nameTpl = dgvRow.Cells["Name"].ToString();

                DialogResult result = MessageBox.Show($"Ви впевнені що хочете видалити шаблон службової '{nameTpl}' ?", "Видалення шаблону службової", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    string logMsg = $"Видалення шаблон службової '{nameTpl}'";
                    AppFuncs.WriteLogTraceMessage(logMsg + "...");
                    bool dbResult = DBContext.DeleteEntityById(NoteTemplate._dbTableName, id);
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

            _dataTable = DBContext.GetNoteTemplates();  // чтение данных о шаблонах сл.зап.
            base.LoadDataToGrid();

            AppFuncs.SetDGVColumnsFromDescr(_dataGrid, AppNoteTemplates.dgvColDescr);
        }
        #endregion

    }  // class
}
