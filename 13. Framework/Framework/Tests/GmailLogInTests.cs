using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Model;
using Tool;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OpenQA.Selenium.Support.Extensions;

namespace Tests
{
    [Ignore]
    [TestClass]
    public class GmailLogInTests
    {
        public TestContext TestContext { get; set; }
        private static IWebDriver browser;
        private static WebDriverWait wait;

        [ClassInitialize]
        public static void OneTimeSetUp(TestContext context)
        {
            browser = DriverSingleton.getDriver();
            wait = new WebDriverWait(browser, TimeSpan.FromSeconds(15.0));
            browser.Manage().Window.Maximize();
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            browser.Quit();
        }

        [TestMethod]
        public void Input_InvalidLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInvalidLogin();
            Gmail email = new(browser);
            string expected = "Не удалось найти аккаунт Google.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = browser.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);

        }

        [TestMethod]
        public void Input_InvalidPassword_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInvalidPassword();
            Gmail email = new(browser);
            string expected = "Неверный пароль. Повторите попытку или нажмите на ссылку \"Забыли пароль?\", чтобы сбросить его.";
            
            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            IWebElement loginError = browser.FindElement(By.CssSelector("div[jsname='B34EJ'] > span[jsslot]"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        public void Input_EmptyLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withEmptyLogin();
            Gmail email = new(browser);
            string expected = "Введите адрес электронной почты или номер телефона.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = browser.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }
        
        [TestMethod]
        public void Input_BlankLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withBlankLogin();
            Gmail email = new(browser);
            string expected = "Введите адрес электронной почты или номер телефона.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = browser.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        public void Input_InappropriateLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInapropriateLogin();
            Gmail email = new(browser);
            string expected = "Не удалось найти аккаунт Google.";

            email.Navigate();
            email.SubmitLogin(user);
            string loginError = browser.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        public void Input_EmptyPassword_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withEmptyPassword();
            Gmail email = new(browser);
            string expected = "Введите пароль";
            email.Navigate();

            email.SubmitLogin(user);
            email.SubmitPassword(user);
            IWebElement loginError = browser.FindElement(By.CssSelector("div[jsname='B34EJ'] > span[jsslot]"));
            string error = loginError.Text.Replace(".", "");

            Assert.AreEqual(expected, error);
        }
        
        [TestMethod]
        public void Input_BlankPassword_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withBlankPassword();
            Gmail email = new(browser);
            string expected = "Введите пароль";
            
            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            IWebElement loginError = browser.FindElement(By.CssSelector("div[jsname='B34EJ'] > span[jsslot]"));
            string error = loginError.Text.Replace(".", "");

            Assert.AreEqual(expected, error);
        }

        [TestMethod]
        public void Input_ValidData_LoginSuccessful()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withCredentialsFromProperty();
            Gmail email = new(browser);
            string expected = user.getLogin();

            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            string accIconInfo = browser.FindElement(By.XPath("//header/div[2]/div[3]/div[1]/div[2]/div/a")).GetAttribute("aria-label");

            Assert.IsTrue(accIconInfo.Contains(expected));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string screenshotPath = $"{PathSetter.toScreenshotsDir()}{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";
                browser.TakeScreenshot().SaveAsFile(screenshotPath);
                TestContext.AddResultFile(screenshotPath);
            }
        }
    }
}
