using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_length : TestHelper
    {
        [Test]
        public void It_returns_0_for_an_empty_list()
        {
            Length(Nil).must_equal(0);
        }
        [Test]
        public void It_returns_the_list_length_for_a_non_empty_list()
        {
            Length(Cons(1, Cons(2, Nil))).must_equal(2);
        }

    }
}