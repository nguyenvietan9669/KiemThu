using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dangnhap.DTO;
using System.Data;

namespace Dangnhap.DAO
{
    public class MENUDAO
    {
        private static MENUDAO instance;

        public static MENUDAO Instance
        {
            get { if (instance == null) instance = new MENUDAO(); return MENUDAO.instance; }
            private set { MENUDAO.instance = value; }
        }
        private MENUDAO() { }
        public List<MENU> GetListMenuByTable(int id)
        {
            string query = "Select f.name,bi.count,f.gia,f.gia*bi.count As totalPrice from dbo.Billinfo As bi,dbo.Bill As b,dbo.Food As f  where bi.idbill = b.ID and bi.idfood = f.ID and b.status = 0 and b.idTableFood = "+id;
            List<MENU> ListMenu = new List<MENU>();
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                MENU menu = new MENU(item);
                ListMenu.Add(menu);
            }
            return ListMenu; 
        }
    }
}
