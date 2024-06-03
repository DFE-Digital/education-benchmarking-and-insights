using System.ComponentModel.DataAnnotations;
using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsByNameViewModel(School school, ComparatorSetUserDefined set)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public ComparatorSetUserDefined Set => set;
}

public record SchoolComparatorsByNameAddViewModel
{
    [Required(ErrorMessage = "Select a school from the suggester")]
    public string? Urn { get; init; }
}