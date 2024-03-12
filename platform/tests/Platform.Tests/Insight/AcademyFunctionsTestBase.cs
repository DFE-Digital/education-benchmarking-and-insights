using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight;

public class AcademyFunctionsTestBase : FunctionsTestBase
{
    protected readonly AcademyFunctions Functions;
    protected readonly Mock<IAcademyDb> Db;

    protected AcademyFunctionsTestBase()
    {
        Db = new Mock<IAcademyDb>();
        Functions = new AcademyFunctions(new NullLogger<AcademyFunctions>(), Db.Object);
    }
}