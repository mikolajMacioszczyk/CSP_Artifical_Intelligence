using Lista2.Managers;
using Lista2.Model;
using System.Text;

namespace Lista2.Services
{
    public class FileSerializer
    {
        #region Binary
        public Binary ReadBinary(string fileName)
        {
            var path = GetInputFilePath(fileName);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var hardcodedValues = new List<FieldHardcodedValue>();

            var lines = File.ReadLines(path).ToList();
            for (int row = 0; row < lines.Count; row++)
            {
                var line = lines[row];
                if (line.Length != lines.Count)
                {
                    throw new ArgumentException($"Inconsistent binary problem size: lines = {lines.Count}, line = {line.Length}");
                }
                for (int column = 0; column < line.Length; column++)
                {
                    switch (line[column])
                    {
                        case '1':
                            hardcodedValues.Add(new FieldHardcodedValue { Row = row, Column = column, Value = 1 });
                            break;
                        case '0':
                            hardcodedValues.Add(new FieldHardcodedValue { Row = row, Column = column, Value = 0 });
                            break;
                        case 'x':
                            break;
                        default:
                            throw new ArgumentException($"Cannot parse character {line[column]}");
                    }
                }
            }

            return new Binary(lines.Count, hardcodedValues);
        }

        public string SaveBinaryToFile(List<List<int>> solution, string fileName)
        {
            string resultFileName = $"{fileName}_{Guid.NewGuid()}";
            var path = GetOutputFilePath(resultFileName);
            
            var lines = new List<string>();
            for (int row = 0; row < solution.Count; row++)
            {
                var sb = new StringBuilder();
                for (int column = 0; column < solution[row].Count; column++)
                {
                     sb.Append($" {solution[row][column]} |");
                }
                lines.Add(sb.ToString());
            }

            File.WriteAllLines(path, lines);

            return resultFileName;
        }

        #endregion

        #region Futoshiki
        public Futoshiki ReadFutoshiki(string fileName)
        {
            var path = GetInputFilePath(fileName);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var hardcodedValues = new List<FieldHardcodedValue>();
            var inequalities = new List<InequalityConstraintModel>();

            var lines = File.ReadLines(path).ToList();
            for (int row = 0; row < lines.Count; row++)
            {
                var line = lines[row];
                if (row % 2 == 0 && line.Length != lines.Count)
                {
                    throw new ArgumentException($"Inconsistent binary problem size: lines = {lines.Count}, line = {line.Length}");
                }
                for (int column = 0; column < line.Length; column++)
                {
                    switch (line[column])
                    {
                        case 'x':
                        case '-':
                            break;
                        case '>':
                        case '<':
                            // is in row with numbers
                            if (row % 2 == 0)
                            {
                                inequalities.Add(new InequalityConstraintModel
                                {
                                    Variable1Row = row / 2,
                                    Variable1Column = (column - 1) / 2,
                                    Variable2Row = row / 2,
                                    Variable2Column = (column + 1) / 2,
                                    Operator = line[column] == '>' ? Enums.InequalityOperator.GreaterThan : Enums.InequalityOperator.LessThan
                                });
                            }
                            // is in row with constraints
                            else
                            {
                                inequalities.Add(new InequalityConstraintModel
                                {
                                    Variable1Row = (row - 1) / 2,
                                    Variable1Column = column,
                                    Variable2Row = (row + 1) / 2,
                                    Variable2Column = column,
                                    Operator = line[column] == '>' ? Enums.InequalityOperator.GreaterThan : Enums.InequalityOperator.LessThan
                                });
                            }
                            break;
                        default:
                            try
                            {
                                var value = int.Parse(line[column].ToString());
                                hardcodedValues.Add(new FieldHardcodedValue { Row = row / 2, Column = column / 2, Value = value });
                                break;
                            }
                            catch (FormatException)
                            {
                                throw new ArgumentException($"Cannot parse character {line[column]}");
                            }
                    }
                }
            }

            return new Futoshiki((lines.Count + 1) / 2, hardcodedValues, inequalities);
        }

        public string SaveFutoshikiToFile(List<List<int>> solution, List<InequalityConstraintModel> inequalities, string fileName)
        {
            string resultFileName = $"{fileName}_{Guid.NewGuid()}";
            var path = GetOutputFilePath(resultFileName);

            var lines = new List<string>();
            for (int row = 0; row < solution.Count; row++)
            {
                var sb = new StringBuilder();
                var constraintSb = new StringBuilder();

                // Constraints within column
                var columnConstraints = inequalities.Where(i => i.Variable1Row == row && i.Variable2Row == row + 1).ToList();

                for (int column = 0; column < solution[row].Count; column++)
                {
                    sb.Append($" {solution[row][column]} |");
                    
                    // Constraint within row
                    var constraint = inequalities.FirstOrDefault(i => i.Variable1Row == row && i.Variable1Column == column
                                                        && i.Variable2Row == row && i.Variable2Column == column + 1);
                    if (constraint is null)
                    {
                        sb.Append($" - |");
                    }
                    else if (constraint.Operator == Enums.InequalityOperator.GreaterThan)
                    {
                        sb.Append($" > |");
                    }
                    else if (constraint.Operator == Enums.InequalityOperator.LessThan)
                    {
                        sb.Append($" < |");
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
                lines.Add(sb.ToString());
                if (row != solution.Count - 1)
                {
                    lines.Add(constraintSb.ToString());
                }
            }

            File.WriteAllLines(path, lines);

            return resultFileName;
        }

        #endregion

        #region Solutions

        public string SaveSolutionsToFile(List<CspSolution<Field, int>> solution)
        {
            string resultFileName = $"Result_{Guid.NewGuid()}";
            var path = GetOutputFilePath(resultFileName);

            var problemSolutions = solution.GroupBy(s => s.ProblemName);

            var lines = new List<string>();

            foreach (var problemGroup in problemSolutions)
            {
                lines.Add($"Solutions for problem {problemGroup.Key}");

                var currentProblemSolutions = problemGroup.Select(s => s).ToList();

                var methodsSolutions = currentProblemSolutions.GroupBy(s => s.Method);

                lines.Add("");

                foreach (var currentMethodSolution in methodsSolutions)
                {
                    lines.Add(currentMethodSolution.Key);
                    lines.Add($"Max Solutions, Found Solutions, Iterations, TotalMiliseconds");

                    foreach (var currentSolution in currentMethodSolution.OrderBy(s => s.MaxSolutions))
                    {
                        lines.Add($"{currentSolution.MaxSolutions},{currentSolution.Solutions.Count},{currentSolution.Iterations},{currentSolution.TotalMiliseconds.ToString("0.####")}");
                    }
                    lines.Add("");
                }
            }

            File.WriteAllLines(path, lines);

            return resultFileName;
        }

        #endregion

        #region Helpers
        private string GetInputFilePath(string fileName)
        {
            var root = GetBaseFilePath();
            return Path.Combine(root, "Input", fileName);
        }

        private string GetOutputFilePath(string fileName)
        {
            var root = GetBaseFilePath();
            return Path.Combine(root, "Output", fileName);
        }

        private string GetBaseFilePath() =>
            Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        #endregion
    }
}
