using Lista2.Model;

namespace Lista2.Services
{
    public class LoggerService
    {
        public static void PrintBinarySolutions(List<Dictionary<Field, int>> solutions, List<Field> variables, 
            string problemName, int size, string method)
        {
            if (!solutions.Any())
            {
                Console.WriteLine($"No solution for problem {problemName} - {method}");
            }
            else
            {
                Console.WriteLine($"{solutions.Count} Solution found for problem {problemName} - {method}!");
                for (int n = 0; n < solutions.Count; n++)
                {
                    var lines = new List<List<int>>();
                    for (int i = 0; i < size; i++)
                    {
                        var line = new List<int>();
                        for (int j = 0; j < size; j++)
                        {
                            var variable = variables[i * size + j];
                            int value = solutions[n][variable];
                            Console.Write($" {value} |");
                            line.Add(value);
                        }
                        lines.Add(line);
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    //Console.WriteLine($"Stored result in {fileSerializer.SaveBinaryToFile(lines, binaryInput)}");
                }
            }
            Console.WriteLine();
        }

        public static void PrintFutoshikiSolutions(List<Dictionary<Field, int>> solutions, List<Field> variables,
            string problemName, int size, string method)
        {
            if (!solutions.Any())
            {
                Console.WriteLine($"No solution for problem {problemName} - {method}");
            }
            else
            {
                Console.WriteLine($"{solutions.Count} Solution found for problem {problemName} - {method}!");
                for (int n = 0; n < solutions.Count; n++)
                {
                    var lines = new List<List<int>>();
                    for (int i = 0; i < size; i++)
                    {
                        var line = new List<int>();
                        for (int j = 0; j < size; j++)
                        {
                            var variable = variables[i * size + j];
                            int value = solutions[n][variable];
                            line.Add(value);
                            Console.Write($" {value} |");
                        }
                        lines.Add(line);
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    //Console.WriteLine($"Stored result in {fileSerializer.SaveFutoshikiToFile(lines, futhosiki.Inequalities, input)}");
                }
            }
        }
    }
}
