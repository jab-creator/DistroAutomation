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
            HomePage homePage = new HomePage(Driver); //initialising home page instance

            homePage.DataSpanOptions.Click(); //selecting the dropdown
            homePage.SelectDataSpan("Year 2005"); //selecting the year

            homePage.GoButton.Click(); //clicking the go button

            IList<IWebElement> tableRows = homePage.TableRows(); //getting the returned table rows

            foreach (var distro in Configs.topFive) //iterating though each of the configs that need to be checked
            {
                Int32.TryParse(distro.Key, out int rowOfRank); //parsing the rank from a string to a int
                string rowText = tableRows[rowOfRank-1].Text; //getting the row for the rank we are comparing to. -1 as lists have 0 based index
                Debug.Print(rowText);
                string[] rowTextArray = rowText.Split(" "); //splitting the row
                string actualRank = rowTextArray[0]; //actual rank on site
                Debug.Print(rowTextArray[0]);
                Assert.AreEqual(distro.Key, actualRank); //asserting that they are equal
            }
        }
        [Test]
        public void CheckIndicatorArrows()
        {
            HomePage homePage = new HomePage(Driver); // initialising home page instance

            homePage.DataSpanOptions.Click(); //selecting the dropdown
            homePage.SelectDataSpan("Last 6 months"); //selecting the year
            homePage.GoButton.Click(); //clicking the go button
            IList<IWebElement> tableRows = homePage.TableRows(); //getting the returned table rows

            foreach (var row in tableRows) //iterating through each row
            {  
                IWebElement positionChange = homePage.PositionChange(row); //getting the cell with the position change info
                string title = positionChange.GetAttribute("title"); //getting yesterdays info
                string shortTitle = title.Substring(title.IndexOf(" ")+1); // only need the number

                Int32.TryParse(shortTitle, out int previousHPD); //previous HDP, parsing to int
                Int32.TryParse(positionChange.Text, out int currentHPD); //current HDP, parsing to int

                string movementImageLink = homePage.MovementImageLink(positionChange);

                if (currentHPD > previousHPD & movementImageLink == Configs.ImgLink.GreenImgLink)
                {
                    Assert.Pass($"Correct green arrow displayed: {currentHPD} > {previousHPD} - {row.Text}");
                }
                else if (currentHPD < previousHPD & movementImageLink == Configs.ImgLink.RedImgLink)
                {
                    Assert.Pass($"Correct red arrow displayed: {currentHPD} < {previousHPD} - {row.Text}");
                }
                else if(previousHPD == currentHPD & movementImageLink == Configs.ImgLink.levelImgLink)
                {
                    Assert.Pass($"Correct red arrow displayed: {currentHPD} = {previousHPD} - {row.Text}");
                }
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