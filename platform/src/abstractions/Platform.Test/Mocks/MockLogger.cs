using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

namespace Platform.Test.Mocks;

public abstract class MockLogger
{
    public static Mock<ILogger<T>> Create<T>(ITestOutputHelper testOutputHelper)
    {
        var logger = new Mock<ILogger<T>>();

        logger.Setup(l => l.BeginScope(It.IsAny<Dictionary<string, object>>()))
            .Callback((Dictionary<string, object> state) =>
            {
                testOutputHelper.WriteLine("Scope:");
                foreach (var (key, value) in state)
                {
                    testOutputHelper.WriteLine($"{key} = {value}");
                }

                testOutputHelper.WriteLine(string.Empty);
            });

        logger.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!))
            .Callback(new InvocationAction(invocation =>
            {
                var logLevel = (LogLevel)invocation.Arguments[0];
                var state = invocation.Arguments[2];
                var exception = (Exception)invocation.Arguments[3];
                var formatter = invocation.Arguments[4];

                var invokeMethod = formatter.GetType().GetMethod("Invoke");
                var logMessage = (string)invokeMethod?.Invoke(formatter, [state, exception])!;

                testOutputHelper.WriteLine($"{logLevel} - {logMessage}");
            }));

        return logger;
    }
}