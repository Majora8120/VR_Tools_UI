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
    public void Priority_Button_1(object source, RoutedEventArgs args)
    {
        Change_Priority.Set_Priority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.RealTime);
    }
    public void Priority_Button_2(object source, RoutedEventArgs args)
    {
        Change_Priority.Set_Priority("OVRRedir", "OVRRedir.exe", ProcessPriorityClass.RealTime);
    }
    public void Priority_Button_3(object source, RoutedEventArgs args)
    {
        Change_Priority.Set_Priority("OculusDash", "OculusDash.exe", ProcessPriorityClass.High);
    }
    public void Priority_Button_4(object source, RoutedEventArgs args)
    {
        Change_Priority.Set_Priority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.RealTime);
    }
    public void Priority_Button_5(object source, RoutedEventArgs args)
    {
        Change_Priority.Set_Priority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.High);
    }
    public void ASW_Enable(object source, RoutedEventArgs args)
    {
        Change_Registry.Edit_Registry("delete_value");
    }
    public void ASW_Disable(object source, RoutedEventArgs args)
    {
        Change_Registry.Edit_Registry("create_value");
    }
}
