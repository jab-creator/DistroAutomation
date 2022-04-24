using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using System.Diagnostics;

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

        public IList<IWebElement> TableRows()
        {
            IList<IWebElement> allTableRows = DistributionTable.FindElements(By.TagName("tr"));
            IList<IWebElement> tableRows = new List<IWebElement>();
            for (int i = 3; i < allTableRows.Count; i++)
            {
                tableRows.Add(allTableRows[i]);
            }
            return tableRows;
        }

        public IWebElement PositionChange(IWebElement row)
        {
            IList<IWebElement> tdTags = row.FindElements(By.TagName("td"));
            Debug.Print(tdTags[0].Text);
            IWebElement positionChange = tdTags[1];
            return positionChange;
        }

        public string MovementImageLink(IWebElement positionChange)
        {
            IWebElement movementImage = positionChange.FindElement(By.TagName("img"));
            string movementImageLink = movementImage.GetDomAttribute("src");
            return movementImageLink;
        }
    }
}
