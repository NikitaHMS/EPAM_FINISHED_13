using OpenQA.Selenium.DevTools.V119.DOM;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Tool;


namespace Tests
{
    public static class Setup
    {
        private static bool isOn = true;
        private static string choice;

         public static void Main()
        {
            while (isOn)
            {

                Console.WriteLine(PathSetter.toSolutionDir());
                Console.WriteLine("\n" + "------Test Setup------" + "\n");

                Console.WriteLine($"1 Start tests");
                Console.WriteLine($"2 Choose Browser.       Current: {PropertiesManager.getBrowser()}");
                Console.WriteLine($"3 Choose Environment.   Current: {PropertiesManager.getEnvironment()}");
                Console.WriteLine($"4 Run with Jenkins?     Current: No");

                Console.Write("\n" + ">");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        
                        isOn = false;

                        PropertiesManager.saveChanges();

                        string scriptArgument = $"cd {PathSetter.toSolutionDir()}; dotnet test"; 
                        string scriptFileName = "powershell.exe";
                        ProcessStartInfo scriptStartInfo = new(scriptFileName, scriptArgument);

                        scriptStartInfo.CreateNoWindow = false;

                        Process scriptProcess = new();
                        scriptProcess.StartInfo = scriptStartInfo;
                        scriptProcess.Start();

                        break;

                    case "2":

                        string[] availableBrowsers = PropertiesManager.getAvailableBrowsers();

                        Console.WriteLine("\n" + "Available browsers: " + "\n");

                        foreach (string browser in availableBrowsers)
                        {
                            Console.WriteLine("\t" + browser);
                        }

                        Console.Write("\n" + ">");
                        choice = Console.ReadLine().ToLower();
                        if (availableBrowsers.Contains(choice) == false)
                        {
                            Console.WriteLine("\n" + $"The browser \"{choice}\" does not exist.");
                            goto case "2";
                        }
                        else
                        {
                            PropertiesManager.setBrowser(choice);
                        }
                        break;

                    case "3":

                        string[] availableEnvironments = PropertiesManager.getAvailableEnvironments();

                        Console.WriteLine("\n" + "Available environments: " + "\n");

                        foreach (string environment in availableEnvironments)
                        {
                            Console.WriteLine("\t" + environment);
                        }

                        Console.Write("\n" + ">");
                        choice = Console.ReadLine().ToLower();
                        if (availableEnvironments.Contains(choice) == false)
                        {
                            Console.WriteLine("\n" + $"The environment \"{choice}\" does not exist.");
                            goto case "3";
                        }
                        else
                        {
                            PropertiesManager.setEnvironment(choice);
                        }

                        break;

                    case "4":

                        Console.WriteLine(PathSetter.toPropertiesFile());
                        break;

                    default:

                        Console.WriteLine("Invalid input.");

                        break;
                }

            }
        }
    }
}
