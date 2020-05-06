using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace GomlekMaliyeti
{
    public partial class Form3 : Form
    {
        Form1 frm1;
        

        public static int k_id;
        public static string k_adi;

        public Form3()
        {
            InitializeComponent();
        }
        
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void kgrs_Click(object sender, EventArgs e)
        {
           

            if (kaditxt.Text == "" || sfrtxt.Text == "")
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Alanlarını Boş Bırakamazsınız.");
                return;
            }
            
            try
            {
                baglanti.Open();
                SqlDataAdapter Kullanici = new SqlDataAdapter();
                SqlCommand KullaniciSec = new SqlCommand();
                KullaniciSec.Connection = baglanti;

                KullaniciSec.CommandText = "SELECT * FROM Kullanici WHERE KullaniciAdi=@KullaniciAdi";
                KullaniciSec.Parameters.AddWithValue("@KullaniciAdi", kaditxt.Text);

                Kullanici.SelectCommand = KullaniciSec;


                DataSet KullaniciDS = new DataSet();
                KullaniciDS.Clear();
                Kullanici.Fill(KullaniciDS);

                if (KullaniciDS.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sistemde böyle bir kullanıcı bulunmamaktadır.");
                    baglanti.Close();
                    return;
                }

                string Sifresi = KullaniciDS.Tables[0].Rows[0][2].ToString();

                if (Sifresi == sfrtxt.Text)
                {
                
                    MessageBox.Show("Giriş Başarılı");

                    SqlDataAdapter Kul = new SqlDataAdapter();
                    SqlCommand kid = new SqlCommand();
                    

                    kid.Connection = baglanti;
                    kid.CommandText = "SELECT KullaniciId,KullaniciAdi FROM Kullanici WHERE KullaniciSifre=@KullaniciSifre";
                    kid.Parameters.AddWithValue("@KullaniciSifre",Sifresi);
                    Kul.SelectCommand = kid;


                    DataSet KulDS = new DataSet();
                    KulDS.Clear();
                    Kul.Fill(KulDS);

                    string ss = KulDS.Tables[0].Rows[0][0].ToString();
                    
                    k_id = Convert.ToInt32(ss);

                    
                    frm1 = new Form1();
                        frm1.Show();

                    this.Hide();            
                    baglanti.Close();
                }
                else
                {
                    MessageBox.Show("Giriş Başarısız");
                    baglanti.Close();
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
