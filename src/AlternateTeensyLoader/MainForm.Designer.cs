namespace AlternateTeensyLoader
{
    partial class MainForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblFirmwarePath = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtFwPath = new System.Windows.Forms.TextBox();
            this.btnSelectFw = new System.Windows.Forms.Button();
            this.btnUploadFw = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFirmwarePath});
            this.statusStrip1.Location = new System.Drawing.Point(0, 48);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(663, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblFirmwarePath
            // 
            this.lblFirmwarePath.Name = "lblFirmwarePath";
            this.lblFirmwarePath.Size = new System.Drawing.Size(132, 17);
            this.lblFirmwarePath.Text = "Please specify a .hex file";
            // 
            // txtFwPath
            // 
            this.txtFwPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFwPath.Location = new System.Drawing.Point(12, 12);
            this.txtFwPath.Name = "txtFwPath";
            this.txtFwPath.Size = new System.Drawing.Size(367, 20);
            this.txtFwPath.TabIndex = 1;
            // 
            // btnSelectFw
            // 
            this.btnSelectFw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFw.Location = new System.Drawing.Point(385, 10);
            this.btnSelectFw.Name = "btnSelectFw";
            this.btnSelectFw.Size = new System.Drawing.Size(130, 23);
            this.btnSelectFw.TabIndex = 2;
            this.btnSelectFw.Text = "Select Firmware (.hex)";
            this.btnSelectFw.UseVisualStyleBackColor = true;
            this.btnSelectFw.Click += new System.EventHandler(this.btnSelectFw_Click);
            // 
            // btnUploadFw
            // 
            this.btnUploadFw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUploadFw.Enabled = false;
            this.btnUploadFw.Location = new System.Drawing.Point(521, 10);
            this.btnUploadFw.Name = "btnUploadFw";
            this.btnUploadFw.Size = new System.Drawing.Size(130, 23);
            this.btnUploadFw.TabIndex = 3;
            this.btnUploadFw.Text = "Upload Firmware";
            this.btnUploadFw.UseVisualStyleBackColor = true;
            this.btnUploadFw.Click += new System.EventHandler(this.btnUploadFw_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 70);
            this.Controls.Add(this.btnUploadFw);
            this.Controls.Add(this.btnSelectFw);
            this.Controls.Add(this.txtFwPath);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alternate Teensy Loader (For Teensy 2 ONLY)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblFirmwarePath;
        private System.Windows.Forms.TextBox txtFwPath;
        private System.Windows.Forms.Button btnSelectFw;
        private System.Windows.Forms.Button btnUploadFw;
    }
}

