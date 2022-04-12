using Lista2.Model;

namespace Lista2.Managers
{
    public class Futoshiki
    {
        private CSP<Field, int> csp;
        public int Size { get; set; }
        public List<Field> Variables { get; private set; } = new List<Field>();
        public Dictionary<Field, List<int>> Domains { get; private set; } = new();

        public Futoshiki(int size, List<FieldHardcodedValue> hardcodedValues)
        {
            Size = size;

            InitializeVariables();
            InitializeDomains(hardcodedValues);

            csp = new CSP<Field, int>(Variables, Domains);

            AddConstraints();
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
            var fullDomain = Enumerable.Range(0, Size);

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

        private void AddConstraints()
        {

        }
    }
}
