using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Cons_ToString : TestHelper
    {
        [Test]
        public void It_prints_an_expression_correctly()
        {
            var c = cons(1, cons(2, cons(cons(3, cons(4, nil)), cons(5, nil))));
            c.ToString().must_equal("(1 2 (3 4) 5)");
            var d = cons(cons(2, 3), 1);
            d.ToString().must_equal("((2 . 3) . 1)");
            var e = cons(1, nil);
            e.ToString().must_equal("(1)");
            //# Is this an illogical list to be testing?
            var f = cons(nil, 1);
            f.ToString().must_equal("(nil . 1)");
        }

        [Test]
        public void It_prints_a_cons_cell_representation_of_a_simple_cell()
        {
            cons('a', 'b').ToString().must_equal("(\"a\" . \"b\")");
        }
        
        [Test]
        public void It_represents_Integers_and_Floats()
        {
            cons(1, 2).ToString().must_equal("(1 . 2)");
        }

        [Test]
        public void It_prints_a_nested_expression()
        {
            cons('a', cons('b', 'c')).ToString().must_equal(@"(""a"" ""b"" . ""c"")");
        }

        [Test]
        public void It_represents_Arrays_correctly_in_printed_form()
        {
            cons('a', new object[0]).ToString().must_equal(@"(""a"" . [])");
        }

        [Test]
        public void It_represents_nil_elements()
        {
            cons('a', nil).ToString().must_equal(@"(""a"")");
        }

    }
}