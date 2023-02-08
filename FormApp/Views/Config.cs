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
using FormApp.Functions;

namespace FormApp.Views
{
    public partial class Config : Form
    {
        StoreRepository storeProceduresRepo;
        public Config()
        {
            InitializeComponent();
            storeProceduresRepo = new StoreRepository();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            filCboxProcess();
            getServerPathFromConfig();
            getSourceFromConfig();
            getNuTimerFromConfig();
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
        private void getNuTimerFromConfig()
        {
            int timerID = ConfigFunctions.getTimerFromConfig();
            if (timerID != 0)
            {
                nuTimer.Value = timerID;
            }
        }
        private void getSourceFromConfig()
        {
            string source = ConfigFunctions.getSourceFromConfig();
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
            int processID = ConfigFunctions.getProcessIdFromConfig();
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
                ConfigFunctions.saveSourceinConfig(source);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //save process in app.config file code
            int processID = Convert.ToInt32(cboxProcess.SelectedValue);
            ConfigFunctions.saveProcessIdInConfig(processID);
            //save timer in app.config file code
            int timer = Convert.ToInt32(nuTimer.Value);
            ConfigFunctions.setTimerInConfig(timer);

            //save source in app.config file 
            string source = txtSource.Text;
            ConfigFunctions.saveSourceinConfig(source);

            //get server path from process id in StoreRepository
            string serverPath = storeProceduresRepo.GetServerPathFromProcessID(processID);
            //save server path in app.config file code
            ConfigFunctions.saveTargetInConfig(serverPath);            
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
