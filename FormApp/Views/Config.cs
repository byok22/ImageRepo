using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationsA.StoreRepository;
using Domain.Common;
using Domain.Entities;
using FormApp.Functions;

namespace FormApp.Views
{
    public partial class Config : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string username;

        StoreRepository storeProceduresRepo;

        public Config(string username)
        {
            InitializeComponent();
            storeProceduresRepo = new StoreRepository();
            EventsForms();
            this.username = username;
        }  


        /// <summary>
        /// Events for forms
        /// </summary>    
        private void EventsForms()
        {
            this.MouseDown += CopyProcess_MouseDown;
            this.MouseMove += CopyProcess_MouseMove;
            this.MouseUp += CopyProcess_MouseUp;        
        }
        private void Config_Load(object sender, EventArgs e)
        {            
            filCboxProcess();
            getServerPathFromConfig();
            getSourceFromConfig();
            GetNuTimerFromConfig();
            GetNuTimerFileFromConfig();
        }
        private void CopyProcess_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void CopyProcess_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
      
        private void CopyProcess_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        /// <summary>
        /// Get server path from config file
        /// </summary>        
        private void getServerPathFromConfig()
        {
            int processID = cboxProcess.SelectedValue != null ? Convert.ToInt32(cboxProcess.SelectedValue) : 0;
            string serverPath =  storeProceduresRepo.GetServerPathFromProcessID(processID);
            if (serverPath != null && serverPath != "")
            {
                lblServerPath.Text = serverPath;
            }
        }


        /// <summary>
        /// Get timer from config file
        /// </summary>
        private void GetNuTimerFromConfig()
        {
            int timerID = ConfigFunctions.GetTimerFromConfig();
            if (timerID >= 60)
            {
                nuTimer.Value = timerID;
            }
        }

        /// <summary>
        /// Get timer from config file
        /// </summary>
        private void GetNuTimerFileFromConfig()
        {
            int timerID = ConfigFunctions.GetTimerFileFromConfig();
            if (timerID >= 5)
            {
                nuTimerFile.Value = timerID;
            }
        }


        /// <summary>
        /// Get source from config file
        /// </summary>
        private void getSourceFromConfig()
        {
            string source = ConfigFunctions.GetSourceFromConfig();
            if (source != null && source != "")
            {
                txtSource.Text = source;
            }

        }


        /// <summary>
        /// Fill combobox process
        /// </summary>
        private void filCboxProcess()
        {
            cboxProcess.DataSource = storeProceduresRepo.GetProcess();
            cboxProcess.DisplayMember = "Process";
            cboxProcess.ValueMember = "ProcessId";
            int processID = ConfigFunctions.GetProcessIdFromConfig();
            if (processID != 0)
            {
                cboxProcess.SelectedValue = processID;
            }

        }


        private void btnGetSource_Click(object sender, EventArgs e)
        {
            //get source folder
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the folder Source";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string source = txtSource.Text = fbd.SelectedPath;
                //save in app.config file code
                ConfigFunctions.SaveSourceinConfig(source);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //save process in app.config file code
            int processID = Convert.ToInt32(cboxProcess.SelectedValue);            
            ConfigFunctions.SaveProcessIdInConfig(processID);
            //save process name in app.config file code
            string processName = cboxProcess.Text;
            ConfigFunctions.SaveProcessNameInConfig(processName);
            //save timer in app.config file code
            int timer = Convert.ToInt32(nuTimer.Value);
            ConfigFunctions.SetTimerInConfig(timer);

            //Save Timer File in app.config File code
            int timerFile = Convert.ToInt32(nuTimerFile.Value);
            ConfigFunctions.SetTimerCreationFileInConfig(timerFile);

            //save source in app.config file 
            string source = txtSource.Text;
            ConfigFunctions.SaveSourceinConfig(source);

            //get server path from process id in StoreRepository
            string serverPath = storeProceduresRepo.GetServerPathFromProcessID(processID);
            //save server path in app.config file code
            ConfigFunctions.SaveTargetInConfig(serverPath);   
            //save username in log file
            LoggerImage.WriteLog(username, "Save Config");   
            try{
                 UserActivity userActivity = new UserActivity();
                userActivity.UserNT = username;
                userActivity.Activity = "Save Config";
                userActivity.FkProcess= processID;
                userActivity.Terminal = Environment.MachineName;
            
                var loginserted = storeProceduresRepo.insertUserActivity(userActivity);
            

            }catch(Exception ex){
               LoggerImage.WriteLog(username, "Error Save Config: " + ex.Message);
            }

            //Save Record UserActivity
            
           

                      
            MessageBox.Show("Configuración guardada correctamente");
            this.DialogResult = DialogResult.OK;
        }


        private void cboxProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            string processIDstr =  cboxProcess.SelectedValue.ToString();
            int processID;
            int.TryParse(processIDstr, out processID);          
            string serverPath = storeProceduresRepo.GetServerPathFromProcessID(processID);
            lblServerPath.Text = serverPath;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
