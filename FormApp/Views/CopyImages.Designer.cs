namespace FormApp
{
    partial class CopyImages
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetSource = new System.Windows.Forms.Button();
            this.btnGetDestination = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.rbSingleFile = new System.Windows.Forms.RadioButton();
            this.rbFolder = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.pbAvance = new System.Windows.Forms.ProgressBar();
            this.chkListFiles = new System.Windows.Forms.CheckedListBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetSource
            // 
            this.btnGetSource.Location = new System.Drawing.Point(25, 138);
            this.btnGetSource.Name = "btnGetSource";
            this.btnGetSource.Size = new System.Drawing.Size(270, 24);
            this.btnGetSource.TabIndex = 0;
            this.btnGetSource.Text = "Source";
            this.btnGetSource.UseVisualStyleBackColor = true;
            this.btnGetSource.Click += new System.EventHandler(this.btnGetSource_Click);
            // 
            // btnGetDestination
            // 
            this.btnGetDestination.Location = new System.Drawing.Point(575, 138);
            this.btnGetDestination.Name = "btnGetDestination";
            this.btnGetDestination.Size = new System.Drawing.Size(272, 24);
            this.btnGetDestination.TabIndex = 1;
            this.btnGetDestination.Text = "Destination";
            this.btnGetDestination.UseVisualStyleBackColor = true;
            this.btnGetDestination.Click += new System.EventHandler(this.btnGetDestination_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(25, 99);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(270, 20);
            this.txtSource.TabIndex = 2;
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(575, 99);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(270, 20);
            this.txtTarget.TabIndex = 3;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(232, 385);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(193, 24);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "Start Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Visible = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // rbSingleFile
            // 
            this.rbSingleFile.AutoSize = true;
            this.rbSingleFile.Location = new System.Drawing.Point(352, 56);
            this.rbSingleFile.Name = "rbSingleFile";
            this.rbSingleFile.Size = new System.Drawing.Size(73, 17);
            this.rbSingleFile.TabIndex = 5;
            this.rbSingleFile.TabStop = true;
            this.rbSingleFile.Text = "Single File";
            this.rbSingleFile.UseVisualStyleBackColor = true;
            // 
            // rbFolder
            // 
            this.rbFolder.AutoSize = true;
            this.rbFolder.Location = new System.Drawing.Point(444, 56);
            this.rbFolder.Name = "rbFolder";
            this.rbFolder.Size = new System.Drawing.Size(101, 17);
            this.rbFolder.TabIndex = 6;
            this.rbFolder.TabStop = true;
            this.rbFolder.Text = "Complete Folder";
            this.rbFolder.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Upload Type";
            // 
            // pbAvance
            // 
            this.pbAvance.Location = new System.Drawing.Point(232, 480);
            this.pbAvance.Name = "pbAvance";
            this.pbAvance.Size = new System.Drawing.Size(407, 23);
            this.pbAvance.TabIndex = 8;
            this.pbAvance.Visible = false;
            // 
            // chkListFiles
            // 
            this.chkListFiles.FormattingEnabled = true;
            this.chkListFiles.Location = new System.Drawing.Point(25, 240);
            this.chkListFiles.Name = "chkListFiles";
            this.chkListFiles.Size = new System.Drawing.Size(835, 139);
            this.chkListFiles.TabIndex = 9;
            this.chkListFiles.Visible = false;
            this.chkListFiles.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(351, 198);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(193, 24);
            this.btnVerify.TabIndex = 10;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnViewLog
            // 
            this.btnViewLog.Location = new System.Drawing.Point(280, 430);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(95, 24);
            this.btnViewLog.TabIndex = 11;
            this.btnViewLog.Text = "View Log";
            this.btnViewLog.UseVisualStyleBackColor = true;
            this.btnViewLog.Visible = false;
            this.btnViewLog.Click += new System.EventHandler(this.btnViewLog_Click);
            // 
            // CopyImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 512);
            this.Controls.Add(this.btnViewLog);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.chkListFiles);
            this.Controls.Add(this.pbAvance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbFolder);
            this.Controls.Add(this.rbSingleFile);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnGetDestination);
            this.Controls.Add(this.btnGetSource);
            this.Name = "CopyImages";
            this.Text = "Upload Images";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetSource;
        private System.Windows.Forms.Button btnGetDestination;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.RadioButton rbSingleFile;
        private System.Windows.Forms.RadioButton rbFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pbAvance;
        private System.Windows.Forms.CheckedListBox chkListFiles;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnViewLog;
    }
}

