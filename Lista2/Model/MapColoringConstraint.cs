using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Model
{
    public class MapColoringConstraint : Constraint<string, string>
    {
        public string Place1 { get; set; }
        public string Place2 { get; set; }

        public MapColoringConstraint(string place1, string place2) 
            : base(new List<string>() { place1, place2})
        {
            Place1 = place1;
            Place2 = place2;
        }

        public override bool IsStisfied(Dictionary<string, string> assignement)
        {
            if (!assignement.ContainsKey(Place1) || !assignement.ContainsKey(Place2))
            {
                return true;
            }

            return assignement[Place1] != assignement[Place2];
        }
    }
}
