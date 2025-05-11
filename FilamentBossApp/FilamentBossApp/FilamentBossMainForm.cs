using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using DataAccessLayer;

namespace FilamentBossApp
{
    public partial class FilamentBossMainForm : Form
    {
        public FilamentBossMainForm()
        {
            InitializeComponent();
        }

        private void FilamentBossMainForm_Load(object sender, EventArgs e)
        {
            AdminLogin frm = new AdminLogin();
            frm.ShowDialog();
            KategoriIslemleri kategorifrm = new KategoriIslemleri();
            kategorifrm.MdiParent = this;
            kategorifrm.WindowState = FormWindowState.Maximized;
            kategorifrm.Show();
        }

        private void kategoriİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] acikFormlar = this.MdiChildren;
            bool acikmi = false;
            foreach (Form form in acikFormlar)
            {
                if (form.GetType() == typeof(KategoriIslemleri))
                {
                    acikmi = true;
                    form.Activate();//Form Acilmissa En One Getir
                }
            }
            if (acikmi == false)
            {
                KategoriIslemleri frm = new KategoriIslemleri();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void ürünİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] acikFormlar = this.MdiChildren;
            bool acikmi = false;
            foreach (Form form in acikFormlar)
            {
                if (form.GetType() == typeof(UrunIslemleri))
                {
                    acikmi = true;
                    form.Activate();//Form Acilmissa En One Getir
                }
            }
            if (acikmi == false)
            {
                UrunIslemleri frm = new UrunIslemleri();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void xMLOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataModel dm = new DataModel();
            dm.XMLOlusturBronz();
            dm.XMLOlusturSilver();
            dm.XMLOlusturGold();
            MessageBox.Show("XML Başarıyla oluşturuldu","Başarılı",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void markaİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] acikFormlar = this.MdiChildren;
            bool acikmi = false;
            foreach (Form form in acikFormlar)
            {
                if (form.GetType() == typeof(MarkaIslemleri))
                {
                    acikmi = true;
                    form.Activate();//Form Acilmissa En One Getir
                }
            }
            if (acikmi == false)
            {
                MarkaIslemleri frm = new MarkaIslemleri();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }
    }
}
