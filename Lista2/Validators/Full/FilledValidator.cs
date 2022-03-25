using Lista2.Interfaces;
using Lista2.Models;

namespace Lista2.Validators
{
    public class FilledValidator : IFullBinaryValidator
    {
        private readonly int _size;

        public FilledValidator(int size)
        {
            _size = size;
        }

        public bool Validate(BinaryState state)
        {
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    if (!state[row, column].HasValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
