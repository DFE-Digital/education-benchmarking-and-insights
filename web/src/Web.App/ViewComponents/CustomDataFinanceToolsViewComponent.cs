﻿using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class CustomDataFinanceToolsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, FinanceTools[] tools)
    {
        return View(new FinanceToolsViewModel(identifier, tools));
    }
}
