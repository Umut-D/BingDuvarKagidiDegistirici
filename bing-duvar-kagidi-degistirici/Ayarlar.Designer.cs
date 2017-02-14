namespace bing_duvar_kagidi_degistirici
{
    partial class FrmAyarlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAyarlar));
            this.gboxDiger = new System.Windows.Forms.GroupBox();
            this.dtpSaat = new System.Windows.Forms.DateTimePicker();
            this.chkboxOtomatikDegistir = new System.Windows.Forms.CheckBox();
            this.chkboxBaslangic = new System.Windows.Forms.CheckBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.gboxDiger.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxDiger
            // 
            this.gboxDiger.Controls.Add(this.dtpSaat);
            this.gboxDiger.Controls.Add(this.chkboxOtomatikDegistir);
            this.gboxDiger.Controls.Add(this.chkboxBaslangic);
            this.gboxDiger.Location = new System.Drawing.Point(12, 12);
            this.gboxDiger.Name = "gboxDiger";
            this.gboxDiger.Size = new System.Drawing.Size(430, 101);
            this.gboxDiger.TabIndex = 8;
            this.gboxDiger.TabStop = false;
            this.gboxDiger.Text = "Diğer";
            // 
            // dtpSaat
            // 
            this.dtpSaat.CustomFormat = "HH:mm";
            this.dtpSaat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSaat.Location = new System.Drawing.Point(333, 58);
            this.dtpSaat.Name = "dtpSaat";
            this.dtpSaat.ShowUpDown = true;
            this.dtpSaat.Size = new System.Drawing.Size(91, 29);
            this.dtpSaat.TabIndex = 10;
            this.dtpSaat.Value = new System.DateTime(2016, 12, 14, 12, 0, 0, 0);
            // 
            // chkboxOtomatikDegistir
            // 
            this.chkboxOtomatikDegistir.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkboxOtomatikDegistir.Location = new System.Drawing.Point(6, 60);
            this.chkboxOtomatikDegistir.Name = "chkboxOtomatikDegistir";
            this.chkboxOtomatikDegistir.Size = new System.Drawing.Size(329, 27);
            this.chkboxOtomatikDegistir.TabIndex = 1;
            this.chkboxOtomatikDegistir.Text = "Duvar kağıdığını otomatik olarak değiştir";
            this.chkboxOtomatikDegistir.UseVisualStyleBackColor = true;
            this.chkboxOtomatikDegistir.CheckedChanged += new System.EventHandler(this.chkboxOtomatikDegistir_CheckedChanged);
            // 
            // chkboxBaslangic
            // 
            this.chkboxBaslangic.AutoSize = true;
            this.chkboxBaslangic.Location = new System.Drawing.Point(6, 28);
            this.chkboxBaslangic.Name = "chkboxBaslangic";
            this.chkboxBaslangic.Size = new System.Drawing.Size(167, 26);
            this.chkboxBaslangic.TabIndex = 0;
            this.chkboxBaslangic.Text = "Başlangıçta çalıştır";
            this.chkboxBaslangic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkboxBaslangic.UseVisualStyleBackColor = true;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(190, 123);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 38);
            this.btnKaydet.TabIndex = 9;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // FrmAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 168);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.gboxDiger);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAyarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.FrmAyarlar_Load);
            this.gboxDiger.ResumeLayout(false);
            this.gboxDiger.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gboxDiger;
        private System.Windows.Forms.CheckBox chkboxOtomatikDegistir;
        private System.Windows.Forms.CheckBox chkboxBaslangic;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.DateTimePicker dtpSaat;
    }
}