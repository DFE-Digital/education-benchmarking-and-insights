using System.Net;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public sealed class ErrorApiResult(HttpStatusCode status) : ApiResult(status);