using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 校车路径
{
    class stops
    {
       // public string busStopname { get; set; }//站点名称busStopname
        public string stopID { get; set; }//站点编号busStopnumber
 //       public double x , y;//定义站点坐标x,y
        public double stopX { get; set; }
        public double stopY { get; set; }
/*        public void busStoplocation(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        */
        public string EP_ID { get; set; }//站点所属学校ID
        public int studentCount { get; set; }//学生人数studentNumber

        //public int busStoprestStudent { get; set; }//站点剩余人数busStoprestStudent
        public stops()
        {

        }

    }
   
}

