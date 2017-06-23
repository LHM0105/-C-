using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace 校车路径
{
    
    class solution:path 
    {
        //此类继承了path类，拥有path类的属性（校车编号、站点集合）
        public int arrivalTime { get; set; }//到达学校时间
        public int waitTime { get; set; }//等待时间
        
        //public int svcTime { get; set; }//最早到校时间

        public solution()
        {
           

        }

        
    }
}
