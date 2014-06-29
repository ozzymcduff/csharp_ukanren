using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_list : TestHelper
    {
        [Test]
        public void It_should_return_a_proper_list_containing_the_given_values()
        {
            List(Sym("a"), Sym("b"), Sym("c")).must_equal(Cons(Sym("a"), Cons(Sym("b"), Cons(Sym("c"), Nil))));
        }
    }
}