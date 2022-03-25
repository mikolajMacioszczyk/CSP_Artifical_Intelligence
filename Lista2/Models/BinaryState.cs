using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Models
{
    public class BinaryState
    {
        public int?[,] Matrix { get; private set; }
        public int Size { get; private set; }

        public BinaryState(int size)
        {
            Matrix = new int?[size, size];
            Size = size;
        }

        public int? this[int index1, int index2]
        {
            get => Matrix[index1, index2];
            set => Matrix[index1, index2] = value;
        }
    }
}
