using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V104.Input;
using OpenQA.Selenium.DevTools.V104.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrangeHRM_Project
{
    [TestClass]
    public class AdminSearchPOM
    {
        GeneralMethods GM =  new GeneralMethods();   

        By MenuItem1 = By.XPath("//span[text()='Admin']");
        By Username = By.XPath("(//input[@class='oxd-input oxd-input--active'])[2]");
        By ResetBtn = By.XPath("//button[text()=' Reset ']");
        By UserRole = By.XPath("(//div[text()='-- Select --'])[1]");
        By EmpName = By.XPath("//input[@placeholder='Type for hints...']");
        By Status = By.XPath("(//div[text()='-- Select --'])[2]");
        By SearchBtn = By.XPath("//button[@type='submit']");

        public void NavigatetoAdmin()
        {
            GM.Element_Click(MenuItem1);
        }

        public void CheckResetFunctionality(string user)
        {
            InputUsername(user);
            // Performing Reset will clear all the entered  data into the filters
            Reset_Search();
        }

        // Search with single field
        public void SearchSingleField(string username)
        {
            InputUsername(username);
            Search_click();
        }

        // Search with multiple fields in Admin role with status: Enabled
        public void SearchEnabledAdminOptions(string username, string empName)//, string status)
        {
            GM.Window_Refresh();
            InputStatusAsEnabled();
            InputUsername(username);
            GM.ImplicitWaits(10);
            InputUserRoleAsAdmin();
            Thread.Sleep(1000);
            InputEmpName(empName);
            Thread.Sleep(1000);
            Search_click();
        }

        // Search with multiple fields in Admin role with status: Disabled
        public void SearchDisabledAdminOptions(string username, string empName)//, string status)
        {
            GM.Window_Refresh();
            InputStatusAsDisabled();
            InputUsername(username);
            GM.ImplicitWaits(10);
            InputUserRoleAsAdmin();
            Thread.Sleep(1000);
            InputEmpName(empName);
            Thread.Sleep(1000);
            Search_click();
        }

        public void SearchEnabledESSOptions(string username, string empName)//, string status)
        {
            GM.Window_Refresh();
            InputStatusAsEnabled();
            InputUsername(username);
            Thread.Sleep(1000);
            InputUserRoleAsESS();
            Thread.Sleep(1000);
            InputEmpName(empName);
            Thread.Sleep(2000);
            Search_click();
        }

        public void SearchDisabledESSOptions(string username, string empName)//, string status)
        {
            GM.Window_Refresh();
            InputStatusAsDisabled();
            InputUsername(username);
            Thread.Sleep(1000);
            InputUserRoleAsESS();
            Thread.Sleep(1000);
            InputEmpName(empName);
            Thread.Sleep(2000);            
            Search_click();
        }        

        public void InputUsername(string user) { GM.SendInputData(Username, user); }

        // Select Admin from User Role Dropdown
        public void InputUserRoleAsAdmin()
        {
            /*
            IWebElement e=  GM.LocateElement(UserRole); 
            e.Click();
            e.SendKeys(Keys.ArrowDown);
            e.SendKeys(Keys.ArrowDown);
            e.SendKeys(Keys.Tab);
            //GM.Element_Click(userRole);
            //GM.MoveDowntheList(userRole);            
            //  GM.Element_Click();
            */

            IWebElement e = GM.LocateElement(UserRole);
            GM.Element_Click(UserRole);
            GM.MoveDowntheList(e);
            GM.SelectDropdownElement(e);
        }

        // Select ESS from User Role Dropdown
        public void InputUserRoleAsESS()
        {
            IWebElement e = GM.LocateElement(UserRole);
            GM.Element_Click(UserRole);
            GM.MoveDowntheList(e);
            GM.MoveDowntheList(e);
            GM.SelectDropdownElement(e);
        }

        // Enter data in Employee Name as hint
        public void InputEmpName(string empName)
        {
            GM.SendInputData(EmpName, empName + Keys.Escape);// +Keys.Tab);        
        }

        // Reset button Click to reset Username
        public void Reset_Search() { GM.Element_Click(ResetBtn); }

        public void InputStatusAsEnabled() 
        {
            IWebElement e = GM.LocateElement(Status);
            GM.Element_Click(Status);
            GM.MoveDowntheList(e);
            GM.SelectDropdownElement(e);
        }

        public void InputStatusAsDisabled()
        {
            IWebElement e = GM.LocateElement(Status);
            GM.Element_Click(Status);
            GM.MoveDowntheList(e);
            GM.MoveDowntheList(e);
            GM.SelectDropdownElement(e);
        }

        public void Search_click() { GM.Element_Click(SearchBtn); }        
    }
}
