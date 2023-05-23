using CrystalDecisions.ReportAppServer.DataDefModel;
using LOT_CONTROL;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TagControl
{
    public partial class frm_printLot : Form
    {
        public frm_printLot()
        {
            InitializeComponent();
        }

        private void frm_printLot_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < 30; i++) { dgDetail.Rows.Add((i.ToString())); }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (textBox1.Text.ToString().Length > 3)
            //{
            //    txt_FCSKID.Text = "";
            //    GenerateDynaminBy_Part(textBox1.Text.ToString());

            //}
        }
        private PictureBox pic;
        private Label price;
        private Label description;


        private void GenerateDynaminBy_Part(string Part)
        {
            flowLayoutPanel1.Controls.Clear();

            string pathDes = @"\\192.168.25.9\part_picture\error.jpg";

            string SQL = "SELECT  *  FROM ViewProduct  WHERE  (FCCODE LIKE '%" + Part + "%') OR   (FCNAME LIKE '%" + Part + "%') ";
            DataTable DT_PART = new DB_OFFICE().GetData(SQL, "tbl1");

            if (DT_PART.Rows.Count > 0)
            {

                for (int i = 0; i < DT_PART.Rows.Count; i++)
                {
                    pic = new PictureBox();
                    pic.Width = 150;
                    try
                    {
                        pic.Image = Image.FromFile(pathDes);
                    }
                    catch
                    {

                    }
                    pic.Height = 100;
                    pic.BackgroundImageLayout = ImageLayout.Stretch;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;

                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.Tag = DT_PART.Rows[i]["FCCODE"].ToString();



                    price = new Label();
                    price.Text = DT_PART.Rows[i]["FCCODE"].ToString().Trim();
                    price.Width = 100;
                    price.BackColor = Color.FromArgb(255, 121, 121);
                    price.TextAlign = ContentAlignment.MiddleCenter;


                    description = new Label();
                    description.Text = DT_PART.Rows[i]["FCNAME"].ToString();
                    description.Width = 50;
                    description.BackColor = Color.FromArgb(46, 134, 222);
                    description.TextAlign = ContentAlignment.MiddleCenter;
                    description.Dock = DockStyle.Bottom;

                    pic.Controls.Add(description);
                    pic.Controls.Add(price);
                    flowLayoutPanel1.Controls.Add(pic);
                    pic.Cursor = Cursors.Hand;
                    //pic.Click += (sender, e) => OnClick(this, e, pic.Tag.ToString());
                    pic.Click += new EventHandler(OnClick);



                }



            }





            //UserControl_Model[] listItems =new  UserControl_Model[5];

            //string[] title = new string[] { "1","2","3","4","5"};

            //string[] subtitle = new string[] { "1dd", "2dd", "3dd", "4dd", "5dd" };

            //for (int i = 0; i < listItems.Length; i++)
            //{

            //    listItems[i] = new UserControl_Model();
            //    listItems[i].Title = title[i];
            //    listItems[i].SubTiltle = subtitle[i];
            //    listItems[i].Click += new System.EventHandler(this.UserControl_click);

            //    flowLayoutPanel1.Controls.Add(listItems[i]);


            //}

        }
        public void OnClick(object sender, EventArgs e)
        {
            try
            {
                string tag = ((PictureBox)sender).Tag.ToString();
                MessageBox.Show(tag.ToString());

                txtPartNo.Text = tag.ToString();

                dataGridView1.Rows.Clear();
                dgDetail.Rows.Clear();
                txtLotNo.Text = "";
                //txt_FCSKID.Text = "";
                txtModel.Text = "";
                txtCus.Text = "";
                //textBox6.Text = "";
                cmbQTYTag.Text = "";
                cmbQTY.Text = "";

                string PrintSql = "SELECT DISTINCT CPART_NO, CLOT FROM TAG_DETAIL WHERE CPART_NO = '" + tag + "'";
                DataTable dt = new DB_OFFICE().GetData(PrintSql, "tbl");
                LbHistory.Text = $"GEN HISTORY PART No. : {tag}";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add((dataGridView1.Rows.Count + 1), dt.Rows[i]["CPART_NO"].ToString(), dt.Rows[i]["CLOT"].ToString());
                }
            }
            catch
            {

            }


        }

        private void txtPartNo_TextChanged(object sender, EventArgs e)
        {
            //string fcprods = "";


            string SQL = "SELECT  top 1  *  FROM ViewProduct  WHERE  FCCODE ='" + txtPartNo.Text.ToString().Trim() + "' ORDER BY FCCODE ";
            DataTable DT_PART = new DB_OFFICE().GetData(SQL, "tbl1");
            if (DT_PART.Rows.Count > 0)
            {
                txtCode.Text = DT_PART.Rows[0]["FCSNAME"].ToString();
                txtPartName.Text = DT_PART.Rows[0]["FCNAME"].ToString();
                txt_FCSKID.Text = DT_PART.Rows[0]["FCSKID"].ToString();

                ///

                string txtSql = "SELECT  * FROM TAG WHERE CPART_NO ='" + txtPartNo.Text.ToString().Trim() + "' ORDER BY CPART_NO ";
                DataTable dtFK = new DB_OFFICE().GetData(txtSql, "dd");
                if (dtFK.Rows.Count > 0)
                {
                    string pathDes = @"\\192.168.25.9\part_picture\error.jpg";
                    try
                    {

                        if (textBox6.Text != "")
                        {
                            pictureBox1.Image = Image.FromFile(dtFK.Rows[0]["CPICTURE"].ToString());
                            textBox6.Text = dtFK.Rows[0]["CPICTURE"].ToString().Trim();
                        }
                        else
                        {
                            try
                            {
                                pictureBox1.Image = Image.FromFile(pathDes);
                                textBox6.Text = pathDes;
                            }
                            catch { }
                        }


                    }
                    catch (Exception ios)
                    {
                        MessageBox.Show(ios.ToString());
                    }


                }
                else
                {
                    string pathDes = @"\\192.168.25.9\part_picture\error.jpg";
                    try
                    {
                        textBox6.Text = pathDes;
                        pictureBox1.Image = Image.FromFile(pathDes);

                    }
                    catch (Exception ios)
                    {
                        MessageBox.Show(ios.ToString());
                    }


                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox6.Text = null;
            string pathDes;

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            openFileDialog1.FileName = "";



            string SQL = "SELECT * FROM tbl_pic  WHERE id ='1'";
            DataTable DT_PIC = new DB_OFFICE().GetData(SQL, "tbl");
            if (DT_PIC.Rows.Count > 0)
            {
                pathDes = DT_PIC.Rows[0]["Path_pic"].ToString();
            }
            else
            {

                pathDes = @"\\192.168.25.9\part_picture\";

            }

            //pathDes = DT_PIC.Rows[0]["Path_pic"].ToString();

            //pathDes = @"\\192.168.25.9\part_picture\";

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string filenname = System.IO.Path.GetFileName(openFileDialog1.FileName);
                string file = openFileDialog1.FileName;
                try
                {
                    //pathDes += lModel.SelectedItem.ToString().Trim();
                    pictureBox1.Image = Image.FromFile(file.ToString());

                    FileInfo f1 = new FileInfo(file);
                    if (!Directory.Exists(pathDes))
                    {
                        Directory.CreateDirectory(pathDes);
                    }
                    pathDes += @"\" + txtPartNo.Text.ToString().Trim(); ;
                    pathDes += ".jpg";
                    f1.CopyTo(pathDes, true);
                    textBox6.Text = pathDes;

                }
                catch (Exception ios)
                {
                    MessageBox.Show(ios.ToString());
                }
            }
        }
        private void btnGen_Click(object sender, EventArgs e)
        {

            if (txtLotNo.Text == "" || txtPartNo.Text == "" || txt_FCSKID.Text == "" || txtCus.Text == "" || txtModel.Text == "" || cmbQTY.Text == "" || cmbQTYTag.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ");
            }
            else
            {

                btnSave_Click(null, null);
                //button3_Click("NO", null);

                string txtSql = "SELECT  * FROM TAG_DETAIL WHERE CPART_NO ='" + txtPartNo.Text.ToString().Trim() + "' AND  CLOT ='" + txtLotNo.Text.ToString().Trim() + "' ORDER BY CPART_NO ";
                //DataSet dsFK = new DBClassVCST().SqlGet(txtSql, "dd");
                //DataTable dtFK = dsFK.Tables[0];


                DataTable dtFK = new DB_OFFICE().GetData(txtSql, "dd");


                int row = dtFK.Rows.Count;
                if (row <= 0)
                {
                    for (int i = 1; i <= Int32.Parse(cmbQTYTag.SelectedItem.ToString()); i++)
                    {

                        txtSql = "INSERT INTO TAG_DETAIL(FCPROD,CPART_NO, KANBAN_ID,CDATE_CREA,CLOT) VALUES(";
                        txtSql += "'" + txt_FCSKID.Text.ToString().Trim() + "','" + txtPartNo.Text.ToString().Trim() + "'";
                        txtSql += "," + i.ToString().Trim() + "";
                        txtSql += ",'" + DateTime.Today.ToString("yyyyMMdd").Trim() + "','" + txtLotNo.Text.ToString().Trim() + "')";
                        //int a = new DBClassVCST().SqlExecute(txtSql);
                        int a = new DB_OFFICE().ExecuteData(txtSql);
                    }
                    MessageBox.Show("Generate Kanban Barcode Successfully");

                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("คุณต้องการเพิ่ม Kanban เพิ่มเข้ามาในระบบอีก : " + Int32.Parse(cmbQTYTag.Text.ToString()) + " ใบ ใช่หรือไม่", "เพิ่ม Kanban", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        txtSql = "SELECT MAX(KANBAN_ID) AS TK FROM TAG_DETAIL WHERE CPART_NO ='" + txtPartNo.Text.ToString() + "' AND  CLOT ='" + txtLotNo.Text.ToString().Trim() + "' ";
                        //DataSet dsFKs = new DBClassVCST().SqlGet(txtSql, "dd");
                        //DataTable dtFKs = dsFKs.Tables[0];txt

                        DataTable dtFKs = new DB_OFFICE().GetData(txtSql, "dd");
                        int rows = dtFKs.Rows.Count;
                        string s = dtFKs.Rows[0]["TK"].ToString();  //dtFK.Rows[0]["CTO"]
                        int RRRR = Convert.ToInt32(s) + 1;
                        for (int i = 0; i <= Int32.Parse(cmbQTYTag.Text.ToString()) - 1; i++)
                        {


                            txtSql = "INSERT INTO TAG_DETAIL(CPART_NO, KANBAN_ID,CDATE_CREA,CLOT) VALUES(";
                            txtSql += "'" + txtPartNo.Text.ToString().Trim() + "'";
                            txtSql += "," + RRRR.ToString().Trim() + "";
                            txtSql += ",'" + DateTime.Today.ToString("yyyyMMdd").Trim() + "','" + txtLotNo.Text.ToString().Trim() + "')";
                            //int a = new DBClassVCST().SqlExecute(txtSql);
                            int a = new DB_OFFICE().ExecuteData(txtSql);

                            RRRR++;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }


                showDeail(txtPartNo.Text.ToString());
                //if (txtLotNo.Text == "" && txtPartNo.Text == "" && txt_FCSKID.Text == "" && txtCus.Text == "" && txtModel.Text == "" && cmbQTY.Text == "" && cmbQTYTag.Text == "")

            }


        }
        void showDeail(string _part_no)
        {
            try
            {
                //string txtSql = "SELECT  * FROM TAG_DETAIL WHERE CPART_NO ='" + _part_no.Trim() + "' AND CLOT ='" + txtLotNo.Text.ToString().Trim() + "'   ORDER BY KANBAN_ID ";
                string txtSql = "SELECT  * FROM TAG_DETAIL INNER JOIN TAG ON TAG_DETAIL.CPART_NO = TAG.CPART_NO WHERE TAG_DETAIL.CPART_NO ='" + _part_no.Trim() + "' AND TAG_DETAIL.CLOT ='" + txtLotNo.Text.ToString().Trim() + "'   ORDER BY TAG_DETAIL.KANBAN_ID ";

                //dgDetail.Rows.Clear();
                DataTable dtFK = new DB_OFFICE().GetData(txtSql, "dd");
                if (dtFK.Rows.Count > 0)
                {
                    dgDetail.Rows.Clear();


                    foreach (DataRow rows in dtFK.Rows)
                    {
                        encryBarcode ceD = new encryBarcode();
                        ceD.date = rows["CDATE_CREA"].ToString().Trim();
                        string conDate = ceD.conDate();
                        ceD.date = rows["CDATE"].ToString().Trim();
                        string printDate = ceD.conDate();

                        dgDetail.Rows.Add();
                        dgDetail.Rows[dgDetail.Rows.Count - 1].Cells[0].Value = 0;
                        dgDetail.Rows[dgDetail.Rows.Count - 1].Cells[1].Value = rows["KANBAN_ID"].ToString();
                        dgDetail.Rows[dgDetail.Rows.Count - 1].Cells[2].Value = txtLotNo.Text.ToString();
                        dgDetail.Rows[dgDetail.Rows.Count - 1].Cells[3].Value = conDate;
                        dgDetail.Rows[dgDetail.Rows.Count - 1].Cells[4].Value = printDate;
                        dgDetail.Rows[dgDetail.Rows.Count - 1].Cells[5].Value = rows["NPRINT"].ToString();
                        dgDetail.Rows[dgDetail.Rows.Count - 1].Cells[6].Value = rows["NSTATUS"].ToString();
                    }


                    dgDetail.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);




                }
                else
                {
                    //comboBox1.SelectedItem = "";
                    //comboBox2.SelectedItem = "";
                    //comboBox3.SelectedItem = "";
                    //comboBox5.SelectedItem = "";
                    //textBox7.Text = "";
                    //textBox5.Text = "";
                    //textBox3.Text = "";
                    //pictureBox3.Image = null;

                }

            }
            catch { }
        }
        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                using (SolidBrush b = new SolidBrush(dgDetail.RowHeadersDefaultCellStyle.ForeColor))
                {
                    e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
                }

            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int _ssTxt = 0;

                string cSql;
                string txtSql = "SELECT  * FROM TAG WHERE CPART_NO ='" + txtPartNo.Text.ToString().Trim() + "' ORDER BY CPART_NO ";
                DataTable dtFK = new DB_OFFICE().GetData(txtSql, "dd");

                int row = dtFK.Rows.Count;
                if (row <= 0)
                {


                    string SSS = cmbQTY.SelectedItem.ToString().Trim();
                    // cSql = "DELETE FROM KANBAN WHERE CPART_NO = '" + textBox3.Text.ToString() + "'";
                    // new DBClassVCST().SqlExecute(cSql);
                    cmbQTY.SelectedValue = 1;
                    cSql = "INSERT INTO TAG(CPART_NO,CFORM,CLOT,CTO,CREV,CISSUED,CPACK,NQTY,CPICTURE,CMODEL,NOPTION) VALUES ( ";
                    cSql += "'" + txtPartNo.Text.ToString().Trim() + "'";
                    cSql += ",''";
                    cSql += ",'" + txtLotNo.Text.ToString().Trim() + "'";
                    cSql += ",''";
                    cSql += ",'00'";
                    cSql += ",'" + DateTime.Today.ToString("yyyyMMdd").Trim() + "'";
                    //cSql += ",'" + textBox8.Text.ToString().Trim() + "'";
                    cSql += ",'" + txtCus.Text.ToString().Trim() + "'";
                    cSql += "," + cmbQTY.SelectedItem.ToString().Trim() + "";
                    cSql += ",'" + textBox6.Text.ToString().Trim() + "'";
                    cSql += ",'" + txtModel.Text.ToString().Trim() + "'";
                    cSql += ",'" + _ssTxt.ToString().Trim() + "')";
                    int a = new DB_OFFICE().ExecuteData(cSql);
                }
                else
                {
                    cSql = "UPDATE TAG SET CFORM = ";//,CTO,CREV,CISSUED,CPACK,NQTY,CPICTURE,CMODEL) VALUES ( ";
                    cSql += " '" + txtLotNo.Text.ToString().Trim() + "'";
                    //cSql += ",CTO = '" + textBox8.Text.ToString().Trim() + "'";
                    //cSql += ",CCOATING = '" + temCoa + "'";
                    cSql += ",CLOT ='" + txtLotNo.Text.ToString().Trim() + "'";
                    cSql += ",CMODEL ='" + txtModel.Text.ToString().Trim() + "'";
                    cSql += ",CPACK ='" + txtCus.Text.ToString().Trim() + "'";
                    cSql += ",NQTY =" + cmbQTY.SelectedItem.ToString().Trim() + "";
                    cSql += ",CPICTURE = '" + textBox6.Text.ToString().Trim() + "'";
                    cSql += ",NOPTION = " + _ssTxt.ToString().Trim() + "";
                    cSql += " WHERE CPART_NO = '" + txtPartNo.Text.ToString().Trim() + "'";
                    int a = new DB_OFFICE().ExecuteData(cSql);
                }


                //if (sender.Equals(null))
                //{
                //    button4_Click(null, null);
                MessageBox.Show("SAVE SUCCESSFULLY");
            }
            catch { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int _ssTxt = 0;
                reportClassBindingSource.DataSource = new List<ReportClass_Kanban>();
                int tKanabn = dgDetail.Rows.Count;
                string cSql;
                int checkR = 0;
                byte[] pic = { };
                byte[] QRpic = { };
                string QrBarcode;
                //string temCoa = "";
                //if (checkBox1.Checked == true)
                //{ temCoa = comboBox5.SelectedItem.ToString().Trim(); }
                /*
                 * 
                 * 
                 * */
                Image img = null;
                try
                {
                    img = Image.FromFile(@textBox6.Text.ToString().Trim());
                }
                catch { }
                byte[] arr;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    arr = ms.ToArray();
                }

                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                foreach (DataGridViewRow row in dgDetail.Rows)
                {

                    if (Convert.ToBoolean(row.Cells[Column1.Name].Value) == true)
                    {
                        checkR = 1;
                        // MessageBox.Show(row.Cells[1].Value.ToString() );
                        //       FileStream(@"E:\page-17.jpg", FileMode.)
                        //  string chCombo5 = "";
                        //  if (!comboBox5.SelectedItem.Equals(null)) { chCombo5 = comboBox5.SelectedItem.ToString().Trim(); }
                        QrBarcode = "";
                        QrBarcode = txtPartNo.Text.ToString().Trim() + "@" + txtLotNo.Text.ToString() + "@" + row.Cells[1].Value.ToString();
                        QrBarcode += "@" + cmbQTY.Text.ToString().Trim();
                        Image d = qrcode.Draw(QrBarcode, 50);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            d.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            QRpic = ms.ToArray();
                        }
                        //     MessageBox.Show(comboBox3.SelectedItem.ToString());
                        reportClassBindingSource.Add(new ReportClass_Kanban()
                        {
                            cPicture = textBox6.Text.ToString().Trim(),//new FileStream(textBox6.Text.ToString().Trim(), FileMode.Open, FileAccess.Read) ,
                            cPart_no = txtPartNo.Text.ToString().Trim(),
                            cPart_name = txtPartName.Text.ToString().Trim(),
                            Ccode = txtCode.Text.ToString().Trim(),
                            Cname = txtCode.Text.ToString().Trim(),
                            Form = txtLotNo.Text.ToString().Trim(),
                            To = "",
                            NQTY = Convert.ToInt32(cmbQTY.SelectedItem.ToString()),
                            Nqtys = Convert.ToInt32(cmbQTY.SelectedItem.ToString()),
                            Pack = txtCus.Text.ToString().Trim(),

                            Model = txtModel.Text.ToString().Trim(),
                            LOT = txtLotNo.Text.ToString().Trim(),
                            Coating = "",
                            QR_stram = QRpic,
                            picture_stram = arr,//qrcode.Draw("goy", 50),
                            ttId = Convert.ToInt32(row.Cells[1].Value.ToString()),
                            Id = Convert.ToInt32(row.Cells[1].Value.ToString())

                        });
                        cSql = "UPDATE TAG_DETAIL SET NPRINT = ";//,CTO,CREV,CISSUED,CPACK,NQTY,CPICTURE,CMODEL) VALUES ( ";
                        cSql += " NPRINT + 1 , ";
                        cSql += "CDATE ='" + DateTime.Today.ToString("yyyyMMdd").Trim() + "'";
                        cSql += " WHERE CPART_NO = '" + txtPartNo.Text.ToString().Trim() + "'";
                        cSql += " AND KANBAN_ID = " + row.Cells[1].Value.ToString() + "";
                        new DB_OFFICE().ExecuteData(cSql);

                    }

                    cSql = "UPDATE TAG SET  ";//,CTO,CREV,CISSUED,CPACK,NQTY,CPICTURE,CMODEL) VALUES ( ";
                    //cSql += " CFORM = '" + textBox8.Text.ToString().Trim() + "'";
                    //cSql += ",CTO = '" + textBox8.Text.ToString().Trim() + "'";
                    //cSql += "CCOATING = ''";
                    cSql += "CLOT ='" + txtLotNo.Text.ToString().Trim() + "'";
                    cSql += ",CPACK ='" + txtCus.Text.ToString().Trim() + "'";
                    cSql += ",CMODEL ='" + txtModel.Text.ToString().Trim() + "'";
                    cSql += ",NQTY =" + cmbQTY.SelectedItem.ToString().Trim() + "";
                    cSql += ",CPICTURE = '" + textBox6.Text.ToString().Trim() + "'";
                    cSql += " WHERE CPART_NO = '" + txtPartNo.Text.ToString().Trim() + "'";
                    int a = new DB_OFFICE().ExecuteData(cSql);
                }//end for


                if (checkR >= 1)
                {


                    TestRPT mm = new TestRPT(reportClassBindingSource.DataSource as List<ReportClass_Kanban>, _ssTxt);
                    mm.Show();
                    showDeail(txtPartNo.Text.ToString().Trim());


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("DO YOU WANT TO CLOSE ?", "CONFIRM CLOSE ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;

                int R_index = dataGridView1.CurrentCell.RowIndex;
                int C_index = dataGridView1.CurrentCell.ColumnIndex;

                //ตำแหน่ง cells index ที่อยู่ในแถวเดียวกัน
                string PartNo = dataGridView1.Rows[R_index].Cells[1].Value.ToString().Trim(); // send to frm_showDetail
                string LotNo = dataGridView1.Rows[R_index].Cells[2].Value.ToString().Trim();

                txtLotNo.Text = LotNo;
                showDeail(PartNo);
                string txtSql = "SELECT  * FROM TAG_DETAIL INNER JOIN TAG ON TAG_DETAIL.CPART_NO = TAG.CPART_NO WHERE TAG_DETAIL.CPART_NO ='" + PartNo.ToString() + "' AND TAG_DETAIL.CLOT ='" + LotNo.ToString() + "'";
                DataTable dtFK = new DB_OFFICE().GetData(txtSql, "dd");
                if (dtFK.Rows.Count > 0)
                {
                    int i = dtFK.Rows.Count - 1;
                    txt_FCSKID.Text = dtFK.Rows[i]["FCPROD"].ToString();
                    txtModel.Text = dtFK.Rows[i]["CMODEL"].ToString();
                    txtCus.Text = dtFK.Rows[i]["CPACK"].ToString();
                    textBox6.Text = dtFK.Rows[i]["CPICTURE"].ToString();
                    cmbQTYTag.Text = dtFK.Rows[i]["KANBAN_ID"].ToString();
                    cmbQTY.Text = dtFK.Rows[i]["NQTY"].ToString();
                }

            }
            catch { }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_FCSKID.Text = "";
                GenerateDynaminBy_Part(textBox1.Text.ToString());

            }
        }
    }
}
