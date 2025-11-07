// ReSharper disable NotAccessedPositionalProperty.Global

using System.Runtime.Serialization;

namespace Web.App.Domain;

public class KS4ProgressBandings : ISerializable
{
    public enum Banding
    {
        WellBelowAverage,
        BelowAverage,
        Average,
        AboveAverage,
        WellAboveAverage
    }

    private readonly Dictionary<string, Banding> _items = new();

    public KS4ProgressBandings(IEnumerable<KeyValuePair<string, string?>> bandings)
    {
        foreach (var (key, value) in bandings)
        {
            var banding = value.ToBanding();
            if (banding.HasValue)
            {
                _items[key] = banding.Value;
            }
        }
    }

    public KS4ProgressBanding[] Items => _items
        .Select(d => new KS4ProgressBanding(d.Key, d.Value))
        .ToArray();

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Items), Items);
    }
}

[Serializable]
public record KS4ProgressBanding(string Urn, KS4ProgressBandings.Banding Banding);

public static class BandingExtensions
{
    private const string WellBelowAverage = "Well below average";
    private const string BelowAverage = "Below average";
    private const string Average = "Average";
    private const string AboveAverage = "Above average";
    private const string WellAboveAverage = "Well above average";

    public static KS4ProgressBandings.Banding? ToBanding(this string? banding) => banding switch
    {
        WellBelowAverage => KS4ProgressBandings.Banding.WellBelowAverage,
        BelowAverage => KS4ProgressBandings.Banding.BelowAverage,
        Average => KS4ProgressBandings.Banding.Average,
        AboveAverage => KS4ProgressBandings.Banding.AboveAverage,
        WellAboveAverage => KS4ProgressBandings.Banding.WellAboveAverage,
        _ => null
    };

    public static string ToStringValue(this KS4ProgressBandings.Banding banding) => banding switch
    {
        KS4ProgressBandings.Banding.WellBelowAverage => WellBelowAverage,
        KS4ProgressBandings.Banding.BelowAverage => BelowAverage,
        KS4ProgressBandings.Banding.Average => Average,
        KS4ProgressBandings.Banding.AboveAverage => AboveAverage,
        KS4ProgressBandings.Banding.WellAboveAverage => WellAboveAverage,
        _ => throw new ArgumentOutOfRangeException(nameof(banding))
    };
}