using System;
using MicroKanren;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_Cons_ctor:TestHelper
    {
        [Test]
        public void It_takes_two_arguments_for_car_and_cdr()
        {
            new Pair<int,int>(1, 2).must_be_instance_of(typeof (Pair));
        }

        [Test]
        public void It_cant_handle_null_due_to_csharp_typesystem()
        {
            Action ctor = () => { new Pair<int, Object>(1, null); }; ctor.must_raise<ArgumentException>();
        }

        [Test]
        public void It_can_handle_null_if_using_cons_factory()
        {
            MicroKanren.Pair.New(1, null).Cdr.must_equal(Nil);
        }
    }
}