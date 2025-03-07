// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain.NonFinancial;

public record LocalAuthorityNumberOfPlansYear : LocalAuthorityNumberOfPlans
{
    public int? Year { get; set; }
}