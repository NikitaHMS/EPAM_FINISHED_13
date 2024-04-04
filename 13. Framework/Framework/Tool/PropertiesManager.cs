using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V119.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tool
{
    public class PropertiesManager
    {
        private static XmlDocument properties;
        private static string path = PathSetter.toPropertiesFile();
        private static string browser;
        private static string environment;

        private static XmlDocument Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = new XmlDocument();
                    properties.Load(path);
                }
                
                return properties;
            }
        }
        private static string Browser
        {
            get
            {
                if (browser != Properties.SelectSingleNode("/properties/testdata/browser").InnerText)
                {
                    browser = Properties.SelectSingleNode("/properties/testdata/browser").InnerText;
                }

                return browser;
            }
        }
        private static string Environment
        {
            get
            {
                if (environment != Properties.SelectSingleNode("/properties/testdata/environment").InnerText)
                {
                    environment = Properties.SelectSingleNode("/properties/testdata/environment").InnerText;
                }

                return environment;
            }
        }

        public static void saveChanges()
        {
            Properties.Save(path);
        }

        public static string getProtonUsername()
        {
            return Properties.SelectSingleNode($"/properties/userdata/proton/username[@env='{Environment}']").InnerText;
        }
        public static string getProtonPassword()
        {
            return Properties.SelectSingleNode($"/properties/userdata/proton/password[@env='{Environment}']").InnerText;
        }

        public static string getGmailUsername()
        {
            return Properties.SelectSingleNode($"/properties/userdata/gmail/username[@env='{Environment}']").InnerText;
        }
        public static string getGmailPassword()
        {
            return Properties.SelectSingleNode($"/properties/userdata/gmail/password[@env='{Environment}']").InnerText;
        }

        public static string getBrowser()
        {
            return Browser;
        }

        public static string getEnvironment()
        {
            return Environment;
        }

        public static void setBrowser(string browser)
        {
            Properties.SelectSingleNode("/properties/testdata/browser").InnerText = browser;        
        }

        public static void setEnvironment(string env)
        {
            Properties.SelectSingleNode("/properties/testdata/environment").InnerText = env;
        }

        public static string[] getAvailableBrowsers()
        {
            string[] browsers = Properties.SelectSingleNode("/properties/available/browsers").InnerText.Replace("\r\n", "").Trim().Split("\t");

            return browsers;
        }

        public static string[] getAvailableEnvironments()
        {
            string[] environments = Properties.SelectSingleNode("/properties/available/environments").InnerText.Replace("\r\n", "").Trim().Split("\t");

            return environments;
        }
    }
}
