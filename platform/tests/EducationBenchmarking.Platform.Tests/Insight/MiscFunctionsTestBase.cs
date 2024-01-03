using AutoFixture;
using EducationBenchmarking.Platform.Api.Insight;
using EducationBenchmarking.Platform.Api.Insight.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class MiscFunctionsTestBase : FunctionsTestBase
{
    protected MiscFunctions Functions;
    protected Fixture Fixture;

    public MiscFunctionsTestBase()
    {
        Functions = new MiscFunctions();
        Fixture = new Fixture();
    }
}