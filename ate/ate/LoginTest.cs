
using System;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace ate
{
    
    public class LoginTest
    {
        protected IWebDriver driver;
        const string LoginURL = "https://appulatebeta.com/signin";
        const string LoggedURL = "https://appulatebeta.com/icc/insuredclientpolicies.aspx";

        public void CorrectLogin(IWebDriver driver)
        {

                
                string _usermail = "david.smith.appulatetest@appulatemail.com";
                string _userpass = "123321";

                driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));
                driver.Navigate().GoToUrl(LoginURL);
                IWebElement useremail = driver.FindElement(By.Id("email"));
                IWebElement userpassword = driver.FindElement(By.Id("password"));
                IWebElement submitBttn = driver.FindElement(By.ClassName("signin-button"));

                var halt = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                Driver.FocusElement(driver, useremail);
                useremail.Clear();
                useremail.SendKeys(_usermail);

                halt.Until(ExpectedConditions.TextToBePresentInElementValue(useremail, _usermail));
                Driver.FocusElement(driver, userpassword);
                userpassword.Clear();
                userpassword.SendKeys(_userpass);
                submitBttn.Click();

                //chrome driver is too fast, and don'wanna wait for onload w\out that
                halt.Until(ExpectedConditions.ElementIsVisible(By.ClassName("logout-icon")));

                const string expectUrl = LoggedURL;
                Assert.AreEqual(expectUrl, driver.Url, "Logging is incomplete");
                Assert.AreEqual(driver.FindElement(By.ClassName("first-name")).Text, "David", "First name is wrong");
                Assert.AreEqual(driver.FindElement(By.ClassName("last-name")).Text, "Smith", "Last name is wrong");
            
            }        


        
        public void EmptyDataLogin(IWebDriver driver)
        {
            
                driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));
                driver.Navigate().GoToUrl(LoginURL);
                IWebElement useremail = driver.FindElement(By.Id("email"));
                IWebElement userpassword = driver.FindElement(By.Id("password"));
                IWebElement submitBttn = driver.FindElement(By.ClassName("signin-button"));

                useremail.Clear();
                userpassword.Clear();
                submitBttn.Click();

                Thread.Sleep(500);

                const string expectUrl = LoginURL;
                Assert.AreEqual(expectUrl, driver.Url, "Smthn changed");


                Assert.IsTrue(driver.FindElement(By.Id("email-error")).Enabled, "Email-missing warning disabled");
                Assert.IsTrue(driver.FindElement(By.Id("password-error")).Enabled, "Password missing warning disabled");

                driver.Dispose();

            
        }


        
        public void WrongLogin(IWebDriver driver)
        {
           
                string _wrongusermail = "davidFake@fakemail.com";
                string _wronguserpass = "123321";

                driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));
                driver.Navigate().GoToUrl(LoginURL);

                IWebElement useremail = driver.FindElement(By.Id("email"));
                IWebElement userpassword = driver.FindElement(By.Id("password"));
                IWebElement submitBttn = driver.FindElement(By.ClassName("signin-button"));

                var halt = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            
                Driver.FocusElement(driver, useremail);
                useremail.Clear();
                useremail.SendKeys(_wrongusermail);

                halt.Until(ExpectedConditions.TextToBePresentInElementValue(useremail, _wrongusermail));
                Driver.FocusElement(driver, userpassword);
                userpassword.Clear();
                userpassword.SendKeys(_wronguserpass);
                submitBttn.Click();

                Thread.Sleep(500);

                const string expectUrl = LoginURL;
                Assert.AreEqual(expectUrl, driver.Url, "Smthn changed");

                Assert.IsTrue(driver.FindElement(By.ClassName("validation-summary-errors")).Enabled, "Email-missing warning disabled");

                driver.Dispose();
            
        }

    
    }

    [TestFixture]
    public class IELoginTestRunner : LoginTest
    {
        
        
        [SetUp]
        public void DriverStart()
        {
            driver = Driver.GetDriver(BrowsersEnum.ie);
        }

        [TearDown]
        public void DriverStop()
        {
            driver.Dispose();

        }

        [Test]
        public void CorrectLogin()
        {
            CorrectLogin(driver);
        }

        [Test]
        public void EmptyDataLogin()
        {
            EmptyDataLogin(driver);
        }


        [Test]
        public void WrongLogin()
        {
            WrongLogin(driver);
        }

    }



    [TestFixture]
    public class ChromeLoginTestRunner : LoginTest
    {

        
        [SetUp]
        public void DriverStart()
        {
            driver = Driver.GetDriver(BrowsersEnum.chrome);
        }

        [TearDown]
        public void DriverStop()
        {
            driver.Dispose();

        }

        [Test]
        public void CorrectLogin()
        {
            CorrectLogin(driver);
        }


        [Test]
        public void EmptyDataLogin()
        {
            EmptyDataLogin(driver);
        }


        [Test]
        public void WrongLogin()
        {
            WrongLogin(driver);
        }

    }

}
