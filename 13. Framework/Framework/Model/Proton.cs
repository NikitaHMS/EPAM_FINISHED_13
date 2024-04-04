using Model;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using PageMaps;

namespace Model
{
    public class Proton : CommonEmail
    {
        private readonly string url = "https://account.proton.me/ru/mail";

        public Proton(IWebDriver browser) : base(browser) { }
        
        public ProtonElementMap Map
        {
            get
            {
                return new ProtonElementMap(browser);
            }
        }


        public void Navigate()
        {
            browser.Navigate().GoToUrl(url);
        }
        public void LogIn(User user)
        {
            isAlertPresent(browser);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[id='username']")));
            Map.LoginField.SendKeys(user.getLogin());
            Map.PasswordField.SendKeys(user.getPassword());
            Map.SubmitButton.Click();
        }

        public void OpenLatestLetter()
        {
            Map.LatestLetter.Click();
        }
        public void SendReply(string reply)
        {
            IWebDriver lttrContent = browser.SwitchTo().Frame(browser.FindElement(By.XPath("//iframe[@data-testid='rooster-iframe']")));
            IWebElement sendReplyBttn = browser.FindElement(By.XPath("//button[@data-testid='composer:send-button']"));
            var openReplyWin = new Actions(browser).SendKeys("r");

            openReplyWin.Perform();
            lttrContent.FindElement(By.XPath("//div[@id='rooster-editor']/div[1]")).SendKeys(reply);
            sendReplyBttn.Click();
        }

        private void isAlertPresent(IWebDriver driver)
        {
            try
            {
                return;
            }
            catch (UnhandledAlertException)
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                return;
            }
        }
    }
}
