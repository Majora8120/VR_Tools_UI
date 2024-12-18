using System.Diagnostics;
using System.IO;

namespace VR_Tools.Functions;

public static class StartProcess
{
    public static LogMessage Program(string name, string path)
    {
        string message = "null";
        string type = "ERROR";

        if (File.Exists(path) == false)
        {
            (message, type) = ($"Invalid Path: {path}", "ERROR");
            return new LogMessage(message, type);
        }

        Process[] process = Process.GetProcessesByName(name);
        if (process.Length != 0)
        {
            (message, type) = ($"Process is already running", "ERROR");
            return new LogMessage(message, type);
        }
        else
        {
            Process.Start(path);
            (message, type) = ($"Opened {name}", "INFO");
            return new LogMessage(message, type);
        }
    }
    public static LogMessage Explorer(string path)
    {
        string message = "null";
        string type = "ERROR";

        if (Directory.Exists(path) == false)
        {
            (message, type) = ($"Invalid Path: {path}", "ERROR");
            return new LogMessage(message, type);
        }

        Process.Start("explorer.exe", path);
        (message, type) = ($"Opened {path}", "INFO");
        return new LogMessage(message, type);
    }
}