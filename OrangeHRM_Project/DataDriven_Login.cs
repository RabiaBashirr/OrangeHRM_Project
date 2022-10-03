using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


namespace OrangeHRM_Project
{

    [TestClass]
    public class DataDriven_Login
    {
        GeneralMethods GM = new GeneralMethods();
        static IWebDriver driver = GeneralMethods.SeleniumBrowserInit("Chrome");
        //string url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
        public const string DataSourceXML = "Microsoft.VisualStudio.TestTools.DataSource.XML";
        //public const string XMLFile = "D:\\Contour Software Automation\\Project\\OrangeHRM_Project\\OrangeHRM_Project\\bin\\Debug\\Login.xml";

        private TestContext instance;
        public TestContext TestContext
        {
            set
            {
                instance = value;
            }
            get
            {
                return instance;
            }
        }
        /*
        [AssemblyInitialize]
        public void AssemblyInit(TestContext TestContext)
        {
            Console.WriteLine("Assembly Method Initialization! ");
        }       
        */
        [ClassInitialize]
        public static void ClassInit(TestContext TestContext)
        {
            Console.WriteLine("Class Method Initilization! ");
        }   

        [TestInitialize]
        public void init()
        {
            Console.WriteLine("Test Method Initialization! ");
        }
     

        [DataSource(DataSourceXML, "Login.xml", "EmptyFieldsLogin", DataAccessMethod.Sequential)]
        [TestMethod]
        public void LoginWithEmptyFields()
        {
            string URL = TestContext.DataRow["URL"].ToString();
            string User = TestContext.DataRow["user"].ToString();
            string Pwd = TestContext.DataRow["pwd"].ToString();

            driver.Url = URL;
            LoginPOM lp = new LoginPOM(driver);
            Thread.Sleep(1000);
            lp.login(User, Pwd);           
        }



        [DataSource(DataSourceXML, "Login.xml", "NonExistingUserLogin", DataAccessMethod.Sequential)]
        [TestMethod]
        public void LoginWithNonExistingUser()
        {
            string URL = TestContext.DataRow["URL"].ToString();
            string User = TestContext.DataRow["user"].ToString();
            string Pwd = TestContext.DataRow["pwd"].ToString();
            driver.Url = URL;
            LoginPOM lp = new LoginPOM(driver);
            Thread.Sleep(1000);
            lp.login(User, Pwd);
            //GM.CloseBrowser();
        }

        [DataSource(DataSourceXML, "Login.xml", "InvalidLoginCredentials", DataAccessMethod.Sequential)]
        [TestMethod]
        public void LoginWithInvalidCredentials()
        {
            string URL = TestContext.DataRow["URL"].ToString();
            string User = TestContext.DataRow["user"].ToString();
            string Pwd = TestContext.DataRow["pwd"].ToString();
            driver.Url = URL;
            LoginPOM lp = new LoginPOM(driver);
            Thread.Sleep(1000);
            lp.login(User, Pwd);
            //GM.CloseBrowser();

        }
        

        [DataSource(DataSourceXML, "Login.xml", "ValidLoginCredentials", DataAccessMethod.Sequential)]
        [TestMethod]
        public void LoginWithValidCredentials()
        {
            string URL = TestContext.DataRow["URL"].ToString();
            string User = TestContext.DataRow["user"].ToString();
            string Pwd = TestContext.DataRow["pwd"].ToString();
            driver.Url = URL;
            LoginPOM lp = new LoginPOM(driver);
            Thread.Sleep(1000);
            lp.login(User, Pwd);
        }

        
        [TestCleanup]
        public void MethodCleaning()
        {
            Console.WriteLine("Test Method Cleanup! ");
            //GM.CloseBrowser();
        }
        /*

         [ClassCleanup]
         public void ClassCleaning()
         {
         Console.WriteLine("Class Method Cleanup! ");
         } 


    [AssemblyCleanup]
    public void AssemblyCleanup()
    {
        Console.WriteLine("Assembly Method Cleanup! ");
    }*/
    }
}
