using System;
using System.Collections.Generic;

namespace MicroKanren
{
    public class Core : Lisp
    {
        public Cons unit(Cons s_c)
        {
            return cons(s_c, mzero);
        }
        public Var var(object c)
        {
            return new Var(new[] { c });
        }
        public bool is_var(object x)
        {
            return x is Var;
        }
        public bool vars_eq(Var x1, Var x2)
        {
            return x1.Equals(x2);
        }

        /// <summary>
        /// Walk environment S and look up value of U, if present.
        /// </summary>
        public object walk(object u, object s)
        {
            if (is_var(u))
            {
                var pr = assp((v) => u.Equals(v), s);
                return truthy(pr) ? walk(cdr(pr), s) : u;

            }
            else
                return u;
        }

        public Cons ext_s(object x, object v, object s)
        {
            return cons(cons(x, v), s);
        }

        public object unify(object u, object v, object s)
        {
            u = walk(u, s);
            v = walk(v, s);
            if (is_var(u) && is_var(v) && vars_eq((Var)u, (Var)v))
            {
                return s;
            }
            else if (is_var(u))
            {
                return ext_s(u, v, s);
            }
            else if (is_var(v))
            {
                return ext_s(v, u, s);
            }
            else if (is_pair(u) && is_pair(v))
            {
                var s2 = unify(car(u), car(v), s);
                if (truthy(s2) )
                    return unify(cdr(u), cdr(v), s2);
                return nil;
            }
            else
            {
                //# Object identity (equal?) seems closest to eqv? in Scheme.
                if (Equals(u, v) )
                    return s;
                return nil;
            }
        }

        public Func<object, object> disj(Func<object, object> g1, Func<object, object> g2)
        {
            return (s_c) => { return mplus(g1.call(s_c), g2.call(s_c)); };
        }
        public Func<object, object> conj(Func<object, object> g1, Func<object, object> g2)
        {
            return (s_c) => { return bind(g1.call(s_c), g2); };
        }

        public object bind(object d, Func<object, object> g)
        {
            if (nil.Equals(d))
            {
                return mzero;
            }
            else if (d is Func<object>)
            {
                var v = new Func<object>(() => bind(((Func<object>)d).call(), g));
                return v;
            }
            else
            {
                return mplus(g.call(car(d)), bind(cdr(d), g));
            }
        }
        private object mplus(object d1, object d2)
        {
            if (nil.Equals(d1))
            {
                return d2;
            }
            else if (d1 is Func<object>)
            {
                //elsif procedure?(d1)
                //-> { mplus(d2, d1.call) }
                Func<object> a = () =>
                {
                    return mplus(d2, ((Func<object>)d1).call());
                };
                return a;
            }
            else
                return cons(car(d1), mplus(cdr(d1), d2));
        }


        /// <summary>
        /// Constrain u to be equal to v
        /// == in Scheme implementation, ≡ in uKanren papers.
        /// </summary>
        public Func<object, object> eq(Object u, Object v)
        {

            /* 
    def eq(u, v)
      ->(s_c) {
        s = unify(u, v, car(s_c))
        s ? unit(cons(s, cdr(s_c))) : mzero
      }
*/
            return (s_c) =>
            {
                var s = unify(u, v, car(s_c));
                return truthy(s) ? unit(cons(s, cdr(s_c))) : mzero;
            };
        }
        /// <summary>
        /// Call function f with a fresh variable.
        /// </summary>
        public Func<object, object> call_fresh(Func<object, Func<object, object>> f)
        {
            return (s_c) =>
            {
                var c = (Int32)cdr(s_c);
                var x = (c + 1);
                return f.call(var(c)).call(cons(car(s_c), x));
            };
        }

    }
}