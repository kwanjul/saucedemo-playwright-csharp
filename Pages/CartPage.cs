using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class CartPage : BasePage
{
    public ILocator PageTitle => Page.GetByText("Your Cart", new() { Exact = true });

    public ILocator CartItems => Page.GetByTestId("inventory-item");

    public ILocator ProductNames => Page.GetByTestId("inventory-item-name");

    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout", Exact = true});

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

    public async Task<CheckoutInformationPage> ProceedToCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
        return new CheckoutInformationPage(Page);
    }
}