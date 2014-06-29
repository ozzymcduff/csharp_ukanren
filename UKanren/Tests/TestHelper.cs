using System;

namespace Tests
{
    public class TestHelper : MicroKanren.Core
    {
        #region TestPrograms

        public Func<object, object> a_and_b
        {
            get
            {
                /*
      a = -> (a) { eq(a, 7) }
      b = -> (b) { disj(eq(b, 5), eq(b, 6)) }

      conj(call_fresh(a), call_fresh(b))
*/
                Func<object, Func<object, object>> a = (_a) => Eq(_a, 7);
                Func<object, Func<object, object>> b = (_b) => Disj(Eq(_b, 5), Eq(_b, 6));
                return Conj(CallFresh(a), CallFresh(b));
            }
        }

        public Func<object, Func<object, object>> fives()
        {
            return (x) => Disj(Eq(x, 5), 
                (a_c) => new Func<object> ( () => fives()(x)(a_c)));
        }

        public Func<object, object, object, Func<object, object>> appendo()
        {
            return (l, s, out_) => 
                Disj(
                    Conj(Eq(Nil, l), Eq(s, out_)),
                    CallFresh((a) => 
                        CallFresh((d) => 
                            Conj(
                                Eq(Cons(a, d), l),
                                CallFresh((res) => 
                                    Conj(
                                        Eq(Cons(a, res), out_),
                                        (s_c) => 
                                            new Func<Object> (() => 
                                                appendo()(d, s, res)(s_c))))))));
        }

        public Func<object, object, object, Func<object, object>> appendo2()
        {
            return (l, s, out_) => 
                Disj(
                    Conj(Eq(Nil, l), Eq(s, out_)),
                    CallFresh((a) => 
                        CallFresh((d) => 
                            Conj(
                                Eq(Cons(a, d), l),
                                CallFresh((res) => 
                                    Conj(
                                        (s_c) => 
                                            new Func<object>( () => 
                                                appendo2()(d, s, res)(s_c)), 
                                        Eq(Cons(a, res), out_)))))));
        }

        public Func<object, object> call_appendo()
        {
            return CallFresh((q) => 
                CallFresh((l) => 
                    CallFresh((s) => 
                        CallFresh((out_) => 
                            Conj(
                                appendo()(l, s, out_),
                                Eq(Cons(l, Cons(s, Cons(out_, Nil))), q))))));
        }

        public Func<object, object> call_appendo2()
        {
            return CallFresh((q) => 
                        CallFresh((l) => 
                            CallFresh((s) => 
                                CallFresh((out_) => 
                                    Conj(
                                        appendo2()(l, s, out_),
                                        Eq(Cons(l, Cons(s, Cons(out_, Nil))), q))))));
        }

        public Func<object, object> ground_appendo()
        {
            // appendo.call(cons(:a, nil), cons(:b, nil), cons(:a, cons(:b, nil)))
            return appendo()(Cons(Sym("a"), Nil), Cons(Sym("b"), Nil), Cons(Sym("a"), Cons(Sym("b"), Nil)));
        }

        public Func<object, object> ground_appendo2()
        {//appendo2.call(cons(:a, nil), cons(:b, nil), cons(:a, cons(:b, nil)))
            return appendo2()(Cons(Sym("a"), Nil), Cons(Sym("b"), Nil), Cons(Sym("a"), Cons(Sym("b"), Nil)));
        }

        public Func<object, Func<object,object>> relo()
        {
            return (x) => 
                CallFresh((x1) => 
                    CallFresh((x2) => 
                        Conj(
                            Eq(x, Cons(x1, x2)),
                            Disj(
                                Eq(x1, x2),
                                (s_c) => new Func<object>(() => relo()(x)(s_c))))));
        }

        public Func<object, object> many_non_ans()
        {
            return CallFresh((x) => 
                Disj(
                    relo()(Cons(5, 6)),
                    Eq(x, 3)));
        }

        #endregion

    }
}
