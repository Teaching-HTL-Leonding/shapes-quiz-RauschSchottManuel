using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesUI.Model
{
    public class ShapeEntity
    {
        public ShapeEntityType Type { get; set; }
        public string Values { get; set; }
        public double Area { get; set; }

        public ShapeEntity()
        {

        }

        public ShapeEntity(ShapeEntityType type, string values, double area)
        {
            Type = type;
            Values = values;
            Area = area;
        }
    }
}
