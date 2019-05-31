using ECO3_Testing.ComponentHelpers;
using ECO3_Testing.Configurations;
using ECO3_Testing.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;

namespace ECO3_Testing.BaseClass
{
    [TestClass]
    public class BaseClass
    {
        private static TestContext context;
        private static ChromeOptions GetChromeOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy.Equals(1);
            options.AddArguments("start-maximized", "-no-sandbox");
            options.Proxy = null;
            return options;
        }

        private static InternetExplorerOptions GetInternetExplorerOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            //options.EnsureCleanSession = true;
            options.IgnoreZoomLevel = true;
            return options;
        }

        private static FirefoxDriverService GetFirefoxDriverService()
        {
            string firefoxPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
                .Parent.Parent.Parent.FullName, @"ECO3-Testing\ECO3-Testing\FirefoxFiles");
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(firefoxPath+ @"\geckodriver-v0.20.1-win64", "geckodriver.exe");
            service.FirefoxBinaryPath = firefoxPath+@"\FxDriver\firefox.exe";
            service.HideCommandPromptWindow = true;
            return service;
        }
        public static IWebDriver GetChromeDriver()
        {
            IWebDriver driver = new ChromeDriver(GetChromeOptions());
            return driver;
        }

        public static IWebDriver GetIEDriver()
        {
            IWebDriver driver = new InternetExplorerDriver(GetInternetExplorerOptions());
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static IWebDriver GetFirefoxDriver()
        {
            IWebDriver driver = new FirefoxDriver(GetFirefoxDriverService());
            return driver;
        }

        [AssemblyInitialize]
        public static void AppConfigInitiate(TestContext tc)
        {
            Console.WriteLine("Initialize...");
            context = tc;
            ObjectRepository.Config = new AppConfigReader();
        }
        
        [AssemblyCleanup]
        public static void TearDown()
        {
            UnitTestOutcome outcome = context.CurrentTestOutcome;

            switch (outcome)
            {
                case UnitTestOutcome.Passed:
                    /*
                     *Do nothing when test passes
                     */
                    break;
                default:
                    /*
                     *Take screenshot when test doesn't pass
                     */
                    Console.WriteLine("This test failed {0}", context.TestName);
                    //GenericHelper.TakeScreensots();
                    break;
            }
            Console.WriteLine("Clean up...");
            if(ObjectRepository.Driver != null)
            {
                GenericHelper.HardWait(1);
                ObjectRepository.Driver.Quit();
                ObjectRepository.Driver.Dispose();
            }
           
        }
    }
}

