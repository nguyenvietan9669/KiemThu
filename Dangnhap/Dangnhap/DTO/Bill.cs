using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Dangnhap.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? ngayVao, DateTime? ngayRa, int status,int discount=0)
        {
            this.ID=iD;
            this.NgayVao = ngayVao;
            this.NgayRa = ngayRa;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["iD"];
            this.NgayVao = (DateTime?)row["ngayVao"];
            var ngayRaTemp = row["ngayRa"];
            if (ngayRaTemp.ToString() != "")
                this.NgayRa = (DateTime?)ngayRaTemp;
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }
        private int discount;

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime? ngayRa;

        public DateTime? NgayRa
        {
            get { return ngayRa; }
            set { ngayRa = value; }
        }
        private DateTime? ngayVao;

        public DateTime? NgayVao
        {
            get { return ngayVao; }
            set { ngayVao = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
