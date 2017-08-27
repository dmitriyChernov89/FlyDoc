using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc
{
    public static class TypeExtensions
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
    }
}
