using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long totalResults, int pageNumber, int pageSize, Func<int, string?> urlBuilder)
    {
        return View(new PaginationViewModel(totalResults, pageNumber, pageSize, urlBuilder));
    }
}