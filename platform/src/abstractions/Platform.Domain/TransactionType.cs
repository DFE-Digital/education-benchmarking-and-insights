// ReSharper disable MemberCanBePrivate.Global
namespace Platform.Domain;

public static class TransactionType
{
    public const string Budget = "budget";
    public const string Outturn = "outturn";

    public static readonly string[] All =
    [
        Budget,
        Outturn
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}