namespace bing_duvar_kagidi_degistirici.Formlar
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
            this.lblHakkindaAd = new System.Windows.Forms.Label();
            this.lblHakkindaVersiyon = new System.Windows.Forms.Label();
            this.lblHakkindaBilgi = new System.Windows.Forms.Label();
            this.llblHakkinda = new System.Windows.Forms.LinkLabel();
            this.pboxHakkinda = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxHakkinda)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHakkindaAd
            // 
            this.lblHakkindaAd.AutoSize = true;
            this.lblHakkindaAd.Font = new System.Drawing.Font("Calibri", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHakkindaAd.Location = new System.Drawing.Point(106, 12);
            this.lblHakkindaAd.Name = "lblHakkindaAd";
            this.lblHakkindaAd.Size = new System.Drawing.Size(219, 22);
            this.lblHakkindaAd.TabIndex = 0;
            this.lblHakkindaAd.Text = "Bing Duvar Kağıdı Değiştirici";
            // 
            // lblHakkindaVersiyon
            // 
            this.lblHakkindaVersiyon.AutoSize = true;
            this.lblHakkindaVersiyon.Location = new System.Drawing.Point(325, 12);
            this.lblHakkindaVersiyon.Name = "lblHakkindaVersiyon";
            this.lblHakkindaVersiyon.Size = new System.Drawing.Size(72, 22);
            this.lblHakkindaVersiyon.TabIndex = 1;
            this.lblHakkindaVersiyon.Text = "ver. 2.12";
            // 
            // lblHakkindaBilgi
            // 
            this.lblHakkindaBilgi.Location = new System.Drawing.Point(106, 46);
            this.lblHakkindaBilgi.Name = "lblHakkindaBilgi";
            this.lblHakkindaBilgi.Size = new System.Drawing.Size(459, 121);
            this.lblHakkindaBilgi.TabIndex = 4;
            this.lblHakkindaBilgi.Text = resources.GetString("lblHakkindaBilgi.Text");
            // 
            // llblHakkinda
            // 
            this.llblHakkinda.AutoSize = true;
            this.llblHakkinda.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.llblHakkinda.LinkArea = new System.Windows.Forms.LinkArea(0, 7);
            this.llblHakkinda.Location = new System.Drawing.Point(106, 167);
            this.llblHakkinda.Name = "llblHakkinda";
            this.llblHakkinda.Size = new System.Drawing.Size(464, 27);
            this.llblHakkinda.TabIndex = 5;
            this.llblHakkinda.TabStop = true;
            this.llblHakkinda.Text = "Umut D. | Açık kaynak lisanslı (GPL) olarak kodlanmıştır | 2018";
            this.llblHakkinda.UseCompatibleTextRendering = true;
            this.llblHakkinda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblHakkinda_LinkClicked);
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
            this.Controls.Add(this.llblHakkinda);
            this.Controls.Add(this.lblHakkindaBilgi);
            this.Controls.Add(this.pboxHakkinda);
            this.Controls.Add(this.lblHakkindaVersiyon);
            this.Controls.Add(this.lblHakkindaAd);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHakkinda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hakkında";
            ((System.ComponentModel.ISupportInitialize)(this.pboxHakkinda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHakkindaAd;
        private System.Windows.Forms.Label lblHakkindaVersiyon;
        private System.Windows.Forms.PictureBox pboxHakkinda;
        private System.Windows.Forms.Label lblHakkindaBilgi;
        private System.Windows.Forms.LinkLabel llblHakkinda;
    }
}