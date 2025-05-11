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
    public partial class UrunIslemleri : Form
    {
        int rowindex = -1;
        DataModel dm = new DataModel();
        public UrunIslemleri()
        {
            InitializeComponent();
        }

        private void UrunIslemleri_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<Categories> clist = new List<Categories>();
            clist = dm.KategoriDoldur();
            foreach (var item in clist)
            {
                cbox_categories.Items.Add(item.CategoryName);
            }
            cbox_categories.SelectedIndex = 0;
            List<Brands> blist = new List<Brands>();
            blist = dm.MarkaDoldur();
            foreach (var item in blist)
            {
                cbox_brands.Items.Add(item.BrandName);
            }
            cbox_brands.SelectedIndex = 0;
            dataGridView1.DataSource = dm.UrunListele();
            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            List<Categories> listc = new List<Categories>();
            listc = dm.KategoriDoldur();
            List<Brands> listb = new List<Brands>();
            listb = dm.MarkaDoldur();
            if (!string.IsNullOrEmpty(cbox_categories.Text) && !string.IsNullOrEmpty(tb_productname.Text) && !string.IsNullOrEmpty(tb_piece.Text)&&!string.IsNullOrEmpty(tb_price.Text) && !string.IsNullOrEmpty(tb_diameter.Text) && !string.IsNullOrEmpty(tb_color.Text))
            {
                if (dm.UrunEkle(listc[cbox_categories.SelectedIndex].ID,listb[cbox_brands.SelectedIndex].ID,tb_productname.Text, Convert.ToInt32(tb_piece.Text),Convert.ToDecimal(tb_price.Text), tb_diameter.Text, tb_color.Text))
                {
                    MessageBox.Show("Ürün Başarıyla Eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Lütfen gerekli yerleri doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridView1.DataSource = dm.UrunListele();
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

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowindex != -1)
            {

                Products p = new Products();
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                p = dm.UrunDoldur(id);
                tb_productid.Text = p.ID.ToString();
                cbox_categories.Text = p.CategoryName;
                tb_productname.Text = p.ProductName;
                tb_piece.Text = p.Piece.ToString();
                tb_price.Text = p.Price.ToString();
                tb_diameter.Text = p.Diameter;
                tb_color.Text = p.Color;
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbox_categories.Text) && !string.IsNullOrEmpty(tb_productname.Text) && !string.IsNullOrEmpty(tb_piece.Text) && !string.IsNullOrEmpty(tb_price.Text) && !string.IsNullOrEmpty(tb_diameter.Text) && !string.IsNullOrEmpty(tb_color.Text))
            {
                List<Categories> list = new List<Categories>();
                list = dm.KategoriDoldur();
                List<Brands> listb = new List<Brands>();
                listb = dm.MarkaDoldur();
                dm.UrunDuzenle(Convert.ToInt32(tb_productid.Text),list[cbox_categories.SelectedIndex].ID, listb[cbox_brands.SelectedIndex].ID,tb_productname.Text,Convert.ToInt32(tb_piece.Text),Convert.ToDecimal(tb_price.Text),tb_diameter.Text,tb_color.Text);
                MessageBox.Show("Ürün Başarıyla Güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dataGridView1.DataSource = dm.UrunListele();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowindex != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                dm.UrunSil(id);
                dataGridView1.DataSource = dm.UrunListele();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_productid.Text = string.Empty;
            tb_piece.Text = string.Empty;
            tb_price.Text = string.Empty;
            tb_diameter.Text = string.Empty;
            tb_color.Text = string.Empty;
            tb_productname.Text = string.Empty;
        }

        private void tb_piece_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
