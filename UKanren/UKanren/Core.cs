using System;
using System.Collections.Generic;

namespace MicroKanren
{
    public class Core : Lisp
    {
        public static Pair Unit(Pair s_c)
        {
            return Cons(s_c, Mzero);
        }
        public static Var Var(object c)
        {
            return new Var(new[] { c });
        }
        public static bool IsVar(object x)
        {
            return x is Var;
        }
        public static bool VarsEq(Var x1, Var x2)
        {
            return x1.Equals(x2);
        }

        /// <summary>
        /// Walk environment S and look up value of U, if present.
        /// </summary>
        public static object Walk(object u, object s)
        {
            if (IsVar(u))
            {
                var pr = Assp((v) => u.Equals(v), s);
                return Truthy(pr) ? Walk(Cdr(pr), s) : u;

            }
            else
                return u;
        }

        public static Pair ExtS(object x, object v, object s)
        {
            return Cons(Cons(x, v), s);
        }

        public static object Unify(object u, object v, object s)
        {
            u = Walk(u, s);
            v = Walk(v, s);
            if (IsVar(u) && IsVar(v) && VarsEq((Var)u, (Var)v))
            {
                return s;
            }
            else if (IsVar(u))
            {
                return ExtS(u, v, s);
            }
            else if (IsVar(v))
            {
                return ExtS(v, u, s);
            }
            else if (IsPair(u) && IsPair(v))
            {
                var s2 = Unify(Car(u), Car(v), s);
                if (Truthy(s2))
                    return Unify(Cdr(u), Cdr(v), s2);
                return Nil;
            }
            else
            {
                //# Object identity (equal?) seems closest to eqv? in Scheme.
                if (Equals(u, v))
                    return s;
                return Nil;
            }
        }

        public static Func<object, object> Disj(Func<object, object> g1, Func<object, object> g2)
        {
            return (s_c) => Mplus(g1.Call(s_c), g2.Call(s_c));
        }
        public static Func<object, object> Conj(Func<object, object> g1, Func<object, object> g2)
        {
            return (s_c) => Bind(g1.Call(s_c), g2);
        }

        public static object Bind(object d, Func<object, object> g)
        {
            if (Nil.Equals(d))
            {
                return Mzero;
            }
            else if (IsProcedure(d))
            {
                return new Func<object>(() => Bind(Call(d), g));
            }
            else
            {
                return Mplus(g.Call(Car(d)), Bind(Cdr(d), g));
            }
        }
        private static object Mplus(object d1, object d2)
        {
            if (Nil.Equals(d1))
            {
                return d2;
            }
            else if (IsProcedure(d1))
            {
                return new Func<object>(() => Mplus(d2, Call(d1)));
            }
            else
                return Cons(Car(d1), Mplus(Cdr(d1), d2));
        }


        /// <summary>
        /// Constrain u to be equal to v
        /// == in Scheme implementation, ≡ in uKanren papers.
        /// </summary>
        public static Func<object, object> Eq(Object u, Object v)
        {
            return (s_c) =>
            {
                var s = Unify(u, v, Car(s_c));
                return Truthy(s) ? Unit(Cons(s, Cdr(s_c))) : Mzero;
            };
        }

        /// <summary>
        /// Call function f with a fresh variable.
        /// </summary>
        public static Func<object, object> CallFresh(Func<Var, Func<Pair, object>> f)
        {
            return (s_c) =>
            {
                var c = (Int32)Cdr(s_c);
                return f.Call(Var(c)).Call(Cons(Car(s_c), c + 1));
            };
        }

        public static Symbol ReifyName(object n)
        {
            return (string.Format("_.{0}",n)).to_sym();
        }

        public static Pair ReifyS(object v, Pair s)
        {
            v = Walk(v, s);
            if (IsVar(v))
            {
                var n = ReifyName(Length(s)); //n = reify_name(length(s))
                return Cons(Cons(v, n), s); //cons(cons(v, n), s)
            }
            else if (IsPair(v))
            {
                return ReifyS(Cdr(v), ReifyS(Car(v), s));//reify_s(cdr(v), reify_s(car(v), s))
            }
            else
            {
                return s;
            }

        }

        public static Object Reify1st(object s_c)
        {
            var v = WalkStar(Var(0), Car(s_c)); //v = walk_star((var 0), car(s_c))
            return WalkStar(v, ReifyS(v, Nil)); //walk_star(v, reify_s(v, nil))
        }

        public static Object WalkStar(object v, object s)
        {
            v = Walk(v, s);
            if (IsVar(v))
            {
                return v;
            }
            //else
            if (IsPair(v))
            {
                return Cons(WalkStar(Car(v), s),
                    WalkStar(Cdr(v), s));
            }
            //else
            return v;
        }


    }
}