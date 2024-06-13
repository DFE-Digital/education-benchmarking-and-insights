using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustComparatorsViewModel(
    Trust trust,
    string? by = null,
    TrustCharacteristicUserDefined[]? userDefined = null,
    string? userDefinedSetId = null)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public string? By => by;
    public string? UserDefinedSetId => userDefinedSetId;

    public IEnumerable<TrustCharacteristicUserDefined> UserDefinedTrusts => userDefined ?? [];
}