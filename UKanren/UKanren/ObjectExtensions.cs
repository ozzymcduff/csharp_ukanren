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

        public static T2 call<T1, T2>(this Func<T1, T2> f, T1 arg)
        {
            return f(arg);
        }
        public static T1 call<T1>(this Func<T1> f)
        {
            return f();
        }

        public static T Tap<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

    }
}