using EducationBenchmarking.Web.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.ViewComponents;

public class OrganisationHeader : ViewComponent
{
    public IViewComponentResult Invoke(string name)
    {
        return View(new OrganisationHeaderViewModel(name));
    }
}