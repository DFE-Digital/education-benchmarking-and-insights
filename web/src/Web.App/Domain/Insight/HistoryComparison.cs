// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain;

public record HistoryComparison<T>
{
    public T[]? School { get; set; }
    public T[]? ComparatorSetAverage { get; set; }
    public T[]? NationalAverage { get; set; }
}