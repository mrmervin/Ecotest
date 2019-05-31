using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.Pages;
using ECO3_Testing.Settings;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.IO;
using TechTalk.SpecFlow;

namespace ECO3_Testing.StepDefinitions.UserStories.UserStoryOne
{
    [Binding]
    public class CommonStepsForUS1
    {
        private static LoginPage loginPage;
        private static InternalHomePage internalHomePage;
        private static MeasureProcessingErrorFilePage measureProcessingErrorFilePage;
        private static ClassInstances classSetter = new ClassInstances();

        #region Logging  and IWebDriver instances
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(CommonStepsForUS1));
        private IWebDriver driver = ObjectRepository.Driver;
        #endregion

        #region
        private static string pageTitle;
        private static string URL = ObjectRepository.Config.InternalQaaUrl;
        #endregion

        #region
        private static readonly string filePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
                .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\Test Plan Template.pdf");
        #endregion

        [When(@"I click measure processing link")]
        public void WhenIClickMeasureProcessingLink()
        {
            ClickMeasureProcessingLink();
        }

        [Then(@"file upload page should display")]
        public void ThenFileUploadPageShouldDisplay()
        {
             Logger.Info("Verifying file upload functionality is displayed");
             Assert.IsTrue(measureProcessingErrorFilePage.UploadBtnDisplayed()); 
        }

        [Given(@"I am Logged on successfully on internal website page")]
        public void GivenIAmLoggedOnSuccessfullyOnInternalWebsitePage()
        {
            if (loginPage == null)
            {
                VerifyOnInternalUrlPage();
            }
        }

        [Given(@"I am on file upload page")]   
        public void GivenIAmOnFileUploadPage()
        {
            if (!measureProcessingErrorFilePage.UploadBtnDisplayed())
            {
                ClickMeasureProcessingLink();
            }
            Logger.Info("Verifying upload feature is displayed");
            Assert.IsTrue(measureProcessingErrorFilePage.UploadBtnDisplayed());
        }

        [Then(@"I should see an error message: ""(.*)""")]
        public void ThenIShouldSeeAnErrorMessage(string p0)
        {
            string displayedError = measureProcessingErrorFilePage.DisplayedMessage()
                                    .Replace("\r", " ").Replace("\n", "");
            Logger.Info(@"Verifying displayed error message is : "+p0);
            Assert.AreEqual(displayedError, p0);
        }

        [When(@"I upload a file that is not a csv file")]
        public void WhenIUploadAFileThatIsNotACsvFile()
        {
            Logger.Info("Uploading file : "+filePath);
            measureProcessingErrorFilePage.UploadFile(filePath);
        }


        [Then(@"I should see a message : ""(.*)""")]
        public void ThenIShouldSeeAWarningMessage(string p0)
        {
            Logger.Info(@"Verifying displayed error message is : "+p0);
            Assert.AreEqual(measureProcessingErrorFilePage.WarningMessageForDuplicateUpload(), p0);
        }

        [Then(@"Reject button will be displayed")]
        public void ThenRejectButtonWillBeDisplayed()
        {
            Assert.IsTrue(measureProcessingErrorFilePage.CheckRejectBtn());
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
        private void ClickMeasureProcessingLink()
        {
            if(classSetter.InternalHomePageInstance == null)
            {
                classSetter.InternalHomePageInstance = new InternalHomePage(driver);
                internalHomePage = classSetter.InternalHomePageInstance;
            }
            Logger.Info("Clicking Measure Processing Error file link");           
            classSetter.MeasureProcessingErrorFilePageInstance = internalHomePage.ClickUploadMeasureLink();
            measureProcessingErrorFilePage = classSetter.MeasureProcessingErrorFilePageInstance;
        }
    }
}
