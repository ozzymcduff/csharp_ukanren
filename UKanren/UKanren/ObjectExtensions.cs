using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace MicroKanren
{
    internal static class ObjectExtensions
    {
        public static string Inspect(this string s)
        {
            return "\"" + s.Replace("\"", @"\\""") + "\"";
        }

        public static string Inspect(this char s)
        {
            return s.ToString(CultureInfo.InvariantCulture).Inspect();
        }

        public static string Inspect(this IEnumerable e)
        {
            return "["+String.Join(", ", e.Cast<Object>())+"]";
        }
    }
}