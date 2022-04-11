using Lista2.Model;

namespace Lista2.Managers
{
    public class Binary
    {
        private CSP<Field, int> csp;
        public int Size { get; set; }
        public List<Field> Variables { get; private set; } = new List<Field>();
        public Dictionary<Field, List<int>> Domains { get; private set; } = new();


        public Binary(int size)
        {
            Size = size;

            InitializeVariables();
            InitializeDomains();

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

        private void InitializeDomains()
        {
            foreach (var variable in Variables)
            {
                Domains.Add(variable, new List<int> { 0, 1 });
            }

            var constant_0_0 = Variables.First(v => v.Row == 0 && v.Column == 0);
            Domains[constant_0_0] = new List<int> { 1 };

            var constant_0_3 = Variables.First(v => v.Row == 0 && v.Column == 3);
            Domains[constant_0_3] = new List<int> { 0 };

            var constant_1_2 = Variables.First(v => v.Row == 1 && v.Column == 2);
            Domains[constant_1_2] = new List<int> { 0 };

            var constant_1_3 = Variables.First(v => v.Row == 1 && v.Column == 3);
            Domains[constant_1_3] = new List<int> { 0 };

            var constant_1_5 = Variables.First(v => v.Row == 1 && v.Column == 5);
            Domains[constant_1_5] = new List<int> { 1 };

            var constant_2_1 = Variables.First(v => v.Row == 2 && v.Column == 1);
            Domains[constant_2_1] = new List<int> { 0 };

            var constant_2_2 = Variables.First(v => v.Row == 2 && v.Column == 2);
            Domains[constant_2_2] = new List<int> { 0 };

            var constant_2_5 = Variables.First(v => v.Row == 2 && v.Column == 5);
            Domains[constant_2_5] = new List<int> { 1 };

            var constant_4_0 = Variables.First(v => v.Row == 4 && v.Column == 0);
            Domains[constant_4_0] = new List<int> { 0 };

            var constant_4_1 = Variables.First(v => v.Row == 4 && v.Column == 1);
            Domains[constant_4_1] = new List<int> { 0 };

            var constant_4_3 = Variables.First(v => v.Row == 4 && v.Column == 3);
            Domains[constant_4_3] = new List<int> { 1 };

            var constant_5_1 = Variables.First(v => v.Row == 5 && v.Column == 1);
            Domains[constant_5_1] = new List<int> { 1 };

            var constant_5_4 = Variables.First(v => v.Row == 5 && v.Column == 4);
            Domains[constant_5_4] = new List<int> { 0 };

            var constant_5_5 = Variables.First(v => v.Row == 5 && v.Column == 5);
            Domains[constant_5_5] = new List<int> { 0 };
        }

        private void AddConstraints()
        {
            // equal row count constraint
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Row == 0).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Row == 1).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Row == 2).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Row == 3).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Row == 4).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Row == 5).ToList()));

            // equal column count constraint
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Column == 0).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Column == 1).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Column == 2).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Column == 3).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Column == 4).ToList()));
            csp.AddConstraint(new EqualCountConstraint(Variables.Where(v => v.Column == 5).ToList()));

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
