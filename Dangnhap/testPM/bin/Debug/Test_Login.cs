using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Dangnhap;
using System.IO;

namespace testPM
{
    [TestClass]
    public class TestLogin
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\testLogin.csv", "testLogin#csv", DataAccessMethod.Sequential),
                    DeploymentItem("testPM\\testLogin.csv"),
        TestMethod]
        public void TestCNLogin()
        {
            string userName = TestContext.DataRow[0].ToString();
            string passWord = TestContext.DataRow[1].ToString();
            bool expected = bool.Parse(TestContext.DataRow[2].ToString());
            if (userName.Length > 100 || passWord.Length > 100)
            {
                throw new InvalidOperationException("số kí tự quá lớn");
            }
            else
            {
                FDangNhap b = new FDangNhap();
                b.userName = userName;
                b.passWord = passWord;
                bool actual = b.isAccepted();
                Assert.AreEqual(expected, actual);
            }
         
        }
        
    }
}
