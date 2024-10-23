using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using VR_Tools_UI.Scripts;

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
                (content, color) = ChangePriority.SetPriority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "OVRRedir":
                (content, color) = ChangePriority.SetPriority("OVRRedir", "OVRRedir.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "OculusDash":
                (content, color) = ChangePriority.SetPriority("OculusDash", "OculusDash.exe", ProcessPriorityClass.High);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "BS_Realtime":
                (content, color) = ChangePriority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "BS_High":
                (content, color) = ChangePriority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.High);
                Output.Content = content;
                Output.Foreground = color;
                break;
        }
    }
    public void ASWEnable(object source, RoutedEventArgs args)
    {
        var (content, color) = ChangeRegistry.EditRegistry("delete_value");
        Output.Content = content;
        Output.Foreground = color;
    }
    public void ASWDisable(object source, RoutedEventArgs args)
    {
        var (content, color) = ChangeRegistry.EditRegistry("create_value");
        Output.Content = content;
        Output.Foreground = color;
    }
}
