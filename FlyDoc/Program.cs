using FlyDoc.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FlyDoc
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FlyDoc.Forms.MainForm mainForm = new FlyDoc.Forms.MainForm();

            Application.Run(mainForm);
        }
    }
}
