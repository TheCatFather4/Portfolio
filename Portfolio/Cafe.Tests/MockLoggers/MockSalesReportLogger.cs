using Cafe.BLL.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.Tests.MockLoggers
{
    public class MockSalesReportLogger : ILogger<SalesReportService>
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            
        }
    }
}