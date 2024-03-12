using System.Net;
using Microsoft.AspNetCore.Mvc;
using Platform.Domain.Responses;
using Platform.Functions.Extensions;

namespace Platform.Functions
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult(object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Content = obj.ToJson();
            ContentType = "application/json" + (obj is IPagedResults ? "+paged" : string.Empty);
            StatusCode = (int)statusCode;
        }
    }
}
