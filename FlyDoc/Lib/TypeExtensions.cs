using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FlyDoc.Lib
{

    public static class ObjectExtensions
    {
        public static int ToInt(this object source)
        {
            if ((source == null) || (source == System.DBNull.Value)) return -1;
            else return source.ToString().ToInt();
        }

        public static string ToStringNull(this object source)
        {
            if ((source == null) || (source == System.DBNull.Value)) return "";
            else return source.ToString();
        }

        public static bool ToBool(this object source)
        {
            if ((source == null) || (source == System.DBNull.Value)) return false;
            else return source.ToString().ToBool();
        }

        public static DateTime ToDateTime(this object source)
        {
            if ((source == null) || (source == System.DBNull.Value)) return DateTime.MinValue;
            else return Convert.ToDateTime(source);
        }

    }

    public static class DateTimeExtensions
    {
        public static string ToSQLExpr(this DateTime source)
        {
            return string.Format("CONVERT(datetime, '{0}', 20)", source.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public static class StringExtensions
    {
        public static bool IsNull(this string source)
        {
            return (string.IsNullOrEmpty(source) || source.Equals(DBNull.Value));
        }

        public static bool IsNumber(this string source)
        {
            return source.All(c => char.IsDigit(c));
        }

        // convert string to bool
        public static bool ToBool(this string source)
        {
            bool retValue = false;
            if (string.IsNullOrEmpty(source)) return retValue;

            string sLower = source.ToLower();

            if (sLower.Equals("true") || sLower.Equals("да") || sLower.Equals("yes") || sLower.Equals("истина"))
                retValue = true;
            else
            {
                int iBuf = 0;
                if (int.TryParse(source, out iBuf) == true) retValue = (iBuf != 0);
            }

            return retValue;
        }  // method

        public static double ToDouble(this string sParam)
        {
            double retVal = 0;
            double.TryParse(sParam, out retVal);
            if (retVal == 0)
            {
                if (sParam.Contains(",")) sParam = sParam.Replace(',', '.');
                double.TryParse(sParam, NumberStyles.Float, CultureInfo.InvariantCulture, out retVal);
            }
            return retVal;
        }

        public static int ToInt(this string source)
        {
            if (source == null) return 0;

            List<string> chars = new List<string>();
            foreach (char c in source)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.DecimalDigitNumber) chars.Add(c.ToString());
            }
            if (chars.Count == 0) return 0;
            else
            {
                string numStr = string.Join("", chars.ToArray());
                return int.Parse(numStr);
            }
        }

    } // class

    public static class IntExtensions
    {
        public static int SetBit(this int bitMask, int bit)
        {
            return (bitMask |= (1 << bit));
        }
        public static int ClearBit(this int bitMask, int bit)
        {
            return (bitMask &= ~(1 << bit));
        }
        public static bool IsSetBit(this int bitMask, int bit)
        {
            int val = (1 << bit);
            return (bitMask & val) == val;
        }

    }


}
