using System.Runtime.CompilerServices;
using System.Xml;

namespace Tool
{
    public class UserDataManager
    {
        private static XmlDocument userData;
        private static string environment;
        private static string path;

        private static XmlDocument UserData
        {
            get 
            {
                if (userData == null)
                {
                    path = PathSetter.toUserDataFile();
                    userData = new XmlDocument();
                    userData.Load(path);
                }
                
                return userData;
            }
        }

        public static void SaveChanges()
        {
            UserData.Save(path);
        }

        public static void SetEnvironment(string env)
        {
            environment = env;
        }

        public static string getProtonUsername()
        {
            return UserData.SelectSingleNode($"/userdata/proton/username[@env='{environment}']").InnerText;
        }
        public static string getProtonPassword()
        {
            return UserData.SelectSingleNode($"/userdata/proton/password[@env='{environment}']").InnerText;
        }

        public static string getGmailUsername()
        {
            return UserData.SelectSingleNode($"/userdata/gmail/username[@env='{environment}']").InnerText;
        }
        public static string getGmailPassword()
        {
            return UserData.SelectSingleNode($"/userdata/gmail/password[@env='{environment}']").InnerText;
        }
    }
}
