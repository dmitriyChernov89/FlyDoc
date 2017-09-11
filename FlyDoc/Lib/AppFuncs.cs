using FlyDoc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Lib
{
    public static class AppFuncs
    {
        public static void SetDGVColumnsFromDescr(DataGridView dgv, Dictionary<string, DGVColDescr> colDescr)
        {
            if ((dgv == null) || (colDescr == null)) return;
            DGVColDescr curDescr;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (colDescr.ContainsKey(col.Name))
                {
                    curDescr = colDescr[col.Name];
                    col.HeaderText = curDescr.Header;
                    col.FillWeight = curDescr.FillWeight;
                    col.DefaultCellStyle.Alignment = curDescr.Alignment;
                    col.HeaderCell.Style.Alignment = curDescr.Alignment;
                    if ((col is DataGridViewCheckBoxColumn) && curDescr.ThreeStates)
                        ((DataGridViewCheckBoxColumn)col).ThreeState = true;
                }
            }
        }

    }  // class
}
