using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 校车路径
{
    class depot
    {
        public string depotName { get; set; }//场站名称stationName
        public string depotNumber { get; set; }//场站编号stationNumber
        public double x, y;
        public void depotLocation(double x, double y)//定义场站位置x，y
        {
            this.x=x;
            this.y=y;
        }//
        public int depotCapacity { get; set; }//停车容量stationCapacity
        public int depotOpentime { get; set; }//开门时间stationOpentime
        public int depotClosetime { get; set; }//关门时间stationClosetime
        

        
    }
}
