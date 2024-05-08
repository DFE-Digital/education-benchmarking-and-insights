using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight.SchoolMetric;

public class SchoolMetricFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolMetricFunctions Functions;
    protected readonly Mock<ISchoolMetricsDb> Db;

    protected SchoolMetricFunctionsTestBase()
    {
        Db = new Mock<ISchoolMetricsDb>();
        Functions = new SchoolMetricFunctions(new NullLogger<SchoolMetricFunctions>(), Db.Object);
    }
}