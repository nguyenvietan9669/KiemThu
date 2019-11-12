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
    public class Test_ThemMon
    {
         public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\TestThemMon.csv","TestThemMon#csv", DataAccessMethod.Sequential),
                    DeploymentItem("testPM\\TestThemMon.csv"),
        TestMethod]
        public void testThemMon()
        {
            int idBill = int.Parse(TestContext.DataRow[0].ToString());
            int foodID = int.Parse(TestContext.DataRow[1].ToString());
            int idtable = int.Parse(TestContext.DataRow[2].ToString());
            int Count = int.Parse(TestContext.DataRow[3].ToString());
            bool expected = bool.Parse(TestContext.DataRow[4].ToString());
            QuanLy ql = new QuanLy();
            bool actual = ql.ThemMon(idBill,foodID,idtable,Count);
            Assert.AreEqual(expected, actual);
        }
    }
}
