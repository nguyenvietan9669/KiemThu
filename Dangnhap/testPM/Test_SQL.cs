using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dangnhap;
using System.Data;
using Dangnhap.DAO;

namespace testPM
{
    [TestClass]
    public class Test_SQL
    {
        [TestMethod]
        public void testsql()
        {
            string query = "USP_Login @userName , @passWord";
            string userName = "admin";
            string passWord = "123";
            DataTable result = dataProvider.Instance.ExecuteQuery(query,
                new object[] { userName, passWord });
            Assert.AreEqual(1,result.Rows.Count);           
        }

        [TestMethod]
        public void testsqlfalse()
        {
            string query = "USP_Login @userName , @passWord";
            string userName = "abc";
            string passWord = "123";
            DataTable result = dataProvider.Instance.ExecuteQuery(query,
                new object[] { userName, passWord });
            Assert.AreEqual(0,result.Rows.Count);
        }
        [TestMethod]
        public void testsqlthrow()
        {
            string query = "usp @userName , @passWord";
            string userName = "abc";
            string passWord = "123";
            DataTable result = dataProvider.Instance.ExecuteQuery(query,
                new object[] { userName, passWord });
            Assert.AreEqual(0, result.Rows.Count);
        }
    }
}
