using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
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
        return View(new ChooseSchoolViewModel());
    }
    
    [HttpPost]
    public IActionResult Index([FromForm]ChooseSchoolViewModel viewModel)
    {
        using (_logger.BeginScope(new { viewModel }))
        {
            try
            {
                if (string.IsNullOrWhiteSpace(viewModel.Urn) || string.IsNullOrEmpty(viewModel.Search))
                {
                    var message = string.IsNullOrEmpty(viewModel.Search)
                        ? "Enter a school name select a school"
                        : "Please select school from the suggester";
                    ModelState.AddModelError("Search", message);
                    return View(viewModel);
                }
                
                return RedirectToAction("Index","School", new {urn = viewModel.Urn});
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred choosing a school: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
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
                    var text = value.Text.Replace("*", "");
                    
                    var additionalDetails = new List<string>();
                    
                    if(!string.IsNullOrWhiteSpace(value.Document.Town)) additionalDetails.Add(text == value.Document.Town ? value.Text : value.Document.Town );
                    if(!string.IsNullOrWhiteSpace(value.Document.Postcode)) additionalDetails.Add(text == value.Document.Postcode ? value.Text : value.Document.Postcode);

                    if (text != value.Document.Name)
                    {
                        value.Text = value.Document.Name;
                    }
                    
                    var additionalText = additionalDetails.Count > 0
                        ? $" ({string.Join(", ", additionalDetails.Select(a => a))})"
                        : "";
                    
                    value.Text = $"{value.Text}{additionalText}";
                    
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