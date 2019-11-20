using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dangnhap;

namespace testPM
{
   
    [TestClass]
    public class Test_Sua
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\testsua.csv", "testsua#csv", DataAccessMethod.Sequential),
                    DeploymentItem("testPM\\testsua.csv"),
        TestMethod]
        public void Testsua()
        {
            string ten = TestContext.DataRow[0].ToString();
            int loai = int.Parse(TestContext.DataRow[1].ToString());
            float gia = float.Parse(TestContext.DataRow[2].ToString());
            int id = int.Parse(TestContext.DataRow[3].ToString());
            bool expected = bool.Parse(TestContext.DataRow[4].ToString());
            Admin ad = new Admin();
            bool actual = ad.sua(ten,loai,gia,id);
            Assert.AreEqual(expected,actual);
        }
    }
}
