using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_cons : TestHelper
    {
        [Test]
        public void It_returns_a_pair()
        {
            Car(Cons(1, 2)).must_equal(1);
            Cdr(Cons(1, 2)).must_equal(2);
        }
    }
}