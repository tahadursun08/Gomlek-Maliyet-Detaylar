using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;


namespace GomlekMaliyeti
{
    ///dataGridView1.Enabled = false; bunları koyma sebebimiz sağ clicteki seçenekler gelmesin 

    public partial class Form1 : Form
    {

        Form2 frm2; //= new Form2();
        Form3 frm3;
        int id;

        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);

        // SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-60D38TB;Initial Catalog=GomlekMaliyet;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            
            if (Form3.k_id == 1)
            {
                logkayitbtn.Visible = true;
                dtgridviewlog.Visible = true;
                gsrbtn.Visible = true;
            }
            groupBox1.Hide();

            baglanti.Open();
            MenuStrip MnuStrip = new MenuStrip();
            this.Controls.Add(MnuStrip);

            string sql = "SELECT ModulAdi,ModulId,Status FROM Moduller";
            SqlDataAdapter da = new SqlDataAdapter(sql, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            frm3 = new Form3();
            yetkial yal = new yetkial();
            yal.yetkicek(Form3.k_id, 2);
            foreach (DataRow dr in dt.Rows)
            {
                ToolStripMenuItem MnuStripItem = new ToolStripMenuItem(dr["ModulAdi"].ToString());
                string a = dr["ModulAdi"].ToString();
                if (yetkiler.Rol == 4)
                {

                    if (a == "Maliyet")
                    {
                        MnuStripItem.Visible = false;
                    }
                    if (a == "Satis")
                    {
                        MnuStripItem.Visible = false;
                    }

                }
                if (yetkiler.Rol == 3)
                {

                    if (a == "Boya")
                    {
                        MnuStripItem.Visible = false;
                    }
                    if (a == "Iplik")
                    {
                        MnuStripItem.Visible = false;
                    }
                    if (a == "Dugme")
                    {
                        MnuStripItem.Visible = false;
                    }

                }
                if (yetkiler.Rol == 2)
                {

                    if (a == "Maliyet")
                    {
                        MnuStripItem.Visible = false;
                    }
                    if (a == "Satis")
                    {
                        MnuStripItem.Visible = false;
                    }
                    if (a == "Stok Takip")
                    {
                        MnuStripItem.Visible = false;
                    }

                }
                if (yetkiler.Rol == 1)
                {

                    // istersen eklersin mudür rolü

                }

                subMenu(MnuStripItem, dr["ModulId"].ToString());
                MnuStrip.Items.Add(MnuStripItem);
            }
            this.MainMenuStrip = MnuStrip;
            baglanti.Close();
        }

        //submenu
        yetkial yal = new yetkial();
 
        public void subMenu(ToolStripMenuItem mnu, string submenu)
        {
            
            String Seqchild = "SELECT AltModulAd FROM AltModuller WHERE ModulId = '" + submenu + "'";
            SqlDataAdapter dachildmnu = new SqlDataAdapter(Seqchild, baglanti);
            DataTable dtchild = new DataTable();
            dachildmnu.Fill(dtchild);
            //yetkilendirme bölümü
            
            foreach (DataRow dr in dtchild.Rows)
            {
                ToolStripMenuItem SSMenu = new ToolStripMenuItem(dr["AltModulAd"].ToString(), null, new EventHandler(ChildClick));
                string a = dr["AltModulAd"].ToString();
                yal.yetkicek(Form3.k_id, 3);
                if (yetkiler.Rol == 4)
                {
                    if(a == "Gomlek Detay")
                    {
                        if (yetkiler.Ekleyebilir == true)
                        {
                            ekleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Guncelleyebilir == true)
                        {
                            guncelleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Silebilir == true)
                        {
                            silToolStripMenuItem.Enabled = true;
                        }
                    }
                    if (a == "Gomlek Olustur")
                    {
                        if (yetkiler.Ekleyebilir == false && yetkiler.Silebilir == false && yetkiler.Guncelleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                    if (a == "Iplik Ekle")
                    {
                        if (yetkiler.Ekleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                    if(a == "Boya Ekle")
                    {
                        if (yetkiler.Ekleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                    if (a == "Dugme Ekle")
                    {
                        if (yetkiler.Ekleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                    if (a == "GomlekDetay")
                    {
                        if (yetkiler.Ekleyebilir == true)
                        {
                            ekleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Guncelleyebilir == true)
                        {
                            guncelleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Silebilir == true)
                        {
                            silToolStripMenuItem.Enabled = true;
                        }
                    }
                }
                if (yetkiler.Rol == 3)
                {
                    if (a == "Gomlek Detay")
                    {
                        if (yetkiler.Ekleyebilir == true)
                        {
                            ekleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Guncelleyebilir == true)
                        {
                            guncelleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Silebilir == true)
                        {
                            silToolStripMenuItem.Enabled = true;
                        }
                    }

                    if (a == "Gomlek Olustur")
                    {
                        if (yetkiler.Ekleyebilir == false && yetkiler.Silebilir == false && yetkiler.Guncelleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                }
                if (yetkiler.Rol == 2)
                {
                    if (a == "Gomlek Detay")
                    {
                        if (yetkiler.Ekleyebilir == true)
                        {
                            ekleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Guncelleyebilir == true)
                        {
                            guncelleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Silebilir == true)
                        {
                            silToolStripMenuItem.Enabled = true;
                        }
                    }

                    if (a == "Gomlek Olustur")
                    {
                        if (yetkiler.Ekleyebilir == false && yetkiler.Silebilir == false && yetkiler.Guncelleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                    if (a == "Iplik Ekle")
                    {
                        if (yetkiler.Ekleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                    if (a == "Boya Ekle")
                    {
                        if (yetkiler.Ekleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                    if (a == "Dugme Ekle")
                    {
                        if (yetkiler.Ekleyebilir == false)
                        {
                            SSMenu.Enabled = false;
                        }
                    }
                }
                if (yetkiler.Rol == 1)
                {
                    if (a == "Gomlek Detay")
                    {
                        if (yetkiler.Ekleyebilir == true)
                        {
                            ekleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Guncelleyebilir == true)
                        {
                            guncelleToolStripMenuItem.Enabled = true;
                        }
                        if (yetkiler.Silebilir == true)
                        {
                            silToolStripMenuItem.Enabled = true;
                        }
                    }
                }
                mnu.DropDownItems.Add(SSMenu);
            }
           
        }

        //altmodulde click olayı gerçekleşince
        public void ChildClick(object sender, EventArgs e)
        {       
            if (sender.ToString()== "Gomlek Listesi")
            {
                dataGridView1.Enabled = false;
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeGomlek");
            }
            if (sender.ToString() == "Gomlek Detay")
            {
                dataGridView1.Enabled = true;
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute Listele");
            }
            if (sender.ToString() == "Gomlek Olustur")
            {
                frm2 = new Form2();
                frm2.Show();
            }
            if (sender.ToString() == "Iplik Listesi")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeIplik");
                dataGridView1.Enabled = false;
            }
            if (sender.ToString() == "Iplik Ekle")
            {
                
                dataGridView1.Enabled = true;
            }
            if (sender.ToString() == "Boya Listesi")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeBoya");
                dataGridView1.Enabled = false;
            }
            if (sender.ToString() == "Boya Ekle")
            {
                
                dataGridView1.Enabled = true;
            }
            if (sender.ToString() == "Dugme Listesi")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeDugme");
                dataGridView1.Enabled = false;
            }
            if (sender.ToString() == "Dugme Ekle")
            {
                
                dataGridView1.Enabled = true;
            }
            if (sender.ToString() == "Maliyet Toplam")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeGomlek");
            }
            if (sender.ToString() == "Maliyet Hesapla")
            {
                if (frm2 == null || frm2.IsDisposed)
                {
                    frm2 = new Form2();
                    frm2.Show();
                }
                else
                {
                    MessageBox.Show("zaten açık");

                }
            }
            if (sender.ToString() == "Satıs Olustur")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeGomlek");
            }
            if (sender.ToString() == "Satıs Hesapla")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeGomlek");
            }
            if (sender.ToString() == "Stok Dugme")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeDugme");
            }
            if (sender.ToString() == "Stok Boya")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeBoya");
            }
            if (sender.ToString() == "Stok Iplik")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeIplik");
            }
            if (sender.ToString() == "Urun Asama")
            {
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele("Execute ListeGomlek");
            }
            if(sender.ToString() == "Stok Gomlek")
            {
                string sql =" select * from Gomlek_Stok";
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele(sql);
            }
            if (sender.ToString()== "Iplik Guncel Rapor")
            {
                string sql = "SELECT * FROM BelliAralikUrun (1, 6)";
                listeal l = new listeal();
                dataGridView1.DataSource = l.listele(sql);
                
                //SqlDataAdapter da = new SqlDataAdapter(sql, baglanti);
                //DataSet ds = new DataSet();
                //DataTable dt = new DataTable();
                //da.Fill(ds);
                //dt = ds.Tables[0];
                //dataGridView1.DataSource = dt;
            }
            

           
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        //güncelle
        private void guncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Show();          
            SqlCommand komut = new SqlCommand("select * from Dugme", baglanti);
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

            textBox1.Text = dataGridView1.CurrentRow.Cells["GomlekAdi"].Value.ToString();
            dytxt.Text = dataGridView1.CurrentRow.Cells["DonemYil"].Value.ToString();
            sftxt.Text = dataGridView1.CurrentRow.Cells["GomlekSatisFiyat"].Value.ToString();

            baglanti.Close();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dataGridView1.HitTest(e.X, e.Y);
                dataGridView1.ClearSelection();
                dataGridView1.Rows[hti.RowIndex].Selected = true;
            }
        }

        //sil
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            //seçilen satırı verisini silme
            baglanti.Open();

            string sorgu1 = "DELETE FROM Gomlek WHERE GomlekId=@id";
            int id1 = Convert.ToInt32(dataGridView1.CurrentRow.Cells["GomlekId"].Value);

            SqlCommand kmt1 = new SqlCommand(sorgu1, baglanti);
            kmt1.Parameters.AddWithValue("@id", id1);

            kmt1.ExecuteNonQuery();

            //////veri tabanından silinen veriyi anlık olarak datagiridviewdan cıkar
            int rowToDelete = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            dataGridView1.Rows.RemoveAt(rowToDelete);
            dataGridView1.ClearSelection();

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("Select DugmeFiyat From Dugme where DugmeTipi=@p1", baglanti);
            kmt1.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = kmt1.ExecuteReader();

            while (dr.Read())
            {
                dftxt.Text = dr[0].ToString();
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
                bftxt.Text = dr[0].ToString();
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
                kftxt.Text = dr[0].ToString();
            }

            baglanti.Close();
        }
        //güncelle ve kaydet
        private void gncbutton_Click(object sender, EventArgs e)
        {
            listele.Visible = true;
            try
            {
                baglanti.Open();
                int gid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["GomlekId"].Value);
                int beId = Convert.ToInt32(comboBox4.SelectedValue);
                int boId = Convert.ToInt32(comboBox2.SelectedValue);
                int duId = Convert.ToInt32(comboBox1.SelectedValue);
                int kuId = Convert.ToInt32(comboBox3.SelectedValue);
                int tiId = Convert.ToInt32(comboBox5.SelectedValue);

                SqlCommand kmt2 = new SqlCommand("UPDATE Gomlek SET GomlekAdi=@p1,BedenId=@p2,BoyaId=@p3,DugmeId=@p4,KumasId=@p5,TipId=@p6 where GomlekId=@p9 ", baglanti);

                kmt2.Parameters.AddWithValue("@p1", textBox1.Text);
                kmt2.Parameters.AddWithValue("@p2", beId);
                kmt2.Parameters.AddWithValue("@p3", boId);
                kmt2.Parameters.AddWithValue("@p4", duId);
                kmt2.Parameters.AddWithValue("@p5", kuId);
                kmt2.Parameters.AddWithValue("@p6", tiId);
                kmt2.Parameters.AddWithValue("@p9", gid);
                kmt2.ExecuteNonQuery();

                int did = Convert.ToInt32(dataGridView1.CurrentRow.Cells["DonemId"].Value);
                SqlCommand kmt3 = new SqlCommand("UPDATE Donem SET DonemYil=@p7,GomlekSatisFiyat=@p8 where DonemId=@p10 ", baglanti);
                kmt3.Parameters.AddWithValue("@p7", dytxt.Text);
                kmt3.Parameters.AddWithValue("@p8", sftxt.Text);
                kmt3.Parameters.AddWithValue("@p10", did);

                kmt3.ExecuteNonQuery();

                dataGridView1.Refresh();

                MessageBox.Show("Güncellen İşlemi Başarılı bir şekilde tamamlandı");
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
            catch
            {
                MessageBox.Show("Lütfen Boş Alan bırakmayınız");
            }
        }

        //ekle
        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            frm2 = new Form2();
            frm2.Show();
        }

        private void gömlekToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gömlekListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView1.Enabled = false;

            listeal l = new listeal();
            dataGridView1.DataSource = l.listele("Execute ListeGomlek");
        }

        private void gömlekDetayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            listeal l = new listeal();
            dataGridView1.DataSource = l.listele("Execute Listele");

        }

        private void gömlekOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm2 = new Form2();
            frm2.Show();
        }

        private void iplikListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listeal l = new listeal();
            dataGridView1.DataSource = l.listele("Execute ListeIplik");
            dataGridView1.Enabled = false;
        }

        private void maliyetOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //kapatılan formun tekrar açılmasını sağlıyor

            if (frm2 == null || frm2.IsDisposed)
            {
                frm2 = new Form2();
                frm2.Show();
            }
            else
            {
                MessageBox.Show("zaten açık");

            }
        }

        private void boyaListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listeal l = new listeal();
            dataGridView1.DataSource = l.listele("Execute ListeBoya");
            dataGridView1.Enabled = false;
        }

        private void düğmeListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listeal l = new listeal();
            dataGridView1.DataSource = l.listele("Execute ListeDugme");
            dataGridView1.Enabled = false;
        }

        private void iplikEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
        }

        private void boyaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
        }

        private void düğmeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void listele_Click(object sender, EventArgs e)
        {
            listeal l = new listeal();
            dataGridView1.DataSource = l.listele("Execute Listele");
        }

        private void logkayitbtn_Click(object sender, EventArgs e)
        {
            
            listeal l = new listeal();
            dtgridviewlog.DataSource = l.listele("EXECUTE ListeleLogSP");
        }

        private void gsrbtn_Click(object sender, EventArgs e)
        {
            listeal l = new listeal();
            dtgridviewlog.DataSource = l.listele("select * from GunDetayIslem()");
        }
    }
}
