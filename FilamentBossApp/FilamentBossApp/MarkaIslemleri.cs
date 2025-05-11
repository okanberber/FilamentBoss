using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;

namespace FilamentBossApp
{
    public partial class MarkaIslemleri : Form
    {
        int rowindex = -1;
        DataModel dm = new DataModel();
        public MarkaIslemleri()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_brandName.Text))
            {
                if (dm.MarkaEkle(tb_brandName.Text))
                {
                    MessageBox.Show("Marka Başarıyla Eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Marka Adı Boş Bırakılamaz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridView1.DataSource = dm.MarkaListele();
        }

        private void MarkaIslemleri_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = dm.MarkaListele();
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowindex != -1)
            {
                Brands b = new Brands();
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                b = dm.MarkaDoldur(id.ToString());
                tb_brandID.Text = b.ID.ToString();
                tb_brandName.Text = b.BrandName;

            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                rowindex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (rowindex != -1)
                {
                    contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
                    dataGridView1.Rows[rowindex].Selected = true;
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_brandName.Text) && !string.IsNullOrEmpty(tb_brandID.Text))
            {
                dm.MarkaDuzenle(tb_brandName.Text, tb_brandID.Text);
                MessageBox.Show("Marka Başarıyla Güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dataGridView1.DataSource = dm.MarkaListele();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowindex != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                if (dm.MarkaSil(id))
                {
                    MessageBox.Show("Marka Başarıyla silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ürünü Olan Marka Silinemez", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dataGridView1.DataSource = dm.MarkaListele();
            }
        }
    }
}
