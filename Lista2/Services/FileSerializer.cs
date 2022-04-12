using Lista2.Managers;
using Lista2.Model;

namespace Lista2.Services
{
    public class FileSerializer
    {
        public Binary ReadBinary(string fileName)
        {
            var path = GetFilePath(fileName);
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
    
        private string GetFilePath(string fileName)
        {
            var root = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            return Path.Combine(root, "Input", fileName);
        }
    }
}
