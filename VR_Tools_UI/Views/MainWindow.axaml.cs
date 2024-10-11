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
    public void Set_Priority_Button(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;
        Debug.WriteLine(source!.Name);

        string content;
        Avalonia.Media.IBrush color;

        switch (source.Name)
        {
            case "OVRServer_x64":
                (content, color) = Change_Priority.Set_Priority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "OVRRedir":
                (content, color) = Change_Priority.Set_Priority("OVRRedir", "OVRRedir.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "OculusDash":
                (content, color) = Change_Priority.Set_Priority("OculusDash", "OculusDash.exe", ProcessPriorityClass.High);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "BS_Realtime":
                (content, color) = Change_Priority.Set_Priority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.RealTime);
                Output.Content = content;
                Output.Foreground = color;
                break;
            case "BS_High":
                (content, color) = Change_Priority.Set_Priority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.High);
                Output.Content = content;
                Output.Foreground = color;
                break;
        }
    }
    public void ASW_Enable(object source, RoutedEventArgs args)
    {
        var (content, color) = Change_Registry.Edit_Registry("delete_value");
        Output.Content = content;
        Output.Foreground = color;
    }
    public void ASW_Disable(object source, RoutedEventArgs args)
    {
        var (content, color) = Change_Registry.Edit_Registry("create_value");
        Output.Content = content;
        Output.Foreground = color;
    }
}
