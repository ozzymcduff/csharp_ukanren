using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroKanren;
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
            var res = car(call_fresh((q) => { return eq(q, 5); })(empty_state));
            res.ToString().must_equal("((([0] . 5)) . 1)");
        }

        [Test]
        public void It_second_set_t2()
        {
            var res = call_fresh((q) => eq(q, 5))(empty_state);
            cdr(res).must_be_nil();
        }

        [Test]
        public void It_second_set_t3()
        {
            var res = car(a_and_b(empty_state));
            res.ToString().must_equal("((([1] . 5) ([0] . 7)) . 2)");
        }

        [Test]
        public void It_second_set_t3_take()
        {
            var res = take(1, (a_and_b(empty_state)));
            res.ToString().must_equal("(((([1] . 5) ([0] . 7)) . 2))");
        }

        [Test]
        public void It_second_set_t4()
        {
            var res = car(cdr(a_and_b(empty_state)));
            res.ToString().must_equal("((([1] . 6) ([0] . 7)) . 2)");
        }

        [Test]
        public void It_second_set_t5()
        {
            cdr(cdr(a_and_b(empty_state))).must_be_nil();
        }

        [Test]
        public void It_who_cares()
        {
            var res = take(1, call_fresh((q) => { return fives()(q); })(empty_state));
            res.ToString().must_equal("(((([0] . 5)) . 1))");
        }

        [Test]
        public void It_take_2_a_and_b_stream()
        {
            var res = take(2, a_and_b(empty_state));

            var expected_ast_string =
                "(((([1] . 5) ([0] . 7)) . 2) ((([1] . 6) ([0] . 7)) . 2))";

            res.ToString().must_equal(expected_ast_string);
        }

        [Test]
        public void It_ground_appendo()
        {
            //    res = car(ground_appendo.call(empty_state).call)
            var res = car(((Func<Object>)ground_appendo()(empty_state))());

            //# Expected result in scheme:
            //# (((#(2) b) (#(1)) (#(0) . a)) . 3)

            var expected_ast_string =
                "((([2] b) ([1]) ([0] . a)) . 3)";

            res.ToString().must_equal(expected_ast_string);
        }

        [Test]
        public void It_ground_appendo2()
        {
            var res = car(((Func<Object>)ground_appendo2()(empty_state))()).ToString();
            res.must_equal("((([2] b) ([1]) ([0] . a)) . 3)");
        }

        [Test]
        public void It_appendo()
        {
            var res = take(2, call_appendo()(empty_state)).ToString();
            res.must_equal(
                "(((([0] [1] [2] [3]) ([2] . [3]) ([1])) . 4) ((([0] [1] [2] [3]) ([2] . [6]) ([5]) ([3] [4] . [6]) ([1] [4] . [5])) . 7))");
        }

        [Test]
        public void It_appendo2()
        {
            var res = take(2, call_appendo2()(empty_state)).ToString();
            res.must_equal(
                "(((([0] [1] [2] [3]) ([2] . [3]) ([1])) . 4) ((([0] [1] [2] [3]) ([3] [4] . [6]) ([2] . [6]) ([5]) ([1] [4] . [5])) . 7))");
        }

        [Test]
        public void It_reify_1st_across_appendo()
        {
            //# Expected result in scheme:
            //# ((() _.0 _.0) ((_.0) _.1 (_.0 . _.1)))
            var res = map(reify_1st, take(2, call_appendo()(empty_state)));
            res.ToString().must_equal("((nil _.0 _.0) ((_.0) _.1 (_.0 . _.1)))");
        }

        [Test]
        public void It_reify_1st_across_appendo2()
        {
            var res = map(reify_1st, take(2, call_appendo2()(empty_state)));
            res.ToString().must_equal("((nil _.0 _.0) ((_.0) _.1 (_.0 . _.1)))");
        }

        [Test]
        public void It_many_non_ans()
        {
            var res = take(1, many_non_ans()(empty_state));
            res.ToString().must_equal("(((([0] . 3)) . 1))");
        }

    }
}
