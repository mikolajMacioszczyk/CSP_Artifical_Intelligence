using Lista2.Interface;

namespace Lista2.Heuristics.Values
{
    public class QueueHeuristic<T> : IValueHeuristic<T>
    {
        private List<T> _queue = new List<T>();

        public IEnumerable<T> OrderValues(IEnumerable<T> values)
        {
            return values.OrderBy(x => _queue.IndexOf(x));
        }

        public void Remove(T value)
        {
            _queue.Remove(value);
        }

        public void Use(T value)
        {
            _queue.Remove(value);
            _queue.Insert(0, value);
        }
    }
}
