using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using VR_Tools_UI.Functions;
using VR_Tools_UI.ViewModels;

namespace VR_Tools_UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void SetPriorityButton(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;
        Debug.WriteLine(source!.Name);

        string content;
        Avalonia.Media.IBrush color;

        switch (source.Name)
        {
            case "OVRServer_x64":
                (content, color) = Priority.SetPriority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "OVRRedir":
                (content, color) = Priority.SetPriority("OVRRedir", "OVRRedir.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "OculusDash":
                (content, color) = Priority.SetPriority("OculusDash", "OculusDash.exe", ProcessPriorityClass.High);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "BS_Realtime":
                (content, color) = Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "BS_High":
                (content, color) = Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.High);
                Output.Content = content;
                Output.Foreground = color;
                break;
        }
    }
    public void ASWEnable(object source, RoutedEventArgs args)
    {
        var (content, color) = Registry.EditRegistry(false);
        Output.Content = content;
        Output.Foreground = color;
    }
    public void ASWDisable(object source, RoutedEventArgs args)
    {
        var (content, color) = Registry.EditRegistry(true);
        Output.Content = content;
        Output.Foreground = color;
    }
}