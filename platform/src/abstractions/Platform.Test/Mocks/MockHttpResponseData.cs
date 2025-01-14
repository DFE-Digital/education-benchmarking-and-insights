﻿using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Platform.Functions.Tests.Mocks;

public class MockHttpResponseData(FunctionContext functionContext) : HttpResponseData(functionContext)
{
    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    public override HttpHeadersCollection Headers { get; set; } = [];

    public override Stream Body { get; set; } = new MemoryStream();

    public override HttpCookies Cookies { get; } = new MockHttpCookies();
}