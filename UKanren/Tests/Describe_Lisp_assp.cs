using NUnit.Framework;
using MicroKanren;
namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_assp : TestHelper
    {
        [Test]
        public void It_returns_the_first_pair_for_which_the_predicate_function_is_true()
        {
            var al1 = cons(3, cons(sym("a"), nil));
            var al2 = cons(1, cons(sym("b"), nil));
            var al3 = cons(4, cons(sym("c"), nil));

            var alist = cons(al1, cons(al2, cons(al3, nil)));

            var res = assp((i) => { return i.is_even(); }, alist);
            res.must_equal (cons(4, cons(sym("c"), nil)));
        }
        [Test]
        public void It_returns_false_if_there_is_no_matching_element_found()
        {
            var pair1= cons(3, cons(sym("a"), nil));
            var pair2= cons(1, cons(sym("b"), nil));
            var pair3= cons(4, cons(sym("c"), nil));

            var alist = cons(pair1, cons(pair2, cons(pair3, nil)));

            var res = assp((i) => { return i.Equals(5); }, alist);
            res.must_equal(false);
        }
    }
}