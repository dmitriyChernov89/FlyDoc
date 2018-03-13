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
        private static string _logsDirectory = @"C:\FlyDoc\Logs";

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // проверка существования папки C:\FlyDoc\Logs и создать логгер приложения
            if (checkLogDir())
            {
                string msg = null;
                msg = AppFuncs.LoggerInit();
            }

            AppFuncs.WriteLogInfoMessage("**** НАЧАЛО работы FlyDoc ****");
            AppFuncs.WriteLogInfoMessage($"{AppFuncs.GetFullName()}, ver. {AppFuncs.GetVersion()}");

            // загрузить аргументы приложения и получить имя компьютера и пользователя
            AppArgsHelper.LoadAppArgs(args);
            string argValue = AppArgsHelper.GetAppArgValue("machine");
            MachineName = (argValue.IsNull() ? System.Environment.MachineName : argValue);
            argValue = AppArgsHelper.GetAppArgValue("user");
            UserName = (argValue.IsNull() ? System.Environment.UserName : argValue);
            AppFuncs.WriteLogInfoMessage($"Авторизация: компьютер '{MachineName}', юзер '{UserName}'");

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
            if ((MachineName.ToUpper() == "KC-500-07") || (MachineName.ToUpper() == "LENOVO-Z710"))
            {
                // -machine KC-500-07 -user d.chernov
                //MachineName = "KC-106-31"; UserName = "kasir-kc";
                MachineName = "KC-500-07"; UserName = "d.chernov";
                //MachineName = "KC-127-01"; UserName = "nachsb";
                //MachineName = "KC-114-06"; UserName = "kc-asistdir";
                //MachineName = "KC-114-06"; UserName = "kc-komdir";
                _user = new User(MachineName, UserName);
            }
#endif

            // делегаты для DBContext-а
            DBContext.BeforeDBCallAction = new Action<string>(dbBeforeCallAction);
            DBContext.DBErrorAction = new Action<string>(dbErrorAction);

            // открытие главного окна приложения
            FlyDoc.Forms.MainForm mainForm = new FlyDoc.Forms.MainForm();
            Application.Run(mainForm);

            AppFuncs.WriteLogInfoMessage("**** ОКОНЧАНИЕ работы FlyDoc ****" + Environment.NewLine);
        }

        // проверка существования папки _logsDirectory
        private static bool checkLogDir()
        {
            bool retVal = true;
            if (System.IO.Directory.Exists(_logsDirectory) == false)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(_logsDirectory);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка создания папки для хранения лог-файлов. Обратитесь к системному администратору.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    retVal = false;
                }
            }
            return retVal;
        }

        private static void appExit(int exitCode, string errMsg)
        {
            if ((exitCode != 0) && (string.IsNullOrEmpty(errMsg) == false))
            {
                MessageBox.Show(errMsg, "Аварийное завершение программы", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            Environment.Exit(exitCode);
        }


        private static void dbBeforeCallAction(string sqlText)
        {
            if (AppFuncs.LogEnable)
            {
                AppFuncs.WriteLogTraceMessage("DBContext call: " + sqlText);
            }
        }

        private static void dbErrorAction(string errMsg)
        {
            AppFuncs.WriteLogErrorMessage(errMsg);

            MessageBox.Show(errMsg, "Ошибка доступа к данным", MessageBoxButtons.OK, MessageBoxIcon.Error);

            FlyDoc.Forms.MainForm.SendMail(@"asu@kc.epicentrk.com", "Error!", "Упс, помилка!\nНа комп'ютері: " + System.Environment.MachineName + " З користувачем: " + System.Environment.UserName + " сталася наступна помилка:\n\n" + errMsg);
        }

    }  // class
}
