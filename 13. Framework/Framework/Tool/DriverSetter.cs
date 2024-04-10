using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V121.Network;
using OpenQA.Selenium.Interactions;
                                                            //TODO: add exception handling
namespace Tool      
{
    public class DriverSetter
    {
        private static IWebDriver? driver;
        private DriverSetter() { }

        public static IWebDriver getDriver(string browser, string environment)
        {
            if (driver != null)
            {
                return driver;
            }

            if (environment == "mobile")
            {
                ChangeEnvironmentToMobile(browser);
            }
            else
            {
                switch (browser)
                {
                    case "firefox":
                        driver = new FirefoxDriver();
                        break;

                    default:
                        driver = new ChromeDriver();
                        break;

                }
            }

            return driver;
        }

        private static IWebDriver ChangeEnvironmentToMobile(string browser)
        {
            switch (browser)
            {
                default:
     
                    ChromeOptions co = new();
                    co.EnableMobileEmulation("iPhone 12 Pro");
                    driver = new ChromeDriver(co);
                    break;
            }

            return driver;
        }
    }
}
