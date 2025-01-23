using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class ChartingViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

    public async Task<IViewComponentResult> InvokeAsync(ChartingViewModel viewModel)
    {
        const string nodeServiceUrl = "http://localhost:3000/generate-svg";

        var response = await _httpClient.GetAsync(nodeServiceUrl);
        
        viewModel.SvgContent = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
        return View(viewModel);
    }
}