using MsBox.Avalonia.Enums;
using System;
using System.Diagnostics;

namespace VR_Tools_UI.Scripts;

public static class Change_Priority
{
    public static void Set_Priority(string process_name, string executable_name, ProcessPriorityClass priority)
    {
        Process[] process = Process.GetProcessesByName(process_name);
        if (process.Length == 0)
        {
            Debug.WriteLine($"{executable_name} is not running");
            Message_Box.Message("Error", $"{executable_name} is not running", ButtonEnum.Ok);
        }
        else
        {
            foreach (Process proc in process)
            {
                Debug.WriteLine("Changing Priority for: " + proc.Id + " To " + Convert.ToString(priority));
                proc.PriorityClass = priority;
                if (proc.PriorityClass == priority)
                {
                    Debug.WriteLine($"{executable_name} Priority Changed To " + Convert.ToString(priority) + "!");
                    Message_Box.Message("Success", $"{executable_name} Priority Changed To " + Convert.ToString(priority) + "!", ButtonEnum.Ok);
                }
            }
        }
    }
}