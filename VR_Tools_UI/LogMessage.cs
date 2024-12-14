namespace VR_Tools_UI
{
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
}
