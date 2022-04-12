using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Model
{
    public class DistinctValuesConstraint : Constraint<Field, int>
    {
        public DistinctValuesConstraint(List<Field> variables) : base(variables)
        {
        }

        public override bool IsStisfied(Dictionary<Field, int> assignement)
        {
            var variableValues = new List<int>();
            foreach (var variable in Variables)
            {
                if (assignement.ContainsKey(variable))
                {
                    var value = assignement[variable];
                    if (variableValues.Contains(value))
                    {
                        return false;
                    }
                    variableValues.Add(value);
                }
            }
            return true;
        }
    }
}
