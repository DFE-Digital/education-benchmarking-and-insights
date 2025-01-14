namespace Platform.Messaging;

public interface IMessageSender
{
    Task Send<T>(T data);
}