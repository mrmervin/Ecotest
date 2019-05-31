using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ECO3_Testing.Pages
{
    public class ExternalHomepage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = "//span[text()='Logout']")]
        private IWebElement logoutConfirmation;

        [FindsBy(How = How.CssSelector, Using = ".page-region-content h2")]
        private IWebElement welcomeMessage;

        #endregion

        private IWebDriver webDriver;

        public ExternalHomepage(IWebDriver driver) : base(driver)
        {
            this.webDriver = driver;
        }

        #region WebElements utilisation
        public void ExternalUserLogout()
        {
            Logout();
            GenericHelper.WaitForElementToBeClickable(logoutConfirmation)
                .Click();
        }

        public string VerifyWelcomeMessage()
        {
            GenericHelper.WaitForElementToBeDisplayed(welcomeMessage);
            return welcomeMessage.Text;
        }
        #endregion
    }
}
