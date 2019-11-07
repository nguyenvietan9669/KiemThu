using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dangnhap.DTO;
using System.Data;

namespace Dangnhap.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }
        public void SwitchTable(int table1, int table2)
        {
            dataProvider.Instance.ExecuteQuery("USP_SwitchTable @idTable1 , @idTable2", new object[]{table1,table2} );
        }
        public List<Table> GetListTable()
        {
            List<Table> list = new List<Table>();
            string query = "Select * from TableFood";
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }
        public TableDAO() { }
        public List<Table> LoadTableList()
        {
            List<Table> Tablelist = new List<Table>();
            DataTable data = dataProvider.Instance.ExecuteQuery("USP_GetTableList");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                Tablelist.Add(table);
            }
            return Tablelist;
        }
     
    }
}
