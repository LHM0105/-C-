using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 校车路径.base_core;
namespace 校车路径.base_core
{
    class suanfa
    {
        string s;//要写入文件的内容（存在字符串s中）
        //从文件中读取出的stops，school，Mixed_SBPR_BenchMark中站点最多为2000，学校最多为100
        string[][] stopsArr = new string[2000][];//ID	X	Y	EP_ID(目标学校)	STUDENT_COUNT
        string[][] schoolArr =new string[100][]; //ID X   Y AMEARLY AMLATE

        List<school> schoolsList = new List<school>();//学校列表

        OD od = new OD();
        public suanfa(OD od1)
        {
            this.od = od1;
        }

        /// <summary>
        /// 读取文件，//从文件中读取出stops和school分别存在上面两个交错数组中
        /// </summary>
        public void read()
        {
            
            try
            {
                //创建StreamReader对象读取文件
                StreamReader sr1 = new StreamReader("Stops.txt");//读取stop，和school,此处先读取一个存储在数组arr1中；
                String line1,line2;//一行中读取的内容
                StreamReader sr2 = new StreamReader("Schools.txt");
                line1 = sr1.ReadLine();//读取
                line2 = sr2.ReadLine();
                //stop文件--存到-数组
                for (int i = 0; (line1 = sr1.ReadLine()) != null; i++)
                {
                    for (int j = 0; j <5; j++)
                    {
                        //把每行读取到的内容以空格为分隔符分割成一维数组，同时赋值给交错数组的每一行
                        stopsArr[i] = line1.Split('\t');
                        Console.Write(stopsArr[i][j]+"\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("########################################################");
                //school文件存到数组中
                for (int i = 0; (line2 = sr2.ReadLine()) != null; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        //把每行读取到的内容以空格为分隔符分割成一维数组，同时赋值给交错数组的每一行
                        schoolArr[i] = line2.Split('\t');
                        Console.Write(schoolArr[i][j]+"\t");
                    }
                     Console.WriteLine();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            } 
        }
       
        ///扫描算法（结果是得到站点的stopsList）
        public void scan(string schoolID,double schoolX,double schoolY, SortedList<double, stops> stopsList)
        {
           ///stop和school的坐标范围是[0,211200]英尺，depot在中心，则坐标为[105600,105600]
           ///sin，cos分别是以depot为中心，stop的正弦余弦值
           ///asin是stop的正弦对应的角度，因函数自身原因，其范围是[-90,90]
           ///acos是stop的余弦对应的角度，范围是[0,180]
           
            double sin, cos,asin,acos, 
                x, y,//站点坐标
                Ox =schoolX, Oy = schoolY,//学校坐标
                jiaodu;
            string ID;//站点ID
            for (int i=0;stopsArr[i]!=null;i++)
            {
                stops stop = new stops();
                x=double.Parse(stopsArr[i][1]);
                y=double.Parse(stopsArr[i][2]);
                ID=stopsArr[i][0];
                //以depot为原点，求各stop对应的正弦余弦
                sin = (x - Ox) / Math.Sqrt(Math.Pow((x - Ox), 2) + Math.Pow((y - Oy), 2));
                cos=(y-Oy)/ Math.Sqrt(Math.Pow((x - Ox), 2) + Math.Pow((y - Oy), 2));
                ///求正弦值对应的弧度asin（0，pi），实际上也可能是asin+pi，
                ///所以下面再根据acos判断
                asin = Math.Asin(sin)*180/Math.PI;//通过乘180/Math.PI转换成角度
                acos = Math.Acos(cos)*180/Math.PI;
                if(sin>=0&&cos>=0)
                {
                    jiaodu = asin;
                }
                else if(sin>=0&&cos<=0)
                {
                    jiaodu = acos;
                }
                else if(sin<=0&&cos<=0)
                {
                    jiaodu = 360 - acos;
                }
                else if(sin<=0&&cos>=0)
                {
                    jiaodu =360+ asin;
                }
                else
                {
                    jiaodu = 0;
                }

                stop.stopID = stopsArr[i][0];
                stop.stopX = double.Parse(stopsArr[i][1]);
                stop.stopY = double.Parse(stopsArr[i][2]);
                stop.EP_ID = stopsArr[i][3];
                stop.studentCount = int.Parse(stopsArr[i][4]);
                //属于目标学校的加入站点列表
                if(stop.EP_ID==schoolID)
                {
                    stopsList.Add(jiaodu, stop);
                }
                      
            }

            //遍历排序列表stopList
            Console.WriteLine("每个学校的站点列表*****************************************************");
            for (int i = 0; i < stopsList.Count; i++)
            {
                Console.WriteLine(i+"\tkey:" + stopsList.ElementAt(i).Key + 
                    "\tstopID:" + stopsList.ElementAt(i).Value.stopID
                    + "\tstopX:" + stopsList.ElementAt(i).Value.stopX + 
                    "\tstopY:" + stopsList.ElementAt(i).Value.stopY +
                    "\tEP_ID:" + stopsList.ElementAt(i).Value.EP_ID +
                    "\tstudentCount:" + stopsList.ElementAt(i).Value.studentCount);
            }
           
           
        }//end public void scan()

        /// <summary>
        /// 讲读取出的学校存入列表schoolsList
        /// </summary>
        public void schools()
        {
            for (int i = 0; schoolArr[i] != null; i++)
            {
                school aSchool = new school();//创建一个school对象
                string schoolX = stopsArr[i][1];
                ///输出此school的坐标
//                Console.WriteLine("x=" + schoolArr[i][1] + "\ty=" + schoolArr[i][2]);
                
                aSchool.schoolID = schoolArr[i][0];
                aSchool.schoolX = double.Parse(stopsArr[i][1]);
                aSchool.schoolY = double.Parse(schoolArr[i][2]);
                aSchool.AMEarly = schoolArr[i][3];
                aSchool.AMLate = schoolArr[i][4];
                schoolsList.Add(aSchool);
               
            } //end for
            ///遍历schoolsList
            Console.WriteLine("所有的学校******************************************************************************");
            Console.WriteLine("schoolID schoolX\tschooY\tAMEarly\tAMLate");
            for(int i=0; i < schoolsList.Count; i++)
            {
                Console.WriteLine(schoolsList[i].schoolID + "\t" + schoolsList[i].schoolX + "\t"
                    + schoolsList[i].schoolY + "\t" + schoolsList[i].AMEarly + "\t" 
                    + schoolsList[i].AMLate);
            }
        }

        ///核心算法
       
        public void solutionMainsuanfa()
        {
            schools();
            
            ///创建path对象depot(每条路径的开始)
            path beginDepot = new path();
            path endDepot = new path();//每条路径的结束
            path aSchool = new path();//每条路径的目标学校

            //vehicleID不确定
            beginDepot.stopID = "900001";//场站ID为900001
            beginDepot.travelTime = 0;//beginDepot之前的行驶时间为0
            //将车辆到达beginDepot的时间设为1点10分,从0开始计时，按秒记，则为70*60s=4200s
            beginDepot.arriveTime = "011000";//后面会被覆盖
            beginDepot.stuCount = 0;//没有学生
            beginDepot.svcTime = 0;//服务时间为0
            //将车辆从场站出发时间定为4点50，（4*60+50）*60=17400s
            beginDepot.leaveTime = "045000";//后面会被覆盖
            //车辆从场站出发的时间由学校的最早到校时间决定

            //目标学校不确定，根据for循环里的学校ID而定

            //遍历学校，对每个学校执行以下操作
            for (int i=0;i<schoolsList.Count;i++)
            {
                ///存放站点的排序列表
                SortedList<double, stops> stopsList = new SortedList<double, stops>();
                ///调用扫描算法,选出此学校的所有站点，且按角度从小到大存在stopsList中
                scan(schoolsList[i].schoolID, schoolsList[i].schoolX, schoolsList[i].schoolY,stopsList);
                ///调用方法aSchoolSuanfa,得到每个学校的所有路径
                aSchoolSuanfa(stopsList, i,beginDepot,endDepot);
            }//end for(i<schoolList.count)
        }
        
        /// <summary>
        /// 针对一个学校的操作,得到的是一所学校是所有路径
        /// </summary>
        /// <param name="stopsList"></param>
        /// <param name="i"></param>
        /// <param name="beginDepot"></param>
        /// <param name="endDepot"></param>
        public void aSchoolSuanfa(SortedList<double, stops> stopsList, int i,path beginDepot,path endDepot)
        {
            ///创建path列表,将depot、school和符合约束的stop添加进去
            List<path> paths = new List<path>();
            //添加到路径中的目标学校
            path schoolInPath = new path();
            paths.Add(beginDepot);//将场站添加进paths

            for (int j = 0; j < stopsList.Count; j++)//遍历所有点，条件应设为j<所有站点长度，还是列表不为空？
            {
                path Apath = new path();
                ///第一个站点
                Apath.stopID = stopsList.ElementAt(j).Value.stopID;//获取目前站点ID

                ///行驶时间Apath.travelTime=od.getTime(上个点ID，目前点ID),
                ///上一个点的ID=存入paths中最后一条的stopID=paths[paths.Count-1]
                Apath.travelTime = (int)od.getTime(paths[paths.Count - 1].stopID, Apath.stopID);

                ///加上此站点的学生总人数=到上一个站点车上的人数 + 这个站点的人数
                Apath.stuCount = paths[paths.Count - 1].stuCount + stopsList.ElementAt(j).Value.studentCount;

                //服务时间，学生上车所用时间
                Apath.svcTime = stopsList.ElementAt(j).Value.studentCount * 2.6 + 19;

                double maxTravelTime = 0;//最大乘车时间

                ///如果路径列表中只有一项，即只有depot
                if (paths.Count == 1)
                {
                    //此点是第一个站点，学生最大乘车时间=此点到学校的行驶时间+在此点的服务时间
                    maxTravelTime = od.getTime(schoolsList[i].schoolID, Apath.stopID) + Apath.svcTime;

                }
                else
                {
                    //计算最大乘车时间
                    //第一个stop的服务时间+此点到学校的travelTime + paths中最后一点到此点的travelTime + 此点服务时间
                    maxTravelTime = paths[1].svcTime + od.getTime(paths[paths.Count - 1].stopID, Apath.stopID)
                                + od.getTime(schoolsList[i].schoolID, Apath.stopID) + Apath.svcTime;
                    for (int b = paths.Count - 1; b > 1; b--)
                    {
                        double pathiTime = paths[b].travelTime + paths[b].svcTime;
                        maxTravelTime += pathiTime;
                    }
                }

                if (Apath.stuCount > 66)
                {

                    continue; //大于的话则跳出本次循环，否则继续判断

                }
                else if (maxTravelTime > 2700)//总的乘车时间是否大于45分钟
                {
                    continue;//大于的话则跳出本次循环，否则将此点加入路径paths
                }
                else
                {
                    //满足“学生人数不大于66”，且到校不会有学生超出最大乘车时间，将此点加入路径
                    paths.Add(Apath);
                    stopsList.RemoveAt(j);//移除站点列表中对应下标的那一项
                }//end if
            }//end for(j< stopsList.Count)

            //给路径中的 目标学校站点 赋值
            schoolInPath.stopID = schoolsList[i].schoolID;
            schoolInPath.travelTime = (int)od.getTime(paths[paths.Count - 1].stopID, schoolInPath.stopID);
            //学校arriveTime定为最早到校时间-10分钟
            // schoolInPath.arriveTime = schoolsList[i].AMEarly - 10;
            double hour = double.Parse(schoolsList[i].AMEarly.Substring(0, 1));
            double minute = double.Parse(schoolsList[i].AMEarly.Substring(1, 2));
            double schoolArrivetime = hour * 3600 + minute * 60 - 10 * 60;
            int inthour = (int)schoolArrivetime / 3600;
            int intminute = ((int)schoolArrivetime % 3600) / 60;
            schoolInPath.arriveTime = inthour.ToString().PadLeft(2, '0') + intminute.ToString().PadLeft(2, '0') + "00";
            
            //stuCount到达学校这一点的学生人数是上一个站点的学生人数（都下车了）
            schoolInPath.stuCount = paths[paths.Count - 1].stuCount;
            //svcTime为学生下车所用时间
            schoolInPath.svcTime = schoolInPath.stuCount * 2.6 + 19;
            //学校leaveTime
            double schoolLeavetime = schoolArrivetime + schoolInPath.svcTime;
            int intSchLeaveHour1 = (int)schoolLeavetime / 3600;
            int intSchLeaveMinute = ((int)schoolLeavetime % 3600) / 60;
            int intSchLeaveSecond = (int)schoolLeavetime % 60;
            schoolInPath.leaveTime = intSchLeaveHour1.ToString().PadLeft(2, '0') + intSchLeaveMinute.ToString().PadLeft(2, '0') + intSchLeaveSecond.ToString().PadLeft(2, '0');

            //目标学校EP_ID
            schoolInPath.EP_ID = schoolsList[i].schoolID;
            //路径添加学校school
            paths.Add(schoolInPath);

            //路径中的结束场站endDepot
            endDepot.stopID = "900001";
            endDepot.travelTime = (int)od.getTime(paths[paths.Count - 1].stopID, endDepot.stopID);
            //到达场站时间endDepotArriveTime=上一站点离开时间+travelTime
            double endDepothour = double.Parse(paths[paths.Count - 1].leaveTime.Substring(0, 2));
            double endDepotminute = double.Parse(paths[paths.Count - 1].leaveTime.Substring(2, 2));
            double endDepotsecond = double.Parse(paths[paths.Count - 1].leaveTime.Substring(4, 2));
            double endDepotarrivetime = endDepothour * 3600 + endDepotminute * 60 + endDepotsecond + endDepot.travelTime;
            int intendDepothour = (int)endDepotarrivetime / 3600;
            int intendDepotminute = ((int)endDepotarrivetime % 3600) / 60;
            int intendDepotsecond = (int)endDepotarrivetime % 60;
            endDepot.arriveTime = intendDepothour.ToString().PadLeft(2, '0') + intendDepotminute.ToString().PadLeft(2, '0') + intendDepotsecond.ToString().PadLeft(2, '0');
            //场站endDepot的svcTime
            endDepot.svcTime = 0;
            //离开时间##
            endDepot.leaveTime = "null";
            //目标学校##
            endDepot.EP_ID = schoolsList[i].schoolID;
            paths.Add(endDepot);//将endPoint加入路径

            //给路径paths中的站点写入到达时间和离开时间  以及目标学校
            for (int c = paths.Count - 2; c > 0; c--)
            {
                //目标学校1
                paths[c - 1].EP_ID = schoolsList[i].schoolID;
                //离开时间
                double pathsiArriveHour = double.Parse(paths[c].leaveTime.Substring(0, 2));
                double pathsiArriveMinute = double.Parse(paths[c].leaveTime.Substring(2, 2));
                double pathsiArriveSecond = double.Parse(paths[c].leaveTime.Substring(4, 2));
                double pathsiArrivetime = pathsiArriveHour * 3600 + pathsiArriveMinute * 60 + pathsiArriveSecond;
                double pathsiiLeavetime = pathsiArrivetime - paths[c].travelTime;
                int intpathsiiLeaveHour = (int)pathsiiLeavetime / 3600;
                int intpathsiiLeaveMinute = ((int)pathsiiLeavetime % 3600) / 60;
                int intpathsiiLeaveSecond = (int)pathsiiLeavetime % 60;
                paths[c - 1].leaveTime = intpathsiiLeaveHour.ToString().PadLeft(2, '0') + intpathsiiLeaveMinute.ToString().PadLeft(2, '0') + intpathsiiLeaveSecond.ToString().PadLeft(2, '0');
                //到达时间
                double pathsiiArrivetime = pathsiiLeavetime - paths[c - 1].svcTime;
                int intpathsiiArriveHour = (int)pathsiiLeavetime / 3600;
                int intpathsiiArriveMinute = ((int)pathsiiLeavetime % 3600) / 60;
                int intpathsiiArriveSecond = (int)pathsiiLeavetime % 60;
                paths[c - 1].arriveTime = intpathsiiLeaveHour.ToString().PadLeft(2, '0') + intpathsiiLeaveMinute.ToString().PadLeft(2, '0') + intpathsiiLeaveSecond.ToString().PadLeft(2, '0');

            }//end for (int c = paths.Count - 2; c > 0; c--)
            //此路径用SHI，totalTime=？
            s += "目标学校：" + schoolsList[i].schoolID + "\r\n";
            Console.WriteLine("目标学校：" + schoolsList[i].schoolID );
///输出目前paths中的数据
            Console.WriteLine("seq\tID\ttravelTime\tarriveTime\tstuCount\tsvcTime\tleaveTime\tEP_ID");
            s+= "seq\tID\ttravelTime\tarriveTime\tstuCount\tsvcTime\tleaveTime\tEP_ID \r\n";
            
            for (int k = 0; k < paths.Count; k++)
            {
                
                s += k + "\t" + paths[k].stopID + "\t" + paths[k].travelTime
                    + "\t" + paths[k].arriveTime + "\t" + paths[k].stuCount
                    + "\t" + paths[k].svcTime + "\t" + paths[k].leaveTime
                    + "\t" + paths[k].EP_ID+"\r\n";
                Console.WriteLine(k + "\t" + paths[k].stopID + "\t" + paths[k].travelTime
                    + "\t" + paths[k].arriveTime + "\t" + paths[k].stuCount
                    + "\t" + paths[k].svcTime + "\t" + paths[k].leaveTime
                    + "\t" + paths[k].EP_ID);
            }
            
///递归:调用自身，i为学校下标，此时的stopList是remove之后的
            if (stopsList.Count > 0)
            {
                aSchoolSuanfa(stopsList, i, beginDepot, endDepot);
            }
        }//end public void aSchoolSuanfa


        //将结果写入文件
        public void Write(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(s);
            
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}
