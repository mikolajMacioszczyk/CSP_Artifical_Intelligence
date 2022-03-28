using Lista2.Interfaces;
using Lista2.Models;

namespace Lista2.Validators
{
    public class NumbersCountValidator : IFullBinaryValidator
    {
        private readonly int _size;

        public NumbersCountValidator(int size)
        {
            _size = size;
        }

        public bool Validate(BinaryState state)
        {
            int firstRowCount = 0;
            for (int column = 0; column < _size; column++)
            {
                if (state[0, column].Value == true)
                {
                    firstRowCount++;
                }
            }

            return ValidateRows(state, firstRowCount) && ValidateColumns(state, firstRowCount);
        }

        private bool ValidateRows(BinaryState state, int count)
        {
            for (int row = 0; row < _size; row++)
            {
                int rowCount = 0;
                for (int column = 0; column < _size; column++)
                {
                    if (state[row, column].Value == true)
                    {
                        rowCount++;
                    }
                }
                if (rowCount != count)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateColumns(BinaryState state, int count)
        {
            for (int column = 0; column < _size; column++)
            {
                int columnCount = 0;
                for (int row = 0; row < _size; row++)
                {
                    if (state[row, column].Value == true)
                    {
                        columnCount++;
                    }
                }
                if (columnCount != count)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
