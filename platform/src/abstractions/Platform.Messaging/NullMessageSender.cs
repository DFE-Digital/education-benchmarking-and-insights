using System.Diagnostics.CodeAnalysis;

namespace Platform.Messaging;

[ExcludeFromCodeCoverage]
public class NullMessageSender : IMessageSender
{
    public Task Send<T>(T data)
    {
        return Task.CompletedTask;
    }
}