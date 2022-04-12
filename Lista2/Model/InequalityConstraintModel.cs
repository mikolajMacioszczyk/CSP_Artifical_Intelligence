using Lista2.Enums;

namespace Lista2.Model
{
    public class InequalityConstraintModel
    {
        public int Variable1Row { get; set; }
        public int Variable1Column { get; set; }

        public int Variable2Row { get; set; }
        public int Variable2Column { get; set; }

        public InequalityOperator Operator { get; set; }
    }
}
