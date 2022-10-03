using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium.Support.UI;

namespace OrangeHRM_Project
{

    public class GeneralMethods
    {
        public static IWebDriver driver;

        public static IWebDriver SeleniumBrowserInit(String browserName)
        {
            if (browserName != null)
            {
                if (browserName == "Chrome")
                {
                    driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                }

                else if (browserName == "MsEdge")
                    driver = new EdgeDriver();
            }
            return driver;
        }


        public static void Navigation(string url)
        {
            driver.Navigate().GoToUrl(url);
        }


        /* ******** Locator Methods (locate, SendKeys, Click) ******* */
        #region Locators

        // FindElement(locator) method
        public IWebElement LocateElement(By path)
        {
            return driver.FindElement(path);
        }

        // Send data to locators
        public void SendInputData(By path, string input)
        {
            IWebElement element = LocateElement(path);
            element.SendKeys(input);
        }

        // Clicking button
        public void Element_Click(By locator)
        {
            LocateElement(locator).Click();
        }

        public void PopulateDropdpwn(By Path)
        {
            IWebElement element = LocateElement(Path);
            element.SendKeys("");
        }

        public void MoveDowntheList(IWebElement element)
        {
            //IWebElement element = LocateElement(path);
            element.SendKeys(Keys.ArrowDown);
        }
        public void SelectDropdownElement(IWebElement element)
        {
            //IWebElement element = LocateElement(path);
            element.SendKeys(Keys.Tab);
        }
        #endregion


        public void Window_Refresh()
        {
            driver.Navigate().Refresh();
        }


        /* ******************** Waits ******************** */
        
        // Implicit Wait
        public void ImplicitWaits(int timespan)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timespan);
        }

        /* Explicit Wait
        public void ExplicitWaits(int timespan)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timespan));
        // or
          IWebElement SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(target_xpath)));
        }

        // Fluent Wait
        public void FluentWaits(int timespan)
        {
            WebDriverWait wait = new WebDriverWait(driver, timespan);
            wait.until(ExpectedConditions.elementToBeClickable(locator));
        }


        // Check if element is appeared on page or not
        public bool IsElementVisible(By by)
        {
            if (driver.FindElement(by).Displayed || driver.FindElement(by).Enabled)
            { 
                return true; 
            }
            else 
            {
                return false; 
            }
        }

        */

        //Closing browser
        public void CloseBrowser()
        {
            driver.Close();
        }

    }
}
