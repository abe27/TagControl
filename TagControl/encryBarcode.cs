using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOT_CONTROL
{
    class encryBarcode
    {
        private string _poNo, _partNo, _date, _Qty, _txtScan, _lotNo;
        private string varOper;

        public string txtScan
        {
            set { _txtScan = value; }
            get { return _txtScan; }
        }
        public string partNo
        {
            set { _partNo = value; }
            get { return _partNo; }
        }
        public string poNo
        {
            set { _poNo = value; }
            get { return _poNo; }
        }
        public string date
        {
            set { _date = value; }
            get { return _date; }
        }
        public string Qty
        {
            set { _Qty = value; }
            get { return _Qty; }
        }

        public string conDate()
        {
            if (!_date.Equals(""))
            {
                _date = _date.Substring(6, 2) + "/" + _date.Substring(4, 2) + "/" + _date.Substring(0, 4);
                return _date;
            }
            else
            {
                return "";
            }

        }
        public string runEncry()
        {
            varOper = ",";
            _poNo = string.Concat(_txtScan.Substring(29, 4), varOper, _txtScan.Substring(33, 4));
            _partNo = string.Concat(_txtScan.Substring(3, 5), varOper, _txtScan.Substring(8, 4), varOper, _txtScan.Substring(12, 1));
            _Qty = _txtScan.Substring(45, 7);
            //  _date = _txtScan.Substring(43, 2) + "/" + _txtScan.Substring(41, 2) + "/" + _txtScan.Substring(37, 4);
            return _poNo;

        }
        public string runEncryVCST()
        {
            varOper = "-";
            _poNo = string.Concat(_txtScan.Substring(29, 4), varOper, _txtScan.Substring(33, 4));
            _partNo = string.Concat(_txtScan.Substring(3, 5), varOper, _txtScan.Substring(8, 4), varOper, _txtScan.Substring(12, 1));
            _Qty = _txtScan.Substring(45, 7);
            _date = _txtScan.Substring(43, 2) + "/" + _txtScan.Substring(41, 2) + "/" + _txtScan.Substring(37, 4);
            return _poNo;

        }

    }
}
