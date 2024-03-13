using System.Diagnostics.CodeAnalysis;

namespace Web.App.Infrastructure.Apis;

[ExcludeFromCodeCoverage]
public class EmptyResponseBody() : ApiResponseBody(Array.Empty<byte>());