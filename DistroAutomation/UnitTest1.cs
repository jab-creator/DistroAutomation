using DistroAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DistroAutomation
{
    public class Tests
    {
        public IWebDriver Driver { get; set; }
        [SetUp]
        public void Setup()
        {
            Driver = Helpers.InitializeDriver();
            Helpers.Launch(Driver, "https://distrowatch.com");
        }

        [Test]
        public void Test1()
        {
            var topFive = new Dictionary<string, string>()
            {
                {"1", "Ubuntu" },
                {"2", "Mandriva" },
                {"3", "SUSE"},
                {"4", "Fedora" },
                {"5", "MEPIS" }
            };
            
            HomePage homePage = new HomePage(Driver);

            homePage.DataSpanOptions.Click();
            homePage.SelectDataSpan("Year 2005");

            //Helpers.ChangeDataSpan(Driver, "2005");
            //IList<IWebElement> options = homePage.DataSpanOptions.FindElements(By.TagName("option"));
            //string[] topFive = { "Ubuntu", "Mandriva", "SUSE", "Fedora", "MEPIS" };
            //foreach (var option in options)
            //{
            //    if (option.GetDomProperty("value") == "2005")
            //    {
            //        option.Click();
            //    }
            //}

            //IWebElement goButton = Driver.FindElement(By.XPath("/html/body/table[2]/tbody/tr/td[3]/table[2]/tbody/tr[2]/td/form/input"));
            //IWebElement goButton = Driver.FindElement(By.CssSelector("body>table.Logo>tbody>tr>td:nth-child(3)>table:nth-child(3)>tbody>tr:nth-child(2)>td>form>input[type=submit]"));
            homePage.GoButton.Click();

            //IWebElement distributionTable = Driver.FindElement(By.CssSelector("body>table.Logo>tbody>tr>td:nth-child(3)>table:nth-child(3)"));
            IList<IWebElement> tableRows = homePage.TableRows();
            
            foreach (var distro in topFive )
            {
                int rowOfRank;
                Int32.TryParse(distro.Key, out rowOfRank);
                string rowText = tableRows[rowOfRank + 2].Text;
                Debug.Print(rowText);
                string[] rowTextArray = rowText.Split(" ");
                Debug.Print(rowTextArray[0]);
                Assert.AreEqual(rowTextArray[0], distro.Key);
            }
        }
        [Test]
        public void CheckIndicatorArrows()
        {
            HomePage homePage = new HomePage(Driver);

            homePage.DataSpanOptions.Click();
            homePage.SelectDataSpan("Last 6 months");
            homePage.GoButton.Click();
            IList<IWebElement> tableRows = homePage.TableRows();

            foreach (var row in tableRows)
            {
                Debug.Print(row.Text);
                
                IWebElement positionChange = homePage.PositionChange(row);
                string title = positionChange.GetAttribute("title");
                string shortTitle = title.Substring(title.IndexOf(" ")+1);

                Int32.TryParse(shortTitle, out int previousHPD);
                Int32.TryParse(positionChange.Text, out int currentHPD);
                
                string movementImageLink = homePage.MovementImageLink(positionChange);
                Debug.Print(movementImageLink);
                if (currentHPD > previousHPD)
                {
                    Debug.Print("current is bigger");
                }
                else if (currentHPD < previousHPD)
                {
                    Debug.Print("Current is smaller");
                }
                else if(previousHPD == currentHPD)
                {
                    Debug.Print("HPD is equal");
                }

                Debug.Print(positionChange.Text);
            }
            Debug.Print(tableRows[0].Text);

        }
        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}