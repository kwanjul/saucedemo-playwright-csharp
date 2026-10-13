using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class InventoryPage : BasePage
{
    public ILocator PageTitle => Page.GetByText("Products", new() { Exact = true });

    public ILocator InventoryItems => Page.GetByTestId("inventory-item");

    public ILocator ProductNames => Page.GetByTestId("inventory-item-name");

    public ILocator ProductPrices => Page.GetByTestId("inventory-item-price");

    private ILocator SortDropdown => Page.GetByTestId("product-sort-container");

    public InventoryPage(IPage page) : base(page)
    {
    }

    public async Task SortProductsAsync(string sortValue)
    {
        await SortDropdown.SelectOptionAsync(sortValue);
    }
}