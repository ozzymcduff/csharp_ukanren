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
                Func<object, Func<object, object>> a = (_a) => eq(_a, 7);
                Func<object, Func<object, object>> b = (_b) => disj(eq(_b, 5), eq(_b, 6));
                return conj(call_fresh(a), call_fresh(b));
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
                return disj(eq(x, 5), (a_c) =>
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
                return disj(conj(eq(nil, l), eq(s, out_)),
                    call_fresh((a) =>
                    {
                        //call_fresh(-> (a) {
                        return call_fresh((d) =>
                        {
                            //call_fresh(-> (d) {
                            return conj(
                                eq(cons(a, d), l),
                                call_fresh((res) =>
                                {
                                    //          call_fresh(-> (res) {
                                    return conj(
                                        eq(cons(a, res), out_),
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
                return disj(
                    conj(eq(nil, l), eq(s, out_)),
                    call_fresh((a) =>
                    {
                        return call_fresh((d) =>
                        {
                            return conj(
                                eq(cons(a, d), l),
                                call_fresh((res) =>
                                {
                                    return conj(
                                        (s_c) =>
                                        {
                                            Func<Object> func = () =>
                                            {
                                                return appendo2()(d, s, res)(s_c);
                                            };
                                            return func;
                                        }, eq(cons(a, res), out_));
                                }));
                        });
                    }));
            };
        }

        public Func<object, object> call_appendo()
        {
            return call_fresh((q) =>
            {
                return call_fresh((l) =>
                {
                    return call_fresh((s) =>
                    {
                        return call_fresh((out_) =>
                        {
                            return conj(
                                appendo()(l, s, out_),
                                eq(cons(l, cons(s, cons(out_, nil))), q));
                        });
                    });
                });
            });
        }

        public Func<object, object> call_appendo2()
        {
            return call_fresh((q) =>
            {
                return call_fresh((l) =>
                {
                    return call_fresh((s) =>
                    {
                        return call_fresh((out_) =>
                        {
                            return conj(
                                appendo2()(l, s, out_),
                                eq(cons(l, cons(s, cons(out_, nil))), q));
                        });
                    });
                });
            });
        }

        public Func<object, object> ground_appendo()
        {
            // appendo.call(cons(:a, nil), cons(:b, nil), cons(:a, cons(:b, nil)))
            return appendo()(cons(sym("a"), nil), cons(sym("b"), nil), cons(sym("a"), cons(sym("b"), nil)));
        }

        public Func<object, object> ground_appendo2()
        {//appendo2.call(cons(:a, nil), cons(:b, nil), cons(:a, cons(:b, nil)))
            return appendo2()(cons(sym("a"), nil), cons(sym("b"), nil), cons(sym("a"), cons(sym("b"), nil)));
        }

        public Func<object, Func<object,object>> relo()
        {
            return (x) =>
            {
                return call_fresh((x1) =>
                {
                    return call_fresh((x2) =>
                    {
                        return conj(
                            eq(x, cons(x1, x2)),
                            disj(
                                eq(x1, x2),
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
            return call_fresh((x) =>
            {
                return disj(
                    relo()(cons(5, 6)),
                    eq(x, 3));
            });
        }

        #endregion

    }
}
