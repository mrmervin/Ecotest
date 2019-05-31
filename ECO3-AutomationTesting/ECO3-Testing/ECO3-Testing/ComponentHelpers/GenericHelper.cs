using ECO3_Testing.CustomException;
using ECO3_Testing.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace ECO3_Testing.ComponentHelpers
{
    public class GenericHelper
    {
        public static bool IsElementPresent(IWebElement element)
        {
            bool isDisplayed = false;
            try
            { 
                isDisplayed = WaitForElementToBeDisplayed (element);
            }
            catch(NoSuchElementException e){
                e.Message.ToString();
                e.GetBaseException();

            }
            return isDisplayed;
        }
        public static bool ElementNotPresent(IWebElement element)
        {
            return !IsElementPresent(element);

        }
        public static IWebElement GetElement(IWebElement element)
        {
            if (IsElementPresent(element))
            {
                return element;
            }
            else
            {
                throw new ECO3TestException(message: $"{element.ToString()} not visible to be interacted with");
            }
        }
        public static void PageLoadTimeout(string url)
        {
            ObjectRepository.Driver.Manage().Timeouts().PageLoad.TotalSeconds.Equals(TimeSpan.FromSeconds(120));
            NavigationHelper.NavigateToUrl(url);
        }
        public void DynamicWait(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            wait.Until(ExpectedConditions.ElementSelectionStateToBe(element, true));
        }
        public static void WaitForAllElementsToDisplay(IList <IWebElement> elements)
        {
            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            wait.Until(WaitForAllElementsVisibility(elements));
        }
        public static void WaitForElementToDisappear(IWebElement element)
        {
            IWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(90);
            wait.PollingInterval = TimeSpan.FromMilliseconds(300);
            wait.Until(WaitForElementToBeInvisible(element));
        }
        public static bool WaitForElementToBeDisplayed(IWebElement element)
        {
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(120);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            return wait.Until(WaitForElementToBeSeen(element));
        }
        public static IWebElement WaitForElementToBeClickable(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
        public static IWebElement WaitForLinkTextElementToBeClickable(string linkText)
        {
            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            return wait.Until(ExpectedConditions.ElementToBeClickable(ObjectRepository.Driver.FindElement(By.PartialLinkText(linkText))));
        }
        public static bool WaitForElementToBeVisible(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            return wait.Until(WaitForElementVisibility(element));
        }
        public static void HardWait(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }
        private static Func<IWebElement, bool> WaitForElementToBeSeen(IWebElement element)
        {
            return ((x) =>
            {
                return element.Displayed;

            });
        }
        private static Func<IWebElement, bool> WaitForElementToBeInvisible(IWebElement element)
        {
            return ((x) =>
            {
                try
                {
                    return !element.Displayed;
                }
                catch (Exception)
                {
                    return true; 
                }
            });
        }
        private static Func<IWebDriver, bool> WaitForElementVisibility(IWebElement element)
        {
            return ((x) =>
            {
                return element.Enabled;

            });
        }
        private static Func<IWebDriver, IList<IWebElement>> WaitForAllElementsVisibility(IList <IWebElement> element)
        {
            return ((x) =>
            {
                return element.ToList();

            });
        }
        public static void WaitForPageToLoad()
        {
            IWait<IWebDriver> wait = new WebDriverWait(ObjectRepository.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            wait.Until(PageLoaded()).Equals("complete");
        }
        private static Func<IWebDriver, string> PageLoaded()
        {
            return ((x) =>
            {
                return ExecuteScriptWithValueReturned("return document.readyState");
            });
        }
        public void ExecuteScript(string script)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)ObjectRepository.Driver;
            scriptExecutor.ExecuteScript(script);
        }
        private static string ExecuteScriptWithValueReturned(string script)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)ObjectRepository.Driver;
            return scriptExecutor.ExecuteScript(script).ToString();
        }
        public void ScrollToElement(IWebElement element)
        {
            if (element.Location.X > 0 || element.Location.X < 0)
            {
                ExecuteScript("window.scroll(" + element.Location.X + "," + element.Location.Y + ")");
            }
            else
            {
                ExecuteScript("window.scroll(0," + element.Location.Y + ")");
            }
        }
        public static void DoubleClick(IWebElement element)
        {
            Actions actions = new Actions(ObjectRepository.Driver);
            actions.DoubleClick(element).Build().Perform();
        }
        public static void MoveToElementAndClick(IWebElement element)
        {
            Actions actions = new Actions(ObjectRepository.Driver);
            WaitForElementToBeSeen(element);
            actions.MoveToElement(element)
                .Click().Build().Perform();
        }
        public static string TakeScreensots(ScenarioContext context)
        {
            Screenshot screenShot = ((ITakesScreenshot)ObjectRepository.Driver).GetScreenshot();
            string filename = @"\screenshot_" +context.ScenarioInfo.Title+ DateTime.Now.ToString("_dd-MM-yyyy-hh-mm-ss") + ".jpeg";

            string screenshotPath =
                Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                @"ECO3-Testing\ECO3-Testing\Screenshot");
            Directory.CreateDirectory(screenshotPath);
            string pathToSave = screenshotPath + filename;
            screenShot.SaveAsFile(pathToSave, ScreenshotImageFormat.Jpeg);
            return pathToSave;
        }
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}
