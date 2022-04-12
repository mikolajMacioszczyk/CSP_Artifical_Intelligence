using Lista2.Enums;

namespace Lista2.Model
{
    internal class InequalityConstraint : Constraint<Field, int>
    {
        public Field Variable1 { get; set; }
        public Field Variable2 { get; set; }
        public InequalityOperator InequalityOperator { get; set; }

        public InequalityConstraint(Field variable1, Field variable2, InequalityOperator inequalityOperator) 
            : base(new List<Field> { variable1, variable2})
        {
            Variable1 = variable1;
            Variable2 = variable2;
            InequalityOperator = inequalityOperator;
        }

        public override bool IsStisfied(Dictionary<Field, int> assignement)
        {
            if (!assignement.ContainsKey(Variable1) || !assignement.ContainsKey(Variable2))
            {
                return true;
            }

            var value1 = assignement[Variable1];
            var value2 = assignement[Variable2];

            switch (InequalityOperator)
            {
                case InequalityOperator.GreaterThan:
                    return value1 > value2;
                case InequalityOperator.GreaterThanOrEqual:
                    return value1 >= value2;
                case InequalityOperator.LessThan:
                    return value1 < value2;
                case InequalityOperator.LessThanOrEqual:
                    return value1 <= value2;
                default:
                    throw new InvalidOperationException(nameof(InequalityOperator));
            }
        }

        public override void Propagate(Field variable, int value, Dictionary<Field, List<int>> domains)
        {
            if (Variable1.Equals(variable))
            {
                switch (InequalityOperator)
                {
                    case InequalityOperator.GreaterThan:
                        domains[Variable2] = domains[Variable2].Where(v => v >= value).ToList();
                        break;
                    case InequalityOperator.GreaterThanOrEqual:
                        domains[Variable2] = domains[Variable2].Where(v => v > value).ToList();
                        break;
                    case InequalityOperator.LessThan:
                        domains[Variable2] = domains[Variable2].Where(v => v <= value).ToList();
                        break;
                    case InequalityOperator.LessThanOrEqual:
                        domains[Variable2] = domains[Variable2].Where(v => v < value).ToList();
                        break;
                    default:
                        throw new ArgumentException(nameof(InequalityOperator));
                }
            }
            else if (Variable2.Equals(variable))
            {
                switch (InequalityOperator)
                {
                    case InequalityOperator.GreaterThan:
                        domains[Variable1] = domains[Variable1].Where(v => v <= value).ToList();
                        break;
                    case InequalityOperator.GreaterThanOrEqual:
                        domains[Variable1] = domains[Variable1].Where(v => v < value).ToList();
                        break;
                    case InequalityOperator.LessThan:
                        domains[Variable1] = domains[Variable1].Where(v => v >= value).ToList();
                        break;
                    case InequalityOperator.LessThanOrEqual:
                        domains[Variable1] = domains[Variable1].Where(v => v > value).ToList();
                        break;
                    default:
                        throw new ArgumentException(nameof(InequalityOperator));
                }
            }
        }
    }
}
