using ECO3_Testing.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ECO3_Testing.ComponentHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECO3_Testing.Pages
{
    public class StartMeasureProcessingPage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.CssSelector, Using = "[value='Start Now >']")]
        private IWebElement StartBttn;

        [FindsBy(How = How.CssSelector, Using = "[class='page-header-content']")]
        private IList<IWebElement> titleScreen;

        #endregion

        private IWebDriver driver;
        public StartMeasureProcessingPage(IWebDriver webDriver) : base(webDriver)
        {
            driver = webDriver;
        }
        public void VerifyThatStartButtonIsPresent(string value)
        {
            GenericHelper.WaitForElementToBeDisplayed(StartBttn);
            Assert.AreEqual(StartBttn.GetAttribute("value"),value);
        }
        public void VerifyStartProcessingPage(string page)
        {
            GenericHelper.WaitForPageToLoad();
            Assert.AreEqual(driver.Title, page);
        }

        public void TitleOfScreens(string title)
        {
            Assert.AreEqual(titleScreen.ElementAt(1).Text, title);
        }
    }
}
