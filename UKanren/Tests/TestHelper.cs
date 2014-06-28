using MicroKanren;

namespace Tests
{
    public class TestHelper
    {
        public Cons cons<T1, T2>(T1 car, T2 cdr)
        {
            return new Cons<T1, T2>(car, cdr);
        }
        public object nil { get { return Cons.Nil; } }
    }
}
