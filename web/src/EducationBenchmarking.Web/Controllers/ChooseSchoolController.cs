using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("choose-school")]
public class ChooseSchoolController : Controller
{
    private readonly IEstablishmentApi _establishmentApi;
    private readonly ILogger<ChooseSchoolController> _logger;

    public ChooseSchoolController(IEstablishmentApi establishmentApi, ILogger<ChooseSchoolController> logger)
    {
        _establishmentApi = establishmentApi;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Index([FromForm]ChooseSchoolViewModel viewModel)
    {
        //TODO: Add validation for empty urn
        return string.IsNullOrWhiteSpace(viewModel.Urn) ? View() : RedirectToAction("Index","School", new {urn = viewModel.Urn});
    }
    
    [HttpGet]
    [Produces("application/json")]
    [Route("suggest")]
    public async Task<IActionResult> Suggest([FromQuery]string search, CancellationToken cancellation)
    {
        using (_logger.BeginScope(new {search}))
        {
            try
            {
                var suggestions = await _establishmentApi.SuggestSchools(search, cancellation).GetResultOrThrow<SuggestOutput<School>>();
                var results = suggestions.Results.Select(value =>
                {
                    var text = value.Text?.Replace("*", "");
                    if (text != value.Document?.Name)
                    {
                        value.Text = value.Document?.Name;
                    }

                    return value;
                });
                return new OkObjectResult(results);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Suggestion request cancelled");
                return new EmptyResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error getting suggestion: {Search}", search);
                return StatusCode(500);
            }
        }
    }
}