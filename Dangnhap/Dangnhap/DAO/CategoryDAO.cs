using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dangnhap.DTO;
using System.Data;

namespace Dangnhap.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }
        private CategoryDAO()
        { }
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from Foodcategory ";
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category); 
            }
            return list;
        }
        public List<Category> GetListDanhMuc()
        {
            List<Category> list = new List<Category>();
            string query = "Select * from FoodCategory";
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }
            return list;
        }
        public Category GetCategoryById(int id)
        {
            Category category = null;
            string query = "select * from Foodcategory where ID = " + id;
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
               category = new Category(item);
               return category;
            }
            return category;
        }
        public bool InsertCategory(string name, int id)
        {
            string query = string.Format("Insert dbo.FoodCategory (id,name) values ({0},N'{1}')", id, name);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateCategory( string name, int id)
        {
            string query = string.Format("Update  dbo.FoodCategory set name = N'{0}'  where ID = {1}", name, id);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteCategory(int id)
        {
            BillinfoDAO.Instance.DeleteBillInfoByFoodID(id);
            string query = string.Format("delete FoodCategory where id = {0}", id);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
