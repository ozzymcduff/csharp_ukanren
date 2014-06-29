using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_MiniKanren_wrappers_pull : TestHelper
    {
        [Test]
        public void It_advances_the_stream_until_it_matures()
        {
            var stream = new Func<Func<int>>(() => () => 42);
            Pull(stream).must_equal(42);
        }
        [Test]
        public void It_returns_nil_in_the_case_of_the_empty_stream()
        {
            Pull(Nil).must_be_nil();
        }

    }
}
