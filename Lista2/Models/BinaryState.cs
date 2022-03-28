namespace Lista2.Models
{
    public class BinaryState
    {
        public BinaryUnit[,] Matrix { get; private set; }
        public int Size { get; private set; }

        public BinaryState(int size)
        {
            Matrix = new BinaryUnit[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Matrix[i, j] = new BinaryUnit();
                }
            }
            Size = size;
        }

        public BinaryUnit this[int index1, int index2]
        {
            get => Matrix[index1, index2];
            set => Matrix[index1, index2] = value;
        }
    }
}
