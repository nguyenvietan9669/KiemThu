using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Dangnhap.DTO
{
    public class MENU
    {
        public MENU(string foodName, int count, float price, float totalPrice = 0 )
        {
            this.Count = count;
            this.FoodName = foodName;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public MENU(DataRow row)
        {
            this.FoodName = row["Name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["gia"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }
        private float totalPrice;

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }
    }
}
