using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public class FormAction
{
    public const string Reset = "reset";
    public const string Remove = "remove";
    public const string Add = "add";
    public const string Continue = "continue";

    public string? Action { get; private set; }
    public string? Identifier { get; private set; }

    public static implicit operator FormAction(string v)
    {
        if (v.StartsWith(Remove))
        {
            var comps = v.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var identifier = comps[1];

            return new FormAction { Action = Remove, Identifier = identifier };
        }

        return new FormAction { Action = v };
    }
}