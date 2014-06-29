namespace MicroKanren
{
    public class EmptyPair : Pair
    {
        public override string ToString(bool consInCdr)
        {
            return "()";
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
}