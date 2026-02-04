using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class RemovableItemCardGridViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(RemovableItemCardViewModel[] cards, string inputName, string id) =>
        View(new RemovableItemCardGridViewModel
        {
            Cards = cards,
            InputName = inputName,
            Id = id
        });
}