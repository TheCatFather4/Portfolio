﻿using Cafe.BLL.Services;
using Microsoft.Extensions.Logging;

namespace Cafe.Tests.MockLoggers
{
    public class MockShoppingBagLogger : ILogger<ShoppingBagService>
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}