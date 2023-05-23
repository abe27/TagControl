using System;
using System.Linq;
using System.Windows.Forms;

namespace TagControl
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "ABCD";

                if (textBox1.Text != "")
                {
                    string chechCharect = "";
                    int found = 0;
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        var res = textBox1.Text[i];
                        var ischeck = result.Contains(res);
                        //var fineIndex = Array.Find(chechCharect, element => element == result);
                        Console.WriteLine($"{i} ::: {res} is {ischeck}");
                        if (ischeck)
                        {
                            if (chechCharect == "")
                            {
                                chechCharect += res;
                                found++;
                            }
                            else
                            {
                                if (!chechCharect.Contains(res))
                                {
                                    chechCharect += res;
                                    found++;
                                }
                            }
                        }
                    }
                    Console.WriteLine($"FOUND: {found}");

                    //string N = "";
                    string index = "";
                    //for (int i = 0; i < result.Length; i++)
                    //{
                    //    if (textBox1.Text[i] == result[0])
                    //    {
                    //        Console.WriteLine(textBox1.Text[i]);
                    //        N = textBox1.Text[i].ToString();
                    //    }
                    //    if (textBox1.Text[i] == result[1])
                    //    {
                    //        Console.WriteLine(textBox1.Text[i]);
                    //        N+= textBox1.Text[i].ToString();
                    //    }
                    //    if (textBox1.Text[i] == result[2])
                    //    {
                    //        Console.WriteLine(textBox1.Text[i]);
                    //        N+= textBox1.Text[i].ToString();
                    //    }
                    //    if (textBox1.Text[i] == result[3])
                    //    {
                    //        Console.WriteLine(textBox1.Text[i]);
                    //        N += textBox1.Text[i].ToString();

                    //    }
                    //}

                    // find หา array ที่ซ้ำกัน
                    if (textBox1.Text[0] == result[0])
                    {
                        Console.WriteLine(textBox1.Text[0]);
                        index = textBox1.Text[0].ToString();
                    }
                    if (textBox1.Text[1] == result[1])
                    {
                        Console.WriteLine(textBox1.Text[1]);
                        index += textBox1.Text[1].ToString();
                    }
                    if (textBox1.Text[2] == result[2])
                    {
                        Console.WriteLine(textBox1.Text[2]);
                        index += textBox1.Text[2].ToString();
                    }
                    if (textBox1.Text[3] == result[3])
                    {
                        Console.WriteLine(textBox1.Text[3]);
                        index += textBox1.Text[3].ToString();

                    }


                    dataGridView1.Rows.Add(result, textBox1.Text, chechCharect.Length, index.Length, (dataGridView1.Rows.Count + 1));

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    //label1.Text = $"จำนวนที่สุ่มเท่ากับ {dataGridView1.Rows.Count} ครั้ง";
                }
            }
            catch { }
        }
    }
}
