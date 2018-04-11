using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressRouter.Classes;
using ExpressRouter.Interfaces;
using ExpressRouter.Delegates;
using Moq;


namespace ExpressRouter.Test
{
    /// <summary>
    /// Summary description for ServerTest
    /// </summary>
    [TestClass]
    public class ServerTest
    {
        public ServerTest()
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
        public void TestServerConstructor()
        {
            var MockRes = new Mock<IResponsable<int>>(MockBehavior.Strict);
            var expected = "testString";
            var expectedTwo = "No descrption provided.";
            var testPath = "test";
            var testDictionary = new Dictionary<string, IServable<int>>();
            var testRouter = new Router<int>(testDictionary);

            Func<IRequestable<int>, IResponsable<int>> testFunc = (req) => { return MockRes.Object; };
            MiddleWareOperation<int> testMW = (ref IRequestable<int> req, ref IResponsable<int> res) => { throw new Exception(); };

            testRouter.AddServer(testPath, testMW);


            var testServer = new Server<int>(expected, testFunc);
            var secondTestServer = new Server<int>(testFunc);


            var actual = testServer.Description;
            var actualTwo = secondTestServer.Description;


            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedTwo, actualTwo);
        }
    }
}
