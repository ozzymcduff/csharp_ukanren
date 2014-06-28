using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroKanren
{
    public class Var
    {
        private readonly object[] _array; 
        public Var(IEnumerable<object> o)
        {
            _array = new List<object>(o).ToArray();
        }

        public object this[int i]
        {
            get { return _array[i]; }
        }
        public static Var operator +(Var c1, object c2)
        {
            return new Var(c1._array.ToList().Tap(a=>a.Add(c2)));
        }

        public override string ToString()
        {
            return _array.Inspect();
        }

        public bool Equals(Var obj)
        {
            if (null == obj)
            {
                return false;
            }
            if (!_array.Length.Equals(obj._array.Length))
            {
                return false;
            }
            for (int i = 0; i < obj._array.Length; i++)
            {
                if (!_array[i].Equals(obj._array[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Var);
        }
    }
}