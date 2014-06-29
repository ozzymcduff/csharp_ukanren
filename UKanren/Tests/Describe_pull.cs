using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Describe_pull: TestHelper
    {
        [Test]
        public void It_advances_the_stream_until_it_matures()
        {
            Func<object> stream = () =>
            {
                Func<object> func = () => { return (object)42; };
                return func;
            };
            pull(stream).must_equal(42);
        }

        [Test]
        public void It_returns_nil_in_the_case_of_the_empty_stream()
        {
            pull(nil).must_be_nil();
        }
    }
}