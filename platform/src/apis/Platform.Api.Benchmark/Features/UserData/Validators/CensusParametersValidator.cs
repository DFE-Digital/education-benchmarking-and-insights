using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Benchmark.Features.UserData.Parameters;

namespace Platform.Api.Benchmark.Features.UserData.Validators;

[ExcludeFromCodeCoverage]
public class UserDataParametersValidator : AbstractValidator<UserDataParameters>
{
    public UserDataParametersValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
    }
}