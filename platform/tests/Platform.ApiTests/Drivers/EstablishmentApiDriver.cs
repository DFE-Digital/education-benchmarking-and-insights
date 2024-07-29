using TechTalk.SpecFlow.Infrastructure;
namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class EstablishmentApiDriver(ISpecFlowOutputHelper output) : ApiDriver(TestConfiguration.Establishment, output);