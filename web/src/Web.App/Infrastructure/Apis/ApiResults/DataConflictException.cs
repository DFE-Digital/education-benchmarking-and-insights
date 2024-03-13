using System.Diagnostics.CodeAnalysis;

namespace Web.App.Infrastructure.Apis;

[ExcludeFromCodeCoverage]
public sealed class DataConflictException : Exception
{
    public ConflictData Details { get; }

    public DataConflictException(ConflictData data)
        : base(
            $"Unable to create {data.Type} with {data.Id}. {data.Type}:{data.Id} already exists, originally created at {data.CreatedAt:G}")
    {
        data.Message = Message;
        Details = data;
    }
}