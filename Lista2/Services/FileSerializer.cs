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

        public string SaveToFile(List<List<int>> solution, string fileName)
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
                                inequalities.Add (new InequalityConstraintModel 
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

            return new Futoshiki(lines.Count, hardcodedValues, inequalities);
        }
    
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
    }
}
