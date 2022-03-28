using Lista2.Interfaces;
using Lista2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Validators
{
    public class ThreeInRowValidator : IPartialBinaryValidator
    {
        const int Limit = 3;
        private readonly int _size;

        public ThreeInRowValidator(int size)
        {
            _size = size;
        }

        public bool Validate(BinaryState state)
        {
            return ValidateColumns(state) && ValidateRows(state);
        }

        private bool ValidateColumns(BinaryState state)
        {
            for (int column = 0; column < _size; column++)
            {
                int inRow = 0;
                bool? lastValue = null;
                for (int row = 0; row < _size; row++)
                {
                    var currentValue = state[row, column].Value;
                    // currentValue is not assigned
                    if (!currentValue.HasValue)
                    {
                        inRow = 0;
                    }
                    // first value
                    else if (!lastValue.HasValue)
                    {
                        inRow = 1;
                    }
                    //  same value
                    else if (currentValue.Value == lastValue.Value)
                    {
                        inRow++;
                        if (inRow == Limit)
                        {
                            return false;
                        }
                    }
                    // others value
                    else
                    {
                        inRow = 1;
                    }
                    lastValue = currentValue;
                }
            }
            return true;
        }

        private bool ValidateRows(BinaryState state)
        {
            for (int row = 0; row < _size; row++)
            {
                int inRow = 0;
                bool? lastValue = null;
                for (int column = 0; column < _size; column++)
                {
                    var currentValue = state[row, column].Value;
                    // currentValue is not assigned
                    if (!currentValue.HasValue)
                    {
                        inRow = 0;
                    }
                    // first value
                    else if (!lastValue.HasValue)
                    {
                        inRow = 1;
                    }
                    //  same value
                    else if (currentValue.Value == lastValue.Value)
                    {
                        inRow++;
                        if (inRow == Limit)
                        {
                            return false;
                        }
                    }
                    // others value
                    else
                    {
                        inRow = 1;
                    }
                    lastValue = currentValue;
                }
            }
            return true;
        }
    }
}
