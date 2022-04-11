// See https://aka.ms/new-console-template for more information
using Lista2.Model;

var variables = new List<Field>();
for (int i = 0; i < 6; i++)
{
    for (int j = 0; j < 6; j++)
    {
        variables.Add(new Field() { Row = i, Column = j });
    }
}

var domains = new Dictionary<Field, List<int>>();
foreach (var variable in variables)
{
    domains.Add(variable, new List<int> { 0, 1});
}

var constant_0_0 = variables.First(v => v.Row == 0 && v.Column == 0);
domains[constant_0_0] = new List<int> { 1 };

var constant_0_3 = variables.First(v => v.Row == 0 && v.Column == 3);
domains[constant_0_3] = new List<int> { 0 };

var constant_1_2 = variables.First(v => v.Row == 1 && v.Column == 2);
domains[constant_1_2] = new List<int> { 0 };

var constant_1_3 = variables.First(v => v.Row == 1 && v.Column == 3);
domains[constant_1_3] = new List<int> { 0 };

var constant_1_5 = variables.First(v => v.Row == 1 && v.Column == 5);
domains[constant_1_5] = new List<int> { 1 };

var constant_2_1 = variables.First(v => v.Row == 2 && v.Column == 1);
domains[constant_2_1] = new List<int> { 0 };

var constant_2_2 = variables.First(v => v.Row == 2 && v.Column == 2);
domains[constant_2_2] = new List<int> { 0 };

var constant_2_5 = variables.First(v => v.Row == 2 && v.Column == 5);
domains[constant_2_5] = new List<int> { 1 };

var constant_4_0 = variables.First(v => v.Row == 4 && v.Column == 0);
domains[constant_4_0] = new List<int> { 0 };

var constant_4_1 = variables.First(v => v.Row == 4 && v.Column == 1);
domains[constant_4_1] = new List<int> { 0 };

var constant_4_3 = variables.First(v => v.Row == 4 && v.Column == 3);
domains[constant_4_3] = new List<int> { 1 };

var constant_5_1 = variables.First(v => v.Row == 5 && v.Column == 1);
domains[constant_5_1] = new List<int> { 1 };

var constant_5_4 = variables.First(v => v.Row == 5 && v.Column == 4);
domains[constant_5_4] = new List<int> { 0 };

var constant_5_5 = variables.First(v => v.Row == 5 && v.Column == 5);
domains[constant_5_5] = new List<int> { 0 };

var csp = new CSP<Field, int>(variables, domains);

// equal row count constraint
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 0).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 1).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 2).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 3).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 4).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 5).ToList()));

// equal column count constraint
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 0).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 1).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 2).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 3).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 4).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 5).ToList()));

var solution = csp.Backtracking(new Dictionary<Field, int>());
if (solution is null)
{
    Console.WriteLine("No solution");
}
else
{
    Console.WriteLine("Solution found!");
    foreach (var item in solution.Keys)
    {
        Console.WriteLine($"{item}: {solution[item]}");
    }
}