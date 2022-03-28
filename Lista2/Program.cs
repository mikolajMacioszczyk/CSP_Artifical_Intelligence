using Lista2.Managers;
using Lista2.Models;
using Lista2.Validators;

int size = 9;

var startingState = GetTestBinaryState();

var manager = new BacktrackingManager();
manager.RegisterPartialValidator(new ThreeInRowValidator(size));
manager.RegisterPartialValidator(new UniqueValidator(size));
manager.RegisterFullValidator(new FilledValidator(size));
manager.RegisterFullValidator(new NumbersCountValidator(size));

var results = manager.Process(startingState);

static BinaryState GetTestBinaryState()
{
    return null;
}