using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight;

[ExcludeFromCodeCoverage]
public record DbOptions
{
    [Required(ErrorMessage = "Missing financial plan collection name")] 
    public string? FinancialPlanCollectionName { get; set; }
    
    [Required(ErrorMessage = "Missing latest CFR year")] 
    public int? CfrLatestYear { get; set; }
    
    [Required(ErrorMessage = "Missing latest AAR year")]
    public int? AarLatestYear { get; set; }
    
    [Required(ErrorMessage = "Missing establishment collection name")] 
    public string? EstablishmentCollectionName { get; set; }
    
    public string? FloorAreaCollectionName { get; set; }
}