using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;
namespace Web.App.ViewComponents;

public class ComparatorSetDetailsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string identifier, bool hasUserDefinedSet, bool hasCustomData, bool hasMissingComparatorSet = false, ComparatorSetType type = ComparatorSetType.Costs) => View(new ComparatorSetDetailsViewModel(identifier, hasUserDefinedSet, hasCustomData, hasMissingComparatorSet, type));
}