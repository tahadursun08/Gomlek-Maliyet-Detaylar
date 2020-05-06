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
    public class KullaniciYetki
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);

        public int yetki(string sql,string ad)
        {

            SqlDataAdapter kk = new SqlDataAdapter();
            SqlCommand yid = new SqlCommand();
            yid.Connection = baglanti;
            yid.CommandText = sql;
            yid.Parameters.AddWithValue("@ad", ad);
            kk.SelectCommand = yid;
            DataSet ds = new DataSet();
            ds.Clear();
            kk.Fill(ds);

            int id = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            return id;
        }

    }
}
