using Lista2.Heuristics.Values;
using Lista2.Heuristics.Variables;
using Lista2.Model;

namespace Lista2.Managers
{
    public class CSP<V, D>
    {
        public List<V> Variables { get; set; }

        public Dictionary<V, List<D>> Domains { get; set; }
        public Dictionary<V, List<Constraint<V, D>>> Constraints { get; set; }

        private int counter = 0;

        public CSP(
            List<V> variables,
            Dictionary<V, List<D>> domains)
        {
            Variables = variables;
            Domains = domains;
            Constraints = new();
            foreach (var variable in Variables)
            {
                Constraints.Add(variable, new List<Constraint<V, D>>());
            }
        }

        public void AddConstraint(Constraint<V, D> constraint)
        {
            foreach (var variable in constraint.Variables)
            {
                Constraints[variable].Add(constraint);
            }
        }

        public bool Consistent(V variable, Dictionary<V, D> assignment)
        {
            foreach (var constraint in Constraints[variable])
            {
                if (!constraint.IsStisfied(assignment))
                {
                    return false;
                }
            }
            return true;
        }

        public (List<Dictionary<V, D>>, int) Backtracking(IValueHeuristic<D> valueHeuristic, 
            IVariableHeuristic<V, D> variableHeuristic, int maxSolutions)
        {
            counter = 0;
            var solutions = new List<Dictionary<V, D>>();
            Backtracking(new Dictionary<V, D>(), solutions, maxSolutions, valueHeuristic, variableHeuristic);
            return (solutions, counter);
        }

        private void Backtracking(Dictionary<V, D> assignments, List<Dictionary<V, D>> solutions, 
            int maxSolutions, IValueHeuristic<D> valueHeuristic, IVariableHeuristic<V, D> variableHeuristic)
        {
            if (assignments.Count == Variables.Count)
            {
                solutions.Add(new Dictionary<V, D>(assignments));
                return;
            }

            var unassigned = Variables.Where(v => !assignments.ContainsKey(v)).ToList();
            unassigned = variableHeuristic.OrderVariables(unassigned, Domains, Constraints).ToList();

            var first = unassigned[0];
            foreach (var value in valueHeuristic.OrderValues(Domains[first]))
            {
                counter++;

                assignments.Add(first, value);
                valueHeuristic.Use(value);

                if (Consistent(first, assignments))
                {
                    Backtracking(assignments, solutions, maxSolutions, valueHeuristic, variableHeuristic);
                    if (solutions.Count >= maxSolutions)
                    {
                        return;
                    }
                }
                assignments.Remove(first);
                valueHeuristic.Remove(value);
            }
        }

        public (List<Dictionary<V, D>>, int) ForwardChecking(IValueHeuristic<D> valueHeuristic, IVariableHeuristic<V, D> variableHeuristic,
            int maxSolutions, bool notNeedConsistencyCheck = true)
        {
            counter = 0;
            var solutions = new List<Dictionary<V, D>>();
            ForwardChecking(new Dictionary<V, D>(), solutions, maxSolutions, valueHeuristic, variableHeuristic, notNeedConsistencyCheck);
            return (solutions, counter);
        }

        private void ForwardChecking(Dictionary<V, D> assigements, List<Dictionary<V, D>> solutions, 
            int maxSolutions, IValueHeuristic<D> valueHeuristic, IVariableHeuristic<V, D> variableHeuristic, bool notNeedConsistencyCheck)
        {
            if (assigements.Count == Variables.Count)
            {
                solutions.Add(new Dictionary<V, D>(assigements));
                return;
            }

            var unassigned = Variables.Where(v => !assigements.ContainsKey(v)).ToList();
            unassigned = variableHeuristic.OrderVariables(unassigned, Domains, Constraints).ToList();

            var first = unassigned[0];
            foreach (var value in valueHeuristic.OrderValues(Domains[first]))
            {
                counter++;

                assigements.Add(first, value);
                valueHeuristic.Use(value);

                var domainsArchive = DeepCopyDomains();

                // propagate
                foreach (var constraint in Constraints[first])
                {
                    constraint.Propagate(first, assigements, Domains);
                }

                // forward checking
                if (notNeedConsistencyCheck || Consistent(first, assigements))
                {
                    ForwardChecking(assigements, solutions, maxSolutions, valueHeuristic, variableHeuristic, notNeedConsistencyCheck);
                    if (solutions.Count >= maxSolutions)
                    {
                        return;
                    }
                }
                
                Domains = domainsArchive;

                assigements.Remove(first);
                valueHeuristic.Remove(value);
            }

            return;
        }

        private Dictionary<V, List<D>> DeepCopyDomains()
        {
            var copy = new Dictionary<V, List<D>>(Domains.Count);
            foreach (var domain in Domains)
            {
                copy.Add(domain.Key, new List<D>(domain.Value));
            }
            return copy;
        }
    }
}
