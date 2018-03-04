using FlyDoc.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Lib
{
    public static class AppFuncs
    {
        public static Logger AppLogger = null;

        public static bool LogEnable;

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
                    if (curDescr.FillWeight <= 0) col.Visible = false;

                    if (col.Visible) col.FillWeight = curDescr.FillWeight;

                    if (curDescr.CellStyle != null)
                    {
                        col.DefaultCellStyle = curDescr.CellStyle;
                    }
                    else
                    {
                        col.DefaultCellStyle.Alignment = curDescr.Alignment;
                    }

                    col.HeaderCell.Style.Alignment = (curDescr.HeaderAlignment == DataGridViewContentAlignment.NotSet)
                        ? curDescr.Alignment
                        : curDescr.HeaderAlignment;


                    if ((col is DataGridViewCheckBoxColumn) && curDescr.ThreeStates)
                        ((DataGridViewCheckBoxColumn)col).ThreeState = true;
                }
                else
                    col.Visible = false;
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

        #region application info
        internal static string GetVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        internal static object GetFullName()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }
        #endregion

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

        #region app logger
        internal static string LoggerInit()
        {
            string retVal = null;
            // логгер приложения
            try
            {
                AppLogger = LogManager.GetLogger("fileLogger");
            }
            catch (Exception ex)
            {
                retVal = ex.Message;
            }
            return retVal;
        }

        // отладочные сообщения
        public static void WriteLogTraceMessage(string msg)
        {
            if (AppLogger != null) AppLogger.Trace(msg ?? "null");
        }
        public static void WriteLogTraceMessage(string format, params object[] args)
        {
            if (AppLogger != null) AppLogger.Trace(format, args);
        }

        public static void WriteLogInfoMessage(string msg)
        {
            if (AppLogger != null) AppLogger.Info(msg ?? "null");
        }
        public static void WriteLogInfoMessage(string format, params object[] args)
        {
            if (AppLogger != null) AppLogger.Info(format, args);
        }

        public static void WriteLogErrorMessage(string msg)
        {
            if (AppLogger != null) AppLogger.Error(msg ?? "null");
        }
        public static void WriteLogErrorMessage(string format, params object[] args)
        {
            if (AppLogger != null) AppLogger.Error(format, args);
        }

        #endregion

        #region общие записи в лог
        internal static void openEditForm(string formName, bool isNew)
        {
            string modeName = (isNew) ? "NEW" : "EDIT";
            AppFuncs.WriteLogTraceMessage($" - open edit form '{formName}', mode {modeName}");
        }
        internal static void closeEditForm(string formName, CloseReason reason)
        {
            AppFuncs.WriteLogTraceMessage($" - close edit form '{formName}': {reason.ToString()}");
        }
        internal static void dialogCloseResult(string formName, DialogResult result)
        {
            AppFuncs.WriteLogTraceMessage($" - dialog result '{formName}': {result.ToString()}");
        }

        internal static void saveToDBResult(bool dbResult)
        {
            AppFuncs.WriteLogTraceMessage("   - save data to DB: " + (dbResult? "SUCCESS" : "NOT success"));
        }
        internal static void deleteFromDBResult(string msg, bool dbResult)
        {
            AppFuncs.WriteLogTraceMessage($"   - {msg}: {(dbResult ? "SUCCESS" : "NOT success")}");
        }
        #endregion


    }  // class
}
