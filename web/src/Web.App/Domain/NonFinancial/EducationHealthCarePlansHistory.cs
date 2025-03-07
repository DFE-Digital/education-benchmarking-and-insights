// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain.NonFinancial;

public record EducationHealthCarePlansHistory<T>
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public T[]? Plans { get; set; }
}