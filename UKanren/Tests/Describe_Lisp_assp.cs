using NUnit.Framework;
using MicroKanren;
namespace Tests
{
    [TestFixture]
    public class Describe_Lisp_assp : TestHelper
    {
        static bool is_even(object i)
        {
            return ((int)i) % 2 == 0;
        }

        [Test]
        public void It_returns_the_first_pair_for_which_the_predicate_function_is_true()
        {
            var al1 = Cons(3, Cons(Sym("a"), Nil));
            var al2 = Cons(1, Cons(Sym("b"), Nil));
            var al3 = Cons(4, Cons(Sym("c"), Nil));

            var alist = Cons(al1, Cons(al2, Cons(al3, Nil)));

            var res = Assp(is_even, alist);
            res.must_equal (Cons(4, Cons(Sym("c"), Nil)));
        }
        [Test]
        public void It_returns_false_if_there_is_no_matching_element_found()
        {
            var pair1= Cons(3, Cons(Sym("a"), Nil));
            var pair2= Cons(1, Cons(Sym("b"), Nil));
            var pair3= Cons(4, Cons(Sym("c"), Nil));

            var alist = Cons(pair1, Cons(pair2, Cons(pair3, Nil)));

            var res = Assp((i) => i.Equals(5), alist);
            res.must_equal(false);
        }
    }
}