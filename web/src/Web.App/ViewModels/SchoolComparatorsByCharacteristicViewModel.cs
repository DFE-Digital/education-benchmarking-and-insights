using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsByCharacteristicViewModel(
    School school,
    SchoolCharacteristic? characteristic,
    UserDefinedSchoolCharacteristicViewModel? userDefinedCharacteristic)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolCharacteristic? Characteristic => characteristic;
    public UserDefinedSchoolCharacteristicViewModel Data => userDefinedCharacteristic ?? new UserDefinedSchoolCharacteristicViewModel(characteristic);
}