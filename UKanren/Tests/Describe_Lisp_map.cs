﻿using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_map : TestHelper
    {
        [Test]
        public void It_maps_a_function_over_a_list()
        {
            var func = new Func<Object, Object>((str) => { return str.ToString().ToUpper(); });
            map(func, cons("foo", cons("bar", nil))).ToString().must_equal(@"(""FOO"" ""BAR"")");
        }
    }
}