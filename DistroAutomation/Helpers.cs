using DistroAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistroAutomation
{
    internal class Helpers
    {
        public static IWebDriver InitializeDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager; //Only waits for the initial HTMl doc to load

            IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //prevents failure when waiting for elements to render
            return driver;
        }
        public static void Launch(IWebDriver driver, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Position = new System.Drawing.Point(-2000, 0); //repositions the browser to the screen on the left, change to 2000 to move it right
                driver.Manage().Window.Maximize();
            }
            catch (Exception)
            {
                Assert.Fail($"Unable to launch browser/url: {url}");
            }
        }
    }
}
