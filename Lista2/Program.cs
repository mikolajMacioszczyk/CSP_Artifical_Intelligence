using Lista2.Managers;
using Lista2.Models;
using Lista2.Validators;

var startingState = GetTestBinaryState();
int size = startingState.Size;

var manager = new BacktrackingManager();
manager.RegisterPartialValidator(new ThreeInRowValidator(size));
manager.RegisterPartialValidator(new UniqueValidator(size));
manager.RegisterFullValidator(new FilledValidator(size));
manager.RegisterFullValidator(new NumbersCountValidator(size));

var results = manager.Process(startingState);

Console.WriteLine("Soultion count: " + results.Count);
Console.ReadLine();

static BinaryState GetTestBinaryState()
{
    var state = new BinaryState(6);
    state[0, 0].Value = true;
    state[0, 0].IsHardcoded = true;

    state[0, 3].Value = false;
    state[0, 3].IsHardcoded = true;

    state[1, 2].Value = false;
    state[1, 2].IsHardcoded = true;

    state[1, 3].Value = false;
    state[1, 3].IsHardcoded = true;

    state[1, 5].Value = true;
    state[1, 5].IsHardcoded = true;

    state[2, 1].Value = false;
    state[2, 1].IsHardcoded = true;

    state[2, 2].Value = false;
    state[2, 2].IsHardcoded = true;

    state[2, 5].Value = true;
    state[2, 5].IsHardcoded = true;

    state[4, 0].Value = false;
    state[4, 0].IsHardcoded = true;

    state[4, 1].Value = false;
    state[4, 1].IsHardcoded = true;

    state[4, 3].Value = true;
    state[4, 3].IsHardcoded = true;

    state[5, 1].Value = true;
    state[5, 1].IsHardcoded = true;

    state[5, 4].Value = false;
    state[5, 4].IsHardcoded = true;

    state[5, 5].Value = false;
    state[5, 5].IsHardcoded = true;

    return state;
}