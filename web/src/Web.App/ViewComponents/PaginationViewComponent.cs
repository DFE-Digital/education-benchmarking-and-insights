using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(int totalResults, int pageNumber, int pageSize, string pageQuery, string path, string query)
    {
        return View(new PaginationViewModel(totalResults, pageNumber, pageSize, pageQuery, path, query));
    }
}