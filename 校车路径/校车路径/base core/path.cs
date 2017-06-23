using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 校车路径.base_core;

namespace 校车路径
{
    class path
    {
        //定义数组存放站点的序列集合
        //int[] rout = new int[50];


        ///车辆ID、站点ID、学生人数count、到达时间（时间点）、
        ///服务时间（时间段）、离开时间（时间点）、目标学校
        ///
       
        //校车id（哪辆校车）
        public string vehicleID { get; set; }//校车ID
        public string stopID { get; set; }//站点ID
        public int stuCount { get; set; }//加上此站学生后，车上的学生人数
        public double travelTime { get; set; }//从上一个点到现在点的行驶时间
        public string arriveTime { get; set; }//到达时间（时间点）
        public double svcTime { get; set; }//服务时间（时间段）
        public string leaveTime { get; set; }//离开时间（时间点）
        public string EP_ID { get; set; }//目标学校ID
        
        //此路径所用总时间       
        public int totalTime { get; set; }

        public path()
        {
            
        }



        ///路径构建
        ///
        ///车从场站出发时间、到达站点时间、在站点停留时间、从站点出发时间、到此站车上人数、到校时间
        ///站点类，学校类，站点与学校的集合构成路径
        ///站点类（到达站点时间、在站点停留时间、从站点出发时间、到此车上人数、到此所用时间）
        ///学校类（到达学校时间、在学校停留时间、从学校出发时间、下车人数）
        ///场站-站点-学校  构成路径

    }
}
