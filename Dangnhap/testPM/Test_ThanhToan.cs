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
    public class Test_ThanhToan
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\testThanhToan.csv", "testThanhToan#csv", DataAccessMethod.Sequential),
                    DeploymentItem("testPM\\testThanhToan.csv"),
        TestMethod]
        public void test_ThanhToan()
        {
            int idBill = int.Parse(TestContext.DataRow[0].ToString());
            int discount = int.Parse(TestContext.DataRow[1].ToString());
            double final = int.Parse(TestContext.DataRow[2].ToString());
            bool expected = bool.Parse(TestContext.DataRow[3].ToString());
            ;
            QuanLy q = new QuanLy();
            bool actual = q.ThanhCong(idBill, discount, final);
            Assert.AreEqual(expected, actual);
        }
    }
}
