using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_list : TestHelper
    {
        [Test]
        public void It_should_return_a_proper_list_containing_the_given_values()
        {
            list(sym("a"), sym("b"), sym("c")).must_equal(cons(sym("a"), cons(sym("b"), cons(sym("c"), nil))));
        }
    }
}