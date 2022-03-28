using Lista2.Interfaces;
using Lista2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Validators
{
    internal class UniqueValidator : IPartialBinaryValidator
    {
        private readonly int _size;

        public UniqueValidator(int size)
        {
            _size = size;
        }

        public bool Validate(BinaryState state)
        {
            return ValidateRows(state) && ValidateColumns(state);
        }

        private bool ValidateRows(BinaryState state)
        {
            var rows = new List<string>();
            var sb = new StringBuilder();
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    var x = state[row, column].Value;
                    if (x.HasValue)
                    {
                        sb.Append(x.Value);
                    }
                    else
                    {
                        sb.Clear();
                        break;
                    }
                }
                if (sb.Length > 0)
                {
                    rows.Add(sb.ToString());
                    sb.Clear();
                }
            }

            var distinctRows = rows.Distinct().ToList();
            return rows.Count == distinctRows.Count;
        }

        private bool ValidateColumns(BinaryState state)
        {
            var columns = new List<string>();
            var sb = new StringBuilder();
            for (int column = 0; column < _size; column++)
            {
                for (int row = 0; row < _size; row++)
                {
                    var x = state[row, column].Value;
                    if (x.HasValue)
                    {
                        sb.Append(x.Value);
                    }
                    else
                    {
                        sb.Clear();
                        break;
                    }
                }
                if (sb.Length > 0)
                {
                    columns.Add(sb.ToString());
                    sb.Clear();
                }
            }

            var distinctColumns = columns.Distinct().ToList();
            return columns.Count == distinctColumns.Count;
        }
    }
}
