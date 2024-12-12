using Avalonia.Media;
using System;
using System.Diagnostics;

namespace VR_Tools_UI.Functions;

public static class Priority
{
    public static (string content, Avalonia.Media.IBrush color) SetPriority(string processName, string executableName, ProcessPriorityClass priority)
    {
        string message = "null";
        Avalonia.Media.IBrush color = Brushes.Crimson;

        Process[] process = Process.GetProcessesByName(processName);
        if (process.Length == 0)
        {
            (message, color) = ($"{executableName} is not running", Brushes.Crimson);
            Debug.WriteLine(message);
        }
        else
        {
            foreach (Process proc in process)
            {
                Debug.WriteLine("Changing Priority for: " + proc.Id + " To " + Convert.ToString(priority));
                proc.PriorityClass = priority;
                if (proc.PriorityClass == priority)
                {
                    (message, color) = ($"{executableName} priority changed to {Convert.ToString(priority)}!", Brushes.LightGreen);
                    Debug.WriteLine(message);
                }
            }
        }
        return (message, color);
    }
}