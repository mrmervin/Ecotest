using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ECO3_Testing.Pages
{
    public class LoginPage : BasePage
    {
        #region WebElements
        [CacheLookup]
        [FindsBy(How = How.CssSelector, Using = "input[type=submit]")]
        private IWebElement loginButton;

        [FindsBy(How = How.Id, Using = "UserName")]
        private IWebElement usernameField;

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = ".page-title")]
        private IWebElement pageTitle;

        #endregion

        private IWebDriver driver;
        public LoginPage(IWebDriver webDriver) : base(webDriver)
        {
            driver = webDriver;
        }
        public LoginPage EnterUserDetails(string user, string password)
        {
            GenericHelper.WaitForElementToBeClickable(usernameField)
                .SendKeys(user);
            GenericHelper.WaitForElementToBeClickable(passwordField)
                .SendKeys(password);
            return new LoginPage(driver);
        }
        public void LoginExternalUser()
        {
            GenericHelper.WaitForElementToBeClickable(loginButton)
                .Click();
        }

        public string GetPageTitle()
        {
            GenericHelper.WaitForElementToBeDisplayed(pageTitle);
            return pageTitle.Text;
        }
        public bool UserAndPasswordFieldDisplayed()
        {
            bool user = GenericHelper.WaitForElementToBeClickable(usernameField).Displayed;
            bool pass = GenericHelper.WaitForElementToBeClickable(usernameField).Displayed;
            if (user && pass)
                return true;
            else
                return false;
        }
    }
}
