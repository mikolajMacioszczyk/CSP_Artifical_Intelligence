namespace Lista2.Heuristics.Values
{
    public class NoHeuristic<T> : IValueHeuristic<T>
    {
        public IEnumerable<T> OrderValues(IEnumerable<T> values)
        {
            return values;
        }

        public void Remove(T value)
        {
        }

        public void Use(T value)
        {
        }
    }
}
