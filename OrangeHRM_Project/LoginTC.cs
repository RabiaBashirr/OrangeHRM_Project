using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;


namespace OrangeHRM_Project
{

    public class LoginMethods
    {
        string url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
        static IWebDriver driver = null;

        public LoginMethods(IWebDriver Driver)
        {
            driver = Driver;
        }

        #region Login_TC01
        // Tried to login without required fields

        public void VerifywithEmptyFields()
        {
            //driver.Url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
            GeneralMethods.Navigation(url);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        #endregion Login_TC01



        #region Login_TC02
        // Tried to login with non-existing user
        [TestMethod]
        public void LoginNonExistingUser()
        {
            //string url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
            GeneralMethods.Navigation(url);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Name("username")).SendKeys("Rabia");
            driver.FindElement(By.Name("password")).SendKeys("Rabia-01");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        #endregion



        #region Login_TC03
        // Tried to login with Invalid credentials
        [TestMethod]
        public void LoginwithInvalidCredentials()
        {
            //string url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
            GeneralMethods.Navigation(url);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("Rabia-01"); // wrong password
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        #endregion



        #region Login_TC04
        // Login with Valid credentials

        public void ValidLogin()
        {
            GeneralMethods.Navigation(url);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        #endregion

    }
}
