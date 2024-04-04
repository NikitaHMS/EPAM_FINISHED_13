using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Model;
using OpenQA.Selenium.DevTools;

namespace Tool
{
    public class UserCreator
    {
        private static string login;
        private static string password; 


        public UserCreator getProtonUser()
        {
            login = PropertiesManager.getProtonUsername();
            password = PropertiesManager.getProtonPassword();
            return this;
        }

        public UserCreator getGmailUser()
        {
            login = PropertiesManager.getGmailUsername();
            password = PropertiesManager.getGmailPassword();
            return this;
        }

        public User withCredentialsFromProperty() { return new User(login, password); }

        public User withEmptyLogin() { return new User("", password); }

        public User withEmptyPassword() { return new User(login, ""); }


        public User withInvalidLogin() { return new User("InvalidLogin@wow.com", password); }

        public User withInvalidPassword() { return new User(login, "InvalidPassword123"); }


        public User withBlankLogin() { return new User(" ", password); }

        public User withBlankPassword() { return new User(login, " "); }


        public User withInapropriateLogin() { return new User("`", password); }
    }
}
