using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dangnhap;

namespace testPM
{
    [TestClass]
    public class Test_Them
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                "|DataDirectory|\\testThem.csv", "testThem#csv", DataAccessMethod.Sequential),
                DeploymentItem("testPM\\testThem.csv"),
    TestMethod]
        public void testThem()
        {
            string name = TestContext.DataRow[0].ToString();
            int loaiId = int.Parse(TestContext.DataRow[1].ToString());
            float gia = float.Parse(TestContext.DataRow[2].ToString());
            bool expected = bool.Parse(TestContext.DataRow[3].ToString());
            Admin ad = new Admin();
            bool actual = ad.Them(name, loaiId, gia);
            Assert.AreEqual(expected, actual);
        }
    }
}
       