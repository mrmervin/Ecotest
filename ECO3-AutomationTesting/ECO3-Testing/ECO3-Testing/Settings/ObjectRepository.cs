using ECO3_Testing.Interfaces;
using OpenQA.Selenium;

namespace ECO3_Testing.Settings
{
    public class ObjectRepository
    {
        public static IWebDriver Driver { get; set; }

        public static IConfig Config { get; set; }
    }
}
