using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FormApp.Functions
{
    public static class ConfigFunctions
    {
        public static void saveSourceinConfig(string source)
        {
            //save in app.config file code
            var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //create new key if not exist
            if (config.AppSettings.Settings["Source"] == null)
            {
                config.AppSettings.Settings.Add("Source", source);
            }
            else
            {
                config.AppSettings.Settings["Source"].Value = source;
            }            
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        public static void saveTargetInConfig(string target)
        {
            //save in app.config file code
            var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //create new key if not exist
            if (config.AppSettings.Settings["Target"] == null)
            {
                config.AppSettings.Settings.Add("Target", target);
            }
            else
            {
                config.AppSettings.Settings["Target"].Value = target;
            }
           
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        public static string getSourceFromConfig()
        {
            string source = ConfigurationManager.AppSettings["Source"];
            return source;
        }
        public static string getTargetFromConfig()
        {
            string target = ConfigurationManager.AppSettings["Target"];
            return target;
        }
        public static void saveProcessIdInConfig(int processId)
        {
            //save in app.config file code
            var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //create new key if not exist
            if (config.AppSettings.Settings["ProcessId"] == null)
            {
                config.AppSettings.Settings.Add("ProcessId", processId.ToString());
            }
            else
            {
                config.AppSettings.Settings["ProcessId"].Value = processId.ToString();
            }

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        public static int getProcessIdFromConfig()
        {
            string processIdstr = ConfigurationManager.AppSettings["ProcessId"];
            int processID;
            int.TryParse(processIdstr,out processID);
            return processID;
        }
        public static void setTimerInConfig(int time)
        {
            //save in app.config file code
            var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //create new key if not exist
            if (config.AppSettings.Settings["Timer"] == null)
            {
                config.AppSettings.Settings.Add("Timer", time.ToString());
            }
            else
            {
                config.AppSettings.Settings["Timer"].Value = time.ToString();
            }
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        public static int getTimerFromConfig()
        {
            string timerstr = ConfigurationManager.AppSettings["Timer"];
            int timer;
            int.TryParse(timerstr, out timer);
            return timer;

        }
    }
}
