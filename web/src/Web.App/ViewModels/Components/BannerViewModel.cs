using Web.App.Domain.Content;

namespace Web.App.ViewModels.Components;

public class BannerViewModel(Banner? banner)
{
    public string? Title => banner?.Title;
    public string? Heading => banner?.Heading == null && banner?.Body != null ? banner?.Body : banner?.Heading;
    public string? Body => banner?.Heading == null ? null : banner?.Body;
}