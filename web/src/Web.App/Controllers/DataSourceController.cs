using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Storage;
using Web.App.ViewModels;

namespace Web.App.Controllers;


[Controller]
[Route("data-sources")]
public class DataSourceController(ILogger<DataSourceController> logger, IDataSourceStorage storage)
    : Controller
{
    private static readonly (string Description, string FileName)[] Academies = [("File1", "file.xls")];
    private static readonly (string Description, string FileName)[] MaintainedSchools = [("File1", "file.xls")];

    [HttpGet]
    public IActionResult Index()
    {
        using (logger.BeginScope(new { }))
        {
            try
            {
                ViewData[ViewDataKeys.UseJsBackLink] = true;

                var sas = storage.GetAccessToken();

                var academies = Academies.Select(x => BuildViewModel(x.Description, x.FileName, sas));
                var maintainedSchools = MaintainedSchools.Select(x => BuildViewModel(x.Description, x.FileName, sas));

                var vm = new DataSourceViewModel(academies, maintainedSchools);

                return View(vm);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred displaying data sources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private static DataSourceFileViewModel BuildViewModel(string description, string fileName, SharedAccessTokenModel accessTokenModel)
    {
        return new DataSourceFileViewModel
        {
            DisplayText = description,
            Link = BuildFileUri(accessTokenModel, fileName)
        };
    }

    private static Uri BuildFileUri(SharedAccessTokenModel accessTokenModel, string fileName)
    {
        return new Uri($"{accessTokenModel.ContainerUri}/{fileName}{accessTokenModel.SasToken}");
    }
}