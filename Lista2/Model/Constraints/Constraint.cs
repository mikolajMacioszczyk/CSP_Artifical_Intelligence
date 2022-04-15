namespace Lista2.Model
{
    public abstract class Constraint<V, D>
    {
        private static int Counter = 1;
        public int Index { get; private set; } = Counter++;
        public List<V> Variables { get; set; }

        public Stack<(bool, List<(V, List<D>)>)> changes = new Stack<(bool, List<(V, List<D>)>)>();

        protected Constraint(List<V> variables)
        {
            Variables = variables;
        }

        abstract public bool IsStisfied(Dictionary<V, D> assignement);

        abstract public void Propagate(V variable, Dictionary<V, D> assigement, Dictionary<V, List<D>> domains);

        public void RestoreDomain(Dictionary<V, List<D>> domains)
        {
            var lastChange = changes.Pop();
            if (lastChange.Item1)
            {
                foreach (var variableDomain in lastChange.Item2)
                {
                    domains[variableDomain.Item1] = variableDomain.Item2;
                }
            }
        }
    }
}
