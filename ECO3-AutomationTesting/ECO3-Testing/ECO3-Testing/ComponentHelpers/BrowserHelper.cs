using ECO3_Testing.Configurations;
using ECO3_Testing.CustomException;
using ECO3_Testing.Settings;

namespace ECO3_Testing.ComponentHelpers
{
    public class BrowserHelper
    {
        public static void RunBrowser()
        {
            switch (ObjectRepository.Config.GetBrowserType())
            {
                case BrowserType.IE:
                    ObjectRepository.Driver = BaseClass.BaseClass.GetIEDriver();
                    break;
                case BrowserType.Chrome:
                    ObjectRepository.Driver = BaseClass.BaseClass.GetChromeDriver();
                    break;
                case BrowserType.Firefox:
                    ObjectRepository.Driver = BaseClass.BaseClass.GetFirefoxDriver();
                    break;
                default:
                    throw new ECO3TestException("Provided driver in App.config file is not supported "+ ObjectRepository.Config.GetBrowserType());
            }
            GenericHelper.WaitForPageToLoad();
        }
    }
}
