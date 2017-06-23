using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 校车路径.base_core
{
    class OD
    {
        string[] points = new string[999];//用于暂时存放读取内容，以便计算使用；

        public OD()
        {
           
        }


        //读取文件信息
        public void read()
        {

            /// <summary>
            /// 读取文件
            /// </summary>
            try
            {
                //创建StreamReader对象读取文件
                StreamReader sr1 = new StreamReader("Schools.txt");//读取stop，和school,此处先读取一个存储在数组arr1中；
                String line1, line2,line3;//一行中读取的内容
                int i = 0;
                line1 = sr1.ReadLine();//读取
                //Console.WriteLine(line + "kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
                //将读取到的每一行的内容暂存入一维数组point[]
                for (; (line1 = sr1.ReadLine()) != null; i++)
                {
                    points[i] = line1;
                    //try
                    // Console.WriteLine(i+"\t"+point[i]);                   
                }
                //Console.WriteLine("school完。");

                int i2 = i;
                StreamReader sr2 = new StreamReader("Stops.txt");//读取stop，和school,此处先读取另一个，继续存储在arr1中；
                line2 = sr2.ReadLine();//没有这句时，数组中只有奇数行的信息？？？
                for (; (line2 = sr2.ReadLine()) != null; i2++)
                {
                    points[i2] = line2;
                }
                int i3 = i2;
                StreamReader sr3 = new StreamReader("depot.txt");//读取stop，和school,此处先读取另一个，继续存储在arr1中；
                line3 = sr3.ReadLine();//没有这句时，数组中只有奇数行的信息？？？
                for (; (line3 = sr3.ReadLine()) != null; i3++)
                {
                    points[i3] = line3;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
 /*          //输出读取到的内容

            for (int j = 0; j < points.Length; j++)
            {
                if (points[j] != null)
                {
                    Console.WriteLine(j + "\t" + points[j]);
                }
            }
*/
        }






        /// <summary>
        /// 距离OD
        /// </summary>
        public double getDistance(string id1,string id2)
        {
            //jisuan();
            return disOD[id1+id2];
            
        }

        //时间OD
        public double getTime(string id1, string id2)
       {
           // jisuan();
             return timeOD[id1 + id2];
        }


        //建立数据字典，键为id1+id2（字符串），值为距离
        Dictionary<string, double> disOD = new Dictionary<string, double>();
        //建立数据字典，键为id1+id2（字符串），值为时间
        Dictionary<string, double> timeOD = new Dictionary<string, double>();

        /// <summary>
        /// 计算OD矩阵的方法
        /// </summary>
        public void jisuan(){
            //int m, n;
            read();
            int q;
            string id1, id2;//定义id变量
            double x1, x2, y1, y2,distance,time;//定义变量坐标点，距离，时间
            string[] pointX = { };//暂存读取的每一行数据，以\t为分隔符，将每一行数据分割，存入arr3，即id=pointX[0]，x=pointX[1],y=pointX[2]
            string[] pointY = { };//同arr3，第二次读arr1存的
 //            double V = 8.94;//行驶速度（单位，m/s）
            double V = 29.33;//行驶速度（单位，英尺/s）


            for (int p=0;pointX !=null ; p++)//p为arr1的行数
			{
                if (points[p] != null)
                {
                    pointX = points[p].Split('\t');//将
                }
                else
                {
                    break;
                }
                //m = int.Parse(pointX[0])%10000 + int.Parse(pointX[0])/1000*10;//读取每一行的ID,
                id1 = pointX[0];
                x1 =double.Parse(pointX[1]);//读取每一行的x坐标,
                y1=double.Parse(pointX[2]);//读取每一行的y坐标,              
                for (q = 0; pointY != null; q++)
                {
                    if (points[q] != null)
                    {
                        pointY = points[q].Split('\t');
                    }
                    else
                    {
                        break;
                    }
                    //n = int.Parse(pointY[0])%10000+int.Parse(pointY[0]) / 1000 * 10;//读取每一行的ID
                    id2 = pointY[0];
                    x2 = double.Parse(pointY[1]);//读取每一行的x坐标
                    y2 = double.Parse(pointY[2]);//读取每一行的y坐标

                    distance = Math.Abs(x2 - x1) + Math.Abs(y2 - y1);//计算距离
                    disOD.Add(id1 + id2, distance);//存入距离数据字典

                    time = distance / V;//计算时间
                    timeOD.Add(id1 + id2, time);//存入时间数据字典

                    // Console.WriteLine("id1={0},id2={1},key={2},value={3}.",id1,id2, id1 + id2, disOD[id1 + id2]);
                    //arr2[m, n] =(Math.Abs(x2 - x1) + Math.Abs(y2 - y1)).ToString();//计算距离，并存入数组相应位置
                    //arr5[m, n] = (double.Parse(arr2[m, n]) / V).ToString();

                    //Console.WriteLine("id为" + pointX[0]+ "的（站点/学校）与ID为" + pointY[0] + "的（站点/学校）之间的距离为" + arr2[m,n]);
                    //Console.ReadKey();
                }
               
                //Console.WriteLine("-----以上是一个id为" + pointX[0]+ "的（站点 / 学校）与任何别的ID之间的距离--------");
            }
            
        }

        ///遍历OD（数据字典）
        public void foreachDis()
        {
            foreach (KeyValuePair<string, double> kvp in disOD )
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine("距离OD完*****************************************");
        }
        public void foreachTime()
        {
            foreach (KeyValuePair<string, double> kvp in timeOD)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine("时间OD完*****************************************");
        }


    }
}
