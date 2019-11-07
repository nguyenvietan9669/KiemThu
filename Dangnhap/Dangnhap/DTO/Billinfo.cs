using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Dangnhap.DTO
{
    public class Billinfo
    {
        public Billinfo(int id, int billId, int foodId, int count)
        {
            this.BillId = billId;
            this.Count = count;
            this.FoodId = foodId;
            this.ID = id;
        }
        public Billinfo(DataRow row)
        {
            this.BillId = (int)row["idbill"];
            this.Count = (int)row["count"];
            this.FoodId = (int)row["idfood"];
            this.ID = (int)row["id"];
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private int foodId;

        public int FoodId
        {
            get { return foodId; }
            set { foodId = value; }
        }
        private int billId;

        public int BillId
        {
            get { return billId; }
            set { billId = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
