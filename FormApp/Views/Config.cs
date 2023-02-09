﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationsA.StoreRepository;
using FormApp.Functions;

namespace FormApp.Views
{
    public partial class Config : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        StoreRepository storeProceduresRepo;
        public Config()
        {
            InitializeComponent();
            storeProceduresRepo = new StoreRepository();
            EventsForms();
        }
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
        private void getServerPathFromConfig()
        {
            int processID = cboxProcess.SelectedValue != null ? Convert.ToInt32(cboxProcess.SelectedValue) : 0;
            string serverPath =  storeProceduresRepo.GetServerPathFromProcessID(processID);
            if (serverPath != null && serverPath != "")
            {
                txtServerPath.Text = serverPath;
            }

        }
        private void GetNuTimerFromConfig()
        {
            int timerID = ConfigFunctions.GetTimerFromConfig();
            if (timerID >= 60)
            {
                nuTimer.Value = timerID;
            }
        }
        private void getSourceFromConfig()
        {
            string source = ConfigFunctions.GetSourceFromConfig();
            if (source != null && source != "")
            {
                txtSource.Text = source;
            }

        }

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
            //save timer in app.config file code
            int timer = Convert.ToInt32(nuTimer.Value);
            ConfigFunctions.SetTimerInConfig(timer);

            //save source in app.config file 
            string source = txtSource.Text;
            ConfigFunctions.SaveSourceinConfig(source);

            //get server path from process id in StoreRepository
            string serverPath = storeProceduresRepo.GetServerPathFromProcessID(processID);
            //save server path in app.config file code
            ConfigFunctions.SaveTargetInConfig(serverPath);            
            MessageBox.Show("Configuración guardada correctamente");
            this.DialogResult = DialogResult.OK;

        }

        private void cboxProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            string processIDstr =  cboxProcess.SelectedValue.ToString();
            int processID;
            int.TryParse(processIDstr, out processID);
          
            string serverPath = storeProceduresRepo.GetServerPathFromProcessID(processID);
            txtServerPath.Text = serverPath;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
