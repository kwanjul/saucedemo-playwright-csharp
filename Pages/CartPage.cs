using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class CartPage : BasePage
{
    public ILocator PageTitle => Page.GetByText("Your Cart", new() { Exact = true });

    public ILocator CartItems => Page.GetByTestId("inventory-item");

    public ILocator ProductNames => Page.GetByTestId("inventory-item-name");

    public CartPage(IPage page) : base(page)
    {
    }

    public async Task RemoveProductFromCartAsync(string productName)
    {
        await CartItems.Filter(new LocatorFilterOptions
            {
                Has = Page.GetByTestId("inventory-item-name")
                    .GetByText(productName, new() { Exact = true })
            })
        .GetByRole(AriaRole.Button, 
            new LocatorGetByRoleOptions { Name = "Remove", Exact = true}).ClickAsync();        
    }
}