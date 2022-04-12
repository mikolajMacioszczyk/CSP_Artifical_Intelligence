// See https://aka.ms/new-console-template for more information
using Lista2.Managers;
using Lista2.Model;

int binarySize = 6;

var hardcodedValues = new List<FieldHardcodedValue> { 
    new FieldHardcodedValue { Row = 0, Column = 0, Value = 1 },
    new FieldHardcodedValue { Row = 0, Column = 3, Value = 0 },
    new FieldHardcodedValue { Row = 1, Column = 2, Value = 0 },
    new FieldHardcodedValue { Row = 1, Column = 3, Value = 0 },
    new FieldHardcodedValue { Row = 1, Column = 5, Value = 1 },
    new FieldHardcodedValue { Row = 2, Column = 1, Value = 0 },
    new FieldHardcodedValue { Row = 2, Column = 2, Value = 0 },
    new FieldHardcodedValue { Row = 2, Column = 5, Value = 1 },
    new FieldHardcodedValue { Row = 4, Column = 0, Value = 0 },
    new FieldHardcodedValue { Row = 4, Column = 1, Value = 0 },
    new FieldHardcodedValue { Row = 4, Column = 3, Value = 1 },
    new FieldHardcodedValue { Row = 5, Column = 1, Value = 1 },
    new FieldHardcodedValue { Row = 5, Column = 4, Value = 0 },
    new FieldHardcodedValue { Row = 5, Column = 5, Value = 0 },
};

var binary = new Binary(binarySize, hardcodedValues);

var solution = binary.Solve();
if (solution is null)
{
    Console.WriteLine("No solution");
}
else
{
    Console.WriteLine("Solution found!");
    for (int i = 0; i < binarySize; i++)
    {
        for (int j = 0; j < binarySize; j++)
        {
            var variable = binary.Variables[i * binarySize + j];
            int value = solution[variable];
            Console.Write($" {value} |");
        }
        Console.WriteLine();
    }
}


int futhoshikiSize = 4;

var futhoshikiHardcodedValues = new List<FieldHardcodedValue>()
{
    new FieldHardcodedValue {Row = 0, Column = 2, Value = 3},
    new FieldHardcodedValue {Row = 1, Column = 1, Value = 3},
    new FieldHardcodedValue {Row = 3, Column = 3, Value = 3},
};

var futhoshikiInequalities = new List<InequalityConstraintModel>()
{
    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 0, Variable2Row = 2, Variable2Column = 0, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 1, Variable2Row = 2, Variable2Column = 1, Operator = Lista2.Enums.InequalityOperator.LessThan},
    new InequalityConstraintModel {Variable1Row = 1, Variable1Column = 2, Variable2Row = 2, Variable2Column = 2, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
    new InequalityConstraintModel {Variable1Row = 2, Variable1Column = 0, Variable2Row = 3, Variable2Column = 0, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
    new InequalityConstraintModel {Variable1Row = 3, Variable1Column = 0, Variable2Row = 3, Variable2Column = 1, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
    new InequalityConstraintModel {Variable1Row = 3, Variable1Column = 2, Variable2Row = 3, Variable2Column = 3, Operator = Lista2.Enums.InequalityOperator.GreaterThan},
};

var futhosiki = new Futoshiki(futhoshikiSize, futhoshikiHardcodedValues, futhoshikiInequalities);

var futhosikiSolution = futhosiki.Solve();

if (futhosikiSolution is null)
{
    Console.WriteLine("No solution");
}
else
{
    Console.WriteLine("Solution found!");
    for (int i = 0; i < futhoshikiSize; i++)
    {
        for (int j = 0; j < futhoshikiSize; j++)
        {
            var variable = futhosiki.Variables[i * futhoshikiSize + j];
            int value = futhosikiSolution[variable];
            Console.Write($" {value} |");
        }
        Console.WriteLine();
    }
}