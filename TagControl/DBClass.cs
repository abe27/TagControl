using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC_CENTER
{
    class ConnectDBs
    {

        //SQL Server
        public SqlConnection SqlStrCon()
        {

            // string[] data = File.ReadAllLines(@"connectDatabase.txt");
            //   string dbAcc = data[0];
            return new SqlConnection("Data Source=  192.168.10.6 ;Initial Catalog=formula;Persist Security Info=True;User ID=fm1234;Password=x2y2");
            //  return new SqlConnection(""+ dbAcc.ToString() + "");
        }

    }
    public class DBClass
    {
        public DBClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetData(string sql, string tblName)
        {
            SqlConnection conn = new ConnectDBs().SqlStrCon();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, tblName);
            return ds.Tables[0];
        }

        public DataTable GetData(string sql, string tblName, SqlParameterCollection parameters)
        {
            SqlConnection conn = new ConnectDBs().SqlStrCon();
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
            SqlConnection conn = new ConnectDBs().SqlStrCon();
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public int ExecuteData(string sql, SqlParameterCollection parameters)
        {
            int i;
            SqlConnection conn = new ConnectDBs().SqlStrCon();
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
            SqlConnection conn = new ConnectDBs().SqlStrCon();
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
