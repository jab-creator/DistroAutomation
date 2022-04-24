using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace DistroAutomation.Pages
{
    public class HomePage
    {
        public HomePage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "dataspan")]
        public IWebElement DataSpanOptions { get; set; }

        public void SelectDataSpan(string value)
        {
            IList<IWebElement> options = DataSpanOptions.FindElements(By.TagName("option"));

            foreach (var option in options)
            {
                if (option.Text == value)
                {
                    option.Click();
                }
            }
        }

        [FindsBy(How = How.CssSelector, Using = "body>table.Logo>tbody>tr>td:nth-child(3)>table:nth-child(3)>tbody>tr:nth-child(2)>td>form>input[type=submit]")]
        public IWebElement GoButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "body>table.Logo>tbody>tr>td:nth-child(3)>table:nth-child(3)")]
        public IWebElement DistributionTable { get; set; }


    }
}
