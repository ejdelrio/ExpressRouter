using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressRouter.Exceptions;
using ExpressRouter.Interfaces;
using Moq;

namespace ExpressRouter.Test
{
    /// <summary>
    /// Summary description for ExceptionClassTest
    /// </summary>
    [TestClass]
    public class ExceptionClassTest
    {
        public ExceptionClassTest()
        {
            //
            // TODO: Add constructor logic here
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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test404NotFoundRequestConstructor()
        {
            var testPath = "test";
            var testException = new Router404Exception(testPath);
            var testTwo = new Router404Exception();

            var actual = testException.Path;

            Assert.AreEqual(testPath, actual);
        }

        [TestMethod]
        public void Test400BadRequestConstructor()
        {
            var testInt = 1;
            var testPath = "test";

            var MockReq = new Mock<IRequestable<int>>(MockBehavior.Strict);
            MockReq.Setup(x => x.Body).Returns(testInt);
            MockReq.Setup(x => x.Path).Returns(testPath);

            var testError = new Router400BadRequestException<int>(MockReq.Object);

            var acutalInt = testError.Body;
            var actualPath = testError.Path;

            Assert.AreEqual(testInt, acutalInt);
            Assert.AreEqual(testPath, actualPath);
        }
    }
}
