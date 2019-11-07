using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dangnhap.DTO;
using System.Data;

namespace Dangnhap.DAO
{
    public class BillinfoDAO
    {
        private static BillinfoDAO instance;

        public static BillinfoDAO Instance
        {
            get { if (instance == null) instance = new BillinfoDAO(); return BillinfoDAO.instance; }
            private set { BillinfoDAO.instance = value; }
        }
        private BillinfoDAO() { }
        public List<Billinfo> GetListBillinfo(int id)
        {
            List<Billinfo> ListBillinfo = new List<Billinfo>();
            DataTable data = dataProvider.Instance.ExecuteQuery("Select * from dbo.Billinfo where idbill ="+id);
            foreach(DataRow item in data.Rows)
            {
                Billinfo info = new Billinfo(item);
                ListBillinfo.Add(info);

            }
            return ListBillinfo;
        }
         public void InsertBillinfo(int idBill,int idFood,int Count)
        {
            dataProvider.Instance.ExecuteQuery("USP_InsertBillinfo @idbill , @idfood , @count", new object[] { idBill, idFood, Count });
        }
         public void DeleteBillInfoByFoodID(int id)
         {
             dataProvider.Instance.ExecuteQuery("delete dbo.BillInfo where idfood = " + id);
         }
    }
}
