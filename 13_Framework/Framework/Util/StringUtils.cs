namespace Util
{
    public class StringUtils
    {
        public static string getRandomContent()
        {
            char[] content = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            new Random().Shuffle(content);
            string randomContent = new(content);
            return randomContent;
        }
        public static string getRandomAlias()
        {
            string alias = "Alias_" + new Random().Next(1, 999);
            return alias;
        }
    }
}
