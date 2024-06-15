using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparatorsByCharacteristicViewModel(
    Trust trust,
    TrustCharacteristic? characteristic,
    UserDefinedTrustCharacteristicViewModel? userDefinedCharacteristic)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public TrustCharacteristic? Characteristic => characteristic;
    public UserDefinedTrustCharacteristicViewModel Data => userDefinedCharacteristic ?? new UserDefinedTrustCharacteristicViewModel();
}