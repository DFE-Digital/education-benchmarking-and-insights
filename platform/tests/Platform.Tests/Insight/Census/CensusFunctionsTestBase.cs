using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight;
using Platform.Api.Insight.Census;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight.Census;

public class CensusFunctionsTestBase : FunctionsTestBase
{
    protected readonly CensusFunctions Functions;
    protected readonly Mock<ICensusService> Service;

    protected CensusFunctionsTestBase()
    {
        Service = new Mock<ICensusService>();
        Functions = new CensusFunctions(new NullLogger<CensusFunctions>(), Service.Object);
    }
}