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
using System.Data.OleDb;
using System.Configuration;

namespace GomlekMaliyeti
{
    public partial class Form2 : Form
    {

        Form3 frm3;
        
        public Form2()
        {
            InitializeComponent();
        }

        double dugumfiyat = 0;
        double topdgmfiyat = 0;

        double toplam = 0;

        double boyfiyat = 0;
        double topbyfiyat = 0;

        double kmsfiyat1 = 0;
        double kmsfiyat2 = 0;
        double kmsfiyat3 = 0;

        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
        //SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-60D38TB;Initial Catalog=GomlekMaliyet;Integrated Security=True");


        private void Form2_Load(object sender, EventArgs e)
        {
            //veri çekme

            SqlCommand komut = new SqlCommand("select * from Dugme",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBox1.ValueMember = "DugmeId";
            comboBox1.DisplayMember = "DugmeTipi";
            comboBox1.DataSource = dt;
            

            SqlCommand komut1 = new SqlCommand("select * from Boya", baglanti);
            SqlDataAdapter da1 = new SqlDataAdapter(komut1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            comboBox2.ValueMember = "BoyaId";
            comboBox2.DisplayMember = "BoyaAdi";
            comboBox2.DataSource = dt1;


            SqlCommand komut2 = new SqlCommand("select * from Kumas", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            comboBox3.ValueMember = "KumasId";
            comboBox3.DisplayMember = "KumasAdi";
            comboBox3.DataSource = dt2;


            SqlCommand komut3 = new SqlCommand("Select * from Beden", baglanti);
            SqlDataAdapter da3 = new SqlDataAdapter(komut3);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);

            comboBox4.ValueMember = "BedenId";
            comboBox4.DisplayMember = "BedenAdi";
            comboBox4.DataSource = dt3;


            SqlCommand komut4 = new SqlCommand("Select * from Tip", baglanti);
            SqlDataAdapter da4 = new SqlDataAdapter(komut4);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            comboBox5.ValueMember = "TipId";
            comboBox5.DisplayMember = "TipAdi";
            comboBox5.DataSource = dt4;

            for(int i = 2; i < 15; i++)
            {
                dgmsys.Items.Add(i);
            }
            comboBox7.Items.Add(0.1);
            comboBox7.Items.Add(0.2);
            comboBox7.Items.Add(0.3);
            comboBox7.Items.Add(0.4);
            comboBox7.Items.Add(0.5);
            comboBox7.Items.Add(0.6);
            comboBox7.Items.Add(0.7);
            comboBox7.Items.Add(0.8);
            comboBox7.Items.Add(0.9);
            
            
            
            

        }

        //HESAPLAMA
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dugumfiyat = Convert.ToDouble(dgmtxt.Text);
                topdgmfiyat = Convert.ToInt32(dgmsys.Text) * dugumfiyat;

                boyfiyat = Convert.ToDouble(bytxt.Text);
                topbyfiyat = Convert.ToDouble(comboBox7.Text) * boyfiyat;

                kmsfiyat1 = (Convert.ToDouble(cistxt.Text) * Convert.ToDouble(cstxt.Text) * Convert.ToDouble(kmstxt.Text));
                kmsfiyat2 = (Convert.ToDouble(aistxt1.Text) * Convert.ToDouble(astxt.Text) * Convert.ToDouble(kmstxt.Text));
                kmsfiyat3 = kmsfiyat1 + kmsfiyat2;

                toplam = kmsfiyat3 + topbyfiyat + topdgmfiyat;
                sonctxt.Text = toplam.ToString();

               

            }
            catch
            {
                MessageBox.Show("Lütfen Boş Alan bırakmayınız");
            }

            



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("Select DugmeFiyat From Dugme where DugmeTipi=@p1",baglanti);
            kmt1.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = kmt1.ExecuteReader();

            while (dr.Read())
            {
                dgmtxt.Text = dr[0].ToString();
            }

            baglanti.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("Select BoyaFiyat From Boya where BoyaAdi=@p1", baglanti);
            kmt1.Parameters.AddWithValue("@p1", comboBox2.Text);
            SqlDataReader dr = kmt1.ExecuteReader();

            while (dr.Read())
            {
                bytxt.Text = dr[0].ToString();
            }

            baglanti.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("Select KumasFiyat From Kumas where KumasAdi=@p1", baglanti);
            kmt1.Parameters.AddWithValue("@p1", comboBox3.Text);
            SqlDataReader dr = kmt1.ExecuteReader();

            while (dr.Read())
            {
                kmstxt.Text = dr[0].ToString();
            }

            baglanti.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //veri tabanına kaydetme

                baglanti.Open();

                int beId = Convert.ToInt32(comboBox4.SelectedValue);
                int boId = Convert.ToInt32(comboBox2.SelectedValue);
                int duId = Convert.ToInt32(comboBox1.SelectedValue);
                int kuId = Convert.ToInt32(comboBox3.SelectedValue);
                int tiId = Convert.ToInt32(comboBox5.SelectedValue);
                int brk = Convert.ToInt32(barkodtxt.Text);

                SqlCommand kmt2 = new SqlCommand("INSERT INTO Gomlek(GomlekAdi,BedenId,BoyaId,DugmeId,KumasId,TipId,Barkod) VALUES(@p15,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);

                kmt2.Parameters.AddWithValue("@p15", textBox1.Text);
                kmt2.Parameters.AddWithValue("@p2", beId);
                kmt2.Parameters.AddWithValue("@p3", boId);
                kmt2.Parameters.AddWithValue("@p4", duId);
                kmt2.Parameters.AddWithValue("@p5", kuId);
                kmt2.Parameters.AddWithValue("@p6", tiId);
                kmt2.Parameters.AddWithValue("@p7", brk);

                kmt2.ExecuteNonQuery();


                int olsyil = Convert.ToInt32(olstxt.Text);
                Double mf = Convert.ToDouble(sonctxt.Text);
                int sf = Convert.ToInt32(sftxt.Text);

                String sorgu = "SELECT TOP 1 GomlekId FROM Gomlek ORDER BY GomlekId DESC";
                SqlCommand kmt3 = new SqlCommand(sorgu, baglanti);
                int id = Convert.ToInt32(kmt3.ExecuteScalar());



                SqlCommand kmt4 = new SqlCommand("INSERT INTO Donem(DonemYil,GomlekId,BoyaId,KumasId,DugmeId,GomlekMaliyetFiyat,GomlekSatisFiyat) VALUES(@p8,@p9,@p10,@p11,@p12,@p13,@p14)", baglanti);

                kmt4.Parameters.AddWithValue("@p8", olsyil);
                kmt4.Parameters.AddWithValue("@p9", id);
                kmt4.Parameters.AddWithValue("@p10", boId);
                kmt4.Parameters.AddWithValue("@p11", kuId);
                kmt4.Parameters.AddWithValue("@p12", duId);
                kmt4.Parameters.AddWithValue("@p13", mf);
                kmt4.Parameters.AddWithValue("@p14", sf);

                kmt4.ExecuteNonQuery();

                double df = Convert.ToDouble(dgmtxt.Text);
                double bf = Convert.ToDouble(bytxt.Text);
                double kf = Convert.ToDouble(kmstxt.Text);

                SqlCommand kmt5 = new SqlCommand("INSERT INTO MaddeYil(DonemYili,DugmeFiyati,KumasFiyati,BoyaFiyati,DugmeId,KumasId,BoyaId,GomlekId) VALUES(@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15)", baglanti);


                kmt5.Parameters.AddWithValue("@p8", olsyil);
                kmt5.Parameters.AddWithValue("@p9", df);
                kmt5.Parameters.AddWithValue("@p10", kf);
                kmt5.Parameters.AddWithValue("@p11", bf);
                kmt5.Parameters.AddWithValue("@p12", duId);
                kmt5.Parameters.AddWithValue("@p13", kuId);
                kmt5.Parameters.AddWithValue("@p14", boId);
                kmt5.Parameters.AddWithValue("@p15", id);

                kmt5.ExecuteNonQuery();
                MessageBox.Show("Yeni Ürün Başarılı bir şekilde Oluşturuldu");


                baglanti.Close();

            #region LogKullanıcıkaydet
            baglanti.Open();
            SqlDataAdapter kulla = new SqlDataAdapter();

            int a = Form3.k_id;
            SqlCommand kmt11 = new SqlCommand("UPDATE Gomlek_Log SET KullaniciId=@p1 where GL_ID=(SELECT top 1 GL_ID  FROM Gomlek_Log order by GL_ID desc)", baglanti);
            kmt11.Parameters.AddWithValue("@p1", a);


            kulla.SelectCommand = kmt11;
            DataSet ds11 = new DataSet();
            ds11.Clear();
            kulla.Fill(ds11);
            baglanti.Close();
            #endregion

        }
    }

}
