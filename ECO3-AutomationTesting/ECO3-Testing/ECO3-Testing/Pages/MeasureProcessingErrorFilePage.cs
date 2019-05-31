using ECO3_Testing.BaseClass;
using ECO3_Testing.ComponentHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace ECO3_Testing.Pages
{
    public class MeasureProcessingErrorFilePage : BasePage
    {
        #region WebElements
        [FindsBy(How = How.Id, Using = "btnupload")]
        private IWebElement UploadBtn;

        [FindsBy(How = How.Id, Using = "fileupload")]
        private IWebElement UploadFileBtn;

        [FindsBy(How = How.CssSelector, Using = "#updatepanelFile>strong")]
        private IWebElement ErrorMessage;

        [FindsBy(How = How.CssSelector, Using = "#btncontiune")]
        private IWebElement ContinueBtn;

        [FindsBy(How = How.CssSelector, Using = "#btncontiuneupload")]
        private IWebElement ContinueDuplicateBtn;

        [FindsBy(How = How.CssSelector, Using = ".page-region-content>p")]
        private IList <IWebElement> Messages;

        [FindsBy(How = How.CssSelector, Using = "#updatepanelFile strong")]
        private IWebElement DuplicateUploadWarningMsg;

        [FindsBy(How = How.Id, Using = "btnreject")]
        private IWebElement RejectBtn;

        [FindsBy(How = How.Id, Using = "btnconfirm")]
        private IWebElement ApproveAndNotifytBtn;

        [FindsBy(How = How.PartialLinkText, Using = "Validation.csv")]
        private IWebElement FileLink;

        #endregion


        private IWebDriver driver;
        public MeasureProcessingErrorFilePage(IWebDriver webDriver) : base(webDriver)
        {
            driver = webDriver;
        }

        public bool UploadBtnDisplayed()
        {
            GenericHelper.WaitForPageToLoad();
            return GenericHelper.IsElementPresent(UploadBtn);
        }

        public void UploadFile(string filePath)
        {
            GenericHelper.WaitForElementToBeClickable(UploadFileBtn)
                .SendKeys(filePath);
            UploadBtn.Click();
        }

        public IWebElement UploadButton()
        {
            return UploadBtn;
        }

        public string DisplayedMessage()
        {
            GenericHelper.WaitForElementToBeVisible(ErrorMessage);
            return ErrorMessage.Text;
        }

        public bool ContinueBtnDisplayed()
        {
            return GenericHelper.WaitForElementToBeDisplayed(ContinueBtn);
        }

        public bool CheckContinueBtn()
        {
            return GenericHelper.IsElementPresent(ContinueDuplicateBtn);
        }

        public void WaitForContinueBtnToDisplay()
        {
            GenericHelper.WaitForElementToBeClickable(ContinueBtn);
        }
        public List <string> AllMessages()
        {
            GenericHelper.WaitForAllElementsToDisplay(Messages);
            List<string> displayedMessages = new List<string>();

            for (int i=0;i<Messages.Count;i++)
            {
             displayedMessages.Add(Messages[i].Text);
            }
            return displayedMessages;
        }

        public string WarningMessageForDuplicateUpload()
        {
            GenericHelper.WaitForElementToBeDisplayed(DuplicateUploadWarningMsg);
            return DuplicateUploadWarningMsg.Text;
        }

        public void DiscontinueByRefreshingPage()
        {
            NavigationHelper.RefreshPage();
        }

        public void ContinueDuplicate()
        {
            GenericHelper.WaitForElementToBeClickable(ContinueDuplicateBtn)
                         .Click();
        }

        public void ClickContinue()
        {
            GenericHelper.WaitForElementToBeClickable(ContinueBtn)
                .Click();
        }

        public bool CheckBtns()
        {
            GenericHelper.WaitForElementToBeVisible(RejectBtn);
            bool rejectionBtn = GenericHelper.IsElementPresent(RejectBtn);
            bool approvalAndNotifyBtn = GenericHelper.IsElementPresent(ApproveAndNotifytBtn);
            return rejectionBtn && approvalAndNotifyBtn;
        }
        public bool CheckRejectBtn()
        {
            GenericHelper.WaitForElementToBeVisible(RejectBtn);
            bool rejectionBtn = GenericHelper.IsElementPresent(RejectBtn);
            return rejectionBtn;
        }

        public void DownloadErrorFile()
        {
            GenericHelper.WaitForElementToBeClickable(FileLink)
                .Click();
        }
    }

}
