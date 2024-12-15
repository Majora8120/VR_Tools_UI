using System;
using System.Diagnostics;

namespace VR_Tools_UI.Functions;

public static class Priority
{
    public static LogMessage SetPriority(string processName, string executableName, ProcessPriorityClass priority)
    {
        string message = "null";
        string type = "ERROR";

        Process[] process = Process.GetProcessesByName(processName);
        if (process.Length == 0)
        {
            (message, type) = ($"{executableName} is not running", "ERROR");
        }
        else
        {
            foreach (Process proc in process)
            {
                proc.PriorityClass = priority;
                if (proc.PriorityClass == priority)
                {
                    (message, type) = ($"{executableName} priority changed to {Convert.ToString(priority)}", "INFO");
                }
            }
        }
        return new LogMessage(message, type);
    }
}