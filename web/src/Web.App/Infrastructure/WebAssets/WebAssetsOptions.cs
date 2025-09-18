// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.Infrastructure.WebAssets;

public record WebAssetsOptions
{
    private string? _imagesBaseUrl;

    public string? FilesBaseUrl { get; set; }

    public string? ImagesBaseUrl
    {
        get => _imagesBaseUrl;
        set
        {
            _imagesBaseUrl = value;

            // extract the host name from the absolute URL as a one-off task when then property is set
            if (Uri.TryCreate(value, UriKind.Absolute, out var webAssetsImagesBaseUri))
            {
                ImagesBaseHostName = webAssetsImagesBaseUri.Host;
            }
        }
    }

    public string? ImagesBaseHostName { get; private set; }
}