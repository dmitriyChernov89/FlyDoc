using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.ViewModel
{
    public class DGVColDescr
    {
        // column name
        public string Name { get; set; }

        // column header
        public string Header { get; set; }

        public DataGridViewContentAlignment Alignment { get; set; }
        public bool ReadOnly { get; set; }
        public bool Visible { get; set; }
        public int FillWeight { get; set; }
        
        // for CheckBox
        public bool ThreeStates { get; set; }

        public DGVColDescr()
        {
            ReadOnly = true;
            Visible = true;
            ThreeStates = false;
        }
    }
}
