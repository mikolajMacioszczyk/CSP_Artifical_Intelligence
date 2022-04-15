using Lista2.Model;

namespace Lista2.Heuristics.Variables
{
    public interface IVariableHeuristic<V, D>
    {
        IEnumerable<V> OrderVariables(
            IEnumerable<V> variables, 
            Dictionary<V, List<D>> domains, 
            Dictionary<V, List<Constraint<V, D>>> constraints);
    }
}
