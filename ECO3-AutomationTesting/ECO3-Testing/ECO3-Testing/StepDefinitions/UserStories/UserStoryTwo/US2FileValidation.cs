using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.FileReader;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace ECO3_Testing.StepDefinitions.UserStories.UserStoryTwo
{
    [Binding]
    public class US2FileValidation : US2ErrorDownload
    {
        #region instances
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(US2FileValidation));
        DownloadFile downloadFile = new DownloadFile();
        CsvFileReader csvFile = new CsvFileReader();
        List<string> retrivedData = new List<string>();
        string downloadedCsvFile;
        ErrorFileColumns columnName = new ErrorFileColumns();
        #endregion
        #region file
        private static readonly string wrongColumnFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
              .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\MisspeltColumn\MP_180817_AUGUST2018_Measure_Processing_Errors.csv");

        private static readonly string missingColumnFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
              .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\MissingColumn\MP_201027_MARCH2020_Measure_Processing_Errors.csv");


        private static readonly string invalidSupplierFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
              .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\InvalidSupplier\MP_200719_JULY2019_Measure_Processing_Errors.csv");

        #endregion

        #region messages
        private string firstMessage = "You are uploading the following file: ";
        private string secondMessage = "Your file has been checked. It contains 2 records of which 2 failed validation and 0 passed.";
        private string singleRowError = "Your file has been checked. It contains 1 records of which 1 failed validation and 0 passed.";
        private string allRowError = "Your file has been checked. It contains 7 records of which 7 failed validation and 0 passed.";
        private string thirdMessage = "The validation errors can be downloaded using link below.";
        private string fourthMessage = string.Concat((Path.GetFileNameWithoutExtension(invalidSupplierFile).Replace("Errors", "Error")), "_Validation.csv");

        private string errorMsg1 = "The value provided for the  field Supplier code is not in the expected range.";
        private string errorMsg2 = "The value provided for the  field Measure reference is not in the expected range.";
        private string errorMsg3 = "The value provided for the field  Field is not in the expected range.";
        private string errorMsg4 = "The value provided for the field Test no and Test name is not in the expected range.";
        private string errorMsg5 = "The value provided for the fields Error message and Error description is not in the expected range.";
        private string errorMsg6 = "The field Observed Value is mandatory however no value has been provided for this row.";
        private string errorMsg7 = "The field Test no and Test name is mandatory however no value has been provided for this row.";
        private string errorMsg8 = "The field Error message and Error description is mandatory however no value has been provided for this row.";
        private string errorMsg9 = "The field Field is mandatory however no value has been provided for this row.";
        private string errorMsg10 = "The field Supplier code is mandatory however no value has been provided for this row.";
        private string errorMsg12 = "The field Measure reference is mandatory however no value has been provided for this row.";

        #endregion

        #region fields
        private int numberOfErrorMessages;
        #endregion

        public static string GetMisspeltFile()
        {
            return wrongColumnFile;
        }

        public static string GetMissingColumnFile()
        {
            return missingColumnFile;
        }

        public static string GetInvalidSupplierFile()
        {
            return invalidSupplierFile;
        }

        [When(@"I upload a file with misspelt column")]
        public void WhenIUploadAFileWithMisspeltColumn()
        {
            UploadMethodUS2("Uploading csv file with misspelt column name", wrongColumnFile);
        }

        [When(@"I upload a file with missing column")]
        public void WhenIUploadAFileWithMissingColumn()
        {
            UploadMethodUS2("Uploading csv file with missing column", missingColumnFile);
        }
        
        [When(@"I upload a file with invalid supplier")]
        public void WhenIUploadAFileWithInvalidSupplier()
        {
            UploadMethodUS2("Uploading invalid supplier file", invalidSupplierFile);
            Logger.Info("Waiting for Upload button to disappear");
            WaitForUploadBtnToDisapper();
        }

        [Then(@"I should see error messages with downloadable link : (.*),""(.*)""")]
        public void ThenIShouldSeeErrorMessagesWithDownloadableLink(int numOfErrors, string scenario)
        {
            Logger.Info("Verifying displayed messages after invalid supplier upload");
            numberOfErrorMessages = classSetter.MeasureProcessingErrorFilePageInstance.AllMessages().Count;
            VerifyErrorMessages(numOfErrors, scenario);
        }

        [When(@"I click the link error file is downloaded with useful message about the download : ""(.*)""")]
        public void WhenIClickTheLinkErrorFileIsDownloadedWithUsefulMessageAboutTheDownload(string scenario)
        {
            int beforeDownload = downloadFile.GetCurrentCSVFilesInDownloadFolder();
            Logger.Info("Before download "+ beforeDownload);
            Logger.Info("Downloading "+Path.GetFileName(invalidSupplierFile)+" files");
            classSetter.MeasureProcessingErrorFilePageInstance.DownloadErrorFile();
            int afterdownload = downloadFile.DownloadIsComplete();
            Logger.Info("After download " + afterdownload);
            downloadedCsvFile = downloadFile.GetLastDownloadedFile();
            Logger.Info("Last downloaded file " + downloadedCsvFile);
            Assert.IsTrue(afterdownload - beforeDownload == 1);
            ValidationErrorsRetrievedFromUpload();
            VerifyDownloadedData(scenario);
            downloadFile.DeleteFile();
        }
        private void ValidationErrorsRetrievedFromUpload()
        {
            int numOfRetrievedError = csvFile.RetrieveDownloadedCsvData(downloadedCsvFile);
            for(int i = 1; i < numOfRetrievedError; i++)
            {
                retrivedData.Add(csvFile.GetColumnData("ValidationError", i));
            }
        }

        private void VerifyErrorMessages(int numOfMessages,string scenario)
        {
            Assert.AreEqual(numberOfErrorMessages, numOfMessages);
            VerifyErrorsFromUpload(scenario);
           
        }
        private void VerifyDownloadedData(string scenario)
        {
            switch (scenario)
            {
                case "Invalid Supplier":
                    Assert.AreEqual(retrivedData[0], errorMsg1);
                    Assert.AreEqual(retrivedData[1], errorMsg2);
                    Assert.AreEqual(retrivedData[2], errorMsg1);
                    Assert.AreEqual(retrivedData[3], errorMsg2);
                    break;
                case "MRN doesn't exist":
                    Assert.AreEqual(retrivedData[0], errorMsg2);
                    Assert.AreEqual(retrivedData[1], errorMsg2);
                    break;
                case "MRN not linked to supplier":
                    Assert.AreEqual(retrivedData[0], errorMsg1);
                    Assert.AreEqual(retrivedData[1], errorMsg2);
                    break;
                case "Invalid field":
                    Assert.AreEqual(retrivedData[0],errorMsg3);
                    break;
                case "All errors":
                    Assert.AreEqual(retrivedData[0], errorMsg4);
                    Assert.AreEqual(retrivedData[1], errorMsg5);
                    Assert.AreEqual(retrivedData[2], errorMsg5);
                    Assert.AreEqual(retrivedData[3], errorMsg5);
                    Assert.AreEqual(retrivedData[4], errorMsg6);
                    Assert.AreEqual(retrivedData[5], errorMsg3);
                    Assert.AreEqual(retrivedData[6], errorMsg7);
                    Assert.AreEqual(retrivedData[7], errorMsg8);
                    Assert.AreEqual(retrivedData[8], errorMsg9);
                    Assert.AreEqual(retrivedData[9], errorMsg3);
                    Assert.AreEqual(retrivedData[10], errorMsg6);
                    Assert.AreEqual(retrivedData[11], errorMsg10);
                    Assert.AreEqual(retrivedData[12], errorMsg12);
                    break;
            }
        }
        private void VerifyErrorsFromUpload(string nameOfScenario)
        {
            switch (nameOfScenario)
            {
                case "Invalid Supplier":
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], firstMessage
                        +Path.GetFileName(invalidSupplierFile));
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], secondMessage);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2], thirdMessage);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[3], fourthMessage);
                    break;
                case "MRN doesn't exist":
                    string neededFile = US2ErrorDownload.GetInvalidMRNFile();
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], firstMessage
                        + Path.GetFileName(neededFile));
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], secondMessage);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2], thirdMessage);
                    string fileLink = RenameToValidation(neededFile);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[3], fileLink);
                    break;
                case "MRN not linked to supplier":
                    string notLinkedFile = US2ErrorDownload.GetMRNNotLinkedFile();
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], firstMessage
                        + Path.GetFileName(notLinkedFile));
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], singleRowError);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2], thirdMessage);
                    string fileLink2 = RenameToValidation(notLinkedFile);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[3], fileLink2);
                    break;
                case "Invalid field":
                    string invalidFieldFile = US2ErrorDownload.GetInvalidFieldFile();
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], firstMessage
                        + Path.GetFileName(invalidFieldFile));
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], singleRowError);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2], thirdMessage);
                    string fileLink3 = RenameToValidation(invalidFieldFile);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[3], fileLink3);
                    break;
                case "All errors":
                    string allFieldsErrorFile = US2ErrorDownload.GetAllErrorFile();
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[0], firstMessage
                        + Path.GetFileName(allFieldsErrorFile));
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[1], allRowError);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[2], thirdMessage);
                    string fileLink4 = RenameToValidation(allFieldsErrorFile);
                    Assert.AreEqual(classSetter.MeasureProcessingErrorFilePageInstance.AllMessages()[3], fileLink4);
                    break;
            }
        }
        private string RenameToValidation(string fileName)
        {
            return string.Concat((Path.GetFileNameWithoutExtension(fileName).Replace("Errors", "Error")), "_Validation.csv"); 
        }

    }
}
