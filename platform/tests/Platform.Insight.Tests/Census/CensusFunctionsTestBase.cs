using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Census;
using Platform.Test;

namespace Platform.Insight.Tests.Census;

public class CensusFunctionsTestBase : FunctionsTestBase
{
    protected readonly CensusFunctions Functions;
    protected readonly Mock<ICensusService> Service;
    protected readonly Mock<ICensusValidator> Validator;
    protected readonly Fixture Fixture;

    protected CensusFunctionsTestBase()
    {
        Validator = new Mock<ICensusValidator>();
        Service = new Mock<ICensusService>();
        Fixture = new Fixture();
        Functions = new CensusFunctions(
            Service.Object,
            Validator.Object);
    }
}