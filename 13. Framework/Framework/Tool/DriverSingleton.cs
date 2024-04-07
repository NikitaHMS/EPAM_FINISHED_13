using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace Tool      
{
    public class DriverSingleton
    {
        private static IWebDriver? driver;
        private DriverSingleton() { }

        public static IWebDriver getDriver(string browser)
        {
            if (driver == null)
            {
                switch (browser)
                {
                    case "firefox":
                        driver = new FirefoxDriver();
                        return driver;

                    case "chrome":
                        driver = new ChromeDriver();
                        return driver;

                    default:
                        driver = new ChromeDriver();
                        return driver;
                }
            }
            else
            {
                return driver;
            }
        }
    }
}
