using MicroBatchFramework;
using Microsoft.Extensions.Logging;
using System;

namespace Base64UrlCoreTool.Tests
{
    public class TestStringLogger : ILogger<BatchEngine>
    {
        readonly LogLevel minLogLevel;
        public string Result { get; set; }

        public TestStringLogger(LogLevel minLogLevel) => this.minLogLevel = minLogLevel;

        public IDisposable BeginScope<TState>(TState state) => NullDisposable.Instance;

        public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.None ? false : (int)logLevel >= (int)this.minLogLevel;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            var msg = formatter(state, exception);

            if (!string.IsNullOrEmpty(msg))
            {
                Result = msg;
            }

            if (exception != null)
            {
                Result = exception.ToString();
            }
        }

        class NullDisposable : IDisposable
        {
            public static readonly IDisposable Instance = new NullDisposable();
            public void Dispose() { }
        }
    }
}
