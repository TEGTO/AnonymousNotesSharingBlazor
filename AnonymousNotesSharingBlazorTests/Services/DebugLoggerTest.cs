using System.Diagnostics;

namespace AnonymousNotesSharingBlazorTests.Services
{
    public class DebugLoggerTest
    {
        [Test]
        public void Error_WritesToDebugConsole()
        {
            // Arrange
            var logger = new DebugLogger();
            var message = "Test error message";
            var writer = new StringWriter();
            Trace.Listeners.Add(new TextWriterTraceListener(writer));
            // Act
            logger.Error(message);
            // Assert
            Trace.Flush(); 
            var output = writer.ToString().Trim(); 
            StringAssert.Contains(message, output);
        }
    }
}
