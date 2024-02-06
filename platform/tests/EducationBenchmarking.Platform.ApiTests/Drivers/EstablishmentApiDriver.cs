using TechTalk.SpecFlow.Infrastructure;

namespace EducationBenchmarking.Platform.ApiTests.Drivers;

public class EstablishmentApiDriver : ApiDriver
{
    public EstablishmentApiDriver(ISpecFlowOutputHelper output) : base(TestConfiguration.Apis.Establishment, output)
    {
    }
}