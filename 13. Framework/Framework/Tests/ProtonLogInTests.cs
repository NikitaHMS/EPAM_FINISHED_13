using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Tool;
using Model;
using OpenQA.Selenium.DevTools.V119.Network;
using OpenQA.Selenium.Support.Extensions;

/// <remarks>
/// May require completing 1 captcha InvalidLogin
/// </remarks>

namespace Tests
{

    [TestClass]
    public class ProtonLogInTests
    {
        public TestContext TestContext { get; set; }
        private static IWebDriver browser;
        private static WebDriverWait wait;

        [ClassInitialize]
        public static void OneTimeSetUp(TestContext context)
        {
            browser = DriverSingleton.getDriver();
            wait = new WebDriverWait(browser, TimeSpan.FromSeconds(30.0));
            browser.Manage().Window.Maximize();
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            browser.Quit();
        }

        [TestMethod]
        public void Input_InvalidLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withInvalidLogin();
            Proton email = new(browser);
            string expected = "Неверные учетные данные для входа. Попробуйте снова.";

            email.Navigate();
            email.LogIn(user);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[@name='loginForm']/div[@data-testid='login:error-block']")));
            IWebElement loginError = browser.FindElement(By.XPath("//form[@name='loginForm']/div[@data-testid='login:error-block']"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        public void Input_InvalidPassword_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withInvalidPassword();
            Proton email = new(browser);
            string expected = "Пароль неверен. Пожалуйста, попробуйте другой пароль.";

            email.Navigate();
            email.LogIn(user);
            IWebElement passwordError = browser.FindElement(By.XPath("//form[@name='loginForm']/div[@data-testid='login:error-block']"));

            Assert.AreEqual(expected, passwordError.Text);
        }

        [TestMethod]
        public void Input_EmptyLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withEmptyLogin();
            Proton email = new(browser);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement loginError = browser.FindElement(By.XPath("//div[@id='id-3']/span"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        public void Input_BlankLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withBlankLogin();
            Proton email = new(browser);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement loginError = browser.FindElement(By.XPath("//div[@id='id-3']/span"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        public void Input_InappropriateLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withInapropriateLogin();
            Proton email = new(browser);
            string expected = "Недопустимое имя пользователя";

            email.Navigate();
            email.LogIn(user);
            wait.Until(ExpectedConditions.ElementIsVisible((By.XPath("//span[@class='notification__content']"))));
            IWebElement loginError = browser.FindElement(By.XPath("//span[@class='notification__content']"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        public void Input_EmptyPassword_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withEmptyPassword();
            Proton email = new(browser);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement passwordError = browser.FindElement(By.XPath("//div[@id='id-4']/span"));

            Assert.AreEqual(expected, passwordError.Text);
        }

        [TestMethod]
        public void Input_BlankPassword_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withBlankPassword();
            Proton email = new(browser);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement passwordError = browser.FindElement(By.XPath("//div[@id='id-4']/span"));

            Assert.AreEqual(expected, passwordError.Text);
        }

        [TestMethod]
        public void Input_ValidData_LoginSuccessful()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withCredentialsFromProperty();
            Proton email = new(browser);
            string expected = user.getLogin();

            email.Navigate();
            email.LogIn(user);
            IWebElement logInConfirm = browser.FindElement(By.XPath("//span[contains(@class, 'user-dropdown-email')]"));

            Assert.AreEqual(expected, logInConfirm.Text);
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