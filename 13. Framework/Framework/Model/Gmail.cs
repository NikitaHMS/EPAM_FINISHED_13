using Model;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PageMaps;
using OpenQA.Selenium.Interactions;

namespace Model
{
    public class Gmail : CommonEmail
    {
        private readonly string url = "https://accounts.google.com/v3/signin/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&ifkv=ASKXGp2LmcgNxQG73DvrrLxxn-gnPesAAgiXfXsWLH6sBqH1ew9CdVPO4_5tK6J-8_NaIzHHSKHr&rip=1&sacu=1&service=mail&flowName=GlifWebSignIn&flowEntry=ServiceLogin&dsh=S503820059%3A1703767017023636&theme=glif";

        public Gmail(IWebDriver browser) : base(browser) { }

        public GmailElementMap Map
        {
            get
            {
                return new GmailElementMap(browser);
            }
        }


        public void Navigate()
        {
            browser.Navigate().GoToUrl(url);
        }
        public void LogIn(User user)
        {
            Map.LoginField.SendKeys($"{user.getLogin()}");
            Map.LoginSubmitButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='password']")));
            Map.PasswordField.SendKeys($"{user.getPassword()}");
            Map.PasswordSubmitButton.Click();
        }

        public void SubmitLogin(User user)
        {
            Map.LoginField.SendKeys($"{user.getLogin()}");
            Map.LoginSubmitButton.Click();
        }
        public void SubmitPassword(User user)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='password']")));
            Map.PasswordField.SendKeys($"{user.getPassword()}");
            Map.PasswordSubmitButton.Click();
        }

        public void SendLetter(string recipient, Letter letter)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(Map.WriteLetterButton));
            Map.WriteLetterButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@name='to']//input[@class='agP aFw']")));
            Map.RecipientField.SendKeys(recipient);
            Map.SubjectField.SendKeys(letter.getSubject());
            Map.LetterTextbox.SendKeys(letter.getContent());
            Map.SendLetterButton.Click();
        }
        public void ChangeAlias(string newAlias)
        {
            browser.Navigate().GoToUrl("https://myaccount.google.com/profile/nickname/edit?add=true&continue=https%3A%2F%2Fmyaccount.google.com%2Fpersonal-info%3Fpli%3D1");
            IJavaScriptExecutor jse = (IJavaScriptExecutor)browser;
            jse.ExecuteScript("arguments[0].value=arguments[1];",
                Map.NewAliasTextbox, newAlias);
            jse.ExecuteScript("arguments[0].removeAttribute('disabled')",
                Map.SaveNewAlliasButton);
            Map.SaveNewAlliasButton.Click();
        }
        public void OpenLatestLetter()
        {
            var open = new Actions(browser).MoveToElement(Map.LatestLetter).Click();

            open.Perform();
        }
    }
}
