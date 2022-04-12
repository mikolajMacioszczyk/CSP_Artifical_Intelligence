// See https://aka.ms/new-console-template for more information
using Lista2.Managers;
using Lista2.Model;
using Lista2.Services;

var fileSerializer = new FileSerializer();
var binaryInputs = new string[] { "binary_6x6", "binary_8x8", "binary_10x10" };
//var binaryInputs = new string[] { };

foreach (var binaryInput in binaryInputs)
{
    var binary = fileSerializer.ReadBinary(binaryInput);

    var solution = binary.Solve();
    if (solution is null)
    {
        Console.WriteLine($"No solution for problem {binaryInput}");
    }
    else
    {
        Console.WriteLine($"Solution found for problem {binaryInput}!");
        var lines = new List<List<int>>();
        for (int i = 0; i < binary.Size; i++)
        {
            var line = new List<int>();
            for (int j = 0; j < binary.Size; j++)
            {
                var variable = binary.Variables[i * binary.Size + j];
                int value = solution[variable];
                Console.Write($" {value} |");
                line.Add(value);
            }
            lines.Add(line);
            Console.WriteLine();
        }
        Console.WriteLine($"Stored result in {fileSerializer.SaveToFile(lines, binaryInput)}");
    }
    Console.WriteLine();
}

var futoshikiInputs = new string[] { "futoshiki_4x4", "futoshiki_5x5", "futoshiki_6x6" };

foreach (var input in futoshikiInputs)
{
    var futhosiki = fileSerializer.ReadFutoshiki(input);

    var futhosikiSolution = futhosiki.Solve();

    if (futhosikiSolution is null)
    {
        Console.WriteLine($"No solution for problem {input}");
    }
    else
    {
        Console.WriteLine($"Solution found for problem {input}!");
        for (int i = 0; i < futhosiki.Size; i++)
        {
            for (int j = 0; j < futhosiki.Size; j++)
            {
                var variable = futhosiki.Variables[i * futhosiki.Size + j];
                int value = futhosikiSolution[variable];
                Console.Write($" {value} |");
            }
            Console.WriteLine();
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