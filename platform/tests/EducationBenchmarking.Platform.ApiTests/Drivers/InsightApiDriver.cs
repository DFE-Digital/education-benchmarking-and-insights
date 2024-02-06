using TechTalk.SpecFlow.Infrastructure;

namespace EducationBenchmarking.Platform.ApiTests.Drivers;

public class InsightApiDriver : ApiDriver
{
    public InsightApiDriver(ISpecFlowOutputHelper output) : base(TestConfiguration.Apis.Insight, output)
    {
    }
}