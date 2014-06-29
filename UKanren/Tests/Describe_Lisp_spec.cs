using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    ///   # http://download.plt-scheme.org/doc/html/reference/pairs.html#(def._((quote._~23~25kernel)._pair~3f))
    /// </summary>
    [TestFixture]
    public class Describe_Lisp_pair : TestHelper
    {
        [Test]
        public void It_is_false_for_an_integer()
        {
            is_pair(1).must_equal(false);
        }
        [Test]
        public void It_is_true_for_a_list_with_an_int_in_the_car_and_cdr()
        {
            is_pair(cons(1, 2)).must_equal(true);
        }
        [Test]
        public void It_is_true_for_a_proper_list()
        {
            is_pair(cons(1, cons(2, nil))).must_equal(true);
        }
        [Test]
        public void It_is_false_for_an_empty_list()
        {
            is_pair(nil).must_equal(false);
        }
    }
}
