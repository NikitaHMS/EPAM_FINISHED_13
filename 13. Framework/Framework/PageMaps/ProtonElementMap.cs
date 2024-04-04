using OpenQA.Selenium;

namespace PageMaps
{
	public class ProtonElementMap
    {
        private readonly IWebDriver browser;
        public ProtonElementMap(IWebDriver browser)
        {
            this.browser = browser;
        }


        public IWebElement LoginField
        {
            get
            {
                return browser.FindElement(By.CssSelector("input[id='username']"));
            }
        }
        public IWebElement PasswordField
        {
            get
            {
                return browser.FindElement(By.CssSelector("input[id='password']"));
            }
        }
        public IWebElement SubmitButton
        {
            get
            {
                return browser.FindElement(By.CssSelector("button[type='submit']"));
            }
        }

        public IWebElement ReplySentNotification
        {
            get
            {
                return browser.FindElement(By.XPath("//span[@class='notification__content']/span"));
            }
        }

        public IWebElement LatestLetter
        {
            get
            {
                return browser.FindElement(By.XPath("//div[@data-shortcut-target='item-container-wrapper']"));
            }
        }
    }
}