using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public static class PathSetter
    {
        private static string fullPath = Environment.CurrentDirectory;
        private static string propertiesPath = @"resources\properties.xml";
        private static string screenshotsPath = @"screenshots\";
        private static string newPath;

        public static string toPropertiesFile()
        {
            return setPath(propertiesPath);
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
            newPath = location;
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
