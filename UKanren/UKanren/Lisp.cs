using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroKanren
{

    public class Lisp
    {
        public Symbol Sym(string name)
        {
            return new Symbol(name);
        }

        public Cons Nil { get { return MicroKanren.Cons.Nil; } }
        public bool Truthy(object s)
        {
            if (Nil.Equals(s))
            {
                return false;
            }
            if (s is Var)
            {
                throw new Exception(s.ToString());
            }
            if (s is Cons)
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

        public int Length(Cons list)
        {
            return list.IsNil() ? 0 : 1 + Length((Cons)Cdr(list));
        }
        public Cons List(params object[] values)
        {
            return ListArray(values);
        }
        public Cons ListArray(object[] values)
        {
            if (values.empty())
            {
                return Nil;
            }
            return Cons(values.First(), ListArray(values.Skip(1).ToArray()));
        }

        public Cons Map(Func<object, object> func, object list)
        {
            if (Truthy(list))
                return Cons(func(Car(list)), Map(func, Cdr(list)));
            return Nil;
        }

        public bool IsProcedure(object elt)
        {
            return elt is Delegate;
        }

        public object Call(object elt)
        {
            if (elt is Delegate)
            {
                return ((Delegate)elt).DynamicInvoke();
            }
            throw new Exception("Not a func!");
        }

        public Cons Cons<T1, T2>(T1 car, T2 cdr)
        {
            return new Cons<T1, T2>(car, cdr);
        }

        /// <summary>
        /// Advances a stream until it matures. Per microKanren document 5.2, "From
        /// Streams to Lists."
        /// </summary>
        public object Pull(object stream)
        {
            //  stream.is_a?(Proc) && !cons?(stream) ? pull(stream.call) : stream
            return IsProcedure(stream) && !IsCons(stream) ? Pull(Call(stream)) : stream;
        }

        public object Take(int n, object stream)
        {
            object cur;
            if (n > 0)
                if (!Nil.Equals(cur = Pull(stream)))
                    return Cons(Car(cur), Take(n - 1, Cdr(cur)));

            return Nil;
        }

        public bool IsCons(object d)
        {
            return d is Cons && !(d is EmptyCons);
        }

        public bool IsPair(object d)
        {
            return IsCons(d);
        }

        public Cons EmptyState { get { return Cons(Mzero, 0); } }

        public object Car(object cons)
        {
            if (cons is Cons)
            {
                return ((Cons)cons).Car;
            }
            throw new Exception("Not cons [" + cons.GetType() + "]->" + cons);
        }
        public object Cdr(object cons)
        {
            if (cons is Cons)
            {
                return ((Cons)cons).Cdr;
            }
            throw new Exception("Not cons [" + cons.GetType() + "]->" + cons);
        }

        public Cons Mzero { get { return MicroKanren.Cons.Nil; } }

        public object Assp(Func<Object, bool> func, object alist)
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