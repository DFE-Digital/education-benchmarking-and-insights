using System.Net;

namespace Web.App.Infrastructure.Apis;

public sealed class CancelledApiResult() : ApiResult((HttpStatusCode)499);