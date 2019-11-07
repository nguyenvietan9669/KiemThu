using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Dangnhap;

namespace TestKiemThu
{
    [TestClass]
    public class TestLogin
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @".\Data\TestData.csv",
            "testlogin#csv", DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestCNLogin()
        {
            string user = TestContext.DataRow[0].ToString();
            string pass = TestContext.DataRow[1].ToString();
            bool expected = bool.Parse(TestContext.DataRow[2].ToString());
            FDangNhap f = new FDangNhap();
            f.txtTen = user;
            f.txtpass = pass;
            bool actual = f.isAccepted();
            Assert.AreEqual(expected, actual);
        }
    }
}
