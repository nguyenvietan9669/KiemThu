using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dangnhap.DTO;

namespace Dangnhap.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if(instance==null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }
        private BillDAO() { }
        public int GetUnBillByTableID(int id)
        {
            DataTable data = dataProvider.Instance.ExecuteQuery("Select * from dbo.Bill where idTableFood =" + id + " And status =0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void CheckOut(int id,int discount,float totalPrice)
        {
            string query = "update dbo.Bill set NgayRa = GETDATE(), status = 1,totalPrice ="+totalPrice+",discount ="+discount+" where ID = "+id;
            dataProvider.Instance.ExecuteNonQuery(query);
        }
        public void InsertBill(int id)
        {
            dataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @idtable", new object[] { id });
        }
        public DataTable GetBillListByDate(DateTime CheckOut, DateTime CheckIn)
        {
            return dataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDate @CheckIn , @CheckOut ", new object[] { CheckIn, CheckOut });
        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)dataProvider.Instance.ExecuteScalar("  Select MAX(id) from dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
