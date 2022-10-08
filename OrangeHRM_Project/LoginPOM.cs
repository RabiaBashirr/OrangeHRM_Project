using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


namespace OrangeHRM_Project
{
    public class LoginPOM
    {
        GeneralMethods GM = new GeneralMethods();

        // Objects to locate element        
        By username = By.Name("username");
        By password = By.Name("password");
        By submit = By.XPath("//button[@type='submit']");
                
        // If we want to maintain the state of base class browser then we need to use it.
        IWebDriver driver;
        public LoginPOM(IWebDriver Driver)
        {
            this.driver = Driver;
        }

        public void login(string UserInput, string PwdInput)
        {
            usernameInput(UserInput);
            passwordInput(PwdInput);
            ClickSubmit();
        }
        public void usernameInput(string UserInput)
        {
            GM.SendInputData(username, UserInput);
        }
        public void passwordInput(string pwd)
        {
            GM.SendInputData(password, pwd);
        }
        public void ClickSubmit()
        {
            GM.Element_Click(submit);
        }
    }
}
