using System;
using System.Collections;
using System.Text;

namespace MicroKanren
{
    public abstract class Cons
    {
        public static EmptyCons Nil
        {
            get { return new EmptyCons(); }
        }

        public abstract String ToString(bool consInCdr);

        public static Cons New(object car, object cdr)
        {
            return new Cons<object, object>(car ?? Nil, cdr ?? Nil);
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
            return obj is Cons && Equals(obj as Cons);
        }

        public virtual bool Equals(Cons other)
        {
            if (other is EmptyCons && this is EmptyCons)
                return true;
            return Equals(Car, other.Car) && Equals(Cdr, other.Cdr);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Car.GetHashCode() * 397) ^ Cdr.GetHashCode();
            }
        }

    }

    public class EmptyCons : Cons
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
            return obj is EmptyCons;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class Cons<T1, T2> : Cons
    {
        public override Object Car { get { return CarT; } }
        public override Object Cdr { get { return CdrT; } }
        public T1 CarT { get; private set; }
        public T2 CdrT { get; private set; }

        /// <summary>
        /// Returns a Cons cell (read: instance) that is also marked as such for
        /// later identification.
        /// </summary>
        /// <param name="car"></param>
        /// <param name="cdr"></param>
        public Cons(T1 car, T2 cdr)
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
            str.Append((this.Car is Cons) ? this.Car.ToString() : AtomString(this.Car));

            str.Append(TypeMatch.Case(this.Cdr,
                (EmptyCons e) => string.Empty,
                (Cons c) => " " + c.ToString(true),
                o => " . " + AtomString(o)));

            return consInCdr ? str.ToString() : str.Append(")").ToString();
        }

        //private

        private static string AtomString(object car)
        {
            return TypeMatch.Case(car,
                (EmptyCons e) => string.Empty,
                (String s) => s.Inspect(),
                (char c) => c.Inspect(),
                (IEnumerable e) => e.Inspect(),
                o => o.ToString()
                );
        }
    }
}
