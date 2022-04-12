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

            return sum != Variables.Count;
        }

        public override void Propagate(Field variable, Dictionary<Field, int> assigement, Dictionary<Field, List<int>> domains)
        {
            throw new NotImplementedException();
        }
    }
}
