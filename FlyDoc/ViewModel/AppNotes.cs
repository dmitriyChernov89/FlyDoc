using FlyDoc.Model;
using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlyDoc.Forms;
using System.Reflection;
using System.Diagnostics;

namespace FlyDoc.ViewModel
{
    public class AppNotes : AppModelBase, IDisposable
    {
        #region static members
        private static Dictionary<string, DGVColDescr> dgvColDescr;
        static AppNotes()
        {
            // стиль ячеек со статусом согласования
            DataGridViewCellStyle apprCellStyle = new DataGridViewCellStyle();
            apprCellStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            apprCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // key - имя поля из DataSource
            dgvColDescr = new Dictionary<string, DGVColDescr>()
            {
                { "Id", new DGVColDescr() { Header="№ служб.", FillWeight=80} },
                { "TemplateName", new DGVColDescr() { Header="Тип", FillWeight=800 } },
                { "DepartmentName", new DGVColDescr() { Header = "Відділ", FillWeight=200} },
                { "DepartmentId", new DGVColDescr() { Visible = false} },
                { "Date", new DGVColDescr() { Header="Дата", FillWeight=250} },
                { "ApprAvtor", new DGVColDescr() { Header="Автор", FillWeight=80, CellStyle = apprCellStyle} },
                { "ApprDir", new DGVColDescr() { Header="Директор", FillWeight=80, CellStyle = apprCellStyle} },
                { "ApprComdir", new DGVColDescr() { Header="КомДир", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprSBNach", new DGVColDescr() { Header="СБНач", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprSB", new DGVColDescr() { Header="СБ", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprKasa", new DGVColDescr() { Header="Каса", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprNach", new DGVColDescr() { Header="Нач", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprFin", new DGVColDescr() { Header="Фiн", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprDostavka", new DGVColDescr() { Header="Доставка", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprEnerg", new DGVColDescr() { Header="Енерг", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprSklad", new DGVColDescr() { Header="Склад", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprBuh", new DGVColDescr() { Header="Бух", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprASU", new DGVColDescr() { Header="АСУ", FillWeight=80, CellStyle = apprCellStyle } },
                { "ApprAll", new DGVColDescr() { Header="Всi", FillWeight=80, CellStyle = apprCellStyle } }
            };
        }
        #endregion

        private List<Note> _viewNotes;  // текущий набор, который отображается
        private DataTable _notesDataTable;

        private Timer _timer;

        public AppNotes()
        {
            base.OnCellFormattingHandler = _dataGrid_CellFormatting;

            _viewNotes = DBContext.GetNotesModelList();

            #region таблица для отображения
            _notesDataTable = new DataTable();
            _notesDataTable.Columns.Add(new DataColumn("Id", typeof(int)));
            _notesDataTable.Columns.Add(new DataColumn("TemplateName", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("DepartmentId", typeof(int)));
            _notesDataTable.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            _notesDataTable.Columns.Add(new DataColumn("ApprAvtor", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprDir", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprComdir", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprSBNach", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprSB", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprKasa", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprNach", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprFin", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprDostavka", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprEnerg", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprSklad", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprBuh", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprASU", typeof(string)));
            _notesDataTable.Columns.Add(new DataColumn("ApprAll", typeof(string)));
            _dataTable = _notesDataTable;
            #endregion

            _timer = new Timer();
            _timer.Interval = 2000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            AppFuncs.LogEnable = false;
            List<Note> dbNotes = DBContext.GetNotesModelList();
            AppFuncs.LogEnable = true;

            bool changed = base.GetItemsListDifference<Note>(_viewNotes, dbNotes);
            Debug.Print($" - is changed - {changed.ToString()}");

            _timer.Start();
        }

        private void _dataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() == "О")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.Red;
                e.CellStyle.SelectionForeColor = System.Drawing.Color.Red;
            }
            else if (e.Value.ToString() == "З")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.Green;
                e.CellStyle.SelectionForeColor = System.Drawing.Color.Yellow;
            }
        }

        #region override methods
        public override void CopyToNewObject()
        {
            base.CopyToNewObject();
        }

        public override void EditObject()
        {
            if (base.AllowEdit == false)
            {
                base.notAllowEditAction();
                return;
            }

            DataGridViewRow dgvRow = base.getSelectedDataRow();
            if (dgvRow != null)
            {
                int editId = (int)dgvRow.Cells["Id"].Value;
                Note note = new Note(editId);

                NewNote frm = new NewNote(note);
                DialogResult result = frm.ShowDialog();
                AppFuncs.dialogCloseResult(frm.GetType().Name, result);
                if (result == DialogResult.OK)
                {
                    bool dbResult = DBContext.UpdateNotes(note);
                    if (dbResult)
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                        MessageBox.Show("Службова оновлена", "Оновлення службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    AppFuncs.saveToDBResult(dbResult);
                }
                base.EditObject();
            }
        }

        public override void CreateNewObject()
        {
            if (base.AllowEdit == false) { base.notAllowEditAction(); return; }

            NewNote frm = new NewNote(null);
            DialogResult result = frm.ShowDialog();
            AppFuncs.dialogCloseResult(frm.GetType().Name, result);
            if ((result == DialogResult.OK) && (frm.Note != null))
            {
                bool dbResult = DBContext.InsertNotes(frm.Note);
                if (dbResult)
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(frm.Note.Id);
                    MessageBox.Show("Створена нова службова за № " + frm.Note.Id.ToString(), "Строверення службової", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                AppFuncs.saveToDBResult(dbResult);
            }
            frm.Dispose();

            base.CreateNewObject();
        }

        public override void DeleteObject()
        {
            if (base.AllowEdit == false) { base.notAllowEditAction(); return; }

            int id = getSelectedId();
            if (id != -1)
            {
                DialogResult result = MessageBox.Show($"Ви впевнені що хочете видалити службову № {id.ToString()} ?", "Видалення службової", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    Note note = new Note(id);
                    string logMsg = $"Видалення службової {id}";
                    AppFuncs.WriteLogTraceMessage(logMsg + "...");
                    if (isAnyApproved(note))
                    {
                        string msg = "Видалити службову не можна, так як вона вже затверджена";
                        AppFuncs.WriteLogTraceMessage(msg);
                        MessageBox.Show(msg, "Видалення службової", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        bool dbResult = DBContext.DeleteNotes(id);
                        if (dbResult)
                        {
                            // TODO сохранение удаленной служебки в таблице NotesDeleted
                            base.DeleteObject();
                        }
                        AppFuncs.deleteFromDBResult(logMsg, dbResult);
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть рядок для видалення");
            }
        }

        private bool isAnyApproved(Note note)
        {
            bool retVal = false;
            PropertyInfo[] aPI = typeof(Note).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo item in aPI)
            {
                if (item.Name.StartsWith("Appr") && item.PropertyType.Equals(typeof(Boolean)))
                {
                    if ((bool)item.GetValue(note, null))
                    {
                        retVal = true; break;
                    }
                }
            }
            return retVal;
        }

        public override void LoadDataToGrid()
        {
            AppFuncs.WriteLogTraceMessage($" - {this.GetType().Name}.LoadDataToGrid()");
            //_dataTable = DBContext.GetNotes();  // чтение данных о сл.зап.

            // создать таблицу для отображения данных
            Dictionary<int, string> deps = DBContext.GetDepartmentNamesDict();
            Dictionary<int, string> templates = DBContext.GetNoteTemplateNamesDict();

            _notesDataTable.Clear();
            // и заполнить таблицу
            foreach (Note note in _viewNotes)
            {
                string[] apvs = (note.Approvers??"").Split(';');

                DataRow row = _notesDataTable.NewRow();
                row["Id"] = note.Id;
                row["TemplateName"] = (templates.ContainsKey(note.Templates) ? templates[note.Templates] : null);
                row["DepartmentName"] = (deps.ContainsKey(note.IdDepartment) ? deps[note.IdDepartment] : null);
                row["DepartmentId"] = note.IdDepartment;
                row["Date"] = note.Date;
                row["ApprAvtor"] = (note.ApprAvtor ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText);
                row["ApprDir"] = (apvs.Contains("ApprDir") ? (note.ApprDir ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprComdir"] = (apvs.Contains("ApprComdir") ? (note.ApprComdir ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprSBNach"] = (apvs.Contains("ApprSBNach") ? (note.ApprSBNach ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprSB"] = (apvs.Contains("ApprSB") ? (note.ApprSB ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprKasa"] = (apvs.Contains("ApprKasa") ? (note.ApprKasa ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprNach"] = (apvs.Contains("ApprNach") ? (note.ApprNach ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprFin"] = (apvs.Contains("ApprFin") ? (note.ApprFin ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : "-");
                row["ApprDostavka"] = (apvs.Contains("ApprDostavka") ? (note.ApprDostavka ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprEnerg"] = (apvs.Contains("ApprEnerg") ? (note.ApprEnerg ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprSklad"] = (apvs.Contains("ApprSklad") ? (note.ApprSklad ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprBuh"] = (apvs.Contains("ApprBuh") ? (note.ApprBuh ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprASU"] = (apvs.Contains("ApprASU") ? (note.ApprASU ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText) : AppConst.DGVNotesApprNoneText);
                row["ApprAll"] = (note.ApprAll ? AppConst.DGVNotesApprYesText : AppConst.DGVNotesApprNoText);

                _notesDataTable.Rows.Add(row);
            }

            base.LoadDataToGrid();

            AppFuncs.SetDGVColumnsFromDescr(_dataGrid, AppNotes.dgvColDescr);
        }

        #endregion

        public void Dispose()
        {
            if (_timer != null)
            {
                if (_timer.Enabled) _timer.Stop();
                _timer.Dispose();
            }
        }

    }  // class
}
