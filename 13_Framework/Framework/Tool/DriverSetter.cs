using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
                                                          
namespace Tool
{
    public class DriverSetter
    {
        private static IWebDriver? driver;
        private DriverSetter() { }

        public static IWebDriver getDriver(string browser)
        {
            if (driver != null)
            {
                return driver;
            }
         
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                default:
                    driver = new ChromeDriver();
                    break;
            }

            return driver;
        }
    }
}
