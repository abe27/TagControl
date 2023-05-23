using CoatingProgram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagControl
{
    public partial class frm_Qreport : Form
    {
        public frm_Qreport()
        {
            InitializeComponent();
        }
        public void Load_txtSearch()
        {
            try
            {
                string SQL = "SELECT *  FROM TAG_ORDER ";
                DataTable DT_PO = new DB_OFFICE().GetData(SQL, "tbl");

                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

                string[] postSource = DT_PO
                        .AsEnumerable()
                        .Select<System.Data.DataRow, String>(x => x.Field<String>("CPart_No"))
                        .ToArray();
                collection.AddRange(postSource);

                textBox1.AutoCompleteCustomSource = collection;
                textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;

                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;


                AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();

                string[] postSource1 = DT_PO
                        .AsEnumerable()
                        .Select<System.Data.DataRow, String>(x => x.Field<String>("CPart_No"))
                        .ToArray();
                collection1.AddRange(postSource1);

                textBox2.AutoCompleteCustomSource = collection1;
                textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;

                textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;


                AutoCompleteStringCollection collection2 = new AutoCompleteStringCollection();

                string[] postSource2 = DT_PO
                        .AsEnumerable()
                        .Select<System.Data.DataRow, String>(x => x.Field<String>("Order_Lot"))
                        .ToArray();
                collection2.AddRange(postSource2);

                textBox3.AutoCompleteCustomSource = collection2;
                textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;

                textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                string Date = dtpFrom.Value.ToString("dd/MM/yyyy");
                string Date2 = dtpTo.Value.ToString("dd/MM/yyyy");
                string PartNo = textBox1.Text.ToString().Trim();

                string SQL = "SELECT TAG_ORDER.Order_Lot, TAG_ORDER.CPart_No, PROD.FCNAME, TAG_ORDER.Order_Number, TAG_ORDER.Order_Qty, TAG_ORDER.Order_ID, TAG_ORDER.FTDATETIME FROM [ITC_INWHOUSE].[dbo].[TAG_ORDER] TAG_ORDER INNER JOIN [formula].[dbo].[PROD] PROD ON PROD.FCCODE = TAG_ORDER.CPart_No ";
                SQL += $" where TAG_ORDER.CPart_No='{PartNo}' AND CAST(CONVERT(varchar(8), TAG_ORDER.FTDATETIME, 112) AS DATE) BETWEEN '{Date}' AND  '{Date2}' ORDER BY TAG_ORDER.FTDATETIME ASC";

                DataTable dt = new DB_OFFICE().GetData(SQL, "tbl");
                DataTable dt2 = new DB_FORMULA().GetData(SQL, "tbl");

                if (textBox1.Text != "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            dataGridView2.Rows.Add("View", (dataGridView2.Rows.Count + 1).ToString(), dt.Rows[i]["Order_Lot"].ToString().Trim(), dt.Rows[i]["CPart_No"].ToString().Trim(), dt2.Rows[i]["FCNAME"].ToString().Trim(), dt.Rows[i]["Order_Number"].ToString().Trim(), dt.Rows[i]["Order_Qty"].ToString().Trim(), dt.Rows[i]["Order_ID"].ToString().Trim(), Convert.ToDateTime(dt.Rows[i]["FTDATETIME"].ToString().Trim()).ToString("dd/MM/yyyy"));

                            dataGridView2.Rows[i].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12F, GraphicsUnit.Pixel);
                            textBox3.Clear();
                        }

                        
                    }
                    else
                    {
                        MessageBox.Show("SELECT DATE WAS NOT FOUND,\rPLEASE TRY AGAIN", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox3.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("INPUT PART NUMBER,\rPLEASE TRY AGAIN", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnSearchLot_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                string refPart = textBox2.Text.ToString().Trim();
                string refLot = textBox3.Text.ToString().Trim();
                string Psql = $"SELECT * FROM PROD WHERE FCCODE ='{refPart}'";
                string Lsql = $"SELECT * FROM TAG_ORDER WHERE CPart_No = '{refPart}' AND Order_Lot = '{refLot}'";
                DataTable dt = new DB_OFFICE().GetData(Lsql, "tbl");
                DataTable Pdt = new DB_FORMULA().GetData(Psql, "tbl");
                if (textBox2.Text != "")
                {
                    if (textBox3.Text != "")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                dataGridView2.Rows.Add("View", (dataGridView2.Rows.Count + 1).ToString(), dt.Rows[i]["Order_Lot"].ToString().Trim(), dt.Rows[i]["CPart_NO"].ToString().Trim(), Pdt.Rows[i]["FCNAME"].ToString().Trim(), dt.Rows[i]["Order_Number"].ToString().Trim(), dt.Rows[i]["Order_Qty"], dt.Rows[i]["Order_ID"].ToString().Trim(), Convert.ToDateTime(dt.Rows[i]["FTDATETIME"].ToString().Trim()).ToString("dd/MM/yyyy"));

                                dataGridView2.Rows[i].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12F, GraphicsUnit.Pixel);
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox2.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("THIS LOT NUMBER WAS NOT FOUND,\rPLEASE TRY AGAIN", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("INPUT LOT NUMBER,\rPLEASE TRY AGAIN", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox2.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("INPUT PART NUMBER,\rPLEASE TRY AGAIN", "SOMETHING WENT WRONG!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox2.Focus();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dataGridView2.DataSource as DataTable;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel Documents (*.xls)|*.xls";
                string FileName = "Lot_Control_Report.xls";
                saveFileDialog1.FileName = FileName;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fname = saveFileDialog1.FileName;

                    StreamWriter wr = new StreamWriter(fname);

                    int iHeader = dataGridView2.Columns.Count;
                    for (int i = 1; i < iHeader; i++)
                    {
                        if (dataGridView2.Columns[i].Name.ToString().ToUpper() != null && dataGridView2.Columns[i].Visible)
                        {
                            wr.Write(dataGridView2.Columns[i].Name.ToString().ToUpper() + "\t");
                        }
                    }
                    wr.WriteLine();

                    //write rows to excel file
                    for (int i = 0; i < (dataGridView2.Rows.Count); i++)
                    {
                        for (int j = 1; j < dataGridView2.Rows[i].Cells.Count; j++)
                        {
                            Console.WriteLine($"{dataGridView2.Columns[j].Name} ==> {dataGridView2.Columns[j].Visible}");
                            if (dataGridView2.Rows[i].Cells[j].Value != null && dataGridView2.Columns[j].Visible)
                            {
                                wr.Write(Convert.ToString(dataGridView2.Rows[i].Cells[j].Value) + "\t");
                            }
                        }
                        //go to next line
                        wr.WriteLine();
                    }
                    //close file
                    wr.Close();

                    // Open Excel after export success
                    if (File.Exists(fname))
                    {
                        System.Diagnostics.Process.Start(fname);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("The file name is the same as the file currently being opened", "SOMETHING WENT WRONG !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_Qreport_Load(object sender, EventArgs e)
        {
            Load_txtSearch();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
        }

        private void tabControl2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
