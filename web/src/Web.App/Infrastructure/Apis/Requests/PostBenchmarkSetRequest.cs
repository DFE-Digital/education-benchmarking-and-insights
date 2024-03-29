using System.Diagnostics.CodeAnalysis;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Web.App.Infrastructure.Apis;

public enum FinanceTypes
{
    MaintainedSchools,
    Academies,
    AllSchools
}

public enum CharacteristicVariableTypes
{
    Array,
    Range,
    Value
}

[ExcludeFromCodeCoverage]
public class PostBenchmarkSetRequest
{
    public string? Urn { get; set; } //Lead (your) school

    public FinanceTypes FinanceType { get; set; }
    public bool ExcludeIncompleteDataSet { get; set; } = true; //Exclude schools that don't have a complete set of financial data
    public int? LocalAuthorityId { get; set; }
    public int? NoOfSchools { get; set; }

    public Dictionary<string, CharacteristicVariable>? Characteristics { get; set; }
}

[ExcludeFromCodeCoverage]
[JsonConverter(typeof(JsonSubtypes), nameof(Kind))]
[JsonSubtypes.FallBackSubTypeAttribute(typeof(ValueCharacteristic))]
public abstract class CharacteristicVariable
{
    public virtual CharacteristicVariableTypes? Kind => null;
}

[ExcludeFromCodeCoverage]
public class ArrayCharacteristic : CharacteristicVariable
{
    public override CharacteristicVariableTypes? Kind => CharacteristicVariableTypes.Array;
    public string[]? Values { get; set; }
}

[ExcludeFromCodeCoverage]
public class RangeCharacteristic : CharacteristicVariable
{
    public override CharacteristicVariableTypes? Kind => CharacteristicVariableTypes.Range;
    public string? ValueFrom { get; set; }
    public string? ValueTo { get; set; }
}

[ExcludeFromCodeCoverage]
public class ValueCharacteristic : CharacteristicVariable
{
    public override CharacteristicVariableTypes? Kind => CharacteristicVariableTypes.Value;
    public string? Value { get; set; }
}