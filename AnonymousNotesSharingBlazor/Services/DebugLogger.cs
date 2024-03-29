using System.Diagnostics;

namespace AnonymousNotesSharingBlazor.Services
{
    public class DebugLogger : ILogger
    {
        public void Error(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
