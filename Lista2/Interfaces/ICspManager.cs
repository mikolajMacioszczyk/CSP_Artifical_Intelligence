using Lista2.Models;

namespace Lista2.Interfaces
{
    public interface ICspManager
    {
        List<BinaryState> Process(BinaryState variables);

        void RegisterFullValidator(IFullBinaryValidator fullValidator);

        void RegisterPartialValidator(IPartialBinaryValidator partialValidator);
    }
}
