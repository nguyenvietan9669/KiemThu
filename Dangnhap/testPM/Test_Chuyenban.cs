using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dangnhap;
using System.Data;

namespace testPM
{
    [TestClass]
    public class Test_Chuyenban
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                "|DataDirectory|\\testchuyenban.csv", "testchuyenban#csv", DataAccessMethod.Sequential),
                DeploymentItem("testPM\\testchuyenban.csv"),
    TestMethod]
        public void testchuyenban()
        {
            int ban1 = int.Parse(TestContext.DataRow[0].ToString());
            int ban2=int.Parse(TestContext.DataRow[1].ToString());
            bool expected = bool.Parse(TestContext.DataRow[2].ToString());
            QuanLy ql = new QuanLy();
            bool actual = ql.thuchuyen(ban1, ban2);
            Assert.AreEqual(expected, actual);
        }
    }
}
