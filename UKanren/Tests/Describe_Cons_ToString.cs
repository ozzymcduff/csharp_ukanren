using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Cons_ToString : TestHelper
    {
        [Test]
        public void It_prints_an_expression_correctly()
        {
            var c = Cons(1, Cons(2, Cons(Cons(3, Cons(4, Nil)), Cons(5, Nil))));
            c.ToString().must_equal("(1 2 (3 4) 5)");
            var d = Cons(Cons(2, 3), 1);
            d.ToString().must_equal("((2 . 3) . 1)");
            var e = Cons(1, Nil);
            e.ToString().must_equal("(1)");
            //# Is this an illogical list to be testing?
            var f = Cons(Nil, 1);
            f.ToString().must_equal("(() . 1)");
        }

        [Test]
        public void It_prints_a_cons_cell_representation_of_a_simple_cell()
        {
            Cons('a', 'b').ToString().must_equal("(\"a\" . \"b\")");
        }
        
        [Test]
        public void It_represents_Integers_and_Floats()
        {
            Cons(1, 2).ToString().must_equal("(1 . 2)");
        }

        [Test]
        public void It_prints_a_nested_expression()
        {
            Cons('a', Cons('b', 'c')).ToString().must_equal(@"(""a"" ""b"" . ""c"")");
        }

        [Test]
        public void It_represents_Arrays_correctly_in_printed_form()
        {
            Cons('a', new object[0]).ToString().must_equal(@"(""a"" . [])");
        }

        [Test]
        public void It_represents_nil_elements()
        {
            Cons('a', Nil).ToString().must_equal(@"(""a"")");
        }

    }
}