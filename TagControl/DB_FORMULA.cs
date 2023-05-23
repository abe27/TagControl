using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoatingProgram
{
    public class ConnectDBs
    {
        public ConnectDBs()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string strConnection()
        {
            string conDatabase = @"Data Source =192.168.10.6 ; Initial Catalog = formula; Persist Security Info = True; User ID = fm1234; Password = x2y2";
            
            return conDatabase;
        }
        public SqlConnection connection()
        {
            SqlConnection conn = new SqlConnection(strConnection());
            return conn;
        }
    }
    public class DB_FORMULA
    {
        public DB_FORMULA()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetData(string sql, string tblName)
        {
            SqlConnection conn = new ConnectDBs().connection();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, tblName);
            return ds.Tables[0];

        }

        public DataTable GetData(string sql, string tblName, SqlParameterCollection parameters)
        {
            SqlConnection conn = new ConnectDBs().connection();
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
            SqlConnection conn = new ConnectDBs().connection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public int ExecuteData(string sql, SqlParameterCollection parameters)
        {
            int i;
            SqlConnection conn = new ConnectDBs().connection();
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
            SqlConnection conn = new ConnectDBs().connection();
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
        //public string GetNameCompany()
        //{

        //    string sql = "SELECT * FROM[formula].[dbo].[vmCorp] ";
        //    SqlParameterCollection param = new SqlCommand().Parameters;
        //    DataTable dtRec_No = GetData(sql, "tbl", param);
        //    if (dtRec_No.Rows.Count > 0)
        //    {

        //        Globals._keyCorp = dtRec_No.Rows[0]["FCSKID"].ToString().Trim();
        //        Globals._corpName = dtRec_No.Rows[0]["FCNAME"].ToString().Trim();
        //    }

        //    return Globals._keyCorp;
        //}

        //public string Select_Order(string id)
        //{

        //    //string Order_num = string.Empty;
        //    //string _SQL = "SELECT  [id] ,[mtm_number] ,[order_number] ,[status] FROM  [dbo].[order_mtm] where [id] =@FCSKID ";
        //    //SqlParameterCollection param = new SqlCommand().Parameters;
        //    //param.AddWithValue("@FCSKID", SqlDbType.NVarChar).Value = id.ToString().Trim();
        //    //DataTable dt = new dbAAA().GetData(_SQL, "tbl_Rec", param);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    Order_num = dt.Rows[0]["order_number"].ToString().Trim();
        //    //}

        //    //return Order_num.ToString();
        //}


    }
}
