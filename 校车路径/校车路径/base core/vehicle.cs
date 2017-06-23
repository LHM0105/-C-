using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 校车路径
{
    class vehicle
    {
        public string vehicleNumber { get; set; } //车辆编号vehicleNumber
        public int vehicleCapacity { get; set; }//容量vehicleCapacity
        public string vehicleStyle { get; set; }//车型vehicleStyle

        public double fixedCost { get; set; }//固定成本fixedCost
        public double variableCost { get; set; }//可变成本variableCost
    public vehicle()
        {

        }
    }
}
