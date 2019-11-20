using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dangnhap.DTO;
using System.Data;

namespace Dangnhap.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }
        public FoodDAO() { }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "Select * from Food where idcategory =" + id;
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();
            string query = "Select * from Food";
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string query = string.Format("Select * from Food where name  like N'%{0}%'",name );
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> SearchFoodByNametrue(string name)
        {
            List<Food> list = new List<Food>();
            string query = ("select * from Food where name = N'"+name+"'");
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public bool InsertFood(string name , int id , float price)
        {
           string query = string.Format("Insert dbo.Food (name, idcategory,gia) values (N'{0}',{1},{2})", name,id, price);
           int result = dataProvider.Instance.ExecuteNonQuery(query);
           return result > 0;
        }
        public bool UpdateFood(int idFood,string name, int id, float price)
        {
            string query = string.Format("Update  dbo.Food set name = N'{0}', idcategory = {1}, gia = {2}  where ID = {3}", name, id, price,idFood);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            BillinfoDAO.Instance.DeleteBillInfoByFoodID(idFood);
            string query = string.Format("delete Food where id = {0}", idFood);
            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }

}
