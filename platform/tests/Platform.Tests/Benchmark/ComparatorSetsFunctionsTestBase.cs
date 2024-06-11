using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;

namespace Platform.Tests.Benchmark;

public class ComparatorSetsFunctionsTestBase : FunctionsTestBase
{
    protected readonly ComparatorSetsFunctions Functions;
    protected readonly Mock<IComparatorSetsService> Service;
    protected readonly Mock<IValidator<ComparatorSetUserDefinedSchool>> SchoolValidator;
    protected readonly Mock<IValidator<ComparatorSetUserDefinedTrust>> TrustValidator;

    protected ComparatorSetsFunctionsTestBase()
    {
        Service = new Mock<IComparatorSetsService>();
        SchoolValidator = new Mock<IValidator<ComparatorSetUserDefinedSchool>>();
        TrustValidator = new Mock<IValidator<ComparatorSetUserDefinedTrust>>();
        Functions = new ComparatorSetsFunctions(Service.Object, new NullLogger<ComparatorSetsFunctions>(), SchoolValidator.Object, TrustValidator.Object);
    }
}