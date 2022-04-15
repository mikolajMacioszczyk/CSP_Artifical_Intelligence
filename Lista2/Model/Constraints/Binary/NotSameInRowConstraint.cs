namespace Lista2.Model
{
    public class NotSameInRowConstraint : Constraint<Field, int>
    {
        public NotSameInRowConstraint(List<Field> variables) : base(variables)
        {}

        public override bool IsStisfied(Dictionary<Field, int> assignement)
        {
            int sum = 0;
            foreach (var variable in Variables)
            {
                if (!assignement.ContainsKey(variable))
                {
                    return true;
                }
                sum += assignement[variable];
            }

            return sum != Variables.Count && sum > 0;
        }

        public override void Propagate(Field variable, Dictionary<Field, int> assigement, Dictionary<Field, List<int>> domains)
        {
            int value = assigement[variable];

            var unassigned = Variables.Where(v => !assigement.ContainsKey(v)).ToList();
            if (unassigned.Count != 1)
            {
                return;
            }
            var onlyUnassigend = unassigned.First();

            if (Variables.Except(unassigned).All(v => assigement[v] == value))
            {
                domains[onlyUnassigend] = domains[onlyUnassigend].Where(v => v != value).ToList();
            }
        }
    }
}
