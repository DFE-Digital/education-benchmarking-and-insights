using TechTalk.SpecFlow.Infrastructure;
namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class InsightApiDriver(ISpecFlowOutputHelper output) : ApiDriver(TestConfiguration.Insight, output);