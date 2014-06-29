using System;
using MicroKanren;

namespace Tests
{
    public partial class TestHelper : MicroKanren.Core
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
            /*      -> (x) {
        disj(eq(x, 5), -> (a_c) { -> { fives(x).call(a_c) } })
      }
*/
            return (x) =>
            {
                return Disj(Eq(x, 5), (a_c) =>
                {
                    //-> { fives(x).call(a_c) }
                    Func<object> func = () => fives()(x)(a_c);
                    return func;
                    //fives()(x).call(a_c)
                });
            };
        }

        public Func<object, object, object, Func<object, object>> appendo()
        {
            return (l, s, out_) =>
            {
                //-> (l, s, out) {
                return Disj(Conj(Eq(Nil, l), Eq(s, out_)),
                    CallFresh((a) =>
                    {
                        //call_fresh(-> (a) {
                        return CallFresh((d) =>
                        {
                            //call_fresh(-> (d) {
                            return Conj(
                                Eq(Cons(a, d), l),
                                CallFresh((res) =>
                                {
                                    //          call_fresh(-> (res) {
                                    return Conj(
                                        Eq(Cons(a, res), out_),
                                        (s_c) =>
                                        {
                                            //-> (s_c) {
                                            Func<Object> func = () =>
                                            {
                                                return appendo()(d, s, res)(s_c);
                                            }; //      -> {appendo.call(d, s, res).call(s_c)
                                            return func;
                                        });
                                })); /*      
                                  -> {appendo.call(d, s, res).call(s_c)}})}))})}))}*/
                        });
                    }));
            };
        }

        public Func<object, object, object, Func<object, object>> appendo2()
        {
            return (l, s, out_) =>
            {
                //-> (l, s, out) {
                return Disj(
                    Conj(Eq(Nil, l), Eq(s, out_)),
                    CallFresh((a) =>
                    {
                        return CallFresh((d) =>
                        {
                            return Conj(
                                Eq(Cons(a, d), l),
                                CallFresh((res) =>
                                {
                                    return Conj(
                                        (s_c) =>
                                        {
                                            Func<Object> func = () =>
                                            {
                                                return appendo2()(d, s, res)(s_c);
                                            };
                                            return func;
                                        }, Eq(Cons(a, res), out_));
                                }));
                        });
                    }));
            };
        }

        public Func<object, object> call_appendo()
        {
            return CallFresh((q) =>
            {
                return CallFresh((l) =>
                {
                    return CallFresh((s) =>
                    {
                        return CallFresh((out_) =>
                        {
                            return Conj(
                                appendo()(l, s, out_),
                                Eq(Cons(l, Cons(s, Cons(out_, Nil))), q));
                        });
                    });
                });
            });
        }

        public Func<object, object> call_appendo2()
        {
            return CallFresh((q) =>
            {
                return CallFresh((l) =>
                {
                    return CallFresh((s) =>
                    {
                        return CallFresh((out_) =>
                        {
                            return Conj(
                                appendo2()(l, s, out_),
                                Eq(Cons(l, Cons(s, Cons(out_, Nil))), q));
                        });
                    });
                });
            });
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
            {
                return CallFresh((x1) =>
                {
                    return CallFresh((x2) =>
                    {
                        return Conj(
                            Eq(x, Cons(x1, x2)),
                            Disj(
                                Eq(x1, x2),
                                (s_c) =>
                                {
                                    var func = new Func<object>(() => { return relo()(x)(s_c); });
                                    return func;
                                }));
                    });
                });
            };
        }

        public Func<object, object> many_non_ans()
        {
            return CallFresh((x) =>
            {
                return Disj(
                    relo()(Cons(5, 6)),
                    Eq(x, 3));
            });
        }

        #endregion

    }
}
