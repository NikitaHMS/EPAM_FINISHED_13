﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Model;
using Tool;
using OpenQA.Selenium.Support.Extensions;
using SeleniumExtras.WaitHelpers;

namespace Tests
{
    [TestClass]
    public class GmailLogInTests
    {
        public TestContext TestContext { get; set; }
        private static IWebDriver driver;
        private static WebDriverWait wait;

        [ClassInitialize]
        public static void OneTimeSetUp(TestContext context)
        {
            string browser = Environment.GetEnvironmentVariable("browser");

            driver = DriverSetter.getDriver(browser);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15.0));

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            driver.Quit();
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_InvalidLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInvalidLogin();
            Gmail email = new(driver);
            string expected = "Не удалось найти аккаунт Google.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='Ekjuhf Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);

        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_InvalidPassword_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInvalidPassword();
            Gmail email = new(driver);
            string expected = "Неверный пароль. Повторите попытку или нажмите на ссылку \"Забыли пароль?\", чтобы сбросить его.";
            
            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            IWebElement loginError = driver.FindElement(By.CssSelector("div[jsname='B34EJ'] > span[jsslot]"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_EmptyLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withEmptyLogin();
            Gmail email = new(driver);
            string expected = "Введите адрес электронной почты или номер телефона.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='Ekjuhf Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_BlankLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withBlankLogin();
            Gmail email = new(driver);
            string expected = "Введите адрес электронной почты или номер телефона.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='Ekjuhf Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_InappropriateLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInapropriateLogin();
            Gmail email = new(driver);
            string expected = "Не удалось найти аккаунт Google.";

            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='Ekjuhf Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_EmptyPassword_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withEmptyPassword();
            Gmail email = new(driver);
            string expected = "Введите пароль";
            email.Navigate();

            email.SubmitLogin(user);
            email.SubmitPassword(user);
            IWebElement loginError = driver.FindElement(By.CssSelector("div[jsname='B34EJ'] > span[jsslot]"));
            string error = loginError.Text.Replace(".", "");

            Assert.AreEqual(expected, error);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_BlankPassword_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withBlankPassword();
            Gmail email = new(driver);
            string expected = "Введите пароль";
            
            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            IWebElement loginError = driver.FindElement(By.CssSelector("div[jsname='B34EJ'] > span[jsslot]"));
            string error = loginError.Text.Replace(".", "");

            Assert.AreEqual(expected, error);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_ValidData_LoginSuccessful()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withCredentialsFromProperty();
            Gmail email = new(driver);

            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            bool hasLoggedIn = driver.FindElement(By.XPath("//div[@id=':3']")).Displayed;

            Assert.IsTrue(hasLoggedIn);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string screenshotPath = $"{PathSetter.toScreenshotsDir()}{TestContext.TestName}{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";

                driver.TakeScreenshot().SaveAsFile(screenshotPath);
            }
        }
    }
}
