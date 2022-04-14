using Lista2.Interface;
using Lista2.Model;

namespace Lista2.Managers
{
    public class Binary
    {
        private CSP<Field, int> csp;
        public int Size { get; set; }
        public List<Field> Variables { get; private set; } = new List<Field>();
        public Dictionary<Field, List<int>> Domains { get; private set; } = new();

        public Binary(int size, List<FieldHardcodedValue> hardcodedValues)
        {
            Size = size;

            InitializeVariables();
            InitializeDomains(hardcodedValues);

            csp = new CSP<Field, int>(Variables, Domains);

            AddConstraints();
        }

        public Dictionary<Field, int> Solve(IValueHeuristic<int> valueHeuristic)
        {
            var result = csp.Backtracking(valueHeuristic);

            Console.WriteLine($"Count = {result.Item2}");

            return result.Item1;
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
            foreach (var variable in Variables)
            {
                Domains.Add(variable, new List<int> { 0, 1 });
            }

            foreach (var hardcodedValue in hardcodedValues)
            {
                var variable = Variables.First(v => v.Row == hardcodedValue.Row && v.Column == hardcodedValue.Column);
                Domains[variable] = new List<int> { hardcodedValue.Value };
            }
        }

        private void AddConstraints()
        {
            // equal row count constraint
            for (int i = 0; i < Size; i++)
            {
                csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Row == i).ToList()));
            }

            // equal column count constraint
            for (int i = 0; i < Size; i++)
            {
                csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Column == i).ToList()));
            }

            // rows
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size - 3; j++)
                {
                    var variable1 = Variables[i * Size + j];
                    var variable2 = Variables[i * Size + j + 1];
                    var variable3 = Variables[i * Size + j + 2];
                    csp.AddConstraint(new NotSameInRowConstraint(new List<Field> { variable1, variable2, variable3 }));
                }
            }

            // columns
            for (int column = 0; column < Size; column++)
            {
                for (int row = 0; row < Size - 3; row++)
                {
                    var variable1 = Variables[row * Size + column];
                    var variable2 = Variables[(row + 1) * Size + column];
                    var variable3 = Variables[(row + 2) * Size + column];
                    csp.AddConstraint(new NotSameInRowConstraint(new List<Field> { variable1, variable2, variable3 }));
                }
            }

            //unique
            csp.AddConstraint(new UniqueConstraint(Variables, Size));
        }
    }
}
