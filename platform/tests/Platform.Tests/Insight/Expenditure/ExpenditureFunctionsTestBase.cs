using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Expenditure;
namespace Platform.Tests.Insight.Expenditure;

public class ExpenditureFunctionsTestBase : FunctionsTestBase
{
    protected readonly ExpenditureFunctions Functions;
    protected readonly Mock<IExpenditureService> Service;

    protected ExpenditureFunctionsTestBase()
    {
        Service = new Mock<IExpenditureService>();
        Functions = new ExpenditureFunctions(new NullLogger<ExpenditureFunctions>(), Service.Object);
    }
}