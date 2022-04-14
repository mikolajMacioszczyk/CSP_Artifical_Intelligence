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

        public CSP(List<V> variables, Dictionary<V, List<D>> domains, Dictionary<V, List<Constraint<V, D>>> constraints)
        {
            Variables = variables;
            Domains = domains;
            Constraints = constraints;
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

        public (Dictionary<V, D>, int) Backtracking()
        {
            counter = 0;
            var result = Backtracking(new Dictionary<V, D>());
            return (result, counter);
        }

        private Dictionary<V, D> Backtracking(Dictionary<V, D> assignments)
        {
            if (assignments.Count == Variables.Count)
            {
                return assignments;
            }

            var unassigned = Variables.Where(v => !assignments.ContainsKey(v)).ToList();

            var first = unassigned[0];
            foreach (var value in Domains[first])
            {
                counter++;

                assignments.Add(first, value);

                if (Consistent(first, assignments))
                {
                    var result = Backtracking(assignments);
                    if (result != null)
                    {
                        return result;
                    }
                }
                assignments.Remove(first);
            }

            return null;
        }

        public (Dictionary<V, D>, int) ForwardChecking()
        {
            counter = 0;
            var result = ForwardChecking(new Dictionary<V, D>());
            return (result, counter);
        }

        private Dictionary<V, D> ForwardChecking(Dictionary<V, D> assigements)
        {
            if (assigements.Count == Variables.Count)
            {
                return assigements;
            }

            var unassigned = Variables.Where(v => !assigements.ContainsKey(v)).ToList();
            var first = unassigned[0];
            foreach (var value in Domains[first])
            {
                counter++;

                assigements.Add(first, value);

                // TODO: Consider removing this check
                if (Consistent(first, assigements))
                {
                    var domainsArchive = DeepCopyDomains();

                    // propagate
                    foreach (var constraint in Constraints[first])
                    {
                        constraint.Propagate(first, assigements, Domains);
                    }

                    // forward checking
                    var result = ForwardChecking(assigements);
                    if (result != null)
                    {
                        return result;
                    }

                    Domains = domainsArchive;
                }
                assigements.Remove(first);
            }

            return null;
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
