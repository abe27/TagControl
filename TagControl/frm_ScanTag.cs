using TagControl;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer.DataDefModel;
using LOT_CONTROL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CoatingProgram;
using CrystalDecisions.Shared;
using static log4net.Appender.RollingFileAppender;
using System.IO;
using Microsoft.VisualStudio.TextManager.Interop;
//using Application = Microsoft.Office.Interop.Excel.Application;

namespace TagControl
{
    public partial class frm_ScanTag : Form
    {

        public frm_ScanTag()
        {
            InitializeComponent();

        }
        DataTable table = new DataTable();
        DataTable table2 = new DataTable();


        private void frm_ScanTag_Load(object sender, EventArgs e)
        {
            Create_Table2();

        }

        public void Create_Table2()
        {
            try
            {
                table2.TableName = "AAA";
                table2.Columns.Add(new DataColumn("NO", typeof(string)));
                table2.Columns.Add(new DataColumn("PartNo", typeof(string)));
                table2.Columns.Add(new DataColumn("PartName", typeof(string)));
                table2.Columns.Add(new DataColumn("QTY", typeof(string)));
                table2.Columns.Add(new DataColumn("ID", typeof(string)));    //QTY Box
                table2.Columns.Add(new DataColumn("Box", typeof(string)));    //QTY Box
                table2.Columns.Add(new DataColumn("LotNo", typeof(string)));
                table2.Columns.Add(new DataColumn("OrderNumber", typeof(string)));
                table2.Columns.Add(new DataColumn("TAG", typeof(string)));
                table2.Columns.Add(new DataColumn("TAGcus", typeof(string)));
                table2.Columns.Add(new DataColumn("STATUS", typeof(string)));
                table2.Columns.Add(new DataColumn("DELETE", typeof(string)));

                gridAAA.DataSource = table2;

                int fontw = 16;// สูง 


                gridAAA.EnableHeadersVisualStyles = false;
                gridAAA.DefaultCellStyle.BackColor = Color.White;
                gridAAA.DefaultCellStyle.ForeColor = Color.Black;
                gridAAA.RowTemplate.Resizable = DataGridViewTriState.True;
                gridAAA.RowTemplate.Height = 16;


                foreach (DataGridViewColumn col in gridAAA.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", fontw, FontStyle.Bold, GraphicsUnit.Pixel);
                    col.HeaderCell.Style.BackColor = SystemColors.Highlight;
                    col.HeaderCell.Style.ForeColor = Color.White;
                    col.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 16F, GraphicsUnit.Pixel);
                }

                gridAAA.Columns[11].DefaultCellStyle.BackColor = Color.Red;
                gridAAA.Columns[6].DefaultCellStyle.BackColor = Color.Pink;
                gridAAA.Columns[7].DefaultCellStyle.BackColor = Color.LightGreen;

                gridAAA.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                gridAAA.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gridAAA.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gridAAA.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                gridAAA.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                gridAAA.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                gridAAA.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gridAAA.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gridAAA.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                gridAAA.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                gridAAA.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                gridAAA.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                gridAAA.Columns[0].HeaderText = "ลำดับ";
                gridAAA.Columns[1].HeaderText = "รหัสสินค้า";
                gridAAA.Columns[2].HeaderText = "ชื่อสินค้า";
                gridAAA.Columns[3].HeaderText = "จำนวน";
                gridAAA.Columns[4].HeaderText = "ID"; //QTY Box
                gridAAA.Columns[5].HeaderText = "จำนวน Box"; //QTY Box
                gridAAA.Columns[6].HeaderText = "หมายเลข Lot";
                gridAAA.Columns[7].HeaderText = "หมายเลข Order";
                gridAAA.Columns[8].HeaderText = "TAG AAA";
                gridAAA.Columns[9].HeaderText = "TAG Customer";
                gridAAA.Columns[10].HeaderText = "STATUS";
                gridAAA.Columns[11].HeaderText = "DELETE";

                //gridAAA.Columns[4].Visible = false;
                gridAAA.Columns[8].Visible = false;
                gridAAA.Columns[9].Visible = false;
                gridAAA.Columns[10].Visible = false;


            }
            catch { }
        }

        private void txtSCAN_CUS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSCAN_CUS.Text != "")
                    {
                        string tag = txtSCAN_CUS.Text.ToString();

                        string PartNo = tag[33].ToString();
                        PartNo += tag[34].ToString();
                        PartNo += tag[35].ToString();
                        PartNo += tag[36].ToString();
                        PartNo += tag[37].ToString();
                        PartNo += tag[38].ToString();
                        PartNo += tag[39].ToString();
                        PartNo += tag[40].ToString();
                        PartNo += tag[41].ToString();
                        PartNo += tag[42].ToString();

                        //Check PartNo in formula
                        string sql = "SELECT * FROM PROD WHERE FCCODE ='" + PartNo + "'";
                        string Tsql = $"SELECT * FROM TAG_ORDER WHERE Qrcode_Cus = '{tag}' ";
                        DataTable dt = new DB_FORMULA().GetData(sql, "tbl");
                        DataTable Tdt = new DB_OFFICE().GetData(Tsql, "tbl");
                        DataRow[] Check_tag = table2.Select($"TAGcus='{tag}'");

                        if (dt.Rows.Count != 0)
                        {
                            if (Tdt.Rows.Count == 0)
                            {
                                if (Check_tag.Count() <= 0)
                                {
                                    txtSCAN.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("TAG นี้ถูกสแกนแล้ว", "SOMETHING WENT WRONG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("TAG นี้มีในระบบแล้ว", "SOMETHING WENT WRONG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSCAN_CUS.Clear();
                                txtSCAN.Clear();
                                txtSCAN_CUS.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("PART NUMBER ของ TAG ลูกค้าไม่ถูกต้อง", "SOMETHING WENT WRONG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSCAN_CUS.Clear();
                            txtSCAN.Clear();
                            txtSCAN_CUS.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("PLEASE INPUT TAG SCAN, \r PLEASE TRY AGAIN", "SOMETHING WENT WRONG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSCAN_CUS.Clear();
                        txtSCAN.Clear();
                        txtSCAN_CUS.Focus();
                    }

                }
            }
            catch { }
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (table2.Rows.Count > 0)
                {
                    if (MessageBox.Show("DO YOU WANT TO SAVE LOT CONTROL", "CONFIRM SAVE ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string sql;
                        string txtSql = "SELECT * FROM TAG_ORDER";
                        DataTable dt = new DB_OFFICE().GetData(txtSql, "tbl");

                        for (int i = 0; i <= table2.Rows.Count - 1; i++)
                        {
                            sql = "INSERT INTO TAG_ORDER (Order_Number, CPart_No, Order_Qty, Order_Box, Order_Lot, Order_Tag, Qrcode_Cus) VALUES ('"
                                  + table2.Rows[i]["OrderNumber"].ToString().Trim() + "','"
                                  + table2.Rows[i]["PartNo"].ToString().Trim() + "','"
                                  + table2.Rows[i]["QTY"].ToString().Trim() + "','"
                                  + table2.Rows[i]["ID"].ToString().Trim() + "','"
                                  + table2.Rows[i]["LotNo"].ToString().Trim() + "','"
                                  + table2.Rows[i]["TAG"].ToString().Trim() + "','"
                                  + table2.Rows[i]["TAGcus"].ToString().Trim() + "')";
                            int a = new DB_OFFICE().ExecuteData(sql);


                        }
                        table.Clear();
                        table2.Clear();
                        MessageBox.Show("TAG DATA, SAVE SUCCESSFULLY", "SUCCESSFULLY !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (table2.Rows.Count > 0)
            {
                if (MessageBox.Show("YOUR LOT CONTROL IS NOT SAVE, DO YOU WANT TO CLOSE ?", "CONFIRM CLOSE ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                if (MessageBox.Show("DO YOU WANT TO CLOSE ?", "CONFIRM CLOSE ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }



        private void gridAAA_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;

                int rowIndex = gridAAA.CurrentCell.RowIndex;
                int colIndex = gridAAA.CurrentCell.ColumnIndex;

                //Row Column ที่คลิก มีค่าเป็น DELETE
                if (gridAAA.Rows[rowIndex].Cells[colIndex].Value.ToString() == "DELETE")
                {

                    //ตำแหน่ง cells index ที่อยู่ในแถวเดียวกัน
                    string Part = gridAAA.Rows[rowIndex].Cells[1].Value.ToString().Trim();
                    string TAG = gridAAA.Rows[rowIndex].Cells[8].Value.ToString().Trim();
                    string OrderNo = gridAAA.Rows[rowIndex].Cells[7].Value.ToString().Trim();

                    if (MessageBox.Show($"DO YOU WANT TO DELETE ORDER NUMBER : {OrderNo}\r\rPART NUMBER : {Part} ?", "CONFIRM ??", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //แถวที่คลิกใน table 1


                        DataRow[] rDetail = table2.Select($"TAG='{TAG}'");
                        ////DataRow[] rDetail2 = table.Select($"PartNo='{Part}' AND STATUS='Customer'");
                        if (rDetail.Count() > 0) //เช็คว่ามี Partno ตรงกับแถวที่คลิก
                        {
                            rDetail[0].Delete();
                        }

                        table2.AcceptChanges();

                        int seq2 = 0;
                        foreach (var i in table2.Rows)
                        {
                            table2.Rows[seq2]["NO"] = (seq2 + 1);
                            seq2++;
                        }
                    }
                }
            }
            catch { }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //try
            //{
            //    if (e.RowIndex == -1)
            //        return;

            //    int R_index = dataGridView1.CurrentCell.RowIndex;
            //    int C_index = dataGridView1.CurrentCell.ColumnIndex;

            //    //ตำแหน่ง cells index ที่อยู่ในแถวเดียวกัน
            //    string NO = dataGridView1.Rows[R_index].Cells[1].Value.ToString().Trim();
            //    string Order_ID = dataGridView1.Rows[R_index].Cells[7].Value.ToString().Trim();
            //    string PartNo = dataGridView1.Rows[R_index].Cells[3].Value.ToString().Trim(); // send to frm_showDetail
            //    string LotNo = dataGridView1.Rows[R_index].Cells[2].Value.ToString().Trim();
            //    string DATE = dataGridView1.Rows[R_index].Cells[8].Value.ToString().Trim(); // send to frm_showDetail
            //    //Row Column ที่คลิก มีค่าเป็น DELETE
            //    if (dataGridView1.Rows[R_index].Cells[C_index].Value.ToString() == "View")
            //    {
            //        frm_showDetail frm = new frm_showDetail(Order_ID, PartNo, DATE);
            //        frm.Order_ID = Order_ID;

            //        frm.ShowDialog();

            //    }
            //}
            //catch { }
        }

        private void txtSCAN_AAA_KeyDown(object sender, KeyEventArgs e)
       {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {

                    if (txtSCAN.Text != "")
                    {
                        if (txtSCAN_CUS.Text != "")
                        {
                            string tag = txtSCAN_CUS.Text.ToString();
                            string OrderNo = tag[0].ToString();
                            OrderNo += tag[1].ToString();
                            OrderNo += tag[2].ToString();
                            OrderNo += tag[3].ToString();
                            OrderNo += tag[4].ToString();
                            OrderNo += tag[5].ToString();
                            OrderNo += tag[6].ToString();
                            OrderNo += tag[7].ToString();
                            OrderNo += tag[8].ToString();
                            OrderNo += tag[9].ToString();

                            string Box = tag[11].ToString();
                            Box += tag[12].ToString();

                            string CpartNo = tag[33].ToString();
                            CpartNo += tag[34].ToString();
                            CpartNo += tag[35].ToString();
                            CpartNo += tag[36].ToString();
                            CpartNo += tag[37].ToString();
                            CpartNo += tag[38].ToString();
                            CpartNo += tag[39].ToString();
                            CpartNo += tag[40].ToString();
                            CpartNo += tag[41].ToString();
                            CpartNo += tag[42].ToString();

                            string Cqty = tag[17].ToString();
                            Cqty += tag[18].ToString();

                            string[] tagArray = txtSCAN.Text.ToString().Split('@');
                            string PartNo = tagArray[0].ToString();
                            string LotNo = tagArray[1].ToString();
                            string Id = tagArray[2].ToString();
                            string Qty = tagArray[3].ToString();
                            string FullTag = $"{PartNo}@{LotNo}@{Id}@{Qty}";

                            string sql = "SELECT * FROM PROD WHERE FCCODE ='" + PartNo + "'";
                            string Tsql = $"SELECT * FROM TAG_ORDER WHERE Order_Tag = '{FullTag}' ";
                            //string Osql = $"SELECT * FROM TAG_ORDER WHERE Order_Number = '{OrderNo}' ";
                            DataTable dt = new DB_FORMULA().GetData(sql, "tbl");
                            DataTable Tdt = new DB_OFFICE().GetData(Tsql, "tbl");
                            DataRow[] Check_tag = table2.Select($"TAG='{FullTag}'");

                            Console.WriteLine(PartNo, dt.Rows[0]["FCNAME"]);
                            if (dt.Rows.Count > 0)
                            {
                                if (Tdt.Rows.Count <= 0) 
                                {
                                    if(Check_tag.Count() <= 0)
                                    {
                                        if (PartNo == CpartNo && Qty == Cqty)
                                        {
                                            table2.Rows.Add((table2.Rows.Count + 1).ToString(), PartNo, dt.Rows[0]["FCNAME"].ToString(), Qty, Id, Box, LotNo, OrderNo, FullTag, tag, "AAA", "DELETE");

                                           
                                        }
                                        else
                                        {
                                            MessageBox.Show("PART NUMBER หรือจำนวนไม่ตรงกับ TAG ลูกค้า", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("TAG นี้ถูกสแกนแล้ว", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }


                                }
                                else
                                {
                                    MessageBox.Show("TAG นี้มีในระบบแล้ว", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                           }
                           else
                           {
                                MessageBox.Show("PART NUMBER นี้ไม่มีใน FORMULA", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           }
                        }
                        else
                        {
                            MessageBox.Show("PLEASE INPUT CASTOMER TAG, \r PLEASE TRY AGAIN", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("PLEASE INPUT AAA TAG, \r PLEASE TRY AGAIN", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    txtSCAN.Clear();
                    txtSCAN_CUS.Clear();
                    txtSCAN_CUS.Focus();
                    int seq = 0;
                    foreach (var i in table2.Rows)
                    {
                        table2.Rows[seq]["NO"] = (seq + 1);
                        seq++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                txtSCAN.Text = "";
                txtSCAN.Focus();
            }
        }
    }
}
