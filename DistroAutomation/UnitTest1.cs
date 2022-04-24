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
        public void ChechTopFive()
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

            homePage.GoButton.Click();

            IList<IWebElement> tableRows = homePage.TableRows();
            
            foreach (var distro in topFive )
            {
                Int32.TryParse(distro.Key, out int rowOfRank);
                string rowText = tableRows[rowOfRank-1].Text;
                Debug.Print(rowText);
                string[] rowTextArray = rowText.Split(" ");
                string actualRank = rowTextArray[0];
                Debug.Print(rowTextArray[0]);
                Assert.AreEqual(distro.Key, actualRank);
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

                string GreenImgLink = "images/other/aup.png";
                string RedImgLink = "images/other/adown.png";
                string levelImgLink = "images/other/alevel.png";

                string movementImageLink = homePage.MovementImageLink(positionChange);
                Debug.Print(movementImageLink);
                if (currentHPD > previousHPD & movementImageLink == GreenImgLink)
                {
                    Debug.Print("Current is bigger");
                    Assert.Pass($"Correct green arrow displayed: {currentHPD} > {previousHPD} - {row.Text}");
                }
                else if (currentHPD < previousHPD & movementImageLink == RedImgLink)
                {
                    Debug.Print("Current is smaller");
                    Assert.Pass($"Correct red arrow displayed: {currentHPD} < {previousHPD} - {row.Text}");
                }
                else if(previousHPD == currentHPD & movementImageLink == levelImgLink)
                {
                    Assert.Pass($"Correct red arrow displayed: {currentHPD} = {previousHPD} - {row.Text}");
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