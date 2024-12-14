using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using System.Collections.ObjectModel;
using System.Diagnostics;
using VR_Tools_UI.Functions;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace VR_Tools_UI.Views;

public partial class MainWindow : Window
{
    public ObservableCollection<LogMessage> Log = new ObservableCollection<LogMessage>();
    public MainWindow()
    {
        InitializeComponent();
        DataGrid.ItemsSource = Log;
    }
    public void SetPriorityButton(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        string message, type;

        switch (source!.Name)
        {
            case "OVRServer_x64":
                (message, type) = Priority.SetPriority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.RealTime);
                Log.Insert(0, new LogMessage(message, type));
                break;
            case "OVRRedir":
                (message, type) = Priority.SetPriority("OVRRedir", "OVRRedir.exe", ProcessPriorityClass.RealTime);
                Log.Insert(0, new LogMessage(message, type));
                break;
            case "OculusDash":
                (message, type) = Priority.SetPriority("OculusDash", "OculusDash.exe", ProcessPriorityClass.High);
                Log.Insert(0, new LogMessage(message, type));
                break;
            case "BS_Realtime":
                (message, type) = Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.RealTime);
                Log.Insert(0, new LogMessage(message, type));
                break;
            case "BS_High":
                (message, type) = Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.High);
                Log.Insert(0, new LogMessage(message, type));
                break;
        }
    }
    public void ASWEnable(object sender, RoutedEventArgs args)
    {
        var (message, type) = Registry.EditRegistry(false);
        Log.Insert(0, new LogMessage(message, type));
    }
    public void ASWDisable(object sender, RoutedEventArgs args)
    {
        var (message, type) = Registry.EditRegistry(true);
        Log.Insert(0, new LogMessage(message, type));
    }
    public void OpenProgram(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        string message, type;

        switch (source!.Name)
        {
            case "OpenOculus":
                (message, type) = StartProcess.Program("OculusClient", @"C:\Program Files\Oculus\Support\oculus-client\OculusClient.exe");
                Log.Insert(0, new LogMessage(message, type));
                break;
            case "OpenOculusDebug":
                (message, type) = StartProcess.Program("OculusDebugTool", @"C:\Program Files\Oculus\Support\oculus-diagnostics\OculusDebugTool.exe");
                Log.Insert(0, new LogMessage(message, type));
                break;
            case "OpenOculusFolder":
                (message, type) = StartProcess.Explorer(@"C:\Program Files\Oculus\Support");
                Log.Insert(0, new LogMessage(message, type));
                break;
        }
    }
    public async void SwitchDash(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        LogMessage logMessage;
        string message, type;

        switch (source!.Name)
        {
            case "DashSteamVR":
                logMessage = await Dash.SwapToSteamVR();
                Log.Insert(0, logMessage);
                break;
            case "DashOculus":
                (message, type) = Dash.SwapToOculusDash();
                Log.Insert(0, new LogMessage(message, type));
                break;
        }
    }
    public async void AboutWindow(object source, RoutedEventArgs args)
    {
        var box = MessageBoxManager.GetMessageBoxCustom(
            new MessageBoxCustomParams
            {
                ButtonDefinitions = new List<ButtonDefinition>
                {
                    new ButtonDefinition { Name = "Ok" }
                },
                ContentTitle = "About",
                ContentMessage = $"Develped by Majora8120\nMade with Avalonia & MessageBox.Avalonia",
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                MaxWidth = 500,
                MaxHeight = 800,
                SizeToContent = SizeToContent.WidthAndHeight,
                ShowInCenter = true,
                Topmost = false,
                HyperLinkParams = new HyperLinkParams
                {
                    Text = "VR Tools GitHub",
                    Action = new Action(() =>
                    {
                        var url = "https://github.com/Majora8120/VR_Tools_UI";
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            using var proc = new Process { StartInfo = { UseShellExecute = true, FileName = url } };
                            proc.Start();

                            return;
                        }
                    })
                }
            });
        await box.ShowAsync();
    }
}