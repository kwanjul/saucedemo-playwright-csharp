using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class InventoryPage : BasePage
{
    public ILocator PageTitle => Page.GetByText("Products", new() { Exact = true });

    public InventoryPage(IPage page) : base(page)
    {
    }
}