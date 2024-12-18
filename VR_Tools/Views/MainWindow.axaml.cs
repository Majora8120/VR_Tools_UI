using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using System.Collections.ObjectModel;
using System.Diagnostics;
using VR_Tools.Functions;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace VR_Tools.Views;

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

        LogMessage logMessage;

        switch (source!.Name)
        {
            case "OVRServer_x64":
                logMessage = Priority.SetPriority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.RealTime);
                Log.Insert(0, logMessage);
                break;
            case "OVRRedir":
                logMessage = Priority.SetPriority("OVRRedir", "OVRRedir.exe", ProcessPriorityClass.RealTime);
                Log.Insert(0, logMessage);
                break;
            case "OculusDash":
                logMessage = Priority.SetPriority("OculusDash", "OculusDash.exe", ProcessPriorityClass.High);
                Log.Insert(0, logMessage);
                break;
            case "BS_Realtime":
                logMessage = Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.RealTime);
                Log.Insert(0, logMessage);
                break;
            case "BS_High":
                logMessage = Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.High);
                Log.Insert(0, logMessage);
                break;
        }
    }
    public void ASWEnable(object sender, RoutedEventArgs args)
    {
        LogMessage logMessage = Registry.EditRegistry(false);
        Log.Insert(0, logMessage);
    }
    public void ASWDisable(object sender, RoutedEventArgs args)
    {
        LogMessage logMessage = Registry.EditRegistry(true);
        Log.Insert(0, logMessage);
    }
    public void OpenProgram(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        LogMessage logMessage;

        switch (source!.Name)
        {
            case "OpenOculus":
                logMessage = StartProcess.Program("OculusClient", @"C:\Program Files\Oculus\Support\oculus-client\OculusClient.exe");
                Log.Insert(0, logMessage);
                break;
            case "OpenOculusDebug":
                logMessage = StartProcess.Program("OculusDebugTool", @"C:\Program Files\Oculus\Support\oculus-diagnostics\OculusDebugTool.exe");
                Log.Insert(0, logMessage);
                break;
            case "OpenOculusFolder":
                logMessage = StartProcess.Explorer(@"C:\Program Files\Oculus\Support");
                Log.Insert(0, logMessage);
                break;
        }
    }
    public async void SwitchDash(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;
        LogMessage logMessage;

        switch (source!.Name)
        {
            case "DashSteamVR":
                logMessage = await Dash.SwapToSteamVR();
                Log.Insert(0, logMessage);
                break;
            case "DashOculus":
                logMessage = Dash.SwapToOculusDash();
                Log.Insert(0, logMessage);
                break;
        }
    }
    public void ServiceButton(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;
        LogMessage logMessage;

        switch (source!.Name)
        {
            case "StartService":
                logMessage = Service.StartService();
                Log.Insert(0, logMessage);
                break;
            case "StopService":
                logMessage = Service.StopService();
                Log.Insert(0, logMessage);
                break;
        }
    }
    public void AboutWindow(object source, RoutedEventArgs args)
    {
        var window = new AboutWindow();
        window.Show();
    }
}