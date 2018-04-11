using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ExpressRouter.Classes;
using ExpressRouter.Interfaces;
using ExpressRouter.Delegates;
using ExpressRouter.Exceptions;

namespace ExpressRouter.Test
{
    /// <summary>
    /// Summary description for RouterTest
    /// </summary>
    [TestClass]
    public class RouterTest
    {
        public RouterTest()
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
        public void TestRouterConstructor()
        {
            //Arrange
            var testDictionary = new Dictionary<string, IServable<int>>();
            Router<int> testRouter = new Router<int>(testDictionary);

            //Apply
            bool expected = testDictionary.Equals(testRouter.DefinedRoutes);


            //Assert
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void TestAddServerMethod()
        {
            var testDictionary = new Dictionary<string, IServable<int>>();
            var testRouter = new Router<int>(testDictionary);
            var testPath = "testPath";
            testRouter.AddServer(testPath);

            bool keyExists = testRouter.DefinedRoutes.ContainsKey(testPath);

            Assert.IsTrue(keyExists);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddServerMethodWithNullString()
        {
            var testDictionary = new Dictionary<string, IServable<int>>();
            var testRouter = new Router<int>(testDictionary);
            string testPath = null;

            testRouter.AddServer(testPath);
        }

        [TestMethod]
        public void TestGetRequestFromServerMethod()
        {
            var MockReq = new Mock<IRequestable<int>>(MockBehavior.Strict);
            MockReq.Setup(x => x.Path).Returns("test");
            MockReq.Setup(x => x.Body).Returns(2);

            var testPath = "test";
            var testDictionary = new Dictionary<string, IServable<int>>();
            var testRouter = new Router<int>(testDictionary);

            testRouter.AddServer(testPath, req => req);

            var output = testRouter.GetResponseFromServer(MockReq.Object);

            var expectedOne = MockReq.Object.Path;
            var expectedTwo = MockReq.Object.Body;

            var actualOne = output.Path;
            var actualTwo = output.Body;

            Assert.AreEqual(expectedOne, actualOne);
            Assert.AreEqual(expectedTwo, actualTwo);
        }
        [TestMethod]
        [ExpectedException(typeof(Router400BadRequestException<int>))]
        public void TestGetRequestFromServerMethodWithBadRequest()
        {
            var MockReq = new Mock<IRequestable<int>>(MockBehavior.Strict);
            MockReq.Setup(x => x.Path).Returns("test");
            MockReq.Setup(x => x.Body).Returns(2);

            var testPath = "test";
            var testDictionary = new Dictionary<string, IServable<int>>();
            var testRouter = new Router<int>(testDictionary);

            testRouter.AddServer(testPath, req => throw new Exception());

            testRouter.GetResponseFromServer(MockReq.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Router404Exception))]
        public void TestGetResponseFromServerMethodWithInvalidRoute()
        {
            var MockReq = new Mock<IRequestable<int>>(MockBehavior.Strict);
            MockReq.Setup(x => x.Path).Returns("test");
            MockReq.Setup(x => x.Body).Returns(2);

            var testPath = "bacon cheeseburgers are life";
            var testDictionary = new Dictionary<string, IServable<int>>();
            var testRouter = new Router<int>(testDictionary);

            testRouter.AddServer(testPath, req => throw new Exception());

            testRouter.GetResponseFromServer(MockReq.Object);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetResponseFromServerMethodWithNullRequest()
        {
            IRequestable<int> testReq = null;
            var testDict = new Dictionary<string, IServable<int>>();
            var testRouter = new Router<int>(testDict);

            testRouter.GetResponseFromServer(testReq);
        }
    }
}
