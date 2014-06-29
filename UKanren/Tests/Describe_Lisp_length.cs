using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_length : TestHelper
    {
        [Test]
        public void It_returns_0_for_an_empty_list()
        {
            length(nil).must_equal(0);
        }
        [Test]
        public void It_returns_the_list_length_for_a_non_empty_list()
        {
            length(cons(1, cons(2, nil))).must_equal(2);
        }

    }
}