using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.DataSource;
using ECO3_Testing.Pages;
using ECO3_Testing.Settings;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace ECO3_Testing.StepDefinitions.UserStories.UserStoryThree
{
    [Binding]
    public class US3And4Steps 
    {
        #region files
        private static readonly string suppliersMRNsFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
           .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\SuppliersMRNs\MP_110904_September2018_Measure_Processing_Errors.csv");

        private static readonly string suppliersMRNsFile2 = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
           .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\SuppliersMRNs\MP_311204_December2018_Measure_Processing_Errors.csv");
        #endregion

        #region expected messages
        private readonly string fileUploadedMessages = "You are uploading the following file: MP_110904_September2018_Measure_Processing_Errors.csv";
        private readonly string rowUploadedMessages = "Your file has been checked. It contains 8 records of which 0 failed validation and 8 passed.";
        private readonly string rowOnSummaryPageMsg = "Your file has been checked. It contains 8 of which 0 failed validation and 8 passed";

        private readonly string fileUploadedMessages2 = "You are uploading the following file: MP_311204_December2018_Measure_Processing_Errors.csv";
        private readonly string rowUploadedMessages2 = "Your file has been checked. It contains 7 records of which 0 failed validation and 7 passed.";
        private readonly string uploadContinueMessages = @"Press ""Continue"" to review valid records.";
        private readonly string rowOnSummaryPageMsg2 = "Your file has been checked. It contains 7 records of which 0 failed validation and 7 passed";
        #endregion

        #region Logging and instances
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(US3And4Steps));
        private static UploadSummaryPage uploadSummaryPages;
        private ClassInstances classSetter = new ClassInstances();
        IWebDriver driver = ObjectRepository.Driver;
        #endregion

        [When(@"I upload a valid file containing MRNs from different suppliers: ""(.*)""")]
        public void WhenIUploadAValidFileContainingMRNsFromDifferentSuppliers(string p0)
        {
            if(p0 == "Approved")
            {
                UploadMethodUS3("Uploading file for more than one Supplier", suppliersMRNsFile);
            }
            else
            {
                UploadMethodUS3("Uploading file for more than one Supplier", suppliersMRNsFile2);
            }
            WaitForUploadBtnToDisapper();
        }

        [Then(@"upload should successfully display such messages with option to continue: ""(.*)""")]
        public void ThenUploadShouldSuccessfullyDisplaySuchMessagesWithOptionToContinue(string p0)
        {
            classSetter.MeasureProcessingErrorFilePageInstance.WaitForContinueBtnToDisplay();
            Assert.IsTrue(classSetter.MeasureProcessingErrorFilePageInstance.ContinueBtnDisplayed());
            if (p0 == "Rejected")
            {
                Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], fileUploadedMessages2);
                Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], rowUploadedMessages2);
                Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2], uploadContinueMessages);
            }
            else if (p0 == "Approved")
            {
                Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], fileUploadedMessages);
                Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], rowUploadedMessages);
                Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2], uploadContinueMessages);
            }
        }


        [Then(@"validation summary of successfully measure uploads against suppliers will display with Reject buttons:""(.*)""")]
        public void ThenValidationSummaryOfSuccessfullyMeasureUploadsAgainstSuppliersWillDisplayWithRejectButtons(string p0)
        {
            classSetter.GetUploadSummaryPage = new UploadSummaryPage(driver);
            uploadSummaryPages = classSetter.GetUploadSummaryPage;
            Assert.IsTrue(classSetter.MeasureProcessingErrorFilePageInstance.CheckRejectBtn());
            if (p0 == "Rejected")
            {
                Assert.AreEqual(uploadSummaryPages.UploadedFileDetails(), Path.GetFileName(suppliersMRNsFile2));
                Assert.AreEqual(uploadSummaryPages.UploadedRowDetails(), rowUploadedMessages2.Replace("passed.","passed"));

                ValidateSuppliersCodeData(GetSuppliersCodes("AAA", "BBB", "EEE", "DDD"));
                ValidateSuppliersRecordData(GetSuppliersCodes("2 Records", "1 Records", "2 Records", "2 Records"));
                ValidateSuppliersMeasureData(GetSuppliersCodes("2 Measures", "1 Measures", "2 Measures", "2 Measures"));
            }
            else if (p0 == "Approved")
            {
                Assert.AreEqual(uploadSummaryPages.UploadedFileDetails(), Path.GetFileName(suppliersMRNsFile));
                Assert.AreEqual(uploadSummaryPages.UploadedRowDetails(), rowOnSummaryPageMsg);

                ValidateSuppliersCodeData(GetSuppliersCodes("AAA", "BBB", "FFF", "CCC"));
                ValidateSuppliersRecordData(GetSuppliersCodes("3 Records", "3 Records", "1 Records", "1 Records"));
                ValidateSuppliersMeasureData(GetSuppliersCodes("3 Measures", "3 Measures", "1 Measures", "1 Measures"));
            }
            
        }

        [When(@"I click reject button")]
        public void WhenIClickRejectButton()
        {
            uploadSummaryPages.RejectMeasure();
            
        }

        [Then(@"file status should update to rejection with option to start again")]
        public void ThenFileStatusShouldUpdateToRejectionWithOptionToStartAgain()
        {
            //Implement front end part here before below code
            int rowReturned = DBConnectHelper.RowReturnedQuery("Select * from ECO.MeasuresProcessingError where FileName" +
                " ='MP_311204_December2018_Measure_Processing_Errors.csv' and StatusProcessingError = 5",DatabaseName.ECO);
            Assert.AreEqual(rowReturned, 7);
            bool deleteFile = DBConnectHelper.DeleteRow("dbo.documents", "fileName", Path.GetFileName(suppliersMRNsFile2), DatabaseName.Documents);
            Assert.IsTrue(deleteFile);
            bool deleteErrors = DBConnectHelper.DeleteRow("ECO.MeasuresProcessingError", "fileName", Path.GetFileName(suppliersMRNsFile2), DatabaseName.ECO);
            Assert.IsTrue(deleteErrors);
            StartAgain();
        }

        [When(@"I click approve and notify button")]
        public void WhenIClickApproveAndNotifyButton()
        {
            uploadSummaryPages.ApproveAndNotify();
           
        }

        [Then(@"file status should update to approved with option to start again")]
        public void ThenFileStatusShouldUpdateToApprovedWithOptionToStartAgain()
        {
            //implement database to check file status is updated to approve
            StartAgain();
        }

        private void UploadMethodUS3(string message, string fileToUpload)
        {
            Logger.Info(message);
            classSetter.MeasureProcessingErrorFilePageInstance
                .UploadFile(fileToUpload);
        }
        private void WaitForUploadBtnToDisapper()
        {
            GenericHelper.WaitForElementToDisappear
               (classSetter.MeasureProcessingErrorFilePageInstance.UploadButton());
        }

        private void StartAgain()
        {
            uploadSummaryPages.StartUploadAgain();
        }

        private List<string> GetSuppliersCodes(string aaa, string bbb, string ccc, string ddd)
        {
            List<string> measureValues = new List<string>();
            measureValues.Add(aaa);
            measureValues.Add(bbb);
            measureValues.Add(ccc);
            measureValues.Add(ddd);
            return measureValues;
        }
        private void ValidateSuppliersCodeData(List<string> values)
        {
            Assert.IsTrue(uploadSummaryPages.GetUploadedMeasureData().Count == 4);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[0].Supplier, values[0]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[1].Supplier, values[1]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[2].Supplier, values[2]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[3].Supplier, values[3]);
        }

        private void ValidateSuppliersRecordData(List<string> values)
        {
            Assert.IsTrue(uploadSummaryPages.GetUploadedMeasureData().Count == 4);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[0].Records, values[0]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[1].Records, values[1]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[2].Records, values[2]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[3].Records, values[3]);
        }

        private void ValidateSuppliersMeasureData(List<string> values)
        {
            Assert.IsTrue(uploadSummaryPages.GetUploadedMeasureData().Count == 4);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[0].Measures, values[0]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[1].Measures, values[1]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[2].Measures, values[2]);
            Assert.AreEqual(uploadSummaryPages.GetUploadedMeasureData()[3].Measures, values[3]);
        }
    }
}
