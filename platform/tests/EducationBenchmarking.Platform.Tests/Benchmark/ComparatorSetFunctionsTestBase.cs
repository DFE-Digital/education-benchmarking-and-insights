using AutoFixture;
using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Api.Benchmark.Requests;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class ComparatorSetFunctionsTestBase : FunctionsTestBase
{
    protected ComparatorSetFunctions Functions;
    protected Mock<IComparatorSetDb> Db;
    protected Mock<IValidator<ComparatorSetRequest>> Validator;
    protected Fixture Fixture;

    public ComparatorSetFunctionsTestBase()
    {
        Db = new Mock<IComparatorSetDb>();
        Validator = new Mock<IValidator<ComparatorSetRequest>>();
        Functions = new ComparatorSetFunctions(Db.Object, new NullLogger<ComparatorSetFunctions>(), Validator.Object );
        Fixture = new Fixture();
    }
}