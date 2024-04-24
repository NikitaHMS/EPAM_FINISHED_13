﻿using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using PageMaps;
using OpenQA.Selenium.Support.Extensions;

namespace Model
{
    public class Proton : CommonEmail
    {
        private readonly string url = "https://account.proton.me/ru/mail";

        public Proton(IWebDriver driver) : base(driver) { }
        
        public ProtonElementMap Map
        {
            get
            {
                return new ProtonElementMap(driver);
            }
        }


        public void Navigate()
        {
            driver.Navigate().GoToUrl(url);
        }

        public void LogIn(User user)
        {
            HandleAlert(driver);
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
            var openReplyWin = new Actions(driver).SendKeys("r");
            openReplyWin.Perform();

            driver.SwitchTo().Frame(2);
            IWebElement lttrContent = driver.FindElement(By.XPath("//div[@id='rooster-editor']/div[1]"));
            lttrContent.SendKeys(reply);

            IWebElement sendReplyBttn = driver.FindElement(By.XPath("//button[@data-testid='composer:send-button']"));
            sendReplyBttn.Click();
        }

        private void HandleAlert(IWebDriver driver)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch(NoAlertPresentException) { }
        }
    }
}
