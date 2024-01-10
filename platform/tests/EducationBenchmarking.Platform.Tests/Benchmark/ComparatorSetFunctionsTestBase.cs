using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Domain.Requests;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class ComparatorSetFunctionsTestBase : FunctionsTestBase
{
    protected readonly ComparatorSetFunctions Functions;
    protected readonly Mock<IComparatorSetDb> Db;
    protected readonly Mock<IValidator<ComparatorSetRequest>> Validator;

    protected ComparatorSetFunctionsTestBase()
    {
        Db = new Mock<IComparatorSetDb>();
        Validator = new Mock<IValidator<ComparatorSetRequest>>();
        Functions = new ComparatorSetFunctions(Db.Object, new NullLogger<ComparatorSetFunctions>(), Validator.Object);
    }
}