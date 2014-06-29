using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_cons : TestHelper
    {
        [Test]
        public void It_returns_a_pair()
        {
            car(cons(1, 2)).must_equal(1);
            cdr(cons(1, 2)).must_equal(2);
        }
    }
}