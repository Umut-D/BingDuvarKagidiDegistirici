namespace bing_duvar_kagidi_degistirici.Formlar
{
    partial class FrmBing
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBing));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiDosya = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAyarlar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmKapat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDuvarKagidi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmIndir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDegistir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBilgi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGuncelle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHakkinda = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSeciliUlke = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tssDurum = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbox = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsGoster = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsKapat = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDosya,
            this.tsmiDuvarKagidi,
            this.tsmiBilgi,
            this.tsmiSeciliUlke});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(1002, 32);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // tsmiDosya
            // 
            this.tsmiDosya.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAyarlar,
            this.tsmKapat});
            this.tsmiDosya.Name = "tsmiDosya";
            this.tsmiDosya.Size = new System.Drawing.Size(67, 26);
            this.tsmiDosya.Text = "Dosya";
            // 
            // tsmAyarlar
            // 
            this.tsmAyarlar.Name = "tsmAyarlar";
            this.tsmAyarlar.Size = new System.Drawing.Size(141, 28);
            this.tsmAyarlar.Text = "Ayarlar";
            this.tsmAyarlar.Click += new System.EventHandler(this.tsmAyarlar_Click);
            // 
            // tsmKapat
            // 
            this.tsmKapat.Name = "tsmKapat";
            this.tsmKapat.Size = new System.Drawing.Size(141, 28);
            this.tsmKapat.Text = "Kapat";
            this.tsmKapat.Click += new System.EventHandler(this.tsmKapat_Click);
            // 
            // tsmiDuvarKagidi
            // 
            this.tsmiDuvarKagidi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmIndir,
            this.tsmDegistir});
            this.tsmiDuvarKagidi.Name = "tsmiDuvarKagidi";
            this.tsmiDuvarKagidi.Size = new System.Drawing.Size(112, 26);
            this.tsmiDuvarKagidi.Text = "Duvar Kağıdı";
            // 
            // tsmIndir
            // 
            this.tsmIndir.Name = "tsmIndir";
            this.tsmIndir.Size = new System.Drawing.Size(144, 28);
            this.tsmIndir.Text = "İndir";
            this.tsmIndir.Click += new System.EventHandler(this.tsmIndir_Click);
            // 
            // tsmDegistir
            // 
            this.tsmDegistir.Name = "tsmDegistir";
            this.tsmDegistir.Size = new System.Drawing.Size(144, 28);
            this.tsmDegistir.Text = "Değiştir";
            this.tsmDegistir.Click += new System.EventHandler(this.tsmDegistir_Click);
            // 
            // tsmiBilgi
            // 
            this.tsmiBilgi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBing,
            this.tsmGuncelle,
            this.tsmHakkinda});
            this.tsmiBilgi.Name = "tsmiBilgi";
            this.tsmiBilgi.Size = new System.Drawing.Size(52, 26);
            this.tsmiBilgi.Text = "Bilgi";
            // 
            // tsmBing
            // 
            this.tsmBing.Name = "tsmBing";
            this.tsmBing.Size = new System.Drawing.Size(195, 28);
            this.tsmBing.Text = "Bing.com\'a Git";
            this.tsmBing.Click += new System.EventHandler(this.tsmBing_Click);
            // 
            // tsmGuncelle
            // 
            this.tsmGuncelle.Name = "tsmGuncelle";
            this.tsmGuncelle.Size = new System.Drawing.Size(195, 28);
            this.tsmGuncelle.Text = "Güncelle";
            this.tsmGuncelle.Click += new System.EventHandler(this.tsmGuncelle_Click);
            // 
            // tsmHakkinda
            // 
            this.tsmHakkinda.Name = "tsmHakkinda";
            this.tsmHakkinda.Size = new System.Drawing.Size(195, 28);
            this.tsmHakkinda.Text = "Hakkında";
            this.tsmHakkinda.Click += new System.EventHandler(this.tsmHakkinda_Click);
            // 
            // tsmiSeciliUlke
            // 
            this.tsmiSeciliUlke.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiSeciliUlke.Font = new System.Drawing.Font("Calibri", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsmiSeciliUlke.Name = "tsmiSeciliUlke";
            this.tsmiSeciliUlke.Size = new System.Drawing.Size(12, 26);
            this.tsmiSeciliUlke.Click += new System.EventHandler(this.tsmiSeciliUlke_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssDurum});
            this.statusStrip.Location = new System.Drawing.Point(0, 574);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1002, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // tssDurum
            // 
            this.tssDurum.Name = "tssDurum";
            this.tssDurum.Size = new System.Drawing.Size(0, 17);
            // 
            // pbox
            // 
            this.pbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbox.Location = new System.Drawing.Point(0, 32);
            this.pbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbox.Name = "pbox";
            this.pbox.Size = new System.Drawing.Size(1002, 564);
            this.pbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbox.TabIndex = 1;
            this.pbox.TabStop = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "Bing Duvar Kağıdı Değiştirici";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Bilgilendirme";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsGoster,
            this.cmsKapat});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(139, 60);
            // 
            // cmsGoster
            // 
            this.cmsGoster.Name = "cmsGoster";
            this.cmsGoster.Size = new System.Drawing.Size(138, 28);
            this.cmsGoster.Text = "Göster";
            this.cmsGoster.Click += new System.EventHandler(this.cmsGoster_Click);
            // 
            // cmsKapat
            // 
            this.cmsKapat.Name = "cmsKapat";
            this.cmsKapat.Size = new System.Drawing.Size(138, 28);
            this.cmsKapat.Text = "Kapat";
            this.cmsKapat.Click += new System.EventHandler(this.cmsKapat_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FrmBing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 596);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pbox);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1020, 645);
            this.Name = "FrmBing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bing Duvar Kağıdı Değiştirici 2.1";
            this.Load += new System.EventHandler(this.FrmBing_Load);
            this.Resize += new System.EventHandler(this.FrmBing_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiDosya;
        private System.Windows.Forms.ToolStripMenuItem tsmKapat;
        private System.Windows.Forms.ToolStripMenuItem tsmiDuvarKagidi;
        private System.Windows.Forms.ToolStripMenuItem tsmIndir;
        private System.Windows.Forms.ToolStripMenuItem tsmDegistir;
        private System.Windows.Forms.ToolStripMenuItem tsmiBilgi;
        private System.Windows.Forms.ToolStripMenuItem tsmBing;
        private System.Windows.Forms.ToolStripMenuItem tsmGuncelle;
        private System.Windows.Forms.ToolStripMenuItem tsmHakkinda;
        private System.Windows.Forms.ToolStripMenuItem tsmAyarlar;
        private System.Windows.Forms.PictureBox pbox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tssDurum;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem cmsGoster;
        private System.Windows.Forms.ToolStripMenuItem cmsKapat;
        private System.Windows.Forms.Timer timer;
        public System.Windows.Forms.ToolStripMenuItem tsmiSeciliUlke;
    }
}

