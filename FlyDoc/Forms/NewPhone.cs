using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlyDoc.Model;

namespace FlyDoc.Forms
{
    public partial class NewPhone : Form
    {
        private PhoneModel _phone;

        public PhoneModel PhoneModel { get { return _phone; } }

        public NewPhone(PhoneModel phone)
        {
            InitializeComponent();

            _phone = phone;
        }
    }
}
