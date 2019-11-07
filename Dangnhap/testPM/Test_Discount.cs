using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dangnhap;
using System.Data;
using System.IO;

namespace testPM
{
    [TestClass]
    public class Test_Discount
    {
        [TestMethod]
        public void TestGiamGia()
        {
            int discount = 101;
            double totalPrice = 100000;
            double excepted = 0;

            if (discount > 100)
            {
                throw new InvalidOperationException("sai định dạng");
            }
                excepted = excepted * 1000;
                QuanLy ql = new QuanLy();
                double actual = ql.GiamGia(discount, totalPrice);
                Assert.AreEqual(excepted, actual);
        }
        [TestMethod]
        public void TestGiamGia1()
       {
           int discount = -1;
           double totalPrice = 100000;
           double excepted = 1000001;

           if (discount > 100 || discount < 0)
           {
               throw new InvalidOperationException("sai định dạng");
           }
           else
           {
               excepted = excepted * 1000;
               QuanLy ql = new QuanLy();
               double actual = ql.GiamGia(discount, totalPrice);
               Assert.AreEqual(excepted, actual);
           }
       }
        [TestMethod]
        public void TestGiamGia2()
        {
            int discount = 10;
            double totalPrice = 100000;
            double excepted = 90000;

            if (discount > 100 || discount < 0)
            {
                throw new InvalidOperationException("sai định dạng");
            }
            else
            {
                excepted = excepted * 1000;
                QuanLy ql = new QuanLy();
                double actual = ql.GiamGia(discount, totalPrice);
                Assert.AreEqual(excepted, actual);
            }
        }
        [TestMethod]
        public void TestGiamGia3()
        {
            int discount = 100;
            double totalPrice = 0;
            double excepted = 0;

            if (discount > 100 || discount < 0)
            {
                throw new InvalidOperationException("sai định dạng");
            }
            else
            {
                excepted = excepted * 1000;
                QuanLy ql = new QuanLy();
                double actual = ql.GiamGia(discount, totalPrice);
                Assert.AreEqual(excepted, actual);
            }
        }
        [TestMethod]
        public void TestGiamGia4()
        {
            int discount = 0;
            double totalPrice = 100000;
            double excepted = 100000;

            if (discount > 100 || discount < 0)
            {
                throw new InvalidOperationException("sai định dạng");
            }
            else
            {
                excepted = excepted * 1000;
                QuanLy ql = new QuanLy();
                double actual = ql.GiamGia(discount, totalPrice);
                Assert.AreEqual(excepted, actual);
            }
        }
        [TestMethod]
        public void TestGiamGia5()
        {
            int discount = 0;
            double totalPrice = 0;
            double excepted = 0;

            if (discount > 100 || discount < 0)
            {
                throw new InvalidOperationException("sai định dạng");
            }
            else
            {
                excepted = excepted * 1000;
                QuanLy ql = new QuanLy();
                double actual = ql.GiamGia(discount, totalPrice);
                Assert.AreEqual(excepted, actual);
            }
        }
    }
}
