// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain;

public record HistoryComparison<T>
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public T[]? School { get; set; }
    public T[]? ComparatorSetAverage { get; set; }
    public T[]? NationalAverage { get; set; }
}