using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlyDoc.Forms
{
    public partial class CoordSeqEditForm : Form
    {
        public List<Coordinator> Coordinators;

        public CoordSeqEditForm()
        {
            InitializeComponent();

            fillCoordsList();
        }

        private void fillCoordsList()
        {

        }

    }  // class
}
