using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Security.Policy;

namespace OrangeHRM_Project
{
    [TestClass]
    public class Base
    {
        static IWebDriver driver = GeneralMethods.SeleniumBrowserInit("Chrome");

        string url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
        
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
            log1.ValidLogin();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
        }




        // LoginViaPOM
        [TestMethod]
        public void LoginWithPOM()
        {
            GeneralMethods.Navigation(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            LoginPOM Lp = new LoginPOM(driver);
            // TestCase-01: Login without any data
            Lp.login("", "");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-02: Login with non-existing credentials
            Lp.login("Rabia", "Rabia-01");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

            // TestCase-03: Login with valid credentials
            Lp.login("Admin", "admin123");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);

        }
    }
}
