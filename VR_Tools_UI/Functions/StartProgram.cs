using System.Diagnostics;
using System.IO;

namespace VR_Tools_UI.Functions;

public static class StartProgram
{
    public static (string message, string type) Start(string name, string path)
    {
        string message = "null";
        string type = "ERROR";

        if (File.Exists(path) == false)
        {
            (message, type) = ($"{name} doesn't exist in {path}", "ERROR");
            Debug.WriteLine($"{name} doesn't exist in {path}");
            return (message, type);
        }

        Process[] process = Process.GetProcessesByName(name);
        if (process.Length != 0)
        {
            (message, type) = ($"{name} is already running", "ERROR");
            Debug.WriteLine($"{name} is already running");
            return (message, type);
        }
        else
        {
            Process.Start(path);
            (message, type) = ($"Started {name}", "INFO");
            return (message, type);
        }
    }
}