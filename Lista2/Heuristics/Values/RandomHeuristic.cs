namespace Lista2.Heuristics.Values
{
    public class RandomHeuristic<T> : IValueHeuristic<T>
    {
        private readonly Random _random = new Random();

        public IEnumerable<T> OrderValues(IEnumerable<T> values)
        {
            return values.OrderBy(x => _random.Next());
        }

        public void Remove(T value)
        {
        }

        public void Use(T value)
        {
        }
    }
}
