#pragma warning disable CA1416 // Validate platform compatibility
using Avalonia.Media;
using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace VR_Tools_UI.Functions;

public static class Registry
{
    public static (string message, Avalonia.Media.IBrush color) EditRegistry(bool disableASW)
    {
        string message = "null";
        Avalonia.Media.IBrush color = Brushes.Crimson;

        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus") is null)
        {
            (message, color) = (@"HKEY_LOCAL_MACHINE\SOFTWARE\Oculus is null. Is Oculus Link installed?", Brushes.Crimson);
            Debug.WriteLine(message);
        }
        else if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus") is not null)
        {
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus", true) ?? throw new ArgumentNullException(nameof(key), "How TF is this null!");
            switch (disableASW)
            {
                case true:
                    if (key.GetValue("AswDisabled")?.ToString() is not "1")
                    {
                        key.SetValue(@"AswDisabled", 1, RegistryValueKind.DWord);

                        (message, color) = ("Registry value created!", Brushes.LightGreen);
                        Debug.WriteLine(message);
                    }
                    else
                    {
                        (message, color) = ("Registry value already exists!", Brushes.Crimson);
                        Debug.WriteLine(message);
                    }
                    break;
                case false:
                    if (key.GetValue("AswDisabled")?.ToString() is not null)
                    {
                        key.DeleteValue("AswDisabled");

                        (message, color) = ("Registry value deleted!", Brushes.LightGreen);
                        Debug.WriteLine(message);
                    }
                    else
                    {
                        (message, color) = ("Registry value doesn't exist!", Brushes.Crimson);
                        Debug.WriteLine(message);
                    }
                    break;
            }
            key.Close();
        }
        return (message, color);
    }
}