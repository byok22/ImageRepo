using ApplicationsA.LDAP_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDAP
{
    public partial class TestLDAP : Form
    {
        public string username { get; set; }
        private string password;
        public TestLDAP()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Ldap ldap = new Ldap();
            username = txtUser.Text;
            password = txtPassword.Text;
            MessageBox.Show(ldap.NTLoginMSJ("JABIL", username, password));
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }

        private void TestLDAP_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            txtPassword.MaxLength = 100;
        }
    }
}
