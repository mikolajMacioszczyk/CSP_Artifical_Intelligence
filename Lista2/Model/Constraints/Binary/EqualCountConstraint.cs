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

        public override void Propagate(Field variable, Dictionary<Field, int> assigement, Dictionary<Field, List<int>> domains)
        {
            var unasigned = Variables.Where(v => !assigement.ContainsKey(v)).ToList();
            if (unasigned.Count == 0)
            {
                return;
            }

            var zeros = Variables.Where(v => assigement.ContainsKey(v)).Where(v => assigement[v] == 0).ToList();
            var ones = Variables.Where(v => assigement.ContainsKey(v)).Where(v => assigement[v] == 1).ToList();

            int onesMore = ones.Count - zeros.Count;
            int zerosMore = zeros.Count - ones.Count;

            if (unasigned.Count == onesMore)
            {
                UpdateDomains(unasigned, domains, 0);
            }
            else if (unasigned.Count == zerosMore)
            {
                UpdateDomains(unasigned, domains, 1);
            }
        }

        private void UpdateDomains(List<Field> variables, Dictionary<Field, List<int>> domains, int allowedValue)
        {
            foreach (var unasigned in variables)
            {
                domains[unasigned] = domains[unasigned].Where(v => v == allowedValue).ToList();
            }
        }
    }
}
