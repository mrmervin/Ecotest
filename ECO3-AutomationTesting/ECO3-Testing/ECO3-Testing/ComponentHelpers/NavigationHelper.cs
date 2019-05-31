using ECO3_Testing.Settings;
using OpenQA.Selenium;

namespace ECO3_Testing.ComponentHelpers
{
    public class NavigationHelper

    {
        private static string ChromeSettingsURL = ObjectRepository.Config.ChromeSettingsUrl;

        public static void NavigateToUrl(string url)
        {
            ObjectRepository.Driver.Navigate().GoToUrl(url);
        }
        public static void RefreshPage()
        {
            ObjectRepository.Driver.Navigate().Refresh();
        }
        public static void NavigateBack()
        {
            ObjectRepository.Driver.Navigate().Back();
        }
        public static void ClearCache()
        {
            ObjectRepository.Driver.Navigate().GoToUrl(ChromeSettingsURL);
            GenericHelper.HardWait(3);
            ObjectRepository.Driver.FindElement(By.CssSelector("* /deep/ #clearBrowsingDataConfirm")).Click();
            ObjectRepository.Driver.Navigate().Refresh();
        }
        public static void NavigateViaDeepLink(string hostUrl, string deepLink)
        {
            ObjectRepository.Driver.Navigate().GoToUrl(string.Concat(hostUrl,deepLink));
        }
    }
}
