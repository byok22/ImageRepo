using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp.Views
{
    public partial class CopyProcess : Form
    {
        private bool statusSS = false;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public CopyProcess()
        {
            InitializeComponent();
            EventsForms();
        }

        #region Events
        private void EventsForms()
        {
            this.MouseDown += CopyProcess_MouseDown;
            this.MouseMove += CopyProcess_MouseMove;
            this.MouseUp += CopyProcess_MouseUp;
            pnlMenu.MouseDown += PnlMenu_MouseDown;
            pnlMenu.MouseMove += PnlMenu_MouseMove;
            pnlMenu.MouseUp += PnlMenu_MouseUp;        
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
        }
        #endregion

        private void btnConfig_Click(object sender, EventArgs e)
        {
            //Show dialog config
            Config config = new Config();
            if(config.ShowDialog() == DialogResult.OK)
            {
                CopyImagesToRedFolder();
            }else{
                //Do something
            }

            
        }
        private void CopyImagesToRedFolder()
        {
            //Copy images to red folder
        }
    }
}
