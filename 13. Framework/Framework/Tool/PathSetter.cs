namespace Tool
{
    public static class PathSetter
    {
        private static string fullPath = Environment.CurrentDirectory;
        private static string userDataPath = @"resources\userData.xml";
        private static string screenshotsPath = @"screenshots\";

        public static string toUserDataFile()
        {
            return setPath(userDataPath);
        }

        public static string toScreenshotsDir()
        {
            return setPath(screenshotsPath);
        }

        public static string toSolutionDir()
        {   
            return setPath("");
        }

        private static string setPath(string location)
        {
            string newPath = location;
            int dirCount = 0;
            string[] splitPath = fullPath.Split(@"\");

            Array.Reverse(splitPath);

            foreach (string elem in splitPath)
            {
                if (elem == "Framework")
                {
                    break;
                }

                dirCount++;
            }

            while (dirCount != 0)
            {
                newPath = @"..\" + newPath;
                dirCount--;
            }

            return newPath;
        }
    }
}
