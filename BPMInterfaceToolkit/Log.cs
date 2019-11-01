using System;
using System.IO;

namespace BPMInterfaceToolkit
{
    public class Log
    {
        static string logpath = System.AppDomain.CurrentDomain.BaseDirectory;//软件所在路径
        static string filename = "Log.txt";
        static string recordpath = System.AppDomain.CurrentDomain.BaseDirectory + "Record\\";
        static string recordfilename = DateTime.Today.ToString("yyyy-MM-dd") + ".txt";

        //public string logmessage;
        //public string logname;

        public static void WriteLog(string logmessage)
        {
            //判断日志文件是否存在，不存在，就创建
            if (File.Exists(logpath + filename))
            {
                FileInfo fi = new FileInfo(logpath + filename);

                if (fi.Length / 1024 / 1024 > 5) //大于5M则备份日志
                {
                    fi.MoveTo(logpath + DateTime.Now.Year.ToString().Trim() + DateTime.Now.Month.ToString().Trim() + DateTime.Now.Day.ToString().Trim());
                }

            }
            File.AppendAllText(logpath + filename, logmessage + "  " + DateTime.Now.ToString().Trim() + "\r\n");
        }

        public static void WriteRecord(string message)
        {
            if (!Directory.Exists(recordpath))
            {
                Directory.CreateDirectory(recordpath);
            }
            File.AppendAllText(recordpath + recordfilename, message);
        }

    }
}
