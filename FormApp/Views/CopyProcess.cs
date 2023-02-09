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

namespace FormApp.Views
{
    public partial class CopyProcess : Form
    {
        #region Private Variables
        private bool statusSS = false;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string sourcePath = string.Empty;
        private string destinationPath = string.Empty;
        private int timerValue = 0;
        private string imagesUpdated = string.Empty;
        private int process_ID = 0;
       
        #endregion Private Variables
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
        private void NewComponents()        
        {
            
            //find icon from current path resources icono.ico
            
          
            notifyIcon.Visible = false;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;            
        }
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }
        #region Events

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
        //when close form minimize to tray icon
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
        }        
        private void btnConfig_Click(object sender, EventArgs e)
        {
            //Show dialog config
            Config config = new Config();
            statusSS = false;
            btnStarStop.Text = "Start";
            if (config.ShowDialog() == DialogResult.OK)
            {
                statusSS = true;
                GetConfigs();
                Task.Run(() => CopyImagesToRedFolderAsync());
                return;
            }
            statusSS = false;
        }
        private void btnStarStop_Click(object sender, EventArgs e)
        {
            if (statusSS)
            {
                statusSS = false;
                btnStarStop.Text = "Start";
                return;
            }            
            statusSS = true;
            btnStarStop.Text = "Stop";
            Task.Run(() => CopyImagesToRedFolderAsync());
            GetConfigs();
        }
        #endregion
        /// <summary>
        /// CopyImagesToRedFolderAsync
        /// </summary>
        /// <returns></returns>
        private async Task CopyImagesToRedFolderAsync()
        {
            StoreRepository storeRepository = new StoreRepository();
            while (statusSS)
            {
                await ProcessMoveImagesAndInsertRecordToDb(storeRepository);
                await Task.Delay(timerValue * 1000);
            }
        }
        /// <summary>
        /// ProcessMoveImagesAndInsertRecordToDb
        /// </summary>
        /// <param name="storeRepository"></param>
        /// <returns></returns>
        private async Task ProcessMoveImagesAndInsertRecordToDb(StoreRepository storeRepository)
        {
            await Task.Run(() =>
            {
                FileOperations fileOperations = new FileOperations();
                List<string> listString = fileOperations.GetFilesFromPath(sourcePath);
                foreach (var item in listString)
                {
                    if (!statusSS)
                    {
                        return;
                    }
                    string validPath = SerialNumbers.GetValidSerialNumberFromPath(item);
                    if (!string.IsNullOrEmpty(validPath))
                    {
                        FormsGenerator.setLabelTextColorSafe(lblCurrent, validPath);
                        // lblCurrent.Text = validPath;
                        var result = fileOperations.CreateFoldersAndUpdateImageFromPath(validPath, destinationPath);
                        if (result.Item1 != null && result.Item1 != "")
                        {
                            ImageRepositoryModel imageRepositoryModel = new ImageRepositoryModel();
                            imageRepositoryModel.SerialNumber = result.Item1;
                            imageRepositoryModel.Path = result.Item2;
                            imageRepositoryModel.FKProcess = process_ID;
                            // Get Creation Date from Image
                            string dateString = fileOperations.GetDateStringFromFileName(validPath);
                            imageRepositoryModel.FileDateTime = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                            //Insert Image to DB
                            ImageRepositoryModel imageRepositoryAfterInsert = storeRepository.insertImageRecord(imageRepositoryModel);
                            if (imageRepositoryAfterInsert != null)
                            {
                                //Delete Image from Source Folder
                                fileOperations.DeleteFilesFromPath(validPath);
                            }
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

        /// <summary>
        /// GetConfigs
        /// </summary>
        private void GetConfigs()
        {
            //Get configs from config file
            sourcePath = ConfigFunctions.GetSourceFromConfig();
            destinationPath = ConfigFunctions.GetTargetFromConfig();
            timerValue = ConfigFunctions.GetTimerFromConfig();
            process_ID = ConfigFunctions.GetProcessIdFromConfig();
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

    }
}
