using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

[ExcludeFromCodeCoverage]
public class EmptyResponseBody() : ApiResponseBody(Array.Empty<byte>());