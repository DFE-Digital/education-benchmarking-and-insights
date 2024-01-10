using System.Net;
using EducationBenchmarking.Platform.Domain;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Platform.Functions
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
