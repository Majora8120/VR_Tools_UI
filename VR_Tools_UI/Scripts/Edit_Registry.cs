#pragma warning disable CA1416 // Validate platform compatibility
using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace VR_Tools_UI.Scripts
{
    public static class Change_Registry
    {
        public static void Edit_Registry(string registry_edit_option)
        {
            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus") is null)
            {
                Debug.WriteLine(@"HKEY_LOCAL_MACHINE\SOFTWARE\Oculus is null. Is Oculus Link installed?");
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

                            Debug.WriteLine("Registry value created!");
                        }
                        else
                        {
                            Debug.WriteLine("Registry value already exists!");
                        }
                        break;
                    case "delete_value":
                        if (key.GetValue("AswDisabled")?.ToString() is not null)
                        {
                            key.DeleteValue("AswDisabled");

                            Debug.WriteLine("Registry value deleted!");
                        }
                        else
                        {
                            Debug.WriteLine("Registry value already deleted!");
                        }
                        break;
                }
                key.Close();
            }
        }
    }
}
