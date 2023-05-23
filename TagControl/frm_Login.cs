using TagControl;
using ITC_CENTER;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COATING_PROGRAM;

namespace TagControl
{
    public partial class frm_Login : Form
    {
        public frm_Login()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtUserName.Text != "")
                    {
                        txtPassword.Focus();
                    }
                    else
                    {
                        MessageBox.Show("PLEASE INPUT USERNAME", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                    }
                }
            }
            catch { }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    if (txtUserName.Text != "" && txtPassword.Text != "")
                    {

                        //if (txtUserName.Text == "zxc" && txtPassword.Text == "zxc")
                        //{
                            //frm_index frm = new frm_index();
                            //frm.Show();
                            //this.Hide();
                            string SQL = "SELECT * FROM EMPLR  WHERE FCLOGIN=@USER AND FCPW=@PASS";

                            SqlParameterCollection param = new SqlCommand().Parameters;
                            param.AddWithValue("@USER", SqlDbType.NVarChar).Value = txtUserName.Text.ToString().Trim();
                            param.AddWithValue("@PASS", SqlDbType.NVarChar).Value = txtPassword.Text.ToString().Trim();
                            DataTable dt_login = new DBClass().GetData(SQL, "tbl", param);
                            //DataTable dt_login = new DBClass().GetData(SQL, "tbl");
                            if (dt_login.Rows.Count > 0)
                            {

                                Globals_User._USER_FCNAME = dt_login.Rows[0]["FCLOGIN"].ToString().Trim();
                                Globals_User._USER_FCSKID = dt_login.Rows[0]["FCSKID"].ToString().Trim();
                                frm_index frm = new frm_index();
                                this.Hide();
                                frm.Show();

                            }
                            else
                            {
                                MessageBox.Show("USERNAME OR PASSWORD AER INCORRCT, PLEASE TRY AGAIN", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtUserName.Clear();
                                txtPassword.Clear();
                                txtUserName.Focus();
                        }

                    }
                    else if (txtUserName.Text == "" && txtPassword.Text != "")
                    {
                        MessageBox.Show("INPUT USERNAME!!\rPLEASE TRY AGAIN!!", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                    }
                    else if (txtUserName.Text != "" && txtPassword.Text == "")
                    {
                        MessageBox.Show("INPUT PASSWORD!!\rPLEASE TRY AGAIN", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                    }
                    else
                    {
                        MessageBox.Show("INPUT USERNAME & PASSWORD!!\rPLEASE TRY AGAIN!!", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text != "" && txtPassword.Text != "")
                {

                    //if (txtUserName.Text == "zxc" && txtPassword.Text == "zxc")
                    //{
                    //frm_index frm = new frm_index();
                    //frm.Show();
                    //this.Hide();
                    string SQL = "SELECT * FROM EMPLR  WHERE FCLOGIN=@USER AND FCPW=@PASS";

                    SqlParameterCollection param = new SqlCommand().Parameters;
                    param.AddWithValue("@USER", SqlDbType.NVarChar).Value = txtUserName.Text.ToString().Trim();
                    param.AddWithValue("@PASS", SqlDbType.NVarChar).Value = txtPassword.Text.ToString().Trim();
                    DataTable dt_login = new DBClass().GetData(SQL, "tbl", param);
                    //DataTable dt_login = new DBClass().GetData(SQL, "tbl");
                    if (dt_login.Rows.Count > 0)
                    {

                        Globals_User._USER_FCNAME = dt_login.Rows[0]["FCLOGIN"].ToString().Trim();
                        Globals_User._USER_FCSKID = dt_login.Rows[0]["FCSKID"].ToString().Trim();
                        frm_index frm = new frm_index();
                        this.Hide();
                        frm.Show();

                    }
                    else
                    {
                        MessageBox.Show("USERNAME OR PASSWORD AER INCORRCT, PLEASE TRY AGAIN", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();
                    }

                }
                else if (txtUserName.Text == "" && txtPassword.Text != "")
                {
                    MessageBox.Show("INPUT USERNAME!!\rPLEASE TRY AGAIN!!", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
                else if (txtUserName.Text != "" && txtPassword.Text == "")
                {
                    MessageBox.Show("INPUT PASSWORD!!\rPLEASE TRY AGAIN", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
                else
                {
                    MessageBox.Show("INPUT USERNAME & PASSWORD!!\rPLEASE TRY AGAIN!!", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }

            }
            catch { }
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {
            label3.BackColor = System.Drawing.Color.Transparent;
            label4.BackColor = System.Drawing.Color.Transparent;
        }


    }
}
    

