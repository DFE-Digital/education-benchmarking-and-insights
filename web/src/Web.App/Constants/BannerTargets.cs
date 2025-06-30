using Web.App.Attributes;
using Web.App.ViewComponents;

namespace Web.App;

public static class BannerTargets
{
    /// <summary>
    ///     Corresponds to banner to show on LA home page.
    /// </summary>
    public const string LocalAuthorityHome = nameof(LocalAuthorityHome);

    /// <summary>
    ///     Corresponds to banner to show on Academy or Maintained school home page.
    ///     Must be used in conjunction with <c>ViewData[ViewDataKeys.IsPartOfTrust]</c>.
    /// </summary>
    public const string SchoolHomePrefix = "SchoolHome-";

    /// <summary>
    ///     Corresponds to banner to show on service home page.
    /// </summary>
    public const string ServiceHome = nameof(ServiceHome);

    /// <summary>
    ///     Corresponds to banner to show on Trust home page.
    /// </summary>
    public const string TrustHome = nameof(TrustHome);

    /// <summary>
    ///     Used to pass value from <see cref="ServiceBannerAttribute.Target" /> in middleware
    ///     to context, for consumption by <see cref="BannerViewComponent" />.
    /// </summary>
    public const string Key = "__BannerTarget";
}