using OpenQA.Selenium;

namespace PageMaps
{
    public class GmailElementMap
    {
        private readonly IWebDriver browser;
        public GmailElementMap(IWebDriver browser)
        {
            this.browser = browser;
        }


        public IWebElement LoginField
        {
            get
            {
                return browser.FindElement(By.CssSelector("input[type='email']"));
            }
        }
        public IWebElement LoginSubmitButton
        {
            get
            {
                return browser.FindElement(By.XPath("//div[@data-primary-action-label='Далее']/div/div[1]//button"));
            }
        }

        public IWebElement PasswordField
        {
            get
            {
                return browser.FindElement(By.CssSelector("input[type='password']"));
            }
        }
        public IWebElement PasswordSubmitButton
        {
            get
            {
                return browser.FindElement(By.XPath("//div[@data-primary-action-label='Далее']/div/div[1]//button"));
            }
        }

        public IWebElement WriteLetterButton
        {
            get
            {
                return browser.FindElement(By.CssSelector("div[class='T-I T-I-KE L3']"));
            }
        }
        public IWebElement RecipientField
        {
            get
            {
                return browser.FindElement(By.XPath("//div[@name='to']//input[@class='agP aFw']"));
            }
        }
        public IWebElement SubjectField
        {
            get
            {
                return browser.FindElement(By.CssSelector("input[name='subjectbox']"));
            }
        }
        public IWebElement LetterTextbox
        {
            get
            {
                return browser.FindElement(By.CssSelector("td[class='Ap'] div[role='textbox']"));
            }
        }
        public IWebElement SendLetterButton
        {
            get
            {
                return browser.FindElement(By.CssSelector("div[class='dC'] div:first-child"));
            }
        }

        public IWebElement NewAliasTextbox
        {
            get
            {
                return browser.FindElement(By.CssSelector("input[jsname='YPqjbf'][type='text']"));
            }
        }
        public IWebElement SaveNewAlliasButton
        {
            get
            {
                return browser.FindElement(By.CssSelector("div[jsname='x8hlje'] button[jsname='Pr7Yme']"));
            }
        }
        public IWebElement LatestLetter
        {
            get
            {
                return browser.FindElement(By.XPath("//div[@class='UI']//tbody/tr[1]"));
            }
        }
    }
}
