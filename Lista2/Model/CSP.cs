namespace Lista2.Model
{
    public abstract class CSP<V, D>
    {
        public List<V> Variables { get; set; }

        public Dictionary<V, List<D>> Domains { get; set; }
        public Dictionary<V, List<Constraint<V, D>>> Constraints { get; set; }

        protected CSP(
            List<V> variables, 
            Dictionary<V, List<D>> domains)
        {
            Variables = variables;
            Domains = domains;
            Constraints = new ();
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

        public Dictionary<V, D> Backtracking(Dictionary<V, D> assignments)
        {
            if (assignments.Count == Variables.Count)
            {
                return assignments;
            }

            // TODO: Choose in better way
            var unassigned = Variables.Where(v => !assignments.ContainsKey(v)).ToList();

            var first = unassigned[0];
            foreach (var value in Domains[first])
            {
                // TODO: Do not copy
                var local_assignment = new Dictionary<V, D>(assignments);
                assignments.Add(first, value);

                if (Consistent(first, local_assignment))
                {
                    var result = Backtracking(local_assignment);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }
}
