using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.Settings;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace ECO3_Testing.Pages
{
    public class InternalHomePage : BasePage
    {
        #region WebElements
        [CacheLookup]
        [FindsBy(How = How.CssSelector, Using = ".page-title")]
        private IWebElement homePage;

        [CacheLookup]
        [FindsBy(How = How.LinkText, Using = "Measure Processing")]
        private IWebElement measureProcessingTab;

        [FindsBy(How = How.PartialLinkText, Using = "Upload Measure Processing Error File")]
        private IWebElement measureProcessingLink;

        [FindsBy(How = How.LinkText, Using = "subHeader")]
        private IWebElement subHeader;
        #endregion

        #region Link
        private static string UploadLink = ObjectRepository.Config.InternalQaaUrl+"/MeasureProcessingError/Upload";
        #endregion

        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(InternalHomePage));

        private IWebDriver webDriver;
        public InternalHomePage(IWebDriver driver) : base(driver)
        {
            webDriver = driver;
        }
        public void GoToHomePage()
        {
            homePage.Click();   
        }
        public MeasureProcessingErrorFilePage ClickUploadMeasureLink()
        {
            try
            {
                measureProcessingTab.Click();
                GenericHelper.MoveToElementAndClick(measureProcessingLink);
            }
            catch(Exception e)
            {
                NavigationHelper.NavigateToUrl(UploadLink); 
            }
            return new MeasureProcessingErrorFilePage(webDriver);
        }
        public void ClickOnSubHeader(string subHeader)
        {
            GenericHelper.WaitForLinkTextElementToBeClickable(subHeader).Click();
            System.Threading.Thread.Sleep(10000);
            Logger.Info("I am able to click "+ subHeader +" sub header");
        }
        public bool CheckIfSubLinkIsPresent(string subLink)
        {
            IWebElement subLinklement = ObjectRepository.Driver.FindElement(By.PartialLinkText(subLink));
            Logger.Info("HyperLink to the "+ subLink + " : - "+  subLinklement.GetAttribute("href"));
            bool subLinkText = GenericHelper.IsElementPresent(subLinklement);
            return subLinkText;
        }
        public bool CheckIfSubLinkIsNotPresent2(string subLink)
        {
            IWebElement subLinklement = ObjectRepository.Driver.FindElement(By.PartialLinkText(subLink));
            bool subLinkText = GenericHelper.ElementNotPresent(subLinklement);
            return true;
        }
        public void ClickOnTheSubLink(string subLink)
        {
            IWebElement subLinklement = ObjectRepository.Driver.FindElement(By.PartialLinkText(subLink));
            GenericHelper.WaitForElementToBeClickable(subLinklement);
            subLinklement.Click();

        }
        public bool CheckIfSubLinkIsNotPresent(string subLink)
        {
            try
            {
                ObjectRepository.Driver.FindElement(By.PartialLinkText(subLink));
                return true;
            }
            catch (NoSuchElementException e)

            {
                e.Message.ToString();
                e.GetBaseException();
                Logger.Info(e+ " ...............................................................");
                return false;
            }
        }
    }
}
