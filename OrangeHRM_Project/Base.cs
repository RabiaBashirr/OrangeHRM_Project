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
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static IWebDriver driver = GeneralMethods.SeleniumBrowserInit("Chrome");
        string url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";

        GeneralMethods GM = new GeneralMethods();
        LoginMethods LM = new LoginMethods(driver);
        LoginPOM Lp = new LoginPOM(driver);
        AdminSearchPOM ASP = new AdminSearchPOM();

        [TestMethod]
        public void FullAutomatedWesbite()
        {
            GeneralMethods.Navigation(url);
            LoginWithPOM();
            AdminMenu_Search();


        }


        /* ***************************
         * Login via DOM - Login Class 
         *************************** */

        [TestMethod]
        public void LoginFunctionality()
        {
            //IWebDriver driver = GeneralMethods.SeleniumBrowserInit("Chrome");

            log.Info("Calling Login function via Dom:");

            // TestCase-01
            log.Info("Verify if it allows to login with empty fields?");
            LM.VerifywithEmptyFields();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);
            log.Info("It throws message of required field");

            /*
            * //Explicit Wait to show results
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));       
            wait.until(ExpectedConditions.presenceOfElementLocated(locator));          
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            */

            // TestCase-02
            log.Info("Verify if it allows to login with non-existing user?");
            LM.LoginNonExistingUser();
            // Implicit Wait to show results
            log.Info("It throws a message of Invalid credentials.");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);

            // TestCase-03
            log.Info("Verify if it allows to login with invalid credntials");
            LM.LoginwithInvalidCredentials();
            log.Info("It throws a message of Invalid credentials.");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);

            // TestCase-04
            log.Info("Verify if it allows to login with valid credentials?");
            LM.ValidLogin();
            log.Info("Login is successful and redirected to PIM");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);

            //AdminMenu_Search();
            //GM.CloseBrowser();
        }


        /* *******************************
         * Login Via POM - LoginPOM class
         ****************************** */

        [TestMethod]
        public void LoginWithPOM()
        {
            // GeneralMethods.Navigation(url);

            log.Info("Calling Login function via POM:");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-01: Login without any data
            log.Info("Verify if it allows to login with empty fields?");
            Lp.login("", "");
            log.Info("It throws message of required field");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-02: Login with non-existing credentials
            log.Info("Verify if it allows to login with non-existing user?");
            Lp.login("Rabia", "Rabia-01");
            log.Info("It throws a message of Invalid credentials.");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-03: Login with Invalid credentials
            log.Info("Verify if it allows to login with invalid credntials");
            Lp.login("Admin", "rabia123");
            log.Info("It throws a message of Invalid credentials.");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-04: Login with valid credentials
            log.Info("Verify if it allows to login with valid credentials?");
            Lp.login("Admin", "admin123");
            log.Info("Login is successful and redirected to PIM");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            //  Thread.Sleep(5000);
            //   GM.CloseBrowser();
        }


        /* ****************************
         * Login with Data-Driven POM
         * -- We can't call datasource methods
         *************************** * /

        [TestMethod]
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
           /* ****************** Data driven POM ********************* * / 

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
            //GeneralMethods.Navigation(url);
            GM.ImplicitWaits(10);

            // Login the site
            //Lp.login("Admin", "admin123"); 

            // Navigate to Admin - Menu item
            log.Info("\nNavigation to Admin Module!");
            ASP.NavigatetoAdmin();

            /* ***** Performing Test Case on Search Section ***** */

            // Test Case - 01: Check reset functionality 
            log.Info("Checking the reset functionality");
            ASP.CheckResetFunctionality("dummy User");

            // Test Case - 02: Search with one field
            log.Info("Search with one field:");
            ASP.SearchSingleField("Hassam");

            // Test Case - 03: Search user as an Admin role with status: Enabled
            log.Info("Search user as an Admin role with status: Enabled");
            ASP.SearchEnabledAdminOptions("Rabi", "Rabia");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // Test Case - 04: Search user as an Admin role with status: Disabled
            log.Info("Search user as an Admin role with status: Disabled");
            ASP.SearchDisabledAdminOptions("Rabi", "Rabia");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // Test Case - 05: Search user as an ESS role with status: Enabled
            log.Info("Search user as an ESS role with status: Enabled");
            ASP.SearchEnabledESSOptions("Ra", "Rabi");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // Test Case - 06: Search user as an ESS role with status: Disabled
            log.Info("Search user as an ESS role with status: Disabled");
            ASP.SearchDisabledESSOptions("Ra", "Rabi");
        }
    }
}