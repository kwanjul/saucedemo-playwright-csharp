using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;

    public ILocator ShoppingCartBadge => Page.GetByTestId("shopping-cart-badge");

    public ILocator ShoppingCartLink => Page.GetByTestId("shopping-cart-link");

    protected BasePage(IPage page)
    {
        Page = page;
    }
    
    public async Task<CartPage> NavigateToCartAsync()
    {
        await ShoppingCartLink.ClickAsync();
        return new CartPage(Page);
    }

}