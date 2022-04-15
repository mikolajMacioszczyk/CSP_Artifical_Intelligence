namespace Lista2.Heuristics.Values
{
    public interface IValueHeuristic<T>
    {
        IEnumerable<T> OrderValues(IEnumerable<T> values);
        void Use(T value);
        void Remove(T value);
    }
}
