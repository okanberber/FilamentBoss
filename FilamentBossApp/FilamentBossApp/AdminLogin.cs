using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;

namespace FilamentBossApp
{
    public partial class AdminLogin : Form
    {
        bool islogin = false;
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings.ConStr))
            {
                if(!string.IsNullOrEmpty(tb_mail.Text)&&!string.IsNullOrEmpty(tb_password.Text))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT ID,Name,Surname,Mail,Password FROM Admin WHERE Mail = @mail AND Password = @pass";
                    cmd.Parameters.AddWithValue("@mail",tb_mail.Text);
                    cmd.Parameters.AddWithValue("@pass",tb_password.Text);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    User u = null;
                    while (reader.Read())
                    {
                        u = new User();
                        u.ID = reader.GetInt32(0);
                        u.Name = reader.GetString(1);
                        u.Surname = reader.GetString(2);
                        u.Mail = reader.GetString(3);
                        u.Password = reader.GetString(4);
                    }
                    
                    if (u != null)
                    {
                        islogin = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Bulunamadı", "Uyarı", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı ve Şifre boş bırakılamaz", "Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(islogin == false)
            {
                Application.Exit();
            }
        }
    }
}
