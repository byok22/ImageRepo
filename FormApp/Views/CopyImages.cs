using System;
using System.Windows.Forms;
using ApplicationsA.Functions;
using FormApp.Functions;

namespace FormApp
{
    public partial class CopyImages : Form
    {
        FileOperations fileOperations = new FileOperations();

        public CopyImages()
        {
            InitializeComponent();
            getSavedSourceAndTarget();
        }
        private void getSavedSourceAndTarget()
        {
            string source = ConfigFunctions.GetSourceFromConfig();
            string target = ConfigFunctions.GetTargetFromConfig();
            txtSource.Text = source;
            txtTarget.Text = target;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        private void btnGetSource_Click(object sender, EventArgs e)
        {
            //check upload Type and get source folder
            if(rbFolder.Checked)
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
                //get destination folder
                //verify
                //upload
                //view log
            }
            else if(rbSingleFile.Checked)
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Select the file to upload";               
                fd.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    string source = txtSource.Text = fd.FileName;
                    ConfigFunctions.SaveSourceinConfig(source);
                }
                //get source folder
                //get destination folder
                //verify
                //upload
                //view log
            }          
            else
            {
                MessageBox.Show("Please select an upload type");
            }

        }

        private void btnGetDestination_Click(object sender, EventArgs e)
        {
            //get destination folder
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the folder Destination";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string target = txtTarget.Text = fbd.SelectedPath;
                //save in app.config file code
                ConfigFunctions.SaveTargetInConfig(target);
            }

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            //verify source and destination
            string source = txtSource.Text;
            string target = txtTarget.Text;
            if (source == "" || target == "")
            {
                MessageBox.Show("Please select source and destination");
              
            }
            else
            {
                //source exists
                //destination exists
                if(!FileFunctions.CheckIfSourceExists(source))
                {
                    MessageBox.Show("Source are Incorrect");
                    return;
                }
                if(!FileFunctions.CheckIfTargetExists(target))
                {
                    MessageBox.Show("Target are Incorrect");
                    return;
                }
                MessageBox.Show("Source and destination are selected");
                btnUpload.Visible = true;
                chkListFiles.Visible = true;
                chkListFiles.Items.Clear();
                chkListFiles.Items.AddRange(FormsGenerator.GetItemsForCheckedListBoXFromPath(source));
                //check all items in chkListFiles and Verify Serial Numbers
                for (int i = 0; i < chkListFiles.Items.Count; i++)
                {
                    
                    //verify serial numbers
                    string serialNumber = SerialNumbers.GetValidSerialNumberFromPath(chkListFiles.Items[i].ToString());
                    if (serialNumber == "")
                    {
                        chkListFiles.SetItemChecked(i, false);
                        chkListFiles.SetItemCheckState(i, CheckState.Indeterminate);
                        chkListFiles.SetItemChecked(i, false);
                    }
                    else
                    {
                        chkListFiles.SetItemChecked(i, true);
                    }
                }
                
               
            }

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //upload
            string source = txtSource.Text;
            string target = txtTarget.Text;
            if (source == "" || target == "")
            {
                MessageBox.Show("Please select source and destination");
              
            }
            else
            {
                //source exists
                //destination exists
                if (!FileFunctions.CheckIfSourceExists(source))
                {
                    MessageBox.Show("Source are Incorrect");
                    return;
                }
                if (!FileFunctions.CheckIfTargetExists(target))
                {
                    MessageBox.Show("Target are Incorrect");
                
                }
               
            }

        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {

        }
    }
}
