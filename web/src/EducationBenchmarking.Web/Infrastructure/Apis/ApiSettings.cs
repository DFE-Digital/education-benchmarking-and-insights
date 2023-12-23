using System.ComponentModel.DataAnnotations;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class ApiSettings
{
    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Key { get; set; }

    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Url { get; set; }
}