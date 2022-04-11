// See https://aka.ms/new-console-template for more information
using Lista2.Managers;
using Lista2.Model;

int size = 6;

var binary = new Binary(size);

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