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
        WellAboveAverage,
        Unknown
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

    public KS4ProgressBanding? this[string? urn]
    {
        get
        {
            if (urn is null)
            {
                return null;
            }

            return _items.TryGetValue(urn, out var banding)
                ? new KS4ProgressBanding(urn, banding)
                : null;
        }
    }

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
    private const string Unknown = "Unknown";

    public static KS4ProgressBandings.Banding? ToBanding(this string? banding)
    {
        if (string.IsNullOrWhiteSpace(banding))
        {
            return null;
        }

        return banding switch
        {
            WellBelowAverage => KS4ProgressBandings.Banding.WellBelowAverage,
            BelowAverage => KS4ProgressBandings.Banding.BelowAverage,
            Average => KS4ProgressBandings.Banding.Average,
            AboveAverage => KS4ProgressBandings.Banding.AboveAverage,
            WellAboveAverage => KS4ProgressBandings.Banding.WellAboveAverage,
            _ => KS4ProgressBandings.Banding.Unknown
        };
    }

    public static string ToStringValue(this KS4ProgressBandings.Banding banding) => banding switch
    {
        KS4ProgressBandings.Banding.WellBelowAverage => WellBelowAverage,
        KS4ProgressBandings.Banding.BelowAverage => BelowAverage,
        KS4ProgressBandings.Banding.Average => Average,
        KS4ProgressBandings.Banding.AboveAverage => AboveAverage,
        KS4ProgressBandings.Banding.WellAboveAverage => WellAboveAverage,
        _ => Unknown
    };

    public static string ToGdsColour(this KS4ProgressBandings.Banding banding) => banding switch
    {
        KS4ProgressBandings.Banding.WellBelowAverage => "red",
        KS4ProgressBandings.Banding.BelowAverage => "orange",
        KS4ProgressBandings.Banding.Average => "yellow",
        KS4ProgressBandings.Banding.AboveAverage => "blue",
        KS4ProgressBandings.Banding.WellAboveAverage => "turquoise",
        _ => "grey"
    };
}