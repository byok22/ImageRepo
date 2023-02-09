using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Diagnostics;

namespace ServicesWindowsoJF
{
    public partial class ServiceWindowsJF : Form
    {
        private readonly Timer _timer;
        public ServiceWindowsJF()
        {
            InitializeComponent();
            _timer = new Timer(1000)
            {
                AutoReset = true
            };
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            var runningProcessByName = Process.GetProcessesByName("CopyProcess");
            if (runningProcessByName.Length == 0)
            {
                try
                {
                    Process.Start(@"CopyProcess");
                }
                catch (Exception ex)
                {

                }


            }
            _timer.Start();
        }

        private void FaceService_Load(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            form.ShowInTaskbar = false;
            form.Opacity = 0;
        }
    }
}
