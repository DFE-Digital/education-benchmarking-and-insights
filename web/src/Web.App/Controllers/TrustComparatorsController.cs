using Microsoft.AspNetCore.Mvc;
namespace Web.App.Controllers;

[Controller]
[Route("trust/{companyNumber}/comparators")]
public class TrustComparatorsController(ILogger<TrustComparatorsController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index(string companyNumber) => new StatusCodeResult(StatusCodes.Status302Found);
}