using System.ComponentModel.DataAnnotations;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class ApiSettings
{
    public const string SchoolApi = "School";

    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Key { get; set; }

    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Url { get; set; }
}