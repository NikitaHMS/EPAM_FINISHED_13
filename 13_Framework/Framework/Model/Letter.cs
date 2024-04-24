using Util;

namespace Model
{
    public class Letter
    {
        private string content;
        private string subject;

        public Letter()
        {
            content = StringUtils.getRandomContent();
            subject = "Test" + new Random().Next(1, 9999).ToString();
        }

        public string getContent()
        {
            return content;
        }
        public string getSubject()
        {
            return subject;
        }
    }
}
