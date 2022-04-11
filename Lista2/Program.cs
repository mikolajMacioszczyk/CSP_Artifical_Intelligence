// See https://aka.ms/new-console-template for more information
using Lista2.Managers;
using Lista2.Model;

int size = 6;

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

var binary = new Binary(size, hardcodedValues);

var solution = binary.Solve();
if (solution is null)
{
    Console.WriteLine("No solution");
}
else
{
    Console.WriteLine("Solution found!");
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            var variable = binary.Variables[i * size + j];
            int value = solution[variable];
            Console.Write($" {value} |");
        }
        Console.WriteLine();
    }
}