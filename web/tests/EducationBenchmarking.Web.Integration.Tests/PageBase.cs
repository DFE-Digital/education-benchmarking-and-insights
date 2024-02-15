using AutoFixture;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class PageBase(BenchmarkingWebAppClient client) : IClassFixture<BenchmarkingWebAppClient>
{
    public Fixture Fixture { get; } = new();
    protected BenchmarkingWebAppClient Client => client;
}