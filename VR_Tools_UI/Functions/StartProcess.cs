using System.Diagnostics;
using System.IO;

namespace VR_Tools_UI.Functions;

public static class StartProcess
{
    public static (string message, string type) Program(string name, string path)
    {
        string message = "null";
        string type = "ERROR";

        if (File.Exists(path) == false)
        {
            (message, type) = ($"Invalid Path: {path}", "ERROR");
            Debug.WriteLine($"Invalid Path: {path}");
            return (message, type);
        }

        Process[] process = Process.GetProcessesByName(name);
        if (process.Length != 0)
        {
            (message, type) = ($"Process is already running", "ERROR");
            Debug.WriteLine($"Process is already running");
            return (message, type);
        }
        else
        {
            Process.Start(path);
            (message, type) = ($"Started {name}", "INFO");
            return (message, type);
        }
    }
    public static (string message, string type) Explorer(string path)
    {
        string message = "null";
        string type = "ERROR";

        if (Directory.Exists(path) == false)
        {
            (message, type) = ($"Invalid Path: {path}", "ERROR");
            Debug.WriteLine($"Invalid Path: {path}");
            return (message, type);
        }

        Process.Start("explorer.exe", path);
        (message, type) = ($"Opened {path}", "INFO");
        Debug.WriteLine($"Opened {path}");
        return (message, type);
    }
}