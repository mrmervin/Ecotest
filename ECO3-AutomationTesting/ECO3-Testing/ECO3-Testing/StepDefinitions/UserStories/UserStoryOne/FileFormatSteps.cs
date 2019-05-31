using AventStack.ExtentReports.Gherkin.Model;
using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.CustomException;
using ECO3_Testing.DataSource;
using ECO3_Testing.Settings;
using ECO3_Testing.StepDefinitions.UserStories.UserStoryTwo;
using ECO3_Testing.WindowsAuthentication;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace ECO3_Testing.StepDefinitions.UserStories.UserStoryOne
{
    [Binding]
    public class FileFormatSteps
    {
        #region Logging  and IWebDriver instances
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(FileFormatSteps));
        private ClassInstances classSetter = new ClassInstances();
        protected IWebDriver driver = ObjectRepository.Driver;
        #endregion

        #region Filepaths
        private static readonly string pdfFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
                .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\NonCSVFiles\Adobe.pdf");

        private static readonly string excelFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
               .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\NonCSVFiles\Excel.xlsx");

        private static readonly string wordFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
               .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\NonCSVFiles\Word.docx");

        private static readonly string blankRowFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
               .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\EmptyFile\MP_191027_OCTOBER2019_Measure_Processing_Errors.csv");

        private static readonly string wrongNamingFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
               .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\WrongNaming\MP_180322_March2018_Measure_Processing_Error.csv");

        private static readonly string wrongColumnFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
               .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\MisspeltColumn\MP_180817_AUGUST2018_Measure_Processing_Errors.csv");

        private static readonly string headerNoRowFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
               .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\EmptyFile\MP_190430_April2018_Measure_Processing_Errors.csv");

        #endregion

        #region messages
        private const string firstMessage = "You are uploading the following file: MP_190430_April2018_Measure_Processing_Errors.csv";
        private const string secondMessage = "Your file has been checked. It contains 0 records of which 0 failed validation and 0 passed.";
        #endregion

        //[Scope(Feature = "FileUpload")]
        //[Scope(Feature = "DuplicateFileUpload")]
        //[Scope(Feature = "US2ColumnsError")]
        //[Scope(Feature = "US3andUS4ErrorSummaryValidation")]
        [BeforeFeature("US1", "US1-1","US2","US3-US4")]

        public static void NavigateToInternalUrlAndLogin()
        {
            BrowserHelper.RunBrowser();
            Logger.Info("Navigating to internal site");
            string userLoginLink = SignInThroughUrl.UsersUrl(LoginUsers.Advanced);
            NavigationHelper.NavigateToUrl(userLoginLink);
            Logger.Info("Waiting for page to load properly");
            GenericHelper.WaitForPageToLoad();
        }
        
        #region Scenarios implementation

        [When(@"I upload a pdf file")]
        public void WhenIUploadAPdfFile()
        {
            UploadMethodUS1("Uploading pdf file",pdfFile);
        }

        [When(@"I upload an excel file")]
        public void WhenIUploadAnExcelFile()
        {
            UploadMethodUS1("Uploading excel file",excelFile);
        }

        [When(@"I upload a word file")]
        public void WhenIUploadAWordFile()
        {
            UploadMethodUS1("Uploading word file",wordFile);
        }

        [When(@"I upload a blank valid file")]
        public void WhenIUploadABlankValidFile()
        {
            UploadMethodUS1("Uploading blank csv file", blankRowFile);
        }

        [When(@"I upload a file that is with wrong naming convention")]
        public void WhenIUploadAFileThatIsWithWrongNamingConvention()
        {
            UploadMethodUS1("Uploading csv file with blank",wrongNamingFile);
        }

        [When(@"I upload a file with headers but no row")]
        public void WhenIUploadAFileWithHeadersButNoRow()
        {
            UploadMethodUS1("Uploading a file with header but no rows", headerNoRowFile);
            WaitForUploadBtnToDisapper();
        }
        private void UploadMethodUS1(string message,string fileToUpload)
        {
            Logger.Info(message);
            classSetter.MeasureProcessingErrorFilePageInstance
                .UploadFile(fileToUpload);
        }

        [Then(@"validation of file done is with no data")]
        public void ThenValidationOfFileDoneWithNoData()
        {
            Logger.Info("Verifying upload was with no data after file upload");
            Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], firstMessage);
            Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], secondMessage);
        }

        [Then(@"Continue button is not displayed")]
        public void ThenContinueButtonIsNotDisplayed()
        {
            Assert.IsFalse(classSetter
                           .MeasureProcessingErrorFilePageInstance
                           .CheckContinueBtn());
        }

        [Then(@"File will not be processed: ""(.*)""")]
        public void ThenFileWillNotBeProcessed(string p0)
        {
            Logger.Info("Verifying file " + p0 + " is not in Documents Database");
            string nameOfFile = WhichFile(p0);
            int numOfRows = DBConnectHelper.RowReturnedQuery("Select * from dbo.documents where filename like '%" + nameOfFile + "%'; ", DatabaseName.Documents);
            Assert.AreEqual(0, numOfRows);
        }

        [Then(@"File will be processed with record created in Database: ""(.*)""")]
        public void ThenFileWillBeProcessedWithRecordCreatedInDatabase(string p0)
        {
            Logger.Info("Verifying database for file deatils");
            string fileName = WhichFile(p0);
            int numOfRows = DBConnectHelper.RowReturnedQuery("Select * from dbo.documents where filename = '" + fileName + "'; ", DatabaseName.Documents);
            Assert.AreEqual(1, numOfRows);
            List<DatabaseResult> output = DBConnectHelper.GetQueryResult(
                "SELECT Version,DocId,Created_at FROM dbo.Documents where filename = '" + fileName + "' order by Created_at desc", DatabaseName.Documents);
            string todaysDate = DateTime.Now.ToString("M/d/yyyy");
            string dateRetrievedFromDB = output[0].thirdColumn.Split(' ')[0];
            Assert.AreEqual(todaysDate, dateRetrievedFromDB);
            bool deleteFile = DBConnectHelper.DeleteRow("dbo.documents", "fileName", fileName, DatabaseName.Documents);
            Assert.IsTrue(deleteFile);
        }

        private void WaitForUploadBtnToDisapper()
        {
            GenericHelper.WaitForElementToDisappear
               (classSetter.MeasureProcessingErrorFilePageInstance.UploadButton());
        }

        public string WhichFile(string fileDetails)
        {
            string fileName = null;
            switch (fileDetails.ToUpper())
            {
                case "PDF":
                    fileName = Path.GetFileName(pdfFile);
                    break;
                case "EXCEL":
                    fileName = Path.GetFileName(excelFile);
                    break;
                case "WORD":
                    fileName = Path.GetFileName(wordFile);
                    break;
                case "BLANK":
                    fileName = Path.GetFileName(blankRowFile);
                    break;
                case "WRONG NAMING":
                    fileName = Path.GetFileName(wrongNamingFile);
                    break;
                case "MISSPELT COLUMN":
                    fileName = Path.GetFileName(US2FileValidation.GetMisspeltFile());
                    break;
                case "MISSING COLUMN":
                    fileName = Path.GetFileName(US2FileValidation.GetMissingColumnFile());
                    break;
                case "BLANK ROW":
                    fileName = Path.GetFileName(headerNoRowFile);
                    break;
                case "INVALID SUPPLIER":
                    fileName = Path.GetFileName(US2FileValidation.GetInvalidSupplierFile());
                    break;
                case "INVALID MRN":
                    fileName = Path.GetFileName(US2ErrorDownload.GetInvalidMRNFile());
                    break;
                case "MRN NOT LINKED":
                    fileName = Path.GetFileName(US2ErrorDownload.GetMRNNotLinkedFile());
                    break;
                case "INVALID FIELD":
                    fileName = Path.GetFileName(US2ErrorDownload.GetInvalidFieldFile());
                    break;
                case "ALL ERRORS":
                    fileName = Path.GetFileName(US2ErrorDownload.GetAllErrorFile());
                    break;
                default:
                    throw new ECO3TestException(fileDetails +" case method is not implemented yet");
            }
            return fileName;
        }
        #endregion
    }
}
