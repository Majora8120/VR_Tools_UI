using Avalonia.Media;
using System;
using System.Diagnostics;

namespace VR_Tools_UI.Scripts;

public static class Change_Priority
{
    public static (string content, Avalonia.Media.IBrush color) Set_Priority(string process_name, string executable_name, ProcessPriorityClass priority)
    {
        string message = "null";
        Avalonia.Media.IBrush color = Brushes.Crimson;

        Process[] process = Process.GetProcessesByName(process_name);
        if (process.Length == 0)
        {
            (message, color) = ($"{executable_name} is not running", Brushes.Crimson);
        }
        else
        {
            foreach (Process proc in process)
            {
                Debug.WriteLine("Changing Priority for: " + proc.Id + " To " + Convert.ToString(priority));
                proc.PriorityClass = priority;
                if (proc.PriorityClass == priority)
                {
                    (message, color) = ($"{executable_name} priority changed to {Convert.ToString(priority)}!", Brushes.LightGreen);
                }
            }
        }
        return (message, color);
    }
}