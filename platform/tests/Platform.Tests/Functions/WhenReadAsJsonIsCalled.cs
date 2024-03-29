﻿using System.Text;
using Platform.Functions.Extensions;
using Xunit;

namespace Platform.Tests.Functions;

public class WhenReadAsJsonIsCalled
{
    [Fact]
    public void ReturnsObjectMatchingType()
    {
        var context = new DefaultHttpContext();
        var jsonContent = new TestObjectType("testValue").ToJson();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

        var result = context.Request.ReadAsJson<TestObjectType>();

        Assert.NotNull(result);
        Assert.IsType<TestObjectType>(result);
        Assert.Equal("testValue", result.TestProp);
    }


    [Fact]
    public void IfResultIsNullThrowsArgumentNullException()
    {
        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(""));

        Assert.Throws<ArgumentNullException>(() => context.Request.ReadAsJson<TestObjectType>());
    }
}



public class TestObjectType
{
    public TestObjectType(string testProp)
    {
        TestProp = testProp;
    }

    public string TestProp { get; }
}