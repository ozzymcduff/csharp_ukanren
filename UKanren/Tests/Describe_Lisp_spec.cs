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
        public void It_1()
        {
    //it "is false for an integer" do
    //  pair?(1).must_equal false
    //end
            Assert.Fail();
        }
        [Test]
        public void It_2()
        {
    //it "is true for a list with an int in the car and cdr" do
    //  pair?(cons(1, 2)).must_equal true
    //end
            Assert.Fail();
        }
        [Test]
        public void It_3()
        {

    //it "is true for a proper list" do
    //  pair?(cons(1, cons(2, nil))).must_equal true
    //end
            Assert.Fail();
        }
        [Test]
        public void It_()
        {

    //it "is false for an empty list" do
    //  pair?(nil).must_equal false
    //end
            Assert.Fail();
        }
    }
}
