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
        // глобальные переменные приложения
        public static string MachineName { get; set; }
        public static string UserName { get; set; }
        private static User _user;
        public static User User { get { return _user; } }


        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // загрузить аргументы приложения
            AppArgsHelper.LoadAppArgs(args);
            string argValue = AppArgsHelper.GetAppArgValue("machine");
            MachineName = (argValue.IsNull() ? System.Environment.MachineName : argValue);
            argValue = AppArgsHelper.GetAppArgValue("user");
            UserName = (argValue.IsNull() ? System.Environment.UserName : argValue);
            _user = new User(MachineName, UserName);
            // если не найдено в табл.Access, то доступ только к телефонному справочнику
            if (_user.Id == 0)
            {
                _user.Phone = true;
            }


            // в режиме отладки вывести аргументы приложения
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
