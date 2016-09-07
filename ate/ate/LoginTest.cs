
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
                driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));
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
                
                //chrome don'wanna wait for onload w\out that
                halt.Until(ExpectedConditions.ElementIsVisible(By.ClassName("logout-icon")));

                const string expectUrl = "https://appulatebeta.com/icc/insuredclientpolicies.aspx";
                Assert.AreEqual(expectUrl, driver.Url);
    
                driver.Dispose();
            }
        }

     
    }
}
