using System;
using System.Linq;

namespace MicroKanren
{

    public class Lisp
    {
        public static Symbol Sym(string name)
        {
            return new Symbol(name);
        }

        public static Pair Nil { get { return Pair.Nil; } }
        public static bool Truthy(object s)
        {
            if (Nil.Equals(s))
            {
                return false;
            }
            if (s is Var)
            {
                throw new Exception(s.ToString());
            }
            if (s is Pair)
            {
                return true;//throw new Exception(s.ToString());
            }
            if (s is Int32)
            {
                return ((Int32)s) >= 0;
            }
            if (s is bool)
            {
                return (bool)s;
            }
            return false;
        }

        public static int Length(Pair list)
        {
            return list.IsNil() ? 0 : 1 + Length((Pair)Cdr(list));
        }
        public Pair List(params object[] values)
        {
            return ListArray(values);
        }
        public Pair ListArray(object[] values)
        {
            if (values.empty())
            {
                return Nil;
            }
            return Cons(values.First(), ListArray(values.Skip(1).ToArray()));
        }

        public static Pair Map(Func<object, object> func, object list)
        {
            if (Truthy(list))
                return Cons(func(Car(list)), Map(func, Cdr(list)));
            return Nil;
        }

        public static bool IsProcedure(object elt)
        {
            return elt is Delegate;
        }

        public static object Call(object elt)
        {
            if (elt is Delegate)
            {
                return ((Delegate)elt).DynamicInvoke();
            }
            throw new Exception(string.Format("Expected <Delegate>, but was given: {0}", elt.GetType()));
        }

        public static Pair Cons<T1, T2>(T1 car, T2 cdr)
        {
            return new Pair<T1, T2>(car, cdr);
        }

        /// <summary>
        /// Advances a stream until it matures. Per microKanren document 5.2, "From
        /// Streams to Lists."
        /// </summary>
        public static object Pull(object stream)
        {
            //  stream.is_a?(Proc) && !cons?(stream) ? pull(stream.call) : stream
            return IsProcedure(stream) && !IsCons(stream) ? Pull(Call(stream)) : stream;
        }

        public static object Take(int n, object stream)
        {
            object cur;
            if (n > 0)
                if (!Nil.Equals(cur = Pull(stream)))
                    return Cons(Car(cur), Take(n - 1, Cdr(cur)));

            return Nil;
        }

        public static bool IsCons(object d)
        {
            return d is Pair && !(d is EmptyPair);
        }

        public static bool IsPair(object d)
        {
            return IsCons(d);
        }

        public static Pair EmptyState { get { return Cons(Mzero, 0); } }

        public static object Car(object cons)
        {
            if (cons is Pair)
            {
                return ((Pair)cons).Car;
            }
            throw new Exception(string.Format("expects argument of type <Pair>, but was given {0}", cons.GetType()) );
        }
        public static object Cdr(object cons)
        {
            if (cons is Pair)
            {
                return ((Pair)cons).Cdr;
            }
            throw new Exception(string.Format("expects argument of type <Pair>, but was given {0}", cons.GetType()));
        }

        public static Pair Mzero { get { return Pair.Nil; } }

        public static object Assp(Func<Object, bool> func, object alist)
        {
            if (Truthy(alist))
            {
                var first_pair = Car(alist);
                var first_value = Car(first_pair);

                if (func(first_value))// if func.call(first_value)
                {
                    return first_pair;
                }
                else
                    return Assp(func, Cdr(alist));// assp(func, cdr(alist))
            }
            else
            {
                return false;
            }
        }
    }
}