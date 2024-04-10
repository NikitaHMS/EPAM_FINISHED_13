using System.Xml;

namespace Tool
{
    public class UserDataManager
    {
        private static XmlDocument userData;
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


        public static string getProtonUsername()
        {
            return UserData.SelectSingleNode($"/userdata/proton/username").InnerText;
        }
        public static string getProtonPassword()
        {
            return UserData.SelectSingleNode($"/userdata/proton/password").InnerText;
        }

        public static string getGmailUsername()
        {
            return UserData.SelectSingleNode($"/userdata/gmail/username").InnerText;
        }
        public static string getGmailPassword()
        {
            return UserData.SelectSingleNode($"/userdata/gmail/password").InnerText;
        }
    }
}
