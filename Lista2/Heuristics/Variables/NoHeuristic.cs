using Lista2.Model;

namespace Lista2.Heuristics.Variables
{
    public class NoHeuristic<V, D> : IVariableHeuristic<V, D>
    {
        public IEnumerable<V> OrderVariables(
            IEnumerable<V> variables, 
            Dictionary<V, List<D>> domains, 
            Dictionary<V, List<Constraint<V, D>>> constraints)
        {
            return variables;
        }
    }
}
