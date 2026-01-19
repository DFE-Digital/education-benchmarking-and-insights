using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ComparatorSetDetailsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
        string identifier,
        bool hasUserDefinedSet,
        bool hasCustomData,
        bool hasMissingComparatorSet = false,
        ComparatorSetType type = ComparatorSetType.Costs,
        bool showAsBulletedList = false,
        bool showCustomDataOption = true)
    {
        var model = new ComparatorSetDetailsViewModel(identifier, hasUserDefinedSet, hasCustomData, hasMissingComparatorSet, type, showCustomDataOption);

        return showAsBulletedList
            ? View("BulletedList", model)
            : View(model);
    }
}