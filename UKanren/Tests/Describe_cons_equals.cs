using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_cons_equals:TestHelper
    {
        [Test]
        public void It_is_true_if_the_lists_are_equal()
        {
            cons(1, cons(2, nil)).must_equal(cons(1, cons(2, nil)));
        }
        [Test]
        public void It_is_false_if_the_lists_contain_different_objects()
        {
            cons(1, cons(2, nil)).wont_equal(cons(1, nil));
        }
    }
}