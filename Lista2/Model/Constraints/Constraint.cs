namespace Lista2.Model
{
    public abstract class Constraint<V, D>
    {
        public List<V> Variables { get; set; }

        protected Constraint(List<V> variables)
        {
            Variables = variables;
        }

        abstract public bool IsStisfied(Dictionary<V, D> assignement);

        abstract public void Propagate(V variable, Dictionary<V, D> assigement, Dictionary<V, List<D>> domains);
    }
}
