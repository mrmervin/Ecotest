using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.CustomException;
using ECO3_Testing.DataSource;
using ECO3_Testing.Pages;
using ECO3_Testing.Settings;
using ECO3_Testing.WindowsAuthentication;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace ECO3_Testing.StepDefinitions.UserStories.UserPermission
{
    [Binding]
    public class UserPermissionSteps
    {
        private static LoginPage loginPage;
        private static InternalHomePage internalHomePage;
        private static StartMeasureProcessingPage startMeasureProcessingPage;

        #region Logging  and IWebDriver instances
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(UserPermissionSteps));
        public ClassInstances classSetter = new ClassInstances();
        protected IWebDriver driver = ObjectRepository.Driver;
        #endregion

        #region
        private static string pageTitle;
        private static string URL = ObjectRepository.Config.InternalQaaUrl;
        #endregion

        [Scope(Feature = "UsersPrivilege")]
        [BeforeFeature("UsersPrivilege")]
        public static void OpenBrowser()
        {
            BrowserHelper.RunBrowser();
            Logger.Info("instantiating the browser......");
        }

        [Given(@"I am Logged on successfully on internal website page as a '(.*)'")]
        public void GivenIAmLoggedOnSuccessfullyOnInternalWebsitePageAsA(string user)
        {
            if (loginPage == null)
            {
                classSetter.LoginPageInstance = new LoginPage(driver);
                loginPage = classSetter.LoginPageInstance;

            }
            Login(user);
            VerifyOnInternalUrlPage();
        }


        [When(@"I navigate to the home page")]
        public void WhenINavigateToTheHomePage()
        {
            VerifyOnInternalUrlPage();
        }

        [Then(@"I should not be able to see ""(.*)""")]
        public void ThenIShouldNotBeAbleToSee(string subHeader)
        {
            if (internalHomePage == null)
            {
                classSetter.InternalHomePageInstance = new InternalHomePage(driver);
                internalHomePage = classSetter.InternalHomePageInstance;

            }
            internalHomePage.CheckIfSubLinkIsNotPresent(subHeader);
        }

        [When(@"I navigate to the '(.*)'")]
        public void WhenINavigateToThe(string subHeader)
        {
            if (internalHomePage == null)
            {
                classSetter.InternalHomePageInstance = new InternalHomePage(driver);
                internalHomePage = classSetter.InternalHomePageInstance;

            }
            internalHomePage.ClickOnSubHeader(subHeader);

        }


        [Then(@"I should be able to see '(.*)' link")]
        public void ThenIShouldBeAbleToSeeLink(string subLink)
        {
            internalHomePage.CheckIfSubLinkIsPresent(subLink);
        }


        [Then(@"I should not be able to see the '(.*)'")]
        public void ThenIShouldNotBeAbleToSeeThe(string subLink)
        {
            internalHomePage.CheckIfSubLinkIsNotPresent(subLink);
        }

        [When(@"I hit '(.*)' deep link")]
        public void WhenIHitDeepLink(string deepLink)
        {
            NavigationHelper.NavigateViaDeepLink(URL, deepLink);
        }

        [When(@"I log on successfully to internal website page as a '(.*)'")]
        public void WhenILogOnSuccessfullyToInternalWebsitePageAsA(string user)
        {
            if (loginPage == null)
            {
                classSetter.LoginPageInstance = new LoginPage(driver);
                loginPage = classSetter.LoginPageInstance;

            }
            Login(user);
            VerifyOnInternalUrlPage();
        }

        [Then(@"I should be able to navigate to the '(.*)'")]
        public void ThenIShouldBeAbleToNavigateToThe(string page)
        {
            
                if (startMeasureProcessingPage == null)
                {
                    classSetter.StartMeasureProcessingPageInstance = new StartMeasureProcessingPage(driver);
                    startMeasureProcessingPage = classSetter.StartMeasureProcessingPageInstance;

                }
            startMeasureProcessingPage.VerifyStartProcessingPage(page);    

        }

        [Then(@"the screen title should be '(.*)'")]
        public void ThenTheScreenTitleShouldBe(string screenTitile)
        {
            startMeasureProcessingPage.TitleOfScreens(screenTitile);
        }


        [When(@"I click on the '(.*)'")]
        public void WhenIClickOnThe(string subLink)
        {
            internalHomePage.ClickOnTheSubLink(subLink); 
        }

        [Then(@"I should be able to clear the cache")]
        public void ThenIShouldBeAbleToClearTheCache()
        {
            NavigationHelper.ClearCache();
            Logger.Info("Browser cache is cleared");
        }
        private void VerifyOnInternalUrlPage()
        {
            Logger.Info("Initiating Logging page instance");
            classSetter.LoginPageInstance = new LoginPage(driver);
            loginPage = classSetter.LoginPageInstance;
            Logger.Info("Retrieving logging page title");
            pageTitle = loginPage.GetPageTitle();
            Logger.Info("Ensuring on right logging page");
            Assert.AreEqual("ECO Internal", pageTitle);
        }

        public static void Login(string user)
        {
            string userRoleURL = null;

            switch (user)
            {
                case "Advanced":
                    userRoleURL = SignInThroughUrl.UsersUrl(LoginUsers.Advanced);
                    break;
                case "Basic":
                    userRoleURL = SignInThroughUrl.UsersUrl(LoginUsers.Basic);
                    break;
                case "InternalAdmin":
                    userRoleURL = SignInThroughUrl.UsersUrl(LoginUsers.InternalAdmin);
                    break;
                case "Standard":
                    userRoleURL = SignInThroughUrl.UsersUrl(LoginUsers.Standard);
                    break;
                default:
                    throw new ECO3TestException(message: "invalid user");
            }
            Logger.Info("Navigating to internal site");
            NavigationHelper.NavigateToUrl(userRoleURL);
        }
    }
}
