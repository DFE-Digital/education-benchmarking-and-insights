// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;

public record LocalAuthorityNumberOfPlansYear : LocalAuthorityNumberOfPlans
{
    public int? Year { get; set; }
}