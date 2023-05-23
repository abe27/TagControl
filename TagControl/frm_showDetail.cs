using CoatingProgram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagControl
{
    public partial class frm_showDetail : Form
    {
        public string Order_ID = string.Empty;
        string PartNo_;
        string Date_;
        DataTable table = new DataTable();
        public frm_showDetail(string OrderID,string PartNo, string Date)
        {
            InitializeComponent();
            Order_ID = OrderID;
            PartNo_ = PartNo;
            Date_ = Date;

        }

        private void frm_showDetail_Load(object sender, EventArgs e)
        {
            Create_table();
            View();
            
        }

        public void Create_table()
        {
            try
            {
                table.TableName = "DT";
                table.Columns.Add(new DataColumn("NO", typeof(string)));
                table.Columns.Add(new DataColumn("PartNo", typeof(string)));
                table.Columns.Add(new DataColumn("PartName", typeof(string)));
                table.Columns.Add(new DataColumn("QTY", typeof(string)));
                table.Columns.Add(new DataColumn("ID", typeof(string))); 
                table.Columns.Add(new DataColumn("LotNo", typeof(string)));
                table.Columns.Add(new DataColumn("OrderNO", typeof(string)));

                dataGridView1.DataSource = table;

                int fontw = 16;// สูง 


                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.DefaultCellStyle.BackColor = Color.White;
                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.RowTemplate.Resizable = DataGridViewTriState.True;
                dataGridView1.RowTemplate.Height = 18;


                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", fontw, FontStyle.Regular, GraphicsUnit.Pixel);
                    col.HeaderCell.Style.BackColor = SystemColors.Highlight;
                    col.HeaderCell.Style.ForeColor = Color.White;
                    col.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 14F, GraphicsUnit.Pixel);
                }

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                dataGridView1.Columns[0].HeaderText = "ลำดับ";
                dataGridView1.Columns[1].HeaderText = "รหัสสินค้า";
                dataGridView1.Columns[2].HeaderText = "ชื่อสินค้า";
                dataGridView1.Columns[3].HeaderText = "จำนวน";
                dataGridView1.Columns[4].HeaderText = "ID ที่ยิง";
                dataGridView1.Columns[5].HeaderText = "Lot number";
                dataGridView1.Columns[6].HeaderText = "Order No";

                dataGridView1.Columns[4].Visible = false;


            }
            catch { }
        }

        public void View()
        {
            try
            {
                if (Order_ID != string.Empty)
                {
                    string SQL = $"SELECT * FROM TAG_ORDER WHERE Order_ID = '{Order_ID}'";
                    string SQL2 = $"SELECT * FROM PROD WHERE FCCODE = '{PartNo_}'";
                    DataTable d2 = new DB_FORMULA().GetData(SQL2, "tbl");
                    DataTable d = new DB_OFFICE().GetData(SQL, "tbl");
                    if (d.Rows.Count > 0)
                    {
                        for (int i = 0; i < d.Rows.Count; i++)
                        {
                            Double qty = Convert.ToDouble(d.Rows[i]["Order_Qty"].ToString().Trim());
                            table.Rows.Add((i + 1).ToString(), d.Rows[i]["CPart_No"].ToString().Trim(), d2.Rows[i]["FCNAME"].ToString().Trim(), String.Format("{0:0.##}", qty), d.Rows[i]["Order_ID"].ToString().Trim(), d.Rows[i]["Order_Lot"].ToString().Trim(),d.Rows[i]["Order_Number"].ToString().Trim());
                        }
                    }
                    label1.Text = $"รายละเอียดข้อมูล LOT CONTROL : Part Number {PartNo_}, วันที่ : {Date_}";
                }
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
