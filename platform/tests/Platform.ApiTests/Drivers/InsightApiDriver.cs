using TechTalk.SpecFlow.Infrastructure;

namespace Platform.ApiTests.Drivers;

public class InsightApiDriver : ApiDriver
{
    public InsightApiDriver(ISpecFlowOutputHelper output) : base(TestConfiguration.Insight, output)
    {
    }
}