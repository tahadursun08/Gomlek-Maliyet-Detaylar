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
     public class listeal 
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
        


        public DataTable listele(string sql)
        {
            DataTable dt=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            dt = ds.Tables[0];

            return dt;
        }
        
    }
}
