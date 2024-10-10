using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace VR_Tools_UI.Scripts
{
    public static class Message_Box
    {
        public static void Message(string Caption, string Text, ButtonEnum Button_Type)
        {
            var box = MessageBoxManager
            .GetMessageBoxStandard(Caption, Text, Button_Type);
            box.ShowAsync();
        }
    }
}