using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dangnhap.DTO;
using System.Security.Cryptography;

namespace Dangnhap.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        public AccountDAO() { }      
        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord ";
            DataTable result = dataProvider.Instance.ExecuteQuery(query, new object[]{userName,passWord});
            return result.Rows.Count>0;
        }

        public bool UpdateAccount(string userName, string DisPlayName, string pass,string newpass)
        {
            int result  = dataProvider.Instance.ExecuteNonQuery("exec USP_updateAccount @userName  , @disPlayName , @password , @newPassword ", new object[]{userName,DisPlayName,pass,newpass});
            return result > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            
            DataTable data = dataProvider.Instance.ExecuteQuery("select * from Account where userName ='" + userName+ "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public DataTable GetListAccount()
        {
            return dataProvider.Instance.ExecuteQuery("select userName , DisplayName, type from dbo.Account");
        }
        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("Insert dbo.Account (userName, displayName , type) values (N'{0}',N'{1}',{2})", name, displayName, type);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateAccount( string name, string displayName, int type)
        {
            string query = string.Format("Update  dbo.Account set displayName = N'{1}',type = {2}  where userName = N'{0}'", name, displayName,type);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
            string query = string.Format("delete dbo.Account where userName = N'{0}'",name);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool ResetAccount(string name)
        {
            string query = string.Format("update dbo.Account set PassWord = N'0' where userName = N'{0}'", name);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
