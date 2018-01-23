using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Lib
{
    public static class AppArgsHelper
    {
        private static Dictionary<string, string> _appArgs = new Dictionary<string, string>();

        public static Dictionary<string, string> GetAppArgs { get { return _appArgs;  } }

        public static string GetAppArgValue(string appArgName)
        {
            if (_appArgs.ContainsKey(appArgName))
                return _appArgs[appArgName];
            else
                return null;
        }

        public static void LoadAppArgs(string[] args)
        {
            string key = null;
            foreach (string item in args)
            {
                if ((item.Length > 1) && item.StartsWith("-"))
                {
                    key = item.Substring(1);
                    _appArgs.Add(key, "");
                }
                else if (!key.IsNull())
                {
                    if (_appArgs[key] == null) _appArgs[key] = "";
                    if (_appArgs[key].Length > 0) _appArgs[key] += " ";
                    _appArgs[key] += item;
                }
            }
        }

    }  // class
}
