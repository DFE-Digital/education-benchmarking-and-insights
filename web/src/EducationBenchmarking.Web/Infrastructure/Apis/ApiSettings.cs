using System.ComponentModel.DataAnnotations;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class ApiSettings
{
    public const string InsightApi = "Insight";
    public const string EstablishmentApi = "Establishment";

    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Key { get; set; }

    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Url { get; set; }
}