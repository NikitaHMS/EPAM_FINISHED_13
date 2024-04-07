using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Model
{
    public class CommonEmail
    {
        protected readonly IWebDriver browser;
        protected WebDriverWait wait;
        public CommonEmail(IWebDriver browser)
        {
            this.browser = browser;
            wait = new WebDriverWait(browser, TimeSpan.FromSeconds(60.0));
        }
    }
}
