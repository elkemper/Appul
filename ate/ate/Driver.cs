using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace ate
{
    public enum BrowsersEnum
    {
        chrome,
        ie
    }


    class Driver
    {
        private static readonly string _downloadFileDir;


       
         static Driver()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            _downloadFileDir = String.Format(@"C:\Users\{0}\Downloads", userName);
        }
        

        public static string DownloadFileDir
        {
            get { return _downloadFileDir; }
        }

        public static IWebDriver GetDriver(BrowsersEnum browser)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case BrowsersEnum.chrome:
                    {
                        driver = new ChromeDriver();
                        break;
                    }
                case BrowsersEnum.ie:
                    {
                        driver = new InternetExplorerDriver();
                            break;
                    }
                default:
                    {
                        driver = new ChromeDriver();
                        break;
                    }
            }

            driver.Manage().Window.Maximize();
            return driver;
        }

    }
}
