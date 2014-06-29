using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_cons_equals:TestHelper
    {
        [Test]
        public void It_is_true_if_the_lists_are_equal()
        {
            Cons(1, Cons(2, Nil)).must_equal(Cons(1, Cons(2, Nil)));
        }
        [Test]
        public void It_is_false_if_the_lists_contain_different_objects()
        {
            Cons(1, Cons(2, Nil)).wont_equal(Cons(1, Nil));
        }
    }
}