
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace ate
{
    [TestFixture]
    public class LoginTest
    {
        private const string _usermail = "david.smith.appulatetest@appulatemail.com";
        private const string _userpass = "123321";

        
       
        [TestCase (BrowsersEnum.chrome)]
        [TestCase (BrowsersEnum.ie)]
        public void Test1(BrowsersEnum browser)
        {
            using (IWebDriver driver = Driver.GetDriver(browser))
            {

                driver.Navigate().GoToUrl("https://appulatebeta.com/signin");
                IWebElement useremail = driver.FindElement(By.Id("email"));
                IWebElement userpassword = driver.FindElement(By.Id("password"));
                IWebElement submitBttn = driver.FindElement(By.ClassName("signin-button"));

                var halt = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                useremail.Clear();
                useremail.SendKeys(_usermail);

                halt.Until(ExpectedConditions.TextToBePresentInElementValue(useremail, _usermail));

                userpassword.Clear();
                userpassword.SendKeys(_userpass);
                submitBttn.Click();

                string expectedUrl = "https://appulatebeta.com/icc/insuredclientpolicies.aspx";
                Assert.IsTrue(driver.Url == expectedUrl, driver.Url);
                driver.Dispose();
            }
        }
    }
}
