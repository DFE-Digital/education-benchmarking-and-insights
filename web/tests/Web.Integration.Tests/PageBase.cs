using AutoFixture;
using Xunit;

namespace Web.Integration.Tests;

public abstract class PageBase<T>(T client) : IClassFixture<T> where T : BenchmarkingWebAppClient
{
    public Fixture Fixture { get; } = new();
    protected T Client => client;
}