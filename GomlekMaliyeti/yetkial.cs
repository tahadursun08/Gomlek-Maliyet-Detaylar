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

namespace GomlekMaliyeti
{
    public class yetkial
    {
        public bool y1;
        public bool y2;
        public bool y3;
        public int y4;
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
        public void yetkicek(int id,int alt1modulId)
        {
            SqlDataAdapter Kullanici = new SqlDataAdapter();
            SqlCommand booltipleri = new SqlCommand();
            booltipleri.Connection = baglanti;
            booltipleri.CommandText = "SELECT Guncelleyebilir, Silebilir, Ekleyebilir, Y.RolId FROM Yetki Y INNER JOIN AltModuller ALTM ON Y.AltModulId = ALTM.AltModulId INNER JOIN Roller R ON Y.RolId = R.RolId WHERE KullaniciId = @Kullaniciid AND ALTM.AltModulId=@AltModulId";
            booltipleri.Parameters.AddWithValue("@Kullaniciid", id);
            booltipleri.Parameters.AddWithValue("@AltModulId", alt1modulId);
            Kullanici.SelectCommand = booltipleri;
            DataSet KulDS = new DataSet();
            KulDS.Clear();
            Kullanici.Fill(KulDS);

            y1 = Convert.ToBoolean(KulDS.Tables[0].Rows[0][0]);
            y2 = Convert.ToBoolean(KulDS.Tables[0].Rows[0][1]);
            y3 = Convert.ToBoolean(KulDS.Tables[0].Rows[0][2]);
            y4 = Convert.ToInt32(KulDS.Tables[0].Rows[0][3]);

            yetkiler.Guncelleyebilir = y1;
            yetkiler.Silebilir = y2;
            yetkiler.Ekleyebilir = y3;
            yetkiler.Rol = y4;
        }
    }
}
