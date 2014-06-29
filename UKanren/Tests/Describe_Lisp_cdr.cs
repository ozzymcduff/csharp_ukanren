using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_cdr : TestHelper
    {
        [Test]
        public void It_returns_the_second_item_in_the_pair()
        {
            cdr(cons(1, 2)).must_equal(2);
        }
    }
}