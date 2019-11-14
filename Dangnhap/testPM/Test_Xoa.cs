using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dangnhap;

namespace testPM
{
    [TestClass]
    public class Test_Xoa
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\testXoa.csv", "testXoa#csv", DataAccessMethod.Sequential),
                    DeploymentItem("testPM\\testXoa.csv"),
        TestMethod]
        public void Testxoa()
        {
            int id = int.Parse(TestContext.DataRow[0].ToString());
            bool expected = bool.Parse(TestContext.DataRow[1].ToString());
            Admin ad = new Admin();
            bool actual = ad.xoa(id);
            Assert.AreEqual(expected, actual);
        }       
    }
}
