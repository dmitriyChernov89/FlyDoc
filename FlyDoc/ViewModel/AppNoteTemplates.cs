using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.ViewModel
{
    public class AppNoteTemplates : AppModelBase
    {
        #region fields
        #endregion

        #region properties
        #endregion

        public AppNoteTemplates()
        {
        }

        #region override methods
        public override void LoadDataToGrid()
        {
            _dataTable = DBContext.GetNoteTemplates();  // чтение данных о шаблонах сл.зап.
            base.LoadDataToGrid();
        }
        #endregion

        #region Public methods
        #endregion

    }  // class
}
