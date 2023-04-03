using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Metrics
    {
        public static double GetMemoryUsageMB()
        {
            Process ProcessName = Process.GetCurrentProcess();
            var ram = new PerformanceCounter("Process", "Working Set - Private", ProcessName.ProcessName, true);
            return Math.Round(ram.NextValue()/1024/1024,2);
        }
        public static double GetCPUUsagePercent()
        {
            Process ProcessName = Process.GetCurrentProcess();
            var cpu = new PerformanceCounter("Process", "% Processor Time", ProcessName.ProcessName, true);
            //cpu.NextValue();
            cpu.NextValue();
            return (cpu.NextValue());
        }
        struct IO_COUNTERS
        {
            public ulong ReadOperationCount;
            public ulong WriteOperationCount;
            public ulong OtherOperationCount;
            public ulong ReadTransferCount;
            public ulong WriteTransferCount;
            public ulong OtherTransferCount;
        }

        [DllImport("kernel32.dll")]
        private static extern bool GetProcessIoCounters(IntPtr ProcessHandle, out IO_COUNTERS IoCounters);
        public static float GetDiskUsage()
        {
            Process ProcessName = Process.GetCurrentProcess();
            IO_COUNTERS counters;                       
            try
            {
                GetProcessIoCounters(ProcessName.Handle, out counters);
                return (counters.ReadTransferCount/1024/1024);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                return 0;
            }
            return 0;
        }

    }
}
