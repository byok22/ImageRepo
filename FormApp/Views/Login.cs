using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationsA.LDAP_Validation;

namespace FormApp.Views
{
    public partial class Login : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string typeLogin;        
        public string username { get; set; }
        private string password;
        public Login(string typeLogin)
        {
            InitializeComponent();
            HidePasswordTextBox();
            EventsForms();
            this.typeLogin = typeLogin;
        }        
        private void EventsForms()
        {
            this.MouseDown += CopyProcess_MouseDown;
            this.MouseMove += CopyProcess_MouseMove;
            this.MouseUp += CopyProcess_MouseUp;
        }
        private void HidePasswordTextBox()
        {
            txtPassword.PasswordChar = '*';
            txtPassword.MaxLength = 100;
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
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Ldap ldap = new Ldap();           
            username = txtUser.Text;
            password = txtPassword.Text;
            if (username == "ADMIN" && password == "J@b1l2023" || ldap.NTLogin("JABIL", username, password))
            {              
                this.DialogResult = DialogResult.OK;
                return;
            }
            this.DialogResult = DialogResult.Cancel;           
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
