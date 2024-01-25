using EducationBenchmarking.Platform.ApiTests.TestSupport;
using TechTalk.SpecFlow.Infrastructure;

namespace EducationBenchmarking.Platform.ApiTests.Drivers;

public class EstablishmentApiDriver : ApiDriver
{
    public EstablishmentApiDriver(ISpecFlowOutputHelper output) : base(Config.Apis.Establishment, output)
    {
    }
}