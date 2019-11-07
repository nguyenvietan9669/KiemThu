using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Dangnhap.DTO
{
    public class Account
    {
        public Account(string userName, string displayName, int type, string passWord = null)
        {
            this.DisplayName = displayName;
            this.UserName = userName;
            this.Type = type;
            this.PassWord = passWord;
        }
        public Account(DataRow row)
        {
            this.DisplayName = row["DisplayName"].ToString();
            this.UserName = row["userName"].ToString();
            this.Type = (int)row["type"];
            this.PassWord = row["PassWord"].ToString();
        }
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
