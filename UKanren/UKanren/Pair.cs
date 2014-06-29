using System;
using System.Collections;
using System.Text;

namespace MicroKanren
{
    public abstract class Pair
    {
        public static EmptyPair Nil
        {
            get { return new EmptyPair(); }
        }

        public abstract String ToString(bool consInCdr);

        public static Pair New(object car, object cdr)
        {
            return new Pair<object, object>(car ?? Nil, cdr ?? Nil);
        }
        public virtual object Car
        {
            get { throw new Exception(); }
        }

        public virtual object Cdr
        {
            get { throw new Exception(); }
        }

        public override bool Equals(object obj)
        {
            return obj is Pair && Equals(obj as Pair);
        }

        public virtual bool Equals(Pair other)
        {
            if (other is EmptyPair && this is EmptyPair)
                return true;
            if (other is EmptyPair && !(this is EmptyPair))
                return false;
            if (!(other is EmptyPair) && this is EmptyPair)
                return false; 
            return Equals(Car, other.Car) && Equals(Cdr, other.Cdr);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Car.GetHashCode() * 397) ^ Cdr.GetHashCode();
            }
        }

        public bool IsNil()
        {
            return Equals(this, Nil);
        }
    }

    public class EmptyPair : Pair
    {
        public override string ToString(bool consInCdr)
        {
            return "nil";
        }

        public override string ToString()
        {
            return ToString(false);
        }

        public override bool Equals(object obj)
        {
            return obj is EmptyPair;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class Pair<T1, T2> : Pair
    {
        public override Object Car { get { return CarT ; } }
        public override Object Cdr { get { return CdrT ; } }

        public T1 CarT { get; private set; }
        public T2 CdrT { get; private set; }

        /// <summary>
        /// Returns a Cons cell (read: instance) that is also marked as such for
        /// later identification.
        /// </summary>
        /// <param name="car"></param>
        /// <param name="cdr"></param>
        public Pair(T1 car, T2 cdr)
        {
            if (Equals(car, null))
            {
                throw new ArgumentException("car");
            }
            if (Equals(cdr, null))
            {
                throw new ArgumentException("cdr");
            }
            CarT = car; CdrT = cdr;
        }

        /// <summary>
        /// Converts Lisp AST to a String. Algorithm is a recursive implementation of
        /// http://www.mat.uc.pt/~pedro/cientificos/funcional/lisp/gcl_22.html#SEC1238.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return ToString(false);
        }

        public override String ToString(bool consInCdr)
        {
            var str = new StringBuilder(consInCdr ? "" : "(");
            str.Append((this.Car is Pair) ? this.Car.ToString() : AtomString(this.Car));

            str.Append(TypeMatch.Case(this.Cdr,
                (EmptyPair e) => string.Empty,
                (Pair c) => " " + c.ToString(true),
                o => " . " + AtomString(o)));

            return consInCdr ? str.ToString() : str.Append(")").ToString();
        }

        //private

        private static string AtomString(object car)
        {
            return TypeMatch.Case(car,
                (EmptyPair e) => string.Empty,
                (String s) => s.Inspect(),
                (char c) => c.Inspect(),
                (IEnumerable e) => e.Inspect(),
                o => o.ToString()
                );
        }
    }
}
