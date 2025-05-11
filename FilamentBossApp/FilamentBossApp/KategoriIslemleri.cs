using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;

namespace FilamentBossApp
{
    public partial class KategoriIslemleri : Form
    {
        int rowindex=-1;
        DataModel dm = new DataModel();
        public KategoriIslemleri()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(tb_categoryName.Text)) 
                {
                if (dm.KategoriEkle(tb_categoryName.Text))
                {
                    MessageBox.Show("Kategori Başarıyla Eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            else
            {
                MessageBox.Show("Lütfen gerekli alanları doldurunuz","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
                dataGridView1.DataSource = dm.KategoriListele();

        }

        private void KategoriIslemleri_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = dm.KategoriListele();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                rowindex = dataGridView1.HitTest(e.X,e.Y).RowIndex;
                if(rowindex != -1)
                {
                    contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
                    dataGridView1.Rows[rowindex].Selected = true;    
                }                
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(rowindex != -1)
            {
                Categories c = new Categories();
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                c=dm.KategoriDoldur(id.ToString());
                tb_id.Text=c.ID.ToString();
                tb_categoryName.Text=c.CategoryName;
                
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_categoryName.Text)&&!string.IsNullOrEmpty(tb_id.Text))
            {
                dm.KategoriDuzenle(tb_categoryName.Text,tb_id.Text);
                MessageBox.Show("Kategori Başarıyla Güncellendi","Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            dataGridView1.DataSource = dm.KategoriListele();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowindex != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                if (dm.KategoriSil(id))
                {
                    MessageBox.Show("Kategori Başarıyla silindi","Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information );
                }
                else
                {
                    MessageBox.Show("Ürünü Olan Kategori Silinemez","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error );
                }
                    dataGridView1.DataSource = dm.KategoriListele();
            }
        }
    }
}
