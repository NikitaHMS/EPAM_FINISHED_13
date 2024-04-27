using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tool;
using Model;
using Util;
using OpenQA.Selenium.Support.Extensions;

namespace Tests
{
    [Ignore]
    [TestClass]
    public class GmailProtonInteractionTests
    {
        public TestContext TestContext { get; set; }
        private static IWebDriver driver;
        private static WebDriverWait wait;

        private static User userProton;
        private static User userGmail;

        private static Proton emailProton;
        private static Gmail emailGmail;

        private static Letter letter;

        [ClassInitialize]
        public static void OneTimeSetUp(TestContext context)
        {
            string? browser = Environment.GetEnvironmentVariable("browser");

            userProton = new UserCreator()
                .getProtonUser()
                .withCredentialsFromProperty();
            userGmail = new UserCreator()
                .getGmailUser()
                .withCredentialsFromProperty();

            driver = DriverSetter.getDriver(browser);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60.0));

            emailGmail = new Gmail(driver);
            emailProton = new Proton(driver);

            letter = new Letter();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            
            driver.Quit();
        }


        [TestMethod]
        public void A_Gmail_SendRandomLetterToProton_LetterIsSent()
        {
            string recipient = userProton.getLogin();
            string expected = "Сообщение отправлено.";                                  
            
            emailGmail.Navigate();
            emailGmail.LogIn(userGmail);
            emailGmail.SendLetter(recipient, letter);
            bool isSent = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("span[class='bAq']"), expected));

            Assert.IsTrue(isSent);
        }

        [TestMethod]
        public void B_Proton_ValidateLetterArrival_HasArrived()
        {
            string subject = letter.getSubject();
            
            emailProton.Navigate();
            emailProton.LogIn(userProton);
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@data-shortcut-target='item-container-wrapper'][1]/div")));
            IWebElement latestLetterData = driver.FindElement(By.XPath("//div[@data-shortcut-target='item-container-wrapper'][1]/div"));
            bool hasArrived = latestLetterData.GetAttribute("data-testid").Contains(subject);

            Assert.IsTrue(hasArrived);
        }

        [TestMethod]
        public void C_Proton_ValidateLetterUnread_IsUnread()
        {
            IWebElement latestLetterData = driver.FindElement(By.XPath("//div[@data-shortcut-target='item-container-wrapper'][1]/div"));
            bool isUnread = latestLetterData.GetAttribute("class").Contains("unread");

            Assert.IsTrue(isUnread);
        }

        [TestMethod]
        public void D_Proton_ValidateCorrectSender_IsCorrect()
        {
            string expected = userGmail.getLogin();

            IWebElement senderEMail = driver.FindElement(By.XPath("//div[@data-testid='message-list-loaded']/div[3]/div[1]/div[1]//span[@title][@data-testid='message-column:sender-address']"));
            string sender = senderEMail.GetAttribute("title");

            Assert.AreEqual(expected, sender);
        }

        [TestMethod]
        public void E_Proton_VerifyLetterContentMatching_IsMatching()
        {
            string expected = letter.getContent();

            emailProton.OpenLatestLetter();
            IWebDriver frame = driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@data-testid='content-iframe']")));
            IWebElement letterContent = frame.FindElement(By.XPath("//body/div/div/div/div[1]"));
            string content = letterContent.Text;

            Assert.AreEqual(expected, content);
        }

        [TestMethod]
        public void F_Proton_SendReplyWithNewUserAlias_IsSent()
        {
            string alias = StringUtils.getRandomAlias();
            string expected = alias;

            emailProton.SendReply(alias);
            Thread.Sleep(3000);                                                     // Waiting for the letter to be sent 
            driver.Navigate().GoToUrl("https://mail.proton.me/u/0/all-sent");
            emailProton.OpenLatestLetter();
            IWebDriver frame = driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@data-testid='content-iframe']")));
            IWebElement sentText = frame.FindElement(By.XPath("//body/div/div/div/div[1]"));
            string sentAlias = sentText.Text;

            Assert.AreEqual(expected, sentAlias);
        }

        [TestMethod]
        public void G_Gmail_LogInChangeAliasVerifyAlias_SentAliasIsSet()
        {
            string expected;
            string newAlias;
            
            emailGmail.Navigate();
            emailGmail.LogIn(userGmail);
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div[class='aRI']")));        
            emailGmail.OpenLatestLetter();                                                                                      
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='a3s aiL ']/div[1]")));
            newAlias = driver.FindElement(By.XPath("//div[@class='a3s aiL ']/div[1]")).Text;
            emailGmail.ChangeAlias(newAlias);
            expected = driver.FindElement(By.XPath("//div[@data-index='1']//div[@class='gWjfMb']")).Text;

            Assert.AreEqual(expected, newAlias);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string screenshotPath = $"{PathSetter.toScreenshotsDir()}{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";

                driver.TakeScreenshot().SaveAsFile(screenshotPath);
            }
        }
    }
}
