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
    public partial class frm_index : Form
    {
        public frm_index()
        {
            InitializeComponent();
        }

        private void frm_index_Load(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            frm_printLot frm = new frm_printLot();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.Show();

            button1.BackColor = Color.Blue;
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.RoyalBlue;
        }

        private void frm_index_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            frm_ScanTag frm = new frm_ScanTag();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.Show();

            button1.BackColor = Color.RoyalBlue;
            button2.BackColor = Color.Blue;
            button3.BackColor = Color.RoyalBlue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            frm_Qreport frm = new frm_Qreport();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.Show();

            button1.BackColor = Color.RoyalBlue;
            button2.BackColor = Color.RoyalBlue;
            button3.BackColor = Color.Blue;
        }
    }
}
