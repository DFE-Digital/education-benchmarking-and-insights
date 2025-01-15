using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Api.Benchmark.UserData;
using Platform.Test;
namespace Platform.Benchmark.Tests;

public class ComparatorSetsFunctionsTestBase : FunctionsTestBase
{
    protected readonly Mock<IComparatorSetsService> ComparatorSetsService;
    protected readonly ComparatorSetsFunctions Functions;
    protected readonly Mock<IValidator<ComparatorSetUserDefinedSchool>> SchoolValidator;
    protected readonly Mock<IValidator<ComparatorSetUserDefinedTrust>> TrustValidator;
    protected readonly Mock<IUserDataService> UserDataService;

    protected ComparatorSetsFunctionsTestBase()
    {
        ComparatorSetsService = new Mock<IComparatorSetsService>();
        UserDataService = new Mock<IUserDataService>();
        SchoolValidator = new Mock<IValidator<ComparatorSetUserDefinedSchool>>();
        TrustValidator = new Mock<IValidator<ComparatorSetUserDefinedTrust>>();
        Functions = new ComparatorSetsFunctions(
            ComparatorSetsService.Object,
            UserDataService.Object,
            new NullLogger<ComparatorSetsFunctions>(),
            SchoolValidator.Object,
            TrustValidator.Object);
    }
}