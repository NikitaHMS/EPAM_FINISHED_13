using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace Tool      
{
    public class DriverSingleton
    {
        private static IWebDriver? driver;
        private DriverSingleton() { }

        public static IWebDriver getDriver()
        {
            if (driver == null)
            {
                switch (PropertiesManager.getBrowser())
                {
                    case "firefox":
                        return new FirefoxDriver();

                    default:
                        return new ChromeDriver();
                }
            }
            else
            {
                return driver;
            }
        }
    }
}
