using AutoFixture;
using Xunit;

namespace Web.Integration.Tests;

public abstract class PageBase(BenchmarkingWebAppClient client) : IClassFixture<BenchmarkingWebAppClient>
{
    public Fixture Fixture { get; } = new();
    protected BenchmarkingWebAppClient Client => client;
}