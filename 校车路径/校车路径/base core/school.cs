using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 校车路径
{
    class school
    {
        public string schoolID { get; set; }// //学校编号
        public double schoolX { get; set; }//坐标X
        public double schoolY { get; set; }//坐标Y
        public string AMEarly { get; set; }//最早到校时间arrivalTime
        public string AMLate { get; set; }//上课时间schoolTime（最晚到校时间）
        
        
        

        public school()     //构造函数
        {

        }

    }
}

