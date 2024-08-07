﻿using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class CommercialResourceViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string section, (string, string)[]? links, bool displayHeading = true)
    {
        return View(new CommercialResourceViewModel(section, links, displayHeading));
    }
}