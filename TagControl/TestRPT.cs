using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using LOT_CONTROL;
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
    public partial class TestRPT : Form
    {
        private int _checkReport;
        List<ReportClass_Kanban> _list;
        public TestRPT(List<ReportClass_Kanban> datasoure, int a)
        {
            InitializeComponent();
            _list = datasoure;
            _checkReport = a;
        }

        private void TestRPT_Load(object sender, EventArgs e)
        {
            ReportDocument rpt = crViewer.ReportSource as ReportDocument;

            //string path = Application.StartupPath + @"\crPW.rpt";
            //rpt.Load(path);


            foreach (Table crTable in rpt.Database.Tables)
            {
                TableLogOnInfo tableLogOnInfo = crTable.LogOnInfo;
                var connectionInfo = tableLogOnInfo.ConnectionInfo;

                // use connectionInfo to set database credentials

                crTable.ApplyLogOnInfo(tableLogOnInfo);
            }


            rpt.SetDataSource(_list);

            crViewer.ReportSource = rpt;

        }
    }
}
