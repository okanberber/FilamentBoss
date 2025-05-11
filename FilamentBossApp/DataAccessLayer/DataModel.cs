using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccessLayer
{
    public class DataModel
    {
        SqlConnection con; SqlCommand cmd;
        
        public DataModel()
        { 
            con = new SqlConnection(ConnectionStrings.ConStr);
            cmd = con.CreateCommand();
        }
        public bool KategoriEkle(string Cname)
        {          
          try
          {
              cmd.CommandText = "INSERT INTO Categories (CategoryName) VALUES(@name)";
              cmd.Parameters.Clear();
              cmd.Parameters.AddWithValue("@name",Cname);
              con.Open();
              cmd.ExecuteNonQuery();
              return true;
          }
          catch
          {
              return false;
          }
          finally
          {
           con.Close();
          }                
        }
        public DataTable KategoriListele()
        {
            try
            {
                cmd.CommandText = "SELECT * FROM Categories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("kategori No");
                dt.Columns.Add("Kategori Isim");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    dt.Rows.Add(id, name);
                }
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public bool KategoriDuzenle(string cn,string id)
        {
            try
            {
                cmd.CommandText = "UPDATE Categories SET CategoryName=@cn WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cn", cn);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool KategoriSil(int id)
        {
            try
            {
                cmd.CommandText = "DELETE Categories WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;   
            }
            finally
            {
                con.Close() ;
            }
        }
        public List<Categories> KategoriDoldur()
        {
            List<Categories> list = new List<Categories>();
            try
            {
                cmd.CommandText = "SELECT ID,CategoryName FROM Categories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Categories c = new Categories();
                    c.ID = reader.GetInt32(0);
                    c.CategoryName = reader.GetString(1);
                    list.Add(c);
                }
                return list;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public Categories KategoriDoldur(string id)
        {
            Categories c = new Categories();
            try
            {
                cmd.CommandText = "SELECT ID,CategoryName FROM Categories WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c.ID = reader.GetInt32(0);
                    c.CategoryName = reader.GetString(1);
                }
                return c;
            }
            catch
            {
                return null ;
            }
            finally
            {
                con.Close( );
            }
        }
        public DataTable MarkaListele()
        {
            try
            {
                cmd.CommandText = "SELECT * FROM Brand";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Marka No");
                dt.Columns.Add("Marka Isim");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    dt.Rows.Add(id, name);
                }
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public bool MarkaEkle(string Bname)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Brand (BrandName) VALUES(@name)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", Bname);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool MarkaDuzenle(string bn, string id)
        {
            try
            {
                cmd.CommandText = "UPDATE Brand SET BrandName=@cn WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cn", bn);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool MarkaSil(int id)
        {
            try
            {
                cmd.CommandText = "DELETE Brand WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public List<Brands> MarkaDoldur()
        {
            List<Brands> list = new List<Brands>();
            try
            {
                cmd.CommandText = "SELECT ID,BrandName FROM Brand";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brands b = new Brands();
                    b.ID = reader.GetInt32(0);
                    b.BrandName = reader.GetString(1);
                    list.Add(b);
                }
                return list;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public Brands MarkaDoldur(string id)
        {
            Brands b = new Brands();
            try
            {
                cmd.CommandText = "SELECT ID,BrandName FROM Brand WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    b.ID = reader.GetInt32(0);
                    b.BrandName = reader.GetString(1);
                }
                return b;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public Products UrunDoldur(int id)
        {
            Products p = new Products();
            try
            {
                cmd.CommandText = "SELECT P.ID,P.CategoryID,P.BrandID,C.CategoryName,B.BrandName,P.ProductName,P.Piece,P.Price,P.Diameter,P.Color FROM Products AS P JOIN Categories AS C ON P.CategoryID = C.ID JOIN Brand AS B ON P.BrandID = B.ID WHERE P.ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    p.ID = reader.GetInt32(0);
                    p.CategoryID = reader.GetInt32(1);
                    p.BrandID = reader.GetInt32(2);
                    p.CategoryName = reader.GetString (3);
                    p.BrandName = reader.GetString (4);
                    p.ProductName = reader.GetString(5);
                    p.Piece=reader.GetInt32(6);
                    p.Price = reader.GetDecimal(7);
                    p.Diameter=reader.GetString(8);
                    p.Color=reader.GetString(9);
                }
                return p;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public List<Products> UrunDoldur(string X)
        {
            int indirim = 0;
            if (X == "BRONZ")
            {
                indirim = 2;
            }
            
            if (X == "SİLVER")
            {
                indirim = 5;
            }
            if(X == "GOLD")
            {
                indirim = 10;
            }
            
            List<Products> liste = new List<Products>();
            
            try
            {
                cmd.CommandText = "SELECT P.ID,P.CategoryID,P.BrandID,C.CategoryName,B.BrandName,P.ProductName,P.Piece,P.Price,P.Diameter,P.Color FROM Products AS P JOIN Categories AS C ON P.CategoryID = C.ID JOIN Brand AS B ON P.BrandID = B.ID";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Products p = new Products();
                    p.ID = reader.GetInt32(0);
                    p.CategoryID = reader.GetInt32(1);
                    p.BrandID = reader.GetInt32(2);
                    p.CategoryName = reader.GetString(3);
                    p.BrandName = reader.GetString(4);
                    p.ProductName = reader.GetString(5);
                    p.Piece = reader.GetInt32(6);
                    p.Price = reader.GetDecimal(7)-(reader.GetDecimal(7)*indirim)/100;
                    p.Diameter = reader.GetString(8);
                    p.Color = reader.GetString(9);
                    liste.Add(p);
                }
                return liste;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UrunEkle(int Cid,int Bid,string Pname,int Ppie,decimal Ppri,string Pdia,string Pcol)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Products (CategoryID,BrandID,ProductName,Piece,Price,Diameter,Color) VALUES(@cid,@bid,@name,@pie,@pri,@dia,@col)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cid",Cid);
                cmd.Parameters.AddWithValue("@bid", Bid);
                cmd.Parameters.AddWithValue("@name", Pname);
                cmd.Parameters.AddWithValue("@pie",Ppie);
                cmd.Parameters.AddWithValue("@pri", Ppri);
                cmd.Parameters.AddWithValue("@dia",Pdia);
                cmd.Parameters.AddWithValue("@col",Pcol);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable UrunListele()
        {
            try
            {
                cmd.CommandText = "SELECT P.ID,P.CategoryID,P.BrandID,C.CategoryName,B.BrandName,P.ProductName,P.Piece,P.Price,P.Diameter,P.Color FROM Products AS P JOIN Categories AS C ON P.CategoryID = C.ID JOIN Brand AS B ON P.BrandID = B.ID";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Ürün No");
                dt.Columns.Add("Kategori No");
                dt.Columns.Add("Marka No");
                dt.Columns.Add("Kategori Adı");
                dt.Columns.Add("Marka Adı");
                dt.Columns.Add("Ürün Adı");
                dt.Columns.Add("Ürün Adedi");
                dt.Columns.Add("Ürün Fiyatı");
                dt.Columns.Add("Ürün Çapı");
                dt.Columns.Add("Ürün Rengi");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int cid = reader.GetInt32(1);
                    int bid=reader.GetInt32(2);
                    string cname = reader.GetString(3);
                    string bname = reader.GetString(4);
                    string pname = reader.GetString(5);
                    int pie = reader.GetInt32(6);
                    decimal pri = reader.GetDecimal(7);
                    string dia = reader.GetString(8);
                    string col = reader.GetString(9);
                    dt.Rows.Add(id,cid,bid,cname,bname,pname,pie,pri,dia,col);
                }
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UrunDuzenle(int Pid,int Cid,int Bid,string Pname,int Pie,decimal Pri,string Dia,string Pcol)
        {
            try
            {
                cmd.CommandText = "UPDATE Products SET CategoryID=@cid,BrandID=@bid,ProductName=@pname,Piece=@pie,Price=@pri,Diameter=@dia,Color=@col WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cid", Cid);
                cmd.Parameters.AddWithValue("@bid",Bid);
                cmd.Parameters.AddWithValue("@pname",Pname);
                cmd.Parameters.AddWithValue("@pie", Pie);
                cmd.Parameters.AddWithValue("@pri",Pri);
                cmd.Parameters.AddWithValue("@dia", Dia);
                cmd.Parameters.AddWithValue("@col",Pcol);
                cmd.Parameters.AddWithValue("@id", Pid);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UrunSil(int id)
        {
            try
            {
                cmd.CommandText = "DELETE Products WHERE ID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        
        public bool XMLOlusturBronz()
        {
            DataModel dm = new DataModel();
            List<Products> productList = dm.UrunDoldur("BRONZ");

            try
            {
                // Hedef klasör yolu
                string directoryPath = @"C:\Export";

                // Klasör yoksa oluştur
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Dosya yolunu oluştur
                string filePath = Path.Combine(directoryPath, "Bronz.xml");

                // XML dosyasını yaz
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Products>));
                    serializer.Serialize(sw, productList);
                }

                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool XMLOlusturSilver()
        {
            DataModel dm = new DataModel();
            List<Products> productList = dm.UrunDoldur("SİLVER");
            try
            {
                string directoryPath = @"C:\Export";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = Path.Combine(directoryPath, "Silver.xml");
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Products>));
                    serializer.Serialize(sw, productList);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XMLOlusturGold()
        {
            DataModel dm = new DataModel();
            List<Products> productList = dm.UrunDoldur("GOLD");
            try
            {
                string directoryPath = @"C:\Export";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = Path.Combine(directoryPath, "Gold.xml");
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Products>));
                    serializer.Serialize(sw, productList);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
