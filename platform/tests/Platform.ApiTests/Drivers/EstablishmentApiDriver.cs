using TechTalk.SpecFlow.Infrastructure;

namespace Platform.ApiTests.Drivers;

public class EstablishmentApiDriver : ApiDriver
{
    public EstablishmentApiDriver(ISpecFlowOutputHelper output) : base(TestConfiguration.Establishment, output)
    {
    }
}