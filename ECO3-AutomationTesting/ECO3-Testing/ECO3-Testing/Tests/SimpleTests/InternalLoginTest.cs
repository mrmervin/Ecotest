using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.Pages;
using ECO3_Testing.Settings;
using ECO3_Testing.WindowsAuthentication;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace ECO3_Testing.Tests.SimpleTests
{
    [TestClass]
    public class InternalLoginTest
    {
        private static LoginPage loginPage;

        #region Logging  and IWebDriver instances
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(InternalLoginTest));
        private IWebDriver driver = ObjectRepository.Driver;
        #endregion

        #region
        private static string URL = ObjectRepository.Config.InternalQaaUrl;
        private static string pageTitle;
        #endregion

        [TestMethod,TestCategory("SimpleTest")]
        public void InternalUserAction()
        {
            NavigationHelper.NavigateToUrl(SignInThroughUrl.UsersUrl(LoginUsers.Standard));
            Logger.Info("Navigating to internal site");
            NavigationHelper.NavigateToUrl(URL);
            Logger.Info("Waiting for page to load properly");
            GenericHelper.WaitForPageToLoad();
            Logger.Info("Initiating Logging page instance");
            loginPage = new LoginPage(driver);
            Logger.Info("Retrieving logging page title");
            pageTitle = loginPage.GetPageTitle();
            Logger.Info("Ensuring on right logging page");
            Assert.AreEqual("ECO Internal", pageTitle);
        }
    }
}
