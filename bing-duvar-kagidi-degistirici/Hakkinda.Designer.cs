namespace BingDuvarKagidiUI
{
    partial class FrmHakkinda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHakkinda));
            this.lblProgramAdi = new System.Windows.Forms.Label();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.lblAltBilgi = new System.Windows.Forms.LinkLabel();
            this.pboxHakkinda = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxHakkinda)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProgramAdi
            // 
            this.lblProgramAdi.AutoSize = true;
            this.lblProgramAdi.Font = new System.Drawing.Font("Calibri", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblProgramAdi.Location = new System.Drawing.Point(106, 12);
            this.lblProgramAdi.Name = "lblProgramAdi";
            this.lblProgramAdi.Size = new System.Drawing.Size(271, 23);
            this.lblProgramAdi.TabIndex = 0;
            this.lblProgramAdi.Text = "Bing Duvar Kağıdı Değiştirici 2.41\r\n";
            // 
            // lblBilgi
            // 
            this.lblBilgi.Location = new System.Drawing.Point(106, 46);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(461, 121);
            this.lblBilgi.TabIndex = 4;
            this.lblBilgi.Text = resources.GetString("lblBilgi.Text");
            // 
            // lblAltBilgi
            // 
            this.lblAltBilgi.AutoSize = true;
            this.lblAltBilgi.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.lblAltBilgi.LinkArea = new System.Windows.Forms.LinkArea(0, 7);
            this.lblAltBilgi.Location = new System.Drawing.Point(106, 167);
            this.lblAltBilgi.Name = "lblAltBilgi";
            this.lblAltBilgi.Size = new System.Drawing.Size(461, 28);
            this.lblAltBilgi.TabIndex = 5;
            this.lblAltBilgi.TabStop = true;
            this.lblAltBilgi.Text = "Umut D. | Açık kaynak lisanslı (MIT) olarak yazılmıştır | 2021";
            this.lblAltBilgi.UseCompatibleTextRendering = true;
            this.lblAltBilgi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblHakkinda_LinkClicked);
            // 
            // pboxHakkinda
            // 
            this.pboxHakkinda.Image = ((System.Drawing.Image)(resources.GetObject("pboxHakkinda.Image")));
            this.pboxHakkinda.Location = new System.Drawing.Point(12, 12);
            this.pboxHakkinda.Name = "pboxHakkinda";
            this.pboxHakkinda.Size = new System.Drawing.Size(81, 82);
            this.pboxHakkinda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pboxHakkinda.TabIndex = 3;
            this.pboxHakkinda.TabStop = false;
            // 
            // FrmHakkinda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 206);
            this.Controls.Add(this.lblAltBilgi);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.pboxHakkinda);
            this.Controls.Add(this.lblProgramAdi);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHakkinda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hakkında";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmHakkinda_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pboxHakkinda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProgramAdi;
        private System.Windows.Forms.PictureBox pboxHakkinda;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.LinkLabel lblAltBilgi;
    }
}