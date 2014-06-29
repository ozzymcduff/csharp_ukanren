using System;
using System.Collections;
using System.Collections.Generic;
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
            return "[" + String.Join(", ", e.Cast<Object>()) + "]";
        }

        public static T2 Call<T1, T2>(this Func<T1, T2> f, T1 arg)
        {
            return f(arg);
        }
        public static T1 Call<T1>(this Func<T1> f)
        {
            return f();
        }

        public static T Tap<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

        public static bool empty<T>(this IEnumerable<T> array)
        {
            return array == null || !array.Any();
        }

        public static Symbol to_sym(this string c)
        {
            return new Symbol(c);
        }
    }
}