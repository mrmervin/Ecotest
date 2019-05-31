using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.DataSource;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace ECO3_Testing.StepDefinitions.UserStories.UserStoryOne
{
    [Binding]
    public class DuplicateFileUploadSteps 
    {
        #region expected messages
        private readonly string firstMessages = "You are uploading the following file: MP_180704_JULY2018_Measure_Processing_Errors.csv";
        private readonly string secondMessages = "Your file has been checked. It contains 3 records of which 0 failed validation and 3 passed.";
        private readonly string thirdMessages = @"Press ""Continue"" to review valid records.";
        #endregion

        #region Instance, Logger and file
        private ClassInstances classSetter = new ClassInstances();
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(DuplicateFileUploadSteps));

        private static readonly string validFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
                .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\ValidFiles\MP_180704_JULY2018_Measure_Processing_Errors.csv");
        #endregion

        [When(@"I upload a correct measure file")]
        public void WhenIUploadACorrectMeasureFile()
        {
            Logger.Info("Uploading valid file : "+Path.GetFileName(validFile));
            classSetter.MeasureProcessingErrorFilePageInstance
                .UploadFile(validFile);
        }

        [Then(@"upload should be successful displaying such messages with option to continue")]
        public void ThenUploadShouldBeSuccessfulDisplayingSuchMessagesWithOptionToContinue()
        {
            classSetter.MeasureProcessingErrorFilePageInstance.WaitForContinueBtnToDisplay();
            Logger.Info("Verifying upload was successful after file upload");
            Assert.IsTrue(classSetter.MeasureProcessingErrorFilePageInstance.ContinueBtnDisplayed());
            Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0],firstMessages);
            Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1],secondMessages);
            Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2],thirdMessages);
        }

        [Given(@"I upload a file that has been uploaded successfully before")]
        public void GivenIUploadAFileThatHasBeenUploadedSuccessfullyBefore()
        {
            Logger.Info("Uploading valid file again : " + Path.GetFileName(validFile));
            WhenIUploadACorrectMeasureFile();
        }

        [Then(@"I choose the discontinue upload")]
        public void WhenIChooseTheDiscontinueOption()
        {
            Logger.Info("Refreshing upload to discontinue upload");
            classSetter.MeasureProcessingErrorFilePageInstance
                       .DiscontinueByRefreshingPage();
        }

        [Then(@"upload will not be done and file record will not be created again")]
        public void ThenUploadWillNotBeDoneAndFileRecordWillNotBeCreatedAgain()
        {
            string fileName = Path.GetFileName(validFile);
            int numOfRows = DBConnectHelper.RowReturnedQuery("Select * from dbo.documents where filename = '" + fileName + "'; ", DatabaseName.Documents);
            Assert.AreEqual(1, numOfRows);
            List<DatabaseResult> output = DBConnectHelper.GetQueryResult(
               "SELECT Version FROM dbo.Documents where filename = '" + fileName + "' order by Created_at desc", DatabaseName.Documents);
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual(1, Convert.ToInt16(output[0].firstColumn));
        }

        [When(@"I choose the continue option")]
        public void WhenIChooseTheContinueOption()
        {
            Logger.Info("Continuing duplicate upload");
            classSetter.MeasureProcessingErrorFilePageInstance
                  .ContinueDuplicate();
        }

        [When(@"I click continue")]
        public void WhenIClickContinue()
        {
            Logger.Info("Clicking continuing button to upload file");
            classSetter.MeasureProcessingErrorFilePageInstance
                .ClickContinue();
        }

        [Then(@"File record will contain filename, uploaded username and creation timestamp: ""(.*)""")]
        public void ThenFileRecordWillContainFilenameUploadedUsernameAndCreationTimestamp(string p0)
        {
            string fileName = Path.GetFileName(validFile);
            switch (p0)
            {
                case "Initial Upload":
                     GetFileDetailsInDB(fileName,1,p0);
                    break;
                case "Duplicate Upload":
                    GetFileDetailsInDB(fileName,2,p0);
                    bool deleteFile = DBConnectHelper.DeleteRow("dbo.documents", "fileName", fileName, DatabaseName.Documents);
                    Assert.IsTrue(deleteFile);
                    break;
            }
        }
        private void GetFileDetailsInDB(string fileName,int rowsReturned,string upload)
        {
            Logger.Info("Verifying database for file deatils");
            int numOfRows = DBConnectHelper.RowReturnedQuery("Select * from dbo.documents where filename = '" + fileName + "'; ", DatabaseName.Documents);
            Assert.AreEqual(rowsReturned, numOfRows);
            List<DatabaseResult> output = DBConnectHelper.GetQueryResult(
                "SELECT FileName,Created_at,Updated_by,Version FROM dbo.Documents where filename = '" + fileName + "' order by Created_at desc", DatabaseName.Documents);
            Logger.Info("Checking " + upload + " file name and versions in database");
            if(upload == "Initial Upload")
            {
                Assert.AreEqual(output[0].firstColumn, fileName);
                Assert.AreEqual(Convert.ToInt16(output[0].forthColumn), 1);
                Assert.AreEqual(output[0].thirdColumn, "ECO Operations");
            }
            else if (upload == "Duplicate Upload")
            {
                Assert.AreEqual(output[0].firstColumn, fileName);
                Assert.AreEqual(output[1].firstColumn, fileName);
                Assert.AreEqual(Convert.ToInt16(output[0].forthColumn), 2);
                Assert.AreEqual(Convert.ToInt16(output[1].forthColumn), 1);
                Assert.AreEqual(output[0].thirdColumn, "ECO Operations");
                Assert.AreEqual(output[1].thirdColumn, "ECO Operations");
            }
            string todaysDate = DateTime.Now.ToString("M/d/yyyy");
            string dateRetrievedFromDB = output[0].secondColumn;
            Assert.AreEqual(todaysDate, dateRetrievedFromDB.Split(' ')[0]);
        }
        public DateTime GetDateTime(string dateInString)
        {
           return Convert.ToDateTime(dateInString);
        }
    }
}
