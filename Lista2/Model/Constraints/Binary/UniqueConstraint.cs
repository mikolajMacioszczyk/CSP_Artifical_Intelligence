using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Model
{
    public class UniqueConstraint : Constraint<Field, int>
    {
        public int Size { get; set; }

        public UniqueConstraint(List<Field> variables, int size) : base(variables)
        {
            Size = size;
        }

        public override bool IsStisfied(Dictionary<Field, int> assignement)
        {
            if (assignement.Count < Variables.Count)
            {
                return true;
            }

            return ValidateRows(assignement) && ValidateColumns(assignement);
        }

        public override void Propagate(Field variable, Dictionary<Field, int> assigement, Dictionary<Field, List<int>> domains) {}

        private bool ValidateRows(Dictionary<Field, int> assigement) 
        {
            var rowStrings = new List<string>();

            for (int row = 0; row < Size; row++)
            {
                var sb = new StringBuilder();
                for (int column = 0; column < Size; column++)
                {
                    var variable = Variables.First(v => v.Row == row && v.Column == column);
                    sb.Append(assigement[variable].ToString());
                }
                rowStrings.Add(sb.ToString());
            }

            return rowStrings.Count == rowStrings.Distinct().Count();
        }

        private bool ValidateColumns(Dictionary<Field, int> assigement)
        {
            var columnStrings = new List<string>();

            for (int column = 0; column < Size; column++)
            {
                var sb = new StringBuilder();
                for (int row = 0; row < Size; row++)
                {
                    var variable = Variables.First(v => v.Row == row && v.Column == column);
                    sb.Append(assigement[variable].ToString());
                }
                columnStrings.Add(sb.ToString());
            }

            return columnStrings.Count == columnStrings.Distinct().Count();
        }
    }
}
