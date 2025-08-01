﻿using Web.App.Domain.Content;

namespace Web.App.ViewModels.Components;

public class BannerViewModel(Banner? banner, string? columnClass = null)
{
    public string? Title => banner?.Title;
    public string? Heading => banner?.Heading ?? banner?.Body;
    public string? Body => banner?.Heading == null ? null : banner?.Body;
    public string ColumnClass => columnClass ?? "govuk-grid-column-full";
}