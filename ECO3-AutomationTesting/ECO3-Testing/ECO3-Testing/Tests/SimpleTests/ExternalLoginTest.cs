using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.DataSource;
using ECO3_Testing.Pages;
using ECO3_Testing.Settings;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace ECO3_Testing.Tests.SimpleTests
{
    [TestClass]
    public class ExternalLoginTest
    {
        #region Class instances
        private static LoginPage loginPage;
        private static ExternalHomepage externalHomepage;
        #endregion

        #region Field properties
        private readonly string URL = ObjectRepository.Config.ExternalQaaUrl;
        private readonly string user = ObjectRepository.Config.ExternalUser;
        private readonly string password = ObjectRepository.Config.GetExternalPassword;
        private readonly string EcoRegister = "ECO Register";
        private readonly string WelcomeMessage = "Welcome to the Ofgem Energy Company Obligation (ECO) Register";
        private static string pageTitle;
        #endregion

        #region Logging  and IWebDriver instances
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(ExternalLoginTest));
        private IWebDriver driver = ObjectRepository.Driver;
        #endregion

        [TestMethod, TestCategory("SimpleTest")]
        public void ExternalUserAction()
        {
            Logger.Info("Navigating to " + URL);
            NavigationHelper.NavigateToUrl(URL);
            Logger.Info("Ensuring page loads properly");
            GenericHelper.WaitForPageToLoad();

            loginPage = new LoginPage(driver);
            pageTitle = loginPage.GetPageTitle();
            Logger.Info("Page title is " + pageTitle);
            Assert.AreEqual(EcoRegister, pageTitle);
            Logger.Info("Logging in external user");

            loginPage.EnterUserDetails(user, password)
                .LoginExternalUser();
            externalHomepage = new ExternalHomepage(driver);
            Logger.Info("Verifying welcome message is seen");
            Assert.AreEqual(WelcomeMessage, externalHomepage.VerifyWelcomeMessage());

            externalHomepage.ExternalUserLogout();
            Logger.Info("Ensuring user logs out successfully");
            Assert.IsTrue(loginPage.UserAndPasswordFieldDisplayed());

        }


}
}
