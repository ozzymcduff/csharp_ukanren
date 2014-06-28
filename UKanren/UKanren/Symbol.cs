using System.Globalization;

namespace MicroKanren
{
    public class Symbol
    {
        public override int GetHashCode()
        {
            return (_name != null ? _name.GetHashCode() : 0);
        }

        private readonly string _name;

        public Symbol(string name)
        {
            _name = name;
        }

        public bool Equals(Symbol obj)
        {
            if (obj == null)
                return false;
            return _name.Equals(obj._name);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Symbol);
        }

        public override string ToString()
        {
            return _name.ToString(CultureInfo.InvariantCulture);
        }
    }
}