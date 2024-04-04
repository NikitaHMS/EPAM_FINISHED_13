using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CommonEmail
    {
        protected readonly IWebDriver browser;
        protected WebDriverWait wait;
        public CommonEmail(IWebDriver browser)
        {
            this.browser = browser;
            wait = new WebDriverWait(browser, TimeSpan.FromSeconds(60.0));
        }
    }
}
