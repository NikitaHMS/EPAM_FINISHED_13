using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tool;
using Model;
using OpenQA.Selenium.Support.Extensions;

namespace Tests
{
    [TestClass]
    public class ProtonLogInTests
    {
        public TestContext TestContext { get; set; }
        private static IWebDriver driver;
        private static WebDriverWait wait;

        [ClassInitialize]
        public static void OneTimeSetUp(TestContext context)
        {
            string browser = Environment.GetEnvironmentVariable("browser");

            driver = DriverSetter.getDriver(browser);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.0));

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
        }



        [TestMethod]
        [TestCategory("smoke")]
        public void Input_InvalidLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withInvalidLogin();
            Proton email = new(driver);
            string expected = "Неверные учетные данные для входа. Попробуйте снова.";

            email.Navigate();
            email.LogIn(user);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[@name='loginForm']/div[@data-testid='login:error-block']")));
            IWebElement loginError = driver.FindElement(By.XPath("//form[@name='loginForm']/div[@data-testid='login:error-block']"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_InvalidPassword_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withInvalidPassword();
            Proton email = new(driver);
            string expected = "Пароль неверен. Пожалуйста, попробуйте другой пароль.";

            email.Navigate();
            email.LogIn(user);
            IWebElement passwordError = driver.FindElement(By.XPath("//form[@name='loginForm']/div[@data-testid='login:error-block']"));

            Assert.AreEqual(expected, passwordError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_EmptyLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withEmptyLogin();
            Proton email = new(driver);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement loginError = driver.FindElement(By.XPath("//div[@id='id-3']/span"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_BlankLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withBlankLogin();
            Proton email = new(driver);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement loginError = driver.FindElement(By.XPath("//div[@id='id-3']/span"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_InappropriateLogin_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withInapropriateLogin();
            Proton email = new(driver);
            string expected = "Недопустимое имя пользователя";

            email.Navigate();
            email.LogIn(user);
            wait.Until(ExpectedConditions.ElementIsVisible((By.XPath("//span[@class='notification__content']"))));
            IWebElement loginError = driver.FindElement(By.XPath("//span[@class='notification__content']"));

            Assert.AreEqual(expected, loginError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_EmptyPassword_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withEmptyPassword();
            Proton email = new(driver);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement passwordError = driver.FindElement(By.XPath("//div[@id='id-4']/span"));

            Assert.AreEqual(expected, passwordError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_BlankPassword_GetError()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withBlankPassword();
            Proton email = new(driver);
            string expected = "Это обязательное поле";

            email.Navigate();
            email.LogIn(user);
            IWebElement passwordError = driver.FindElement(By.XPath("//div[@id='id-4']/span"));

            Assert.AreEqual(expected, passwordError.Text);
        }

        [TestMethod]
        [TestCategory("smoke")]
        public void Input_ValidData_LoginSuccessful()
        {
            User user = new UserCreator()
                .getProtonUser()
                .withCredentialsFromProperty();
            Proton email = new(driver);
            string expected = user.getLogin();

            email.Navigate();
            email.LogIn(user);
            driver.FindElement(By.XPath("//button[@data-testid='explore-mail']")).Click();
            IWebElement loginConfirm = driver.FindElement(By.XPath("//span[contains(@class, 'user-dropdown-email')]"));

            Assert.AreEqual(expected, loginConfirm.Text);
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