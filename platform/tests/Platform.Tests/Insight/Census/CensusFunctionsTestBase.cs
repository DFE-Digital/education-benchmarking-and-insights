using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight.Census;

public class CensusFunctionsTestBase : FunctionsTestBase
{
    protected readonly CensusFunctions Functions;
    protected readonly Mock<ICensusDb> Db;

    protected CensusFunctionsTestBase()
    {
        Db = new Mock<ICensusDb>();
        Functions = new CensusFunctions(new NullLogger<CensusFunctions>(), Db.Object);
    }
}