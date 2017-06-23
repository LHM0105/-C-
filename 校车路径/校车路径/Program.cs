using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 校车路径.base_core;

namespace 校车路径
{
    class Program
    {
        static void Main(string[] args)
        {
            OD od = new OD();

            od.jisuan();

            suanfa suan = new suanfa(od);
            suan.read();

            suan.solutionMainsuanfa();
            suan.Write("result.txt");
            //           od.read();
            
            //遍历距离OD（数据字典）
          //  od.foreachDis();
           
            //od.write();

            // od.getTime("200001", "200122");
            //输出距离
 //           double dis = od.getDistance("100011", "900001");
  //          Console.WriteLine("距离="+dis);
            //输出时间
//            Console.WriteLine("时间=" + od.getTime("900001","100141"));
       

            Console.ReadKey();

        }
    }
}
