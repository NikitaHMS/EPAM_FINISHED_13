using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Model
{
    public class CommonEmail
    {
        protected readonly IWebDriver driver;
        protected WebDriverWait wait;
        public CommonEmail(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60.0));
        }
    }
}
