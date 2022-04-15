namespace Lista2.Model
{
    public class DistinctValuesConstraint : Constraint<Field, int>
    {
        public DistinctValuesConstraint(List<Field> variables) : base(variables)
        {
        }

        public override bool IsStisfied(Dictionary<Field, int> assignement)
        {
            var variableValues = new List<int>();
            foreach (var variable in Variables)
            {
                if (assignement.ContainsKey(variable))
                {
                    var value = assignement[variable];
                    if (variableValues.Contains(value))
                    {
                        return false;
                    }
                    variableValues.Add(value);
                }
            }
            return true;
        }

        public override void Propagate(Field variable, Dictionary<Field, int> assigement, Dictionary<Field, List<int>> domains)
        {
            int value = assigement[variable];
            if (Variables.Contains(variable))
            {
                var newChanges = new List<(Field, List<int>)>();
                foreach (var currentVariable in Variables.Where(v => !v.Equals(variable)))
                {
                    newChanges.Add((currentVariable, domains[currentVariable]));
                    domains[currentVariable] = domains[currentVariable].Where(v => v != value).ToList();
                }
                changes.Push((true, newChanges));
            }
            else
            {
                changes.Push((false, null));
            }
        }
    }
}
