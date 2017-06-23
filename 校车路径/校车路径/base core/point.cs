using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 校车路径.base_core
{
    class point
    {
        public string pointID { get; set; }//ID（学校or站点or场站）
        public int studentsNum { get; set; }//此站等待学生人数
        public double arrival_time { get; set; }//车辆到达此站时间
        public double travel_time { get; set; }//车辆到达此站点前行驶的时间（与上一个point之间）
        public int SvcTime { get; set; }//车辆在此站停留时间
        public double wait_time { get; set; }//车辆在此处停留时间（相当于小场站，一般停留较长时间）

        ///目的地
        public string destination { get; set; }
    }
}
