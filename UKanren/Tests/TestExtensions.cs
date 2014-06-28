using System;
using MicroKanren;
using NUnit.Framework;

namespace Tests
{
    internal static class TestExtensions
    {
        public static T must_be_instance_of<T>(this T t, Type type)
        {
            Assert.That(t, Is.InstanceOf(type));
            return t;
        }
        public static T must_equal<T>(this T t, T val)
        {
            Assert.That(t, Is.EqualTo(val));
            return t;
        }

        public static T wont_equal<T>(this T t, T val)
        {
            Assert.That(t, Is.Not.EqualTo(val));
            return t;
        }
        public static void must_raise<TException>(this Action a) where TException : System.Exception
        {
            Assert.Throws<TException>(() => a());
        }

        public static void must_be_nil(this object o)
        {
            Assert.That(o,Is.EqualTo(Cons.Nil));
        }

    }
}