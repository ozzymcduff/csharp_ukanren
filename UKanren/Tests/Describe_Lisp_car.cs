using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_car : TestHelper
    {
        [Test]
        public void It_returns_the_first_element_in_the_pair()
        {
            car(cons(1, 2)).must_equal(1);
        }
    }
}