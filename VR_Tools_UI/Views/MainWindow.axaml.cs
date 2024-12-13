﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia.Enums;
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
    public void ASWEnable(object source, RoutedEventArgs args)
    {
        var (message, type) = Registry.EditRegistry(false);
        Log.Insert(0, new LogMessage(message, type));
    }
    public void ASWDisable(object source, RoutedEventArgs args)
    {
        var (message, type) = Registry.EditRegistry(true);
        Log.Insert(0, new LogMessage(message, type));
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
                        var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
public class LogMessage
{
    public string Message { get; set; }
    public string Type { get; set; }

    public LogMessage(string message, string type)
    {
        Message = message;
        Type = type;
    }
}