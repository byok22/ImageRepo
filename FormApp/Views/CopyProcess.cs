using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationsA.Functions;
using FormApp.Functions;
using Domain.Common;
using Domain.Entities;
using ApplicationsA.StoreRepository;
using System.Diagnostics;
using System.IO;
using Domain;

namespace FormApp.Views
{
    public partial class CopyProcess : Form
    {
        #region Private Variables
        private bool statusSS = false;
        private bool inProcess = false;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string sourcePath = string.Empty;
        private string destinationPath = string.Empty;
        private int timerValue = 0;
        private string imagesUpdated = string.Empty;
        private int process_ID = 0;
        private string processName;
        private int timerFileValue = 0;

        #endregion Private Variables

        #region Load
        public CopyProcess()
        {
            InitializeComponent();
            KillDuplicateProcesses();
            NewComponents();
            EventsForms();
            GetConfigs();       
        }
        /// <summary>
        /// KillDuplicateProcesses
        /// </summary>
        private void KillDuplicateProcesses()
        {
            Process process = Process.GetCurrentProcess();
            var arrayProcesses = (Process.GetProcessesByName(process.ProcessName));
            if (arrayProcesses.Length > 1)
            {
                foreach (var oneProcess in arrayProcesses)
                {
                    if (oneProcess.Id != process.Id)
                        oneProcess.Kill();
                }
            }
        }
        /// <summary>
        /// NewComponents
        /// </summary>
        private void NewComponents()        
        {
            notifyIcon.Visible = false;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;  
            notifyIcon.Text = "Copy Images";          
        }
        private void CopyProcess_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.lblVersion.Text = $"Copy Images {Application.ProductVersion}   © Software Development Team GDL ";

            var runningProcessByName = Process.GetProcessesByName("ServicesWindowsoJF");
            if (runningProcessByName.Length == 0)
            {
                System.Diagnostics.Process.Start("ServicesWindowsoJF.exe");
            }
            Task.Run(() => CopyImagesToRedFolderAsync());
            Task.Run(() => MetricsFun());
        }
        #endregion

        #region Form Events
        private void EventsForms()
        {
            this.FormClosing += CopyProcess_FormClosing;
            this.MouseDown += CopyProcess_MouseDown;
            this.MouseMove += CopyProcess_MouseMove;
            this.MouseUp += CopyProcess_MouseUp;
            pnlMenu.MouseDown += PnlMenu_MouseDown;
            pnlMenu.MouseMove += PnlMenu_MouseMove;
            pnlMenu.MouseUp += PnlMenu_MouseUp;
        }


        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        private void CopyProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;            
            }
        }


        private void btnMinimize_Click(object sender, EventArgs e)
        {
            //Minimize the form to the system tray
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
        }

        private void PnlMenu_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void PnlMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void PnlMenu_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
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
        #endregion

        #region Button Process Events
        private void btnConfig_Click(object sender, EventArgs e)
        {
            //Verify User and Password before open Config
            string typeLogin = "Config";
            Login login = new Login(typeLogin);
             login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() == DialogResult.OK)
            {
                
                Config config = new Config(login.username);
                statusSS = false;
                btnStarStop.Text = "Start";                
                config.StartPosition = FormStartPosition.CenterParent;
                var result = config.ShowDialog();
                if (result == DialogResult.OK)
                {
                    statusSS = true;
                    GetConfigs();
                    Task.Run(() => CopyImagesToRedFolderAsync());
                    return;
                }
                if(result == DialogResult.Cancel)
                {
                    statusSS = true;                  
                    Task.Run(() => CopyImagesToRedFolderAsync());
                    return;
                }
                statusSS = false;
            }            
            MessageBox.Show("You don't have permission to access this module", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void btnStarStop_Click(object sender, EventArgs e)
        {
            //Verify User and Password before stop or start process
            string typeLogin = "StartStop";
            Login login = new Login(typeLogin);            
                //Logger the user who start or stop the process               
                if (statusSS)
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        LoggerImage.WriteLog($"NT User {login.username} stop the process", "StartStop");
                        statusSS = false;
                        btnStarStop.Text = "Start";
                        return;
                    }
                    return;
                }              
                statusSS = true;
                btnStarStop.Text = "Stop";
                Task.Run(() => CopyImagesToRedFolderAsync());
                GetConfigs();
                return;
            
            MessageBox.Show("You don't have permission stop/start the Proccess", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
        }
        #endregion

        #region Functions
        /// <summary>
        /// CopyImagesToRedFolderAsync
        /// </summary>
        /// <returns></returns>
        private async Task CopyImagesToRedFolderAsync()
        {
            if(!inProcess)
            {
                inProcess = true;
                StoreRepository storeRepository = new StoreRepository();
                while (statusSS)
                {
                    await ProcessMoveImagesAndInsertRecordToDb(storeRepository);
                    await Task.Delay(timerValue * 1000);
                }
                inProcess = false;
            }           
        }

        private async Task MetricsFun()
        {
              
                while (true)
                {
                    string ram = $"{Metrics.GetMemoryUsageMB().ToString()} MB";
                    var cpuD = Metrics.GetCPUUsagePercent()/10 / Environment.ProcessorCount;
                    string cpu = $"{Math.Round(cpuD,2 ).ToString()} %";
                    FormsGenerator.setLabelTextColorSafe(lblRam, ram);
                    FormsGenerator.setLabelTextColorSafe(lblCpu, cpu);
                    await Task.Delay(500);
                }
                
           
        }


        /// <summary>
        /// ProcessMoveImagesAndInsertRecordToDb
        /// </summary>
        /// <param name="storeRepository"></param>
        /// <returns></returns>
        private async Task ProcessMoveImagesAndInsertRecordToDb(StoreRepository storeRepository)
        {
            try
            {
                await Task.Run(() =>
                {
                    FileOperations fileOperations = new FileOperations();
                    string ext = processName == "AXI" ? "tif" : processName == "AOI" ? "jpg" : (processName == "AA" || processName == "EOL" || processName == "IC")? "all" : "";
                    List<string> listString = fileOperations.GetFilesFromPath(sourcePath,ext);
                    foreach (var item in listString)
                    {
                        if(fileOperations.CheckFilelockedByRecentCreation(item, timerFileValue))
                        {
                            continue;
                        }
                        if (!statusSS)
                        {
                            return;
                        }
                        string validPath = SerialNumbers.GetValidSerialNumberFromPath(item, processName);
                        if (!string.IsNullOrEmpty(validPath))
                        {
                            FormsGenerator.setLabelTextColorSafe(lblCurrent, validPath);
                            // lblCurrent.Text = validPath;

                            var result = (processName == "AXI"|| processName == "AA" || processName == "EOL" || processName == "IC") ? fileOperations.CreateFoldersAndUpdateImageFromPath(validPath, destinationPath, processName, true) : fileOperations.CreateFoldersAndUpdateImageFromPath(validPath, destinationPath, processName);
                            if (result.Item1 != null && result.Item1 != "")
                            {
                                ImageRepositoryModel imageRepositoryModel = new ImageRepositoryModel();
                                imageRepositoryModel.SerialNumber = result.Item1;
                                imageRepositoryModel.Path = Path.GetDirectoryName(result.Item2);
                                imageRepositoryModel.FileName = Path.GetFileName(result.Item2);
                                imageRepositoryModel.FKProcess = process_ID;
                                // Get Creation Date from Image
                                string dateString = fileOperations.GetDateStringFromFileName(validPath);
                                if (dateString.Length == 0)
                                {
                                    //get datetime with hour minuts and secons from dateString
                                    dateString = File.GetLastWriteTime(validPath).ToString("yyyyMMddHHmmss");
                                }
                                imageRepositoryModel.FileDateTime = DateTime.ParseExact(dateString, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                                //Insert Image to DB
                                ImageRepositoryModel imageRepositoryAfterInsert = storeRepository.insertImageRecord(imageRepositoryModel);

                                if (imageRepositoryAfterInsert != null && processName=="AA" && validPath.ToUpper().Contains("CSV"))
                                 {
                                    fileOperations.DeleteFilesFromPath(validPath);

                                }
                                if (imageRepositoryAfterInsert != null &&
                               ( validPath.ToUpper().Contains("JPG") ||  validPath.ToUpper().Contains("TIF") || validPath.ToUpper().Contains("PNG") || validPath.ToUpper().Contains("BMP") || validPath.ToUpper().Contains("PSD")))
                                {
                                    //Delete Image from Source Folder

                                   
                                    
                                  fileOperations.DeleteFilesFromPath(validPath);                                   
                                }
                            }
                            int numLines = imagesUpdated.Split('\n').Length;
                            if (numLines > 20)
                            {
                                imagesUpdated = "";
                            }
                            imagesUpdated = result.Item1 != null && result.Item1 != "" ? imagesUpdated + validPath + "  Target:" + result.Item2 + Environment.NewLine : imagesUpdated;
                           
                            FormsGenerator.setTextboxTextColorSafe(txtLog, imagesUpdated);
                            LoggerImage.WriteLog(validPath, "CopyProcess");
                            FormsGenerator.setLabelTextColorSafe(lblLast, validPath);
                            FormsGenerator.setLabelTextColorSafe(lblCurrent, "");
                            continue;
                        }
                    }
                });
            }
            catch(Exception ex)
            {
                LoggerImage.WriteLog(ex.Message, "ProcessMoveImagesAndInsertRecordToDb");
            }           
        }

        /// <summary>
        /// GetConfigs
        /// </summary>
        private void GetConfigs()
        {
            //Get configs from config file
            sourcePath = ConfigFunctions.GetSourceFromConfig();
            destinationPath = ConfigFunctions.GetTargetFromConfig();
            timerValue = ConfigFunctions.GetTimerFromConfig();
            timerFileValue = ConfigFunctions.GetTimerFileFromConfig()==0?5: ConfigFunctions.GetTimerFileFromConfig();
            process_ID = ConfigFunctions.GetProcessIdFromConfig();
            processName = ConfigFunctions.GetProcessNameFromConfig();

            this.lblProcess.Text = $"Process: {processName}";
            //Check if the paths are empty and set StatusSS to false
            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destinationPath) || timerValue<60)
            {
                statusSS = false;
                btnStarStop.Text = "Start";
                return;
            }           
            statusSS = true;
            btnStarStop.Text = "Stop";
        }
        #endregion


    }
}
