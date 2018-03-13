using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyDoc.Model;
using FlyDoc.Forms;
using FlyDoc.Lib;
using System.Windows.Forms;

namespace FlyDoc.Presenter
{
    public class AppPhone : AppModelBase
    {
        #region static members
        private static Dictionary<string, DGVColDescr> dgvColDescr;
        static AppPhone()
        {
            // key - имя поля из DataSource
            /*SELECT        dbo.Phonebook.Id, dbo.Phonebook.Department AS DepartmentId, dbo.Department.Name AS Відділ, dbo.Phonebook.Positions AS Посада, dbo.Phonebook.FIO AS [П.І.Б.], dbo.Phonebook.Dect AS Трубка, 
             dbo.Phonebook.Phone AS Телефон, dbo.Phonebook.Mobile AS Мобільний, dbo.Phonebook.Mail AS Пошта
FROM            dbo.Phonebook INNER JOIN
             dbo.Department ON dbo.Phonebook.Department = dbo.Department.Id*/
            dgvColDescr = new Dictionary<string, DGVColDescr>()
            {
                { "Id", new DGVColDescr() { Header="Id", Visible = false } },
                { "DepartmentId", new DGVColDescr() { Visible = false} },
                { "Відділ", new DGVColDescr() { Header = "Відділ", FillWeight=20} },
                { "Посада", new DGVColDescr() { Header = "Посада", FillWeight=30} },
                { "П.І.Б.", new DGVColDescr() { Header = "П.І.Б.", FillWeight=20} },
                { "Трубка", new DGVColDescr() { Header = "Трубка", FillWeight=10} },
                { "Телефон", new DGVColDescr() { Header = "Телефон", FillWeight=10} },
                { "Мобільний", new DGVColDescr() { Header = "Мобільний", FillWeight=15} },
                { "Пошта", new DGVColDescr() { Header = "Пошта", FillWeight=15} }
            };
        }
        #endregion


        public AppPhone()
        {
        }
        public override void EditObject()
        {
            if (base.AllowEdit == false) { base.notAllowEditAction(); return; }

            DataGridViewRow dgvRow = base.getSelectedDataRow();
            if (dgvRow != null)
            {
                int editId = (int)dgvRow.Cells["Id"].Value;
                PhoneModel phone = new PhoneModel(editId);

                NewPhone frm = new NewPhone(phone);
                DialogResult result = frm.ShowDialog();
                AppFuncs.dialogCloseResult(frm.GetType().Name, result);
                if (result == DialogResult.OK)
                {
                    bool dbResult = DBContext.UpdateEntity(phone);
                    if (dbResult)
                    {
                        this.LoadDataToGrid();
                        base.selectGridRowById(editId);
                    }
                    AppFuncs.saveToDBResult(dbResult);
                }
                base.EditObject();
            }
        }//Edit

        public override void CreateNewObject()
        {
            if (base.AllowEdit == false) { base.notAllowEditAction(); return; }

            NewPhone frm = new NewPhone(null);
            DialogResult result = frm.ShowDialog();
            AppFuncs.dialogCloseResult(frm.GetType().Name, result);
            if ((result == DialogResult.OK) && (frm.PhoneModel != null))
            {
                bool dbResult = DBContext.InsertEntity(frm.PhoneModel);
                if (dbResult)
                {
                    this.LoadDataToGrid();
                    base.selectGridRowById(frm.PhoneModel.Id);
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
                DialogResult result = MessageBox.Show("Ви впевнені що хочете видалити поточний телефон?", "Видалення телефона", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    string logMsg = $"Видалення з телефоного довiдника, id {id}";
                    AppFuncs.WriteLogTraceMessage(logMsg + "...");

                    bool dbResult = DBContext.DeleteEntityById(PhoneModel._dbTableName, id);
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

            _dataTable = DBContext.GetPhones();  // чтение данных о сл.зап.

            base.LoadDataToGrid();

            AppFuncs.SetDGVColumnsFromDescr(_dataGrid, AppPhone.dgvColDescr);
        }

    }
}
