using Lista2.Model;
using System.Text;

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
            List<InequalityConstraintModel> inequalities, string problemName, int size, string method)
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
                    Console.WriteLine(new string('-', size * 8));
                    var lines = new List<List<int>>();
                    for (int row = 0; row < size; row++)
                    {
                        var line = new List<int>();

                        // Constraints within column
                        var columnConstraints = inequalities.Where(i => i.Variable1Row == row && i.Variable2Row == row + 1).ToList();
                        var constraintSb = new StringBuilder();

                        for (int column = 0; column < size; column++)
                        {
                            var variable = variables[row * size + column];
                            int value = solutions[n][variable];
                            line.Add(value);
                            Console.Write($" {value} |");

                            // Constraint within row
                            var constraint = inequalities.FirstOrDefault(i => i.Variable1Row == row && i.Variable1Column == column
                                                                && i.Variable2Row == row && i.Variable2Column == column + 1);

                            if (constraint is null)
                            {
                                Console.Write($" - |");
                            }
                            else if (constraint.Operator == Enums.InequalityOperator.GreaterThan)
                            {
                                Console.Write($" > |");
                            }
                            else if (constraint.Operator == Enums.InequalityOperator.LessThan)
                            {
                                Console.Write($" < |");
                            }
                            else
                            {
                                throw new ArgumentException($"Unhandled operator {constraint.Operator}");
                            }

                            var columnConstraint = columnConstraints.FirstOrDefault(c => c.Variable1Column == column && c.Variable2Column == column);
                            if (columnConstraint is null)
                            {
                                constraintSb.Append(" -  ");
                            }
                            else if (columnConstraint.Operator == Enums.InequalityOperator.GreaterThan)
                            {
                                constraintSb.Append($" >  ");
                            }
                            else if (columnConstraint.Operator == Enums.InequalityOperator.LessThan)
                            {
                                constraintSb.Append($" <  ");
                            }
                            else
                            {
                                throw new ArgumentException($"Unhandled operator {columnConstraint.Operator}");
                            }
                            constraintSb.Append(" -  ");
                        }
                        lines.Add(line);
                        Console.WriteLine();
                        if (row != size - 1)
                        {
                            Console.WriteLine(constraintSb.ToString());
                        }
                    }
                    Console.WriteLine(new string('-', size * 8));
                    Console.WriteLine();
                    //Console.WriteLine($"Stored result in {fileSerializer.SaveFutoshikiToFile(lines, futhosiki.Inequalities, input)}");
                }
            }
        }
    }
}
