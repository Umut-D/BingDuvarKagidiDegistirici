namespace BingDuvarKagidiUI
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
            this.tsmiDuvarKagidi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBilgi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGuncelle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHakkinda = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSeciliUlke = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlmanya = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAmerika = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAvustralya = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBrezilya = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBritanya = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFransa = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHindistan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiJaponya = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKanada = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTurkiye = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tssDurum = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsGoster = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsKapat = new System.Windows.Forms.ToolStripMenuItem();
            this.pbox = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDuvarKagidi,
            this.tsmiBilgi,
            this.tsmiSeciliUlke});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(1002, 33);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // tsmiDuvarKagidi
            // 
            this.tsmiDuvarKagidi.Enabled = false;
            this.tsmiDuvarKagidi.Name = "tsmiDuvarKagidi";
            this.tsmiDuvarKagidi.Size = new System.Drawing.Size(154, 27);
            this.tsmiDuvarKagidi.Text = "Duvar Kağıdı Yap";
            this.tsmiDuvarKagidi.Click += new System.EventHandler(this.TsmiDuvarKagidi_Click);
            // 
            // tsmiBilgi
            // 
            this.tsmiBilgi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBing,
            this.tsmGuncelle,
            this.tsmHakkinda});
            this.tsmiBilgi.Name = "tsmiBilgi";
            this.tsmiBilgi.Size = new System.Drawing.Size(57, 27);
            this.tsmiBilgi.Text = "Bilgi";
            // 
            // tsmBing
            // 
            this.tsmBing.Name = "tsmBing";
            this.tsmBing.Size = new System.Drawing.Size(214, 30);
            this.tsmBing.Text = "Bing.com\'a Git";
            this.tsmBing.Click += new System.EventHandler(this.TsmBing_Click);
            // 
            // tsmGuncelle
            // 
            this.tsmGuncelle.Name = "tsmGuncelle";
            this.tsmGuncelle.Size = new System.Drawing.Size(214, 30);
            this.tsmGuncelle.Text = "Güncelle";
            this.tsmGuncelle.Click += new System.EventHandler(this.TsmGuncelle_Click);
            // 
            // tsmHakkinda
            // 
            this.tsmHakkinda.Name = "tsmHakkinda";
            this.tsmHakkinda.Size = new System.Drawing.Size(214, 30);
            this.tsmHakkinda.Text = "Hakkında";
            this.tsmHakkinda.Click += new System.EventHandler(this.TsmHakkinda_Click);
            // 
            // tsmiSeciliUlke
            // 
            this.tsmiSeciliUlke.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiSeciliUlke.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAlmanya,
            this.tsmiAmerika,
            this.tsmiAvustralya,
            this.tsmiBrezilya,
            this.tsmiBritanya,
            this.tsmiFransa,
            this.tsmiHindistan,
            this.tsmiJaponya,
            this.tsmiKanada,
            this.tsmiTurkiye});
            this.tsmiSeciliUlke.Font = new System.Drawing.Font("Calibri", 9.969231F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsmiSeciliUlke.Name = "tsmiSeciliUlke";
            this.tsmiSeciliUlke.Size = new System.Drawing.Size(16, 27);
            this.tsmiSeciliUlke.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TsmiSeciliUlke_DropDownItemClicked);
            // 
            // tsmiAlmanya
            // 
            this.tsmiAlmanya.Image = global::BingDuvarKagidiUI.Properties.Resources.Almanya_icon;
            this.tsmiAlmanya.Name = "tsmiAlmanya";
            this.tsmiAlmanya.Size = new System.Drawing.Size(189, 30);
            this.tsmiAlmanya.Text = "Almanya";
            // 
            // tsmiAmerika
            // 
            this.tsmiAmerika.Image = global::BingDuvarKagidiUI.Properties.Resources.Amerika_icon;
            this.tsmiAmerika.Name = "tsmiAmerika";
            this.tsmiAmerika.Size = new System.Drawing.Size(189, 30);
            this.tsmiAmerika.Text = "Amerika";
            // 
            // tsmiAvustralya
            // 
            this.tsmiAvustralya.Image = global::BingDuvarKagidiUI.Properties.Resources.Avustralya_icon;
            this.tsmiAvustralya.Name = "tsmiAvustralya";
            this.tsmiAvustralya.Size = new System.Drawing.Size(189, 30);
            this.tsmiAvustralya.Text = "Avustralya";
            // 
            // tsmiBrezilya
            // 
            this.tsmiBrezilya.Image = global::BingDuvarKagidiUI.Properties.Resources.Brezilya_icon;
            this.tsmiBrezilya.Name = "tsmiBrezilya";
            this.tsmiBrezilya.Size = new System.Drawing.Size(189, 30);
            this.tsmiBrezilya.Text = "Brezilya";
            // 
            // tsmiBritanya
            // 
            this.tsmiBritanya.Image = global::BingDuvarKagidiUI.Properties.Resources.Britanya_icon;
            this.tsmiBritanya.Name = "tsmiBritanya";
            this.tsmiBritanya.Size = new System.Drawing.Size(189, 30);
            this.tsmiBritanya.Text = "Britanya";
            // 
            // tsmiFransa
            // 
            this.tsmiFransa.Image = global::BingDuvarKagidiUI.Properties.Resources.Fransa_icon;
            this.tsmiFransa.Name = "tsmiFransa";
            this.tsmiFransa.Size = new System.Drawing.Size(189, 30);
            this.tsmiFransa.Text = "Fransa";
            // 
            // tsmiHindistan
            // 
            this.tsmiHindistan.Image = global::BingDuvarKagidiUI.Properties.Resources.Hindistan_icon;
            this.tsmiHindistan.Name = "tsmiHindistan";
            this.tsmiHindistan.Size = new System.Drawing.Size(189, 30);
            this.tsmiHindistan.Text = "Hindistan";
            // 
            // tsmiJaponya
            // 
            this.tsmiJaponya.Image = global::BingDuvarKagidiUI.Properties.Resources.Japonya_icon;
            this.tsmiJaponya.Name = "tsmiJaponya";
            this.tsmiJaponya.Size = new System.Drawing.Size(189, 30);
            this.tsmiJaponya.Text = "Japonya";
            // 
            // tsmiKanada
            // 
            this.tsmiKanada.Image = global::BingDuvarKagidiUI.Properties.Resources.Kanada_icon;
            this.tsmiKanada.Name = "tsmiKanada";
            this.tsmiKanada.Size = new System.Drawing.Size(189, 30);
            this.tsmiKanada.Text = "Kanada";
            // 
            // tsmiTurkiye
            // 
            this.tsmiTurkiye.Image = global::BingDuvarKagidiUI.Properties.Resources.Turkiye_icon;
            this.tsmiTurkiye.Name = "tsmiTurkiye";
            this.tsmiTurkiye.Size = new System.Drawing.Size(189, 30);
            this.tsmiTurkiye.Text = "Turkiye";
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssDurum});
            this.statusStrip.Location = new System.Drawing.Point(0, 569);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1002, 27);
            this.statusStrip.TabIndex = 2;
            // 
            // tssDurum
            // 
            this.tssDurum.Name = "tssDurum";
            this.tssDurum.Size = new System.Drawing.Size(0, 20);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "Bing Duvar Kağıdı Değiştirici";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Bilgilendirme";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsGoster,
            this.cmsKapat});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(133, 60);
            // 
            // cmsGoster
            // 
            this.cmsGoster.Name = "cmsGoster";
            this.cmsGoster.Size = new System.Drawing.Size(132, 28);
            this.cmsGoster.Text = "Göster";
            this.cmsGoster.Click += new System.EventHandler(this.CmsGoster_Click);
            // 
            // cmsKapat
            // 
            this.cmsKapat.Name = "cmsKapat";
            this.cmsKapat.Size = new System.Drawing.Size(132, 28);
            this.cmsKapat.Text = "Kapat";
            this.cmsKapat.Click += new System.EventHandler(this.CmsKapat_Click);
            // 
            // pbox
            // 
            this.pbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbox.Location = new System.Drawing.Point(0, 33);
            this.pbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbox.Name = "pbox";
            this.pbox.Size = new System.Drawing.Size(1002, 563);
            this.pbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbox.TabIndex = 1;
            this.pbox.TabStop = false;
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
            this.Text = "Bing Duvar Kağıdı Değiştirici";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBing_FormClosing);
            this.Load += new System.EventHandler(this.FrmBing_Load);
            this.Resize += new System.EventHandler(this.FrmBing_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiDuvarKagidi;
        private System.Windows.Forms.ToolStripMenuItem tsmiBilgi;
        private System.Windows.Forms.ToolStripMenuItem tsmBing;
        private System.Windows.Forms.ToolStripMenuItem tsmGuncelle;
        private System.Windows.Forms.ToolStripMenuItem tsmHakkinda;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tssDurum;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem cmsGoster;
        private System.Windows.Forms.ToolStripMenuItem cmsKapat;
        public System.Windows.Forms.ToolStripMenuItem tsmiSeciliUlke;
        public System.Windows.Forms.PictureBox pbox;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlmanya;
        private System.Windows.Forms.ToolStripMenuItem tsmiAmerika;
        private System.Windows.Forms.ToolStripMenuItem tsmiAvustralya;
        private System.Windows.Forms.ToolStripMenuItem tsmiBrezilya;
        private System.Windows.Forms.ToolStripMenuItem tsmiBritanya;
        private System.Windows.Forms.ToolStripMenuItem tsmiFransa;
        private System.Windows.Forms.ToolStripMenuItem tsmiHindistan;
        private System.Windows.Forms.ToolStripMenuItem tsmiJaponya;
        private System.Windows.Forms.ToolStripMenuItem tsmiKanada;
        private System.Windows.Forms.ToolStripMenuItem tsmiTurkiye;
    }
}

