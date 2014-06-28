using System;
using System.Collections.Generic;

namespace MicroKanren
{
    public class Ruby
    {
        public Symbol sym(string name)
        {
            return new Symbol(name);
        }

        public object nil { get { return Cons.Nil; } }
        public bool truthy(object s)
        {
            if (nil.Equals(s))
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
    }

    public class Lisp:Ruby
    {
        public Cons cons<T1, T2>(T1 car, T2 cdr)
        {
            return new Cons<T1, T2>(car, cdr);
        }

        //        # Advances a stream until it matures. Per microKanren document 5.2, "From
        //# Streams to Lists."
        public object pull(object stream)
        {
            //  stream.is_a?(Proc) && !cons?(stream) ? pull(stream.call) : stream
            Console.Write("" + stream.GetType()+"\n");
            return stream is Func<Object> && !is_cons(stream) ? pull(((Func<Object>)stream).call()) : stream;
        }
        public object take(int n, object stream)
        {
            object cur;
            if (n > 0)
                if (!nil.Equals(cur = pull(stream)))
                    return cons(car(cur), take(n - 1, cdr(cur)));

            return nil;
        }

        public Func<Object, Object> car(Func<Object, Object> func)
        {
            return (o) =>
            {
                return car(o);
            };
        }

        public bool is_cons(object d)
        {
            return d is Cons && !(d is EmptyCons);
        }

        public bool is_pair(object d)
        {
            return is_cons(d);
        }

        public Cons empty_state { get { return cons(mzero, 0); } }

        public object car(object cons)
        {
            if (cons is Cons)
            {
                return ((Cons)cons).Car;
            }
            throw new Exception("Not cons ["+cons.GetType()+"]->"+cons);
            //public override Cons Car { get { return CarT as Cons ?? New(CarT, null); } }
            //public override Cons Cdr { get { return CdrT as Cons ?? New(CdrT, null); } }

        }
        public object cdr(object cons)
        {
            if (cons is Cons)
            {
                return ((Cons)cons).Cdr;
            }
            throw new Exception("Not cons [" + cons.GetType() + "]->" + cons);
        }

        public Cons mzero { get { return Cons.Nil; } }

        public object assp(Func<Object, bool> func, object alist)
        {
            if (truthy(alist))
            {
                //Console.WriteLine(alist);
                var first_pair = car(alist);
                var first_value = car(first_pair);

                if (func(first_value))
                {
                    return first_pair;
                }
                else
                    return assp(func, cdr(alist));
                /*

           if func.call(first_value)
             first_pair
           else
             assp(func, cdr(alist))
           end
   */
            }
            else
            {
                return false;
            }
        }
    }
}