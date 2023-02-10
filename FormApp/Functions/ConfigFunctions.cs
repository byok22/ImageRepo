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

        /// <summary>
        /// SaveSourceinConfig
        /// </summary>
        public static void SaveSourceinConfig(string source)
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


        /// <summary>
        /// SaveTargetInConfig
        /// </summary>    
        public static void SaveTargetInConfig(string target)
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


        /// <summary>
        /// SaveProcessNameInConfig
        /// </summary>
        public static void SaveProcessNameInConfig(string processName)
        {
            //save in app.config file code
            var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //create new key if not exist
            if (config.AppSettings.Settings["ProcessName"] == null)
            {
                config.AppSettings.Settings.Add("ProcessName", processName);
            }
            else
            {
                config.AppSettings.Settings["ProcessName"].Value = processName;
            }

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }


        /// <summary>
        /// SaveProcessIdInConfig
        /// </summary>
        public static void SaveProcessIdInConfig(int processId)
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


        /// <summary>
        /// GetSourceFromConfig
        /// </summary>
        public static string GetSourceFromConfig()
        {
            string source = ConfigurationManager.AppSettings["Source"];
            return source;
        }


        /// <summary>
        /// GetTargetFromConfig
        /// </summary>        
        public static string GetTargetFromConfig()
        {
            string target = ConfigurationManager.AppSettings["Target"];
            return target;
        }
               
        /// <summary>
        /// GetProcessIdFromConfig  
        /// </summary>    
        public static int GetProcessIdFromConfig()
        {
            string processIdstr = ConfigurationManager.AppSettings["ProcessId"];
            int processID;
            int.TryParse(processIdstr,out processID);
            return processID;
        }


        public static string GetProcessNameFromConfig()
        {
            string processName = ConfigurationManager.AppSettings["ProcessName"];
            return processName;
        }


        public static void SetTimerInConfig(int time)
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


        public static int GetTimerFromConfig()
        {
            string timerstr = ConfigurationManager.AppSettings["Timer"];
            int timer;
            int.TryParse(timerstr, out timer);
            return timer;

        }
    }
}
