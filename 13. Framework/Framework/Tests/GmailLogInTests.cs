using OpenQA.Selenium;
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
            string environment = Environment.GetEnvironmentVariable("environment");

            driver = DriverSetter.getDriver(browser, environment);
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
        [TestCategory("desktop")]
        [TestCategory("mobile")]
        public void Input_InvalidLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInvalidLogin();
            Gmail email = new(driver);
            string expected = "Не удалось найти аккаунт Google.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);

        }

        [TestMethod]
        [TestCategory("smoke")]
        [TestCategory("desktop")]
        [TestCategory("mobile")]
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
        [TestCategory("desktop")]
        [TestCategory("mobile")]
        public void Input_EmptyLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withEmptyLogin();
            Gmail email = new(driver);
            string expected = "Введите адрес электронной почты или номер телефона.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        [TestCategory("smoke")]
        [TestCategory("desktop")]
        [TestCategory("mobile")]
        public void Input_BlankLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withBlankLogin();
            Gmail email = new(driver);
            string expected = "Введите адрес электронной почты или номер телефона.";
            
            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        [TestCategory("smoke")]
        [TestCategory("desktop")]
        [TestCategory("mobile")]
        public void Input_InappropriateLogin_GetError()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withInapropriateLogin();
            Gmail email = new(driver);
            string expected = "Не удалось найти аккаунт Google.";

            email.Navigate();
            email.SubmitLogin(user);
            string loginError = driver.FindElement(By.XPath("//div[@class='o6cuMc Jj6Lae']")).Text;

            Assert.AreEqual(expected, loginError);
        }

        [TestMethod]
        [TestCategory("smoke")]
        [TestCategory("desktop")]
        [TestCategory("mobile")]
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
        [TestCategory("desktop")]
        [TestCategory("mobile")]
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
        [TestCategory("desktop")]
        public void Input_ValidData_LoginSuccessful_Desktop()
        {
            User user = new UserCreator()
                .getGmailUser()
                .withCredentialsFromProperty();
            Gmail email = new(driver);
            string expected = user.getLogin();

            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            string accIconInfo = driver.FindElement(By.XPath("//header/div[2]/div[3]/div[1]/div[2]/div/a")).GetAttribute("aria-label");

            Assert.IsTrue(accIconInfo.Contains(expected));
        }

        [TestMethod]
        [TestCategory("smoke")]
        [TestCategory("mobile")]
        public void Input_ValidData_LoginSuccessful_Mobile()
        {
            User user = new UserCreator()
               .getGmailUser()
               .withCredentialsFromProperty();
            Gmail email = new(driver);
            string expected = user.getLogin();

            email.Navigate();
            email.SubmitLogin(user);
            email.SubmitPassword(user);
            driver.FindElement(By.XPath("//button[@class='Jn']")).Click();
            driver.FindElement(By.XPath("//div[@class='  Lf']//div[@role='button'][2]")).Click();
            IWebElement accData = driver.FindElement(By.XPath("//div[@class='xm']/div[3]/div[1]"));
            bool isValidLogin = wait.Until(ExpectedConditions.TextToBePresentInElement(accData, expected));
                
            Assert.IsTrue(isValidLogin);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string screenshotPath = $"{PathSetter.toScreenshotsDir()}{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";

                driver.TakeScreenshot().SaveAsFile(screenshotPath);
                TestContext.AddResultFile(screenshotPath);
            }
        }
    }
}
