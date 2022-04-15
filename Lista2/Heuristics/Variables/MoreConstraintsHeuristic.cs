using Lista2.Model;

namespace Lista2.Heuristics.Variables
{
    public class MoreConstraintsHeuristic<V, D> : IVariableHeuristic<V, D>
    {
        public IEnumerable<V> OrderVariables(IEnumerable<V> variables, Dictionary<V, List<D>> domains, Dictionary<V, List<Constraint<V, D>>> constraints)
        {
            return variables.OrderByDescending(v => constraints[v].Count);
        }
    }
}
