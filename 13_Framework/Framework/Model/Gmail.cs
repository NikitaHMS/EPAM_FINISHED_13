using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using PageMaps;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

namespace Model
{
    public class Gmail : CommonEmail
    {
        private readonly string url = "https://accounts.google.com/v3/signin/identifier?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&flowName=GlifWebSignIn&flowEntry=AccountChooser&ec=asw-gmail-globalnav-signin&theme=mn&ddm=0";

        public Gmail(IWebDriver driver) : base(driver) { }

        public GmailElementMap Map
        {
            get
            {
                return new GmailElementMap(driver);
            }
        }


        public void Navigate()
        {
            driver.Navigate().GoToUrl(url);
        }
        public void LogIn(User user)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='email']")));
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
            driver.Navigate().GoToUrl("https://myaccount.google.com/profile/nickname/edit?add=true&continue=https%3A%2F%2Fmyaccount.google.com%2Fpersonal-info%3Fpli%3D1");
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].value=arguments[1];",
                Map.NewAliasTextbox, newAlias);
            jse.ExecuteScript("arguments[0].removeAttribute('disabled')",
                Map.SaveNewAlliasButton);
            Map.SaveNewAlliasButton.Click();
        }
        public void OpenLatestLetter()
        {
            var open = new Actions(driver).MoveToElement(Map.LatestLetter).Click();

            open.Perform();
        }
    }
}
