using System.Diagnostics;
using Tool;

namespace Tests
{
    public static class Setup
    {
        private static bool isOn = true;
        private static bool isJenkins = false;
        private static string choice;

         public static void Main()
        {
            while (isOn)
            {
                Console.WriteLine("\n" + "------Test Setup------" + "\n");

                Console.WriteLine($"1 Start tests");
                Console.WriteLine($"2 Choose Browser.       Current: {PropertiesManager.getBrowser()}");
                Console.WriteLine($"3 Choose Environment.   Current: {PropertiesManager.getEnvironment()}");
                Console.WriteLine($"4 Run with Jenkins?     Current: {(isJenkins ? "Yes" : "No")}");

                Console.Write("\n" + ">");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        if (isJenkins == false)
                        {
                            PropertiesManager.saveChanges();

                            string scriptArgument = $"cd {PathSetter.toSolutionDir()}; dotnet test";
                            string scriptFileName = "powershell.exe";
                            ProcessStartInfo scriptStartInfo = new(scriptFileName, scriptArgument);

                            scriptStartInfo.CreateNoWindow = false;

                            Process scriptProcess = new();
                            scriptProcess.StartInfo = scriptStartInfo;
                            scriptProcess.Start();
                        }
                        else
                        {
                            Process proc = new();
                            proc.StartInfo.UseShellExecute = true;
                            proc.StartInfo.FileName = "localhost:8080/job/Framework/build?token=FrmwrkTkn";
                            proc.Start();
                        }

                        isOn = false;

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

                        Console.WriteLine("\n" + "Run tests in Jenkins?" + "\n");
                        Console.WriteLine("1 Yes");
                        Console.WriteLine("2 No");
                        Console.Write("\n" + ">");
                        choice = Console.ReadLine();

                        if (choice == "1")
                        {
                            isJenkins = true;
                        }
                        else if (choice == "2")
                        {
                            isJenkins = false;
                        }
                        else
                        {
                            Console.WriteLine("\n" + "Invalid input.");
                            goto case "4";
                        }

                        break;

                    default:

                        Console.WriteLine("\n" + "Invalid input.");

                        break;
                }

            }
        }
    }
}
