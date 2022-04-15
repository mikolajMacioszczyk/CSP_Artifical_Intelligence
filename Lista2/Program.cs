// See https://aka.ms/new-console-template for more information
using Lista2.Heuristics.Values;
using Lista2.Interface;
using Lista2.Services;

IValueHeuristic<int> GetValueGeuristic()
{
    return new NoHeuristic<int>();
}
int maxSolutions = 1;

var fileSerializer = new FileSerializer();
var binaryInputs = new string[] { "binary_6x6", "binary_8x8", "binary_10x10" };
//var binaryInputs = new string[] { };

foreach (var binaryInput in binaryInputs)
{
    var binary = fileSerializer.ReadBinary(binaryInput);

    var solutions = binary.Solve(GetValueGeuristic(), maxSolutions);
    if (!solutions.Any())
    {
        Console.WriteLine($"No solution for problem {binaryInput}");
    }
    else
    {
        Console.WriteLine($"{solutions.Count} Solution found for problem {binaryInput}!");
        for (int n = 0; n < solutions.Count; n++)
        {
            var lines = new List<List<int>>();
            for (int i = 0; i < binary.Size; i++)
            {
                var line = new List<int>();
                for (int j = 0; j < binary.Size; j++)
                {
                    var variable = binary.Variables[i * binary.Size + j];
                    int value = solutions[n][variable];
                    Console.Write($" {value} |");
                    line.Add(value);
                }
                lines.Add(line);
                Console.WriteLine();
            }
            Console.WriteLine($"Stored result in {fileSerializer.SaveBinaryToFile(lines, binaryInput)}");
        }
    }
    Console.WriteLine();
}

var futoshikiInputs = new string[] { "futoshiki_4x4", "futoshiki_5x5", "futoshiki_6x6" };

foreach (var input in futoshikiInputs)
{
    var futhosiki = fileSerializer.ReadFutoshiki(input);

    var futhosikiSolutions = futhosiki.SolveBacktracking(GetValueGeuristic(), maxSolutions);
    var futhosikiSolutions2 = futhosiki.SolveForwardChecking(GetValueGeuristic(), maxSolutions);

    if (!futhosikiSolutions.Any())
    {
        Console.WriteLine($"No solution for problem {input} - Backtracking");
    }
    else
    {
        Console.WriteLine($"{futhosikiSolutions.Count} Solution found for problem {input} - Backtracking!");
        for (int n = 0; n < futhosikiSolutions.Count; n++)
        {
            var lines = new List<List<int>>();
            for (int i = 0; i < futhosiki.Size; i++)
            {
                var line = new List<int>();
                for (int j = 0; j < futhosiki.Size; j++)
                {
                    var variable = futhosiki.Variables[i * futhosiki.Size + j];
                    int value = futhosikiSolutions[n][variable];
                    line.Add(value);
                    Console.Write($" {value} |");
                }
                lines.Add(line);
                Console.WriteLine();
            }
            Console.WriteLine($"Stored result in {fileSerializer.SaveFutoshikiToFile(lines, futhosiki.Inequalities, input)}");
        }
    }

    if (!futhosikiSolutions2.Any())
    {
        Console.WriteLine($"No solution for problem {input} - Forward Checking");
    }
    else
    {
        Console.WriteLine($"{futhosikiSolutions2.Count} Solution found for problem {input} - Forward Checking!");
        for (int n = 0; n < futhosikiSolutions2.Count; n++)
        {
            var lines = new List<List<int>>();
            for (int i = 0; i < futhosiki.Size; i++)
            {
                var line = new List<int>();
                for (int j = 0; j < futhosiki.Size; j++)
                {
                    var variable = futhosiki.Variables[i * futhosiki.Size + j];
                    int value = futhosikiSolutions2[n][variable];
                    line.Add(value);
                    Console.Write($" {value} |");
                }
                lines.Add(line);
                Console.WriteLine();
            }
            Console.WriteLine($"Stored result in {fileSerializer.SaveFutoshikiToFile(lines, futhosiki.Inequalities, input)}");
        }
    }
}

//var hardcodedValues = new List<FieldHardcodedValue> { 
//    new FieldHardcodedValue { Row = 0, Column = 0, Value = 1 },
//    new FieldHardcodedValue { Row = 0, Column = 3, Value = 0 },
//    new FieldHardcodedValue { Row = 1, Column = 2, Value = 0 },
//    new FieldHardcodedValue { Row = 1, Column = 3, Value = 0 },
//    new FieldHardcodedValue { Row = 1, Column = 5, Value = 1 },
//    new FieldHardcodedValue { Row = 2, Column = 1, Value = 0 },
//    new FieldHardcodedValue { Row = 2, Column = 2, Value = 0 },
//    new FieldHardcodedValue { Row = 2, Column = 5, Value = 1 },
//    new FieldHardcodedValue { Row = 4, Column = 0, Value = 0 },
//    new FieldHardcodedValue { Row = 4, Column = 1, Value = 0 },
//    new FieldHardcodedValue { Row = 4, Column = 3, Value = 1 },
//    new FieldHardcodedValue { Row = 5, Column = 1, Value = 1 },
//    new FieldHardcodedValue { Row = 5, Column = 4, Value = 0 },
//    new FieldHardcodedValue { Row = 5, Column = 5, Value = 0 },
//};

//var futhoshikiHardcodedValues = new List<FieldHardcodedValue>()
//{
//    new FieldHardcodedValue {Row = 0, Column = 2, Value = 3},
//    new FieldHardcodedValue {Row = 1, Column = 1, Value = 3},
//    new FieldHardcodedValue {Row = 3, Column = 3, Value = 3},
//};

//var futhoshikiInequalities = new List<InequalityConstraintModel>()
//{
//    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 0, Variable2Row = 2, Variable2Column = 0, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 1, Variable2Row = 2, Variable2Column = 1, Operator = Lista2.Enums.InequalityOperator.LessThan},
//    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 2, Variable2Row = 2, Variable2Column = 2, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 2, Variable1Column = 0, Variable2Row = 3, Variable2Column = 0, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 3, Variable1Column = 0, Variable2Row = 3, Variable2Column = 1, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//    new InequalityConstraintModel {Variable1Row = 3, Variable1Column = 2, Variable2Row = 3, Variable2Column = 3, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
//};