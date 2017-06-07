using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn_Game
{
    class ConnectionCreation
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter da;

        public void ConnectDB(){
            try
            {
                con = new SqlConnection(@"Data Source=MANS-PC;Initial Catalog=TYPINGGAME;Integrated Security=True");
                if(con != null)
                    con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                ConnectDB();
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            return dt;
        }

        public bool ExecuteNoneQuery(string query)
        {
            bool kq = true;
            try
            {
                ConnectDB();
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return kq;
        }

        public void CloseConnection()
        {
            con.Close();
        }
    }
}
