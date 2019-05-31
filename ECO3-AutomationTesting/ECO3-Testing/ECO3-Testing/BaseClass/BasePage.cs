using ECO3_Testing.ComponentHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ECO3_Testing.BaseClass
{
    public class BasePage
    {
        [FindsBy(How = How.Id, Using = "confirmLogout")]
        private IWebElement logOut;

        private IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        protected void Logout()
        {
            GenericHelper.WaitForElementToBeClickable(logOut);
            logOut.Click();
        }
    }
}
