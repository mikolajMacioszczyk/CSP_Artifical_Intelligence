using Lista2.Model;

namespace Lista2.Managers
{
    public class Futoshiki
    {
        private CSP<Field, int> csp;
        public int Size { get; set; }
        public List<Field> Variables { get; private set; } = new List<Field>();
        public Dictionary<Field, List<int>> Domains { get; private set; } = new();

        public Futoshiki(int size, List<FieldHardcodedValue> hardcodedValues, 
            List<InequalityConstraintModel> inequalityConstraints)
        {
            Size = size;

            InitializeVariables();
            InitializeDomains(hardcodedValues);

            csp = new CSP<Field, int>(Variables, Domains);

            AddConstraints(inequalityConstraints);
        }

        public Dictionary<Field, int> Solve()
        {
            return csp.Backtracking(new Dictionary<Field, int>());
        }

        private void InitializeVariables()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Variables.Add(new Field() { Row = i, Column = j });
                }
            }
        }

        private void InitializeDomains(List<FieldHardcodedValue> hardcodedValues)
        {
            var fullDomain = Enumerable.Range(1, Size);

            foreach (var variable in Variables)
            {
                Domains.Add(variable, new List<int>(fullDomain));
            }

            foreach (var hardcodedValue in hardcodedValues)
            {
                var variable = Variables.First(v => v.Row == hardcodedValue.Row && v.Column == hardcodedValue.Column);
                Domains[variable] = new List<int> { hardcodedValue.Value };
            }
        }

        private void AddConstraints(List<InequalityConstraintModel> inequalityConstraints)
        {
            // distinct values at row
            for (int row = 0; row < Size; row++)
            {
                var rowVariables = new List<Field>();
                for (int column = 0; column < Size; column++)
                {
                    rowVariables.Add(Variables.First(v => v.Row == row && v.Column == column));
                }
                csp.AddConstraint(new DistinctValuesConstraint(rowVariables));
            }

            // distinct values at column
            for (int column = 0; column < Size; column++)
            {
                var columnVariables = new List<Field>();
                for (int row = 0; row < Size; row++)
                {
                    columnVariables.Add(Variables.First(v => v.Row == row && v.Column == column));
                }
                csp.AddConstraint(new DistinctValuesConstraint(columnVariables));
            }

            // inequality
            foreach (var inequality in inequalityConstraints)
            {
                var variable1 = Variables.First(v => v.Row == inequality.Variable1Row && v.Column == inequality.Variable1Column);
                var variable2 = Variables.First(v => v.Row == inequality.Variable2Row && v.Column == inequality.Variable2Column);

                csp.AddConstraint(new InequalityConstraint(variable1, variable2, inequality.Operator));
            }
        }
    }
}
