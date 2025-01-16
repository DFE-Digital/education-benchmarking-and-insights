using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Test;

namespace Platform.Benchmark.Tests;

public class ComparatorSetsFunctionsTestBase : FunctionsTestBase
{
    protected readonly Mock<IComparatorSetsService> ComparatorSetsService;
    protected readonly ComparatorSetsFunctions Functions;
    protected readonly Mock<IValidator<ComparatorSetUserDefinedSchool>> SchoolValidator;
    protected readonly Mock<IValidator<ComparatorSetUserDefinedTrust>> TrustValidator;

    protected ComparatorSetsFunctionsTestBase()
    {
        ComparatorSetsService = new Mock<IComparatorSetsService>();
        SchoolValidator = new Mock<IValidator<ComparatorSetUserDefinedSchool>>();
        TrustValidator = new Mock<IValidator<ComparatorSetUserDefinedTrust>>();
        Functions = new ComparatorSetsFunctions(
            ComparatorSetsService.Object,
            new NullLogger<ComparatorSetsFunctions>(),
            SchoolValidator.Object,
            TrustValidator.Object);
    }
}