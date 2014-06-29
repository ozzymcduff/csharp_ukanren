using System;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// These tests follow the reference implementation in Scheme located at
    /// https://github.com/jasonhemann/microKanren/blob/master/microKanren-test.scm
    /// </summary>
    [TestFixture]
    public class Describe_Core : TestHelper
    {
        [Test]
        public void It_second_set_t1()
        {
            var res = Car(CallFresh((q) => Eq(q, 5))(EmptyState));
            res.ToString().must_equal("((([0] . 5)) . 1)");
        }

        [Test]
        public void It_second_set_t2()
        {
            var res = CallFresh((q) => Eq(q, 5))(EmptyState);
            Cdr(res).must_be_nil();
        }

        [Test]
        public void It_second_set_t3()
        {
            var res = Car(a_and_b(EmptyState));
            res.ToString().must_equal("((([1] . 5) ([0] . 7)) . 2)");
        }

        [Test]
        public void It_second_set_t3_take()
        {
            var res = Take(1, (a_and_b(EmptyState)));
            res.ToString().must_equal("(((([1] . 5) ([0] . 7)) . 2))");
        }

        [Test]
        public void It_second_set_t4()
        {
            var res = Car(Cdr(a_and_b(EmptyState)));
            res.ToString().must_equal("((([1] . 6) ([0] . 7)) . 2)");
        }

        [Test]
        public void It_second_set_t5()
        {
            Cdr(Cdr(a_and_b(EmptyState))).must_be_nil();
        }

        [Test]
        public void It_who_cares()
        {
            var res = Take(1, CallFresh((q) => fives()(q))(EmptyState));
            res.ToString().must_equal("(((([0] . 5)) . 1))");
        }

        [Test]
        public void It_take_2_a_and_b_stream()
        {
            var res = Take(2, a_and_b(EmptyState));

            var expected_ast_string =
                "(((([1] . 5) ([0] . 7)) . 2) ((([1] . 6) ([0] . 7)) . 2))";

            res.ToString().must_equal(expected_ast_string);
        }

        [Test]
        public void It_ground_appendo()
        {
            //    res = car(ground_appendo.call(empty_state).call)
            var res = Car(Call(ground_appendo()(EmptyState)));

            //# Expected result in scheme:
            //# (((#(2) b) (#(1)) (#(0) . a)) . 3)

            var expected_ast_string =
                "((([2] b) ([1]) ([0] . a)) . 3)";

            res.ToString().must_equal(expected_ast_string);
        }

        [Test]
        public void It_ground_appendo2()
        {
            var res = Car(Call(ground_appendo2()(EmptyState))).ToString();
            res.must_equal("((([2] b) ([1]) ([0] . a)) . 3)");
        }

        [Test]
        public void It_appendo()
        {
            var res = Take(2, call_appendo()(EmptyState)).ToString();
            res.must_equal(
                "(((([0] [1] [2] [3]) ([2] . [3]) ([1])) . 4) ((([0] [1] [2] [3]) ([2] . [6]) ([5]) ([3] [4] . [6]) ([1] [4] . [5])) . 7))");
        }

        [Test]
        public void It_appendo2()
        {
            var res = Take(2, call_appendo2()(EmptyState)).ToString();
            res.must_equal(
                "(((([0] [1] [2] [3]) ([2] . [3]) ([1])) . 4) ((([0] [1] [2] [3]) ([3] [4] . [6]) ([2] . [6]) ([5]) ([1] [4] . [5])) . 7))");
        }

        [Test]
        public void It_reify_1st_across_appendo()
        {
            //# Expected result in scheme:
            //# ((() _.0 _.0) ((_.0) _.1 (_.0 . _.1)))
            var res = Map(Reify1st, Take(2, call_appendo()(EmptyState)));
            res.ToString().must_equal("((() _.0 _.0) ((_.0) _.1 (_.0 . _.1)))");
        }

        [Test]
        public void It_reify_1st_across_appendo2()
        {
            var res = Map(Reify1st, Take(2, call_appendo2()(EmptyState)));
            res.ToString().must_equal("((() _.0 _.0) ((_.0) _.1 (_.0 . _.1)))");
        }

        [Test]
        public void It_many_non_ans()
        {
            var res = Take(1, many_non_ans()(EmptyState));
            res.ToString().must_equal("(((([0] . 3)) . 1))");
        }

    }
}
