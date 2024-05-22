using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;
namespace Web.App.ViewComponents;

public class RagStack : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, int red, int amber, int green, int? height = null) => View(new RagStackViewModel(identifier, red, amber, green, height ?? 25));
}