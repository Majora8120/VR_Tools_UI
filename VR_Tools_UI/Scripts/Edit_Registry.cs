#pragma warning disable CA1416 // Validate platform compatibility
using Avalonia.Media;
using Microsoft.Win32;
using System;

namespace VR_Tools_UI.Scripts
{
    public static class Change_Registry
    {
        public static (string message, Avalonia.Media.IBrush color) Edit_Registry(string registry_edit_option)
        {
            string message = "null";
            Avalonia.Media.IBrush color = Brushes.Crimson;

            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus") is null)
            {
                (message, color) = (@"HKEY_LOCAL_MACHINE\SOFTWARE\Oculus is null. Is Oculus Link installed?", Brushes.Crimson);
            }
            else if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus") is not null)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus", true) ?? throw new ArgumentNullException(nameof(key), "How TF is this null!");
                switch (registry_edit_option)
                {
                    case "create_value":
                        if (key.GetValue("AswDisabled")?.ToString() is not "1")
                        {
                            key.SetValue(@"AswDisabled", 1, RegistryValueKind.DWord);

                            (message, color) = ("Registry value created!", Brushes.LightGreen);
                        }
                        else
                        {
                            (message, color) = ("Registry value already exists!", Brushes.Crimson);
                        }
                        break;
                    case "delete_value":
                        if (key.GetValue("AswDisabled")?.ToString() is not null)
                        {
                            key.DeleteValue("AswDisabled");

                            (message, color) = ("Registry value deleted!", Brushes.LightGreen);
                        }
                        else
                        {
                            (message, color) = ("Registry value already deleted!", Brushes.Crimson);
                        }
                        break;
                }
                key.Close();
            }
            return (message, color);
        }
    }
}
