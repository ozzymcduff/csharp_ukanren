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


        public Func<object, object> ground_appendo()
        {
            // appendo.call(cons(:a, nil), cons(:b, nil), cons(:a, cons(:b, nil)))
            return appendo()(cons(sym("a"), nil), cons(sym("b"), nil), cons(sym("a"), cons(sym("b"), nil)));
        }
        /*module MicroKanren
module TestPrograms
def a_and_b
  a = -> (a) { eq(a, 7) }
  b = -> (b) { disj(eq(b, 5), eq(b, 6)) }

  conj(call_fresh(a), call_fresh(b))
end

def fives
  -> (x) {
    disj(eq(x, 5), -> (a_c) { -> { fives(x).call(a_c) } })
  }
end

def appendo
  -> (l, s, out) {
    disj(
      conj(eq(nil, l), eq(s, out)),
      call_fresh(-> (a) {
        call_fresh(-> (d) {
          conj(
            eq(cons(a, d), l),
            call_fresh(-> (res) {
              conj(
                eq(cons(a, res), out),
                -> (s_c) {
                  -> {appendo.call(d, s, res).call(s_c)}})}))})}))}
end

def appendo2
  -> (l, s, out) {
    disj(
      conj(eq(nil, l), eq(s, out)),
      call_fresh(-> (a) {
        call_fresh(-> (d) {
          conj(
            eq(cons(a, d), l),
            call_fresh(-> (res) {
              conj(
                -> (s_c) {
                  -> { appendo2.call(d, s, res).call(s_c) }
                },
                eq(cons(a, res), out))}))})}))}
end

def call_appendo
  call_fresh(-> (q) {
    call_fresh(-> (l) {
      call_fresh(-> (s) {
        call_fresh(-> (out) {
          conj(
            appendo.call(l, s, out),
            eq(cons(l, cons(s, cons(out, nil))), q))})})})})
end

def call_appendo2
  call_fresh(-> (q) {
    call_fresh(-> (l) {
      call_fresh(-> (s) {
        call_fresh(-> (out) {
          conj(
            appendo2.call(l, s, out),
            eq(cons(l, cons(s, cons(out, nil))), q))})})})})
end

def ground_appendo
  appendo.call(cons(:a, nil), cons(:b, nil), cons(:a, cons(:b, nil)))
end

def ground_appendo2
  appendo2.call(cons(:a, nil), cons(:b, nil), cons(:a, cons(:b, nil)))
end

def relo
  -> (x) {
    call_fresh(-> (x1) {
      call_fresh(-> (x2) {
        conj(
          eq(x, cons(x1, x2)),
          disj(
            eq(x1, x2),
            -> (s_c) {
              -> { relo.call(x).call(s_c) }
            }
          )
        )
      })
    })
  }
end

def many_non_ans
  call_fresh(-> (x) {
    disj(
      relo.call(cons(5, 6)),
      eq(x, 3))})
end
end
end
*/

        #endregion

    }
}
