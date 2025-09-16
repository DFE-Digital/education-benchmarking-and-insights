using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Infrastructure.Extensions;
using Web.App.Infrastructure.WebAssets;
using Web.App.ViewModels;
using File = Web.App.Domain.Content.File;

namespace Web.App.Controllers;

[Controller]
[Route("data-sources")]
public class DataSourceController(ILogger<DataSourceController> logger, IOptions<WebAssetsOptions> options, IFilesApi filesApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<DataSourceFileViewModel> academies = [];
        IEnumerable<DataSourceFileViewModel> maintainedSchools = [];

        try
        {
            ViewData[ViewDataKeys.UseJsBackLink] = true;

            var files = await filesApi.GetTransparencyFiles().GetResultOrDefault<File[]>() ?? [];
            academies = files
                .Where(f => f.Type == "transparency-aar")
                .OrderByDescending(x => x.Label)
                .Select(x => BuildViewModel(x.Label, x.FileName, options.Value.FilesBaseUrl));

            maintainedSchools = files
                .Where(f => f.Type == "transparency-cfr")
                .OrderByDescending(x => x.Label)
                .Select(x => BuildViewModel(x.Label, x.FileName, options.Value.FilesBaseUrl));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred displaying data sources: {DisplayUrl}", Request.GetDisplayUrl());
        }

        var vm = new DataSourceViewModel(academies, maintainedSchools);
        return View(vm);
    }

    private static DataSourceFileViewModel BuildViewModel(string? description, string? fileName, string? filesBaseUrl) => new()
    {
        DisplayText = description,
        Link = string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(filesBaseUrl)
            ? null
            : BuildFileUri(filesBaseUrl, fileName)
    };

    private static Uri BuildFileUri(string filesBaseUrl, string fileName) => new($"{filesBaseUrl.TrimEnd('/')}/{fileName}", UriKind.RelativeOrAbsolute);
}