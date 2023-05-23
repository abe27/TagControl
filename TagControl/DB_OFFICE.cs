using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagControl
{
    public class ConnectDB2
    {

        public ConnectDB2()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string strConnection2()
        {
            string conDatabase = @"Data Source = 192.168.10.6; Initial Catalog = ITC_INWHOUSE; Persist Security Info = True; User ID = fm1234; Password = x2y2";
            return conDatabase;
        }
        public SqlConnection connection()
        {
            SqlConnection conn = new SqlConnection(strConnection2());
            return conn;
        }
    }
    public class DB_OFFICE
    {
        public DB_OFFICE()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetData(string sql, string tblName)
        {
            SqlConnection conn = new ConnectDB2().connection();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, tblName);
            return ds.Tables[0];
        }

        public DataSet GetDataSet(string sql, string tblName)
        {
            SqlConnection conn = new ConnectDB2().connection();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, tblName);
            return ds;
        }

        public DataTable GetData(string sql, string tblName, SqlParameterCollection parameters)
        {
            SqlConnection conn = new ConnectDB2().connection();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            foreach (SqlParameter param in parameters)
            {
                da.SelectCommand.Parameters.AddWithValue(param.ParameterName, param.SqlDbType).Value = param.Value;
            }
            da.Fill(ds, tblName);
            return ds.Tables[0];
        }

        public int ExecuteData(string sql)
        {
            int i;
            SqlConnection conn = new ConnectDB2().connection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public int ExecuteData(string sql, SqlParameterCollection parameters)
        {
            int i;
            SqlConnection conn = new ConnectDB2().connection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            foreach (SqlParameter param in parameters)
            {
                cmd.Parameters.AddWithValue(param.ParameterName, param.SqlDbType).Value = param.Value;
            }
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }

        public DataSet ExcStorePro(string stpName, string tblName, SqlParameterCollection parameters)
        {
            SqlConnection conn = new ConnectDB2().connection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = stpName;
            foreach (SqlParameter param in parameters)
            {
                cmd.Parameters.AddWithValue(param.ParameterName, param.SqlDbType).Value = param.Value;
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, tblName);
            return ds;


        }


    }
}
