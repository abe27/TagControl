using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOT_CONTROL
{
    public class ReportClass_Kanban
    {
        //public FileStream cPicture { get; set; }
        public string cPicture { get; set; }
        public string cPart_no { get; set; }
        public string cPart_name { get; set; }
        public string Ccode { get; set; }
        public string Cname { get; set; }
        public string Model { get; set; }
        public string Form { get; set; }
        public string To { get; set; }
        public string Coating { get; set; }
        public string Pack { get; set; }
        public string LOT { get; set; }
        public int Nqtys { get; set; }
        public int Id { get; set; }
        public int ttId { get; set; }
        public byte[] picture_stram { get; set; }
        public byte[] QR_stram { get; set; }




        //////MTM RPT
        public string MTM_NO { get; set; }
        public string PART_MOM { get; set; }
        public string NAME_MOM { get; set; }
        public string PART_SON { get; set; }
        public string NAME_SON { get; set; }
        public double NQTY { get; set; }
        public string UM { get; set; }
        public string FCNAME { get; set; }
        public DateTime RMTM_DATE { get; set; }
        public byte[] QR_MTM { get; set; }
        public string CKBID { get; set; }
        public string KANBAN { get; set; }
        public string QTY_KB { get; set; }
        public string USER_NAME { get; set; }
        public string PD_TIME { get; set; }
        public string tId { get { return string.Format("{0} / {1}", Id, ttId); } }
    }
}
