using Lista2.Interfaces;
using Lista2.Models;

namespace Lista2.Managers
{
    public class BacktrackingManager : ICspManager
    {
        private static readonly Random Random = new Random();
        private List<IFullBinaryValidator> fullValidators = new List<IFullBinaryValidator>();
        private List<IPartialBinaryValidator> partialValidators = new List<IPartialBinaryValidator>();

        public List<BinaryState> Process(BinaryState currentState)
        {
            var successStates = new List<BinaryState>();
            Process(currentState, 0, 0, successStates);
            return successStates;
        }

        private void Process(BinaryState currentState, int x, int y, List<BinaryState> successStates)
        {
            // check if state filled - should be validated
            if (y >= currentState.Size)
            {
                if (fullValidators.All(f => f.Validate(currentState)))
                {
                    Console.WriteLine("Soultion found!");
                    // if validation succeded then match
                    successStates.Add(currentState);
                } 
                else
                {
                    // else should go back
                    return;
                }
            }

            if (!currentState[x, y].IsHardcoded)
            {
                // draw value
                currentState[x, y].Value = Random.Next(2) > 0 ? true : false;
            }

            var (newX, newY) = GetNextCoordinates(currentState.Size, x, y);

            // process next
            if (partialValidators.All(f => f.Validate(currentState)))
            {
                Process(currentState, newX, newY, successStates);
            }

            if (!currentState[x, y].IsHardcoded)
            {
                // toggle value
                currentState[x, y].Value = !currentState[x, y].Value;

                if (partialValidators.All(f => f.Validate(currentState)))
                {
                    Process(currentState, newX, newY, successStates);
                }
            }

            currentState[x, y].Value = null;
        }

        private (int, int) GetNextCoordinates(int size, int x, int y)
        {
            x++;
            if (x >= size)
            {
                y++;
                x = 0;
            }
            return (x, y);
        }

        public void RegisterFullValidator(IFullBinaryValidator fullValidator)
        {
            fullValidators.Add(fullValidator);
        }

        public void RegisterPartialValidator(IPartialBinaryValidator partialValidator)
        {
            partialValidators.Add(partialValidator);
        }
    }
}
