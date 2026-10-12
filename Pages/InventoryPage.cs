using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class InventoryPage
{
    private readonly IPage _page;

    public ILocator PageTitle => _page.GetByText("Products", new() { Exact = true });

    public InventoryPage(IPage page)
    {
        _page = page;
    }
}