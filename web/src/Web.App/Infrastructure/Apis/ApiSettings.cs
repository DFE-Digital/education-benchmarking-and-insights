using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.App.Infrastructure.Apis;

[ExcludeFromCodeCoverage]
public class ApiSettings
{
    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Key { get; set; }

    [Required(ErrorMessage = "Api {0} is mandatory")]
    public string? Url { get; set; }
}