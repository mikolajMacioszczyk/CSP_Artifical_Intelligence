namespace Lista2.Model
{
    public class EqualCountConstraint : Constraint<Field, int>
    {
        public EqualCountConstraint(List<Field> variables) : base(variables)
        {}

        public override bool IsStisfied(Dictionary<Field, int> assignement)
        {
            int count = 0;
            foreach (Field field in Variables)
            {
                if (!assignement.ContainsKey(field))
                {
                    return true;
                }
                count += assignement[field];
            }
            return count * 2 == Variables.Count;
        }
    }
}
