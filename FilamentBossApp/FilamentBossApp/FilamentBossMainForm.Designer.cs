namespace FilamentBossApp
{
    partial class FilamentBossMainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kategoriİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ürünİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markaİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaToolStripMenuItem,
            this.kategoriİşlemleriToolStripMenuItem,
            this.markaİşlemleriToolStripMenuItem,
            this.ürünİşlemleriToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1295, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLOluşturToolStripMenuItem});
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.dosyaToolStripMenuItem.Text = "Dosya";
            // 
            // xMLOluşturToolStripMenuItem
            // 
            this.xMLOluşturToolStripMenuItem.Name = "xMLOluşturToolStripMenuItem";
            this.xMLOluşturToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.xMLOluşturToolStripMenuItem.Text = "XML Oluştur";
            this.xMLOluşturToolStripMenuItem.Click += new System.EventHandler(this.xMLOluşturToolStripMenuItem_Click);
            // 
            // kategoriİşlemleriToolStripMenuItem
            // 
            this.kategoriİşlemleriToolStripMenuItem.Name = "kategoriİşlemleriToolStripMenuItem";
            this.kategoriİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.kategoriİşlemleriToolStripMenuItem.Text = "Kategori İşlemleri";
            this.kategoriİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.kategoriİşlemleriToolStripMenuItem_Click);
            // 
            // ürünİşlemleriToolStripMenuItem
            // 
            this.ürünİşlemleriToolStripMenuItem.Name = "ürünİşlemleriToolStripMenuItem";
            this.ürünİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.ürünİşlemleriToolStripMenuItem.Text = "Ürün İşlemleri";
            this.ürünİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.ürünİşlemleriToolStripMenuItem_Click);
            // 
            // markaİşlemleriToolStripMenuItem
            // 
            this.markaİşlemleriToolStripMenuItem.Name = "markaİşlemleriToolStripMenuItem";
            this.markaİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.markaİşlemleriToolStripMenuItem.Text = "Marka İşlemleri";
            this.markaİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.markaİşlemleriToolStripMenuItem_Click);
            // 
            // FilamentBossMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 649);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FilamentBossMainForm";
            this.Text = "FilamentBoss A.Ş";
            this.Load += new System.EventHandler(this.FilamentBossMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLOluşturToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kategoriİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ürünİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markaİşlemleriToolStripMenuItem;
    }
}