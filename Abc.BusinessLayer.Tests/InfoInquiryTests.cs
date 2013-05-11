using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Abc.Entity;
using Abc.BusinessLayer;

namespace Abc.BusinessLayer.Tests
{
    /// <summary>
    /// Summary description for InfoInquiry
    /// </summary>
    [TestClass]
    public class InfoInquiryTests
    {
        InfoInquiryManager _mgrInfo;

        public InfoInquiryTests()
        {
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void TestInitialize()
        {
            _mgrInfo = new InfoInquiryManager(true);
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void HappyPathTest()
        {
            InfoInquiry one = new InfoInquiry { Email = "Fred@Flintstone.com" };

            InfoInquiry newOne = _mgrInfo.Add(one);

            Assert.IsTrue(newOne.Id > 0, "New object failed to have a valid Id");
        }
    }
}
