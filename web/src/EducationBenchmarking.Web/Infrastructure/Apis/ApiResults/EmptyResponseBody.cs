using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

[ExcludeFromCodeCoverage]
public class EmptyResponseBody : ApiResponseBody
{
    public EmptyResponseBody() : base(Array.Empty<byte>())
    {
    }
}