using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ComparatorSetDetailsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, bool hasUserDefinedSet, bool hasCustomData, bool hasMissingComparatorSet = false, ComparatorSetType type = ComparatorSetType.Costs, bool showAsBulletedList = false)
    {
        var model = new ComparatorSetDetailsViewModel(identifier, hasUserDefinedSet, hasCustomData, hasMissingComparatorSet, type);

        return showAsBulletedList
            ? View("BulletedList", model)
            : View(model);
    }
}