using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Security.Policy;
using System.Data;
using System.Xml;
using System.Threading;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using OpenQA.Selenium.Interactions;

namespace OrangeHRM_Project
{
    [TestClass]
    public class Base
    {
        GeneralMethods GM = new GeneralMethods();
        LoginPOM Lp = new LoginPOM(driver);
        AdminSearchPOM ASP = new AdminSearchPOM();

        static IWebDriver driver = GeneralMethods.SeleniumBrowserInit("Chrome");
        string url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";


        // Login via DOM - Login Class
        [TestMethod]
        public void LoginFunctionality()
        {
            LoginMethods log1 = new LoginMethods(driver);

            // TestCase-01
            log1.VerifywithEmptyFields();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);

            /*
            * //Explicit Wait to show results
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));       
            wait.until(ExpectedConditions.presenceOfElementLocated(locator));          
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            */

            // TestCase-02
            log1.LoginNonExistingUser();
            // Implicit Wait to show results
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);

            // TestCase-03
            log1.LoginwithInvalidCredentials();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);

            // TestCase-04
            log1.ValidLogin();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);

            //GM.CloseBrowser();
        }



        // Login Via POM - LoginPOM class
        [TestMethod]
        public void LoginWithPOM()
        {
            GeneralMethods.Navigation(url);
            Thread.Sleep(1000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);
         
            // TestCase-01: Login without any data
            Lp.login("", "");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-02: Login with non-existing credentials
            Lp.login("Rabia", "Rabia-01");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-03: Login with Invalid credentials
            Lp.login("Admin", "rabia123");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-04: Login with valid credentials
            Lp.login("Admin", "admin123");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            Thread.Sleep(5000);
            GM.CloseBrowser();
        }



        // Login with Data-Driven POM
        /*   [TestMethod]
           public void DDT_Login()
           {
               //GeneralMethods.Navigation(url);
               //  driver.Navigate().GoToUrl(url);

               DataDriven_Login d = new DataDriven_Login();
               //Thread.Sleep(1000);
               d.EmptyFieldsLogin();
               //d.Clean();
           }

        #region Data Driven POM
           /* ****************** Data driven POM ********************* / 

           // Calling from Execution Class - DataDriven_Login & LoginPOM class

           GeneralMethods GM = new GeneralMethods();

           public const string DataSourceXML = "Microsoft.VisualStudio.TestTools.DataSource.XML";
           private TestContext instance;
           public TestContext TestContext
           {   set { instance = value; }               
               get { return instance; }    }

           [DataSource(DataSourceXML, "Login.xml", "ValidLoginCredentials", DataAccessMethod.Sequential)]
           [TestMethod]
           public void EmptyFieldsLogin()
           {
               string URL = TestContext.DataRow["URL"].ToString();
               string User = TestContext.DataRow["user"].ToString();
               string Pwd = TestContext.DataRow["pwd"].ToString();
               driver.Url = URL;
               LoginPOM lp = new LoginPOM(driver);
               Thread.Sleep(10000);
               lp.login(User, Pwd);
           }

           /*
           [TestCleanup]
           public void Clean()
           {
               GM.CloseBrowser();
           }           
        #endregion
        */



        /* ************* ADMIN ****************** */
        [TestMethod]
        public void AdminMenu_Search()
        {
            // Signin - Main Page URL
            GeneralMethods.Navigation(url);
            GM.ImplicitWaits(10);

            // Login the site
            Lp.login("Admin", "admin123");

            // Navigate to Admin - Menu item
            ASP.NavigatetoAdmin();

            // Calling Search functions
            ASP.SearchWithAdminOptions("Rabi", "Rabia");
            ASP.SearchWitESSOptions("Ra", "Rabi");
        }
    }
}
