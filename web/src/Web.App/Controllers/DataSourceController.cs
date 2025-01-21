using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Infrastructure.Storage;
using Web.App.ViewModels;
using File = Web.App.Domain.File;

namespace Web.App.Controllers;

[Controller]
[Route("data-sources")]
public class DataSourceController(ILogger<DataSourceController> logger, IDataSourceStorage storage, IFilesApi filesApi)
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

            var sas = storage.GetAccessToken();

            var files = await filesApi.GetTransparencyFiles().GetResultOrDefault<File[]>() ?? [];
            academies = files
                .Where(f => f.Type == "transparency-aar")
                .OrderByDescending(x => x.Label)
                .Select(x => BuildViewModel(x.Label, x.FileName, sas));

            maintainedSchools = files
                .Where(f => f.Type == "transparency-cfr")
                .OrderByDescending(x => x.Label)
                .Select(x => BuildViewModel(x.Label, x.FileName, sas));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred displaying data sources: {DisplayUrl}", Request.GetDisplayUrl());
        }

        var vm = new DataSourceViewModel(academies, maintainedSchools);
        return View(vm);
    }

    private static DataSourceFileViewModel BuildViewModel(string? description, string? fileName, SharedAccessTokenModel accessTokenModel) => new()
    {
        DisplayText = description,
        Link = string.IsNullOrWhiteSpace(fileName) ? null : BuildFileUri(accessTokenModel, fileName)
    };

    private static Uri BuildFileUri(SharedAccessTokenModel accessTokenModel, string fileName) => new($"{accessTokenModel.ContainerUri}/{fileName}{accessTokenModel.SasToken}");
}