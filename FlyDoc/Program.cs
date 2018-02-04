using FlyDoc.Lib;
using FlyDoc.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
        static void Main(string[] args)
        {
            AppArgsHelper.LoadAppArgs(args);
#if DEBUG
            if (AppArgsHelper.GetAppArgs.Count > 0)
            {
                System.Diagnostics.Debug.Print("** Application arguments:");
                int i = 0;
                foreach (KeyValuePair<string, string> kvp in AppArgsHelper.GetAppArgs)
                {
                    System.Diagnostics.Debug.Print("  {0}. {1} = '{2}'", ++i, kvp.Key, kvp.Value);
                }
            }
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FlyDoc.Forms.MainForm mainForm = new FlyDoc.Forms.MainForm();

            Application.Run(mainForm);
        }
    }
}
