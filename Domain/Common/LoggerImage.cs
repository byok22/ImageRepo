using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public static class LoggerImage
    {
        private static string _pathLog = string.Empty;
      
        public static string PathLog
        {
            get
            {
                return _pathLog;
            }
            set
            {
                //current windows folder
                _pathLog = Environment.CurrentDirectory.ToString();
            }
        }

       //Write log in file and add date and time and type of log
        public static void WriteLog(string message, string type)
        {
            try
            {
                string path = Environment.CurrentDirectory.ToString();
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string fileName = string.Format("{0}\\{1}.txt", path, DateTime.Now.ToString("yyyyMMdd"));
                if (!System.IO.File.Exists(fileName))
                {
                    System.IO.File.Create(fileName).Close();
                }
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName, true))
                {
                    writer.WriteLine(string.Format("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, message));
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      


    }
}
