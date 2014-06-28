using System;

namespace MicroKanren
{
    internal class TypeMatch
    {
        public static TRet Case<T1, TRet>(object o, Func<T1, TRet> a1, Func<Object, TRet> _else)
        {
            if (o is T1)
            {
                return a1((T1)o);
            }
            if (_else != null) return _else(o);
            return default(TRet);
        }
        public static TRet Case<T1, T2, TRet>(object o, Func<T1, TRet> a1, Func<T2, TRet> a2, Func<Object, TRet> _else)
        {
            if (o is T1)
            {
                return a1((T1)o);
            }
            if (o is T2)
            {
                return a2((T2)o);
            }
            if (_else != null) return _else(o);
            return default(TRet);
        }
        public static TRet Case<T1, T2, T3, TRet>(object o, Func<T1, TRet> a1, Func<T2, TRet> a2, Func<T3, TRet> a3, Func<Object, TRet> _else)
        {
            if (o is T1)
            {
                return a1((T1)o);
            }
            if (o is T2)
            {
                return a2((T2)o);
            }
            if (o is T3)
            {
                return a3((T3)o);
            }
            if (_else != null) return _else(o);
            return default(TRet);
        }
        public static TRet Case<T1, T2, T3,T4, TRet>(object o, Func<T1, TRet> a1, Func<T2, TRet> a2, Func<T3, TRet> a3,Func<T4, TRet> a4, Func<Object, TRet> _else)
        {
            if (o is T1)
            {
                return a1((T1)o);
            }
            if (o is T2)
            {
                return a2((T2)o);
            }
            if (o is T3)
            {
                return a3((T3)o);
            }
            if (o is T4)
            {
                return a4((T4)o);
            }
            if (_else != null) return _else(o);
            return default(TRet);
        }
    }
}