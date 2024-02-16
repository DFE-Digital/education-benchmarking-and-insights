using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public class ComparatorSetAction
{
    public const string Reset = "reset";
    public const string Remove = "remove";

    public string? Action { get; private set; }
    public string? Urn { get; private set; }

    public static implicit operator ComparatorSetAction(string v)
    {
        if (v.StartsWith(Remove))
        {
            var comps = v.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var urn = comps[1];

            return new ComparatorSetAction { Action = Remove, Urn = urn };
        }

        return new ComparatorSetAction { Action = Reset };
    }
}