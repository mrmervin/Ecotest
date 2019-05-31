using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.FileReader;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace ECO3_Testing.Pages
{
    public class UploadSummaryPage : BasePage
    {

        [FindsBy(How = How.CssSelector, Using = ".page-region-content p>b")]
        private IWebElement uploadedFileName;

        [FindsBy(How = How.CssSelector, Using = ".page-region-content p:nth-child(3)")]
        private IWebElement rowDetails;

        [FindsBy(How = How.CssSelector, Using = "table tr")]
        private IList<IWebElement> allRecords;

        [FindsBy(How = How.Id, Using = "btnreject")]
        private IWebElement rejectBtn;

        [FindsBy(How = How.Id, Using = "btnstartagain")]
        private IWebElement starAgainBtn;

        [FindsBy(How = How.Id, Using = "btnconfirm")]
        private IWebElement approveAndNotifyBtn;

        private IWebDriver webDriver;

        private static List<UploadedMeasureReader> listOfRecords;
        public UploadSummaryPage(IWebDriver driver): base(driver)
        {
            webDriver = driver;
        }

        public string UploadedFileDetails()
        {
           return GenericHelper.WaitForElementToBeClickable(uploadedFileName)
                .Text;
        }

        public string UploadedRowDetails()
        {
            return GenericHelper.WaitForElementToBeClickable(rowDetails)
                 .Text.Trim();
        }
        private void ValidatedMeasures()
        {
            GenericHelper.WaitForAllElementsToDisplay(allRecords);
            listOfRecords = new List<UploadedMeasureReader>();
            

            for(int i = 0; i < allRecords.Count; i++)
            {
                UploadedMeasureReader reader = new UploadedMeasureReader();
                var table = webDriver.FindElement(By.CssSelector("table tr:nth-child(" + (i+1) + ")"));
                var supplierCloumnData = table.FindElement(By.CssSelector("td:nth-child(1)"));
                var recordCloumnData = table.FindElement(By.CssSelector("td:nth-child(2)"));
                var measureCloumnData = table.FindElement(By.CssSelector("td:nth-child(3)"));

                reader.Supplier = supplierCloumnData.Text;
                reader.Records = recordCloumnData.Text;
                reader.Measures = measureCloumnData.Text;
                listOfRecords.Add(reader);
            }
        }
        public List<UploadedMeasureReader> GetUploadedMeasureData()
        {
            ValidatedMeasures();
            return listOfRecords;
        }
        public void RejectMeasure()
        {
            GenericHelper.WaitForElementToBeClickable(rejectBtn)
                            .Click();
        }
        public void StartUploadAgain()
        {
            GenericHelper.WaitForElementToBeClickable(starAgainBtn)
                            .Click();
        }
        public void ApproveAndNotify()
        {
            GenericHelper.WaitForElementToBeClickable(approveAndNotifyBtn)
                            .Click();
        }
    }
}
