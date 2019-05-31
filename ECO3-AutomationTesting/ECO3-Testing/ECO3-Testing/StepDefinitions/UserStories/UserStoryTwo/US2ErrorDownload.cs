using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using log4net;
using System.IO;
using TechTalk.SpecFlow;

namespace ECO3_Testing.StepDefinitions.UserStories.UserStoryTwo
{
    public class US2ErrorDownload 
    {
        #region Files 
        private static readonly string invalidMRNFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
              .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\InvalidMRN\MP_181122_November2018_Measure_Processing_Errors.csv");
        private static readonly string mrnNotLinkedFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
              .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\MRNNotLinked\MP_141220_December2020_Measure_Processing_Errors.csv");
        private static readonly string invalidFieldFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
              .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\InvalidField\MP_240918_SEPTEMBER2018_Measure_Processing_Errors.csv");
        private static readonly string allErrorsFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
              .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FileUpload\AllErrors\MP_311219_December2019_Measure_Processing_Errors.csv");

        #endregion

        #region Logger and Instance
        private static readonly ILog Logger = LoggingHelper.GetLogger(typeof(US2ErrorDownload));
        public ClassInstances classSetter = new ClassInstances();
        #endregion

        public static string GetInvalidMRNFile()
        {
            return invalidMRNFile;
        }
        public static string GetMRNNotLinkedFile()
        {
            return mrnNotLinkedFile;
        }
        public static string GetInvalidFieldFile()
        {
            return invalidFieldFile;
        }

        public static string GetAllErrorFile()
        {
            return allErrorsFile;
        }

        [When(@"I upload a file that MRN doesn't exist")]
        public void WhenIUploadAFileThatMRNDoesnTExist()
        {
            UploadMethodUS2("Uploading a file with Invalid MRN",invalidMRNFile);
            WaitForUploadBtnToDisapper();
        }

        [When(@"I upload a file where MRN is not linked with supplier")]
        public void WhenIUploadAFileWhereMRNIsNotLinkedWithSupplier()
        {
            UploadMethodUS2("Uploading a file where MRN is not linked to supplier",mrnNotLinkedFile);
            WaitForUploadBtnToDisapper();
        }

        [When(@"I upload a file with invalid field")]
        public void WhenIUploadAFileWithInvalidField()
        {
            UploadMethodUS2("Upload csv with invalid field",invalidFieldFile);
            WaitForUploadBtnToDisapper();
        }

        [When(@"I upload a file with all possible errors")]
        public void WhenIUploadAFileWithAllPossibleErrors()
        {
            UploadMethodUS2("Uploading csv file with all possible errors", allErrorsFile);
            WaitForUploadBtnToDisapper();
        }
        public void UploadMethodUS2(string message, string fileToUpload)
        {
            Logger.Info(message);
            classSetter.MeasureProcessingErrorFilePageInstance
                .UploadFile(fileToUpload);
        }
        public void WaitForUploadBtnToDisapper()
        {
            GenericHelper.WaitForElementToDisappear
               (classSetter.MeasureProcessingErrorFilePageInstance.UploadButton());
        }
    }
}
