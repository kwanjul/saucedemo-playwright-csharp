using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class CheckoutOverviewPage : BasePage
{
    public ILocator PageTitle => Page.GetByText("Checkout: Overview", new() { Exact = true });

    public ILocator CartItems => Page.GetByTestId("inventory-item");

    public ILocator ProductNames => Page.GetByTestId("inventory-item-name");

    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish", Exact = true});

    public CheckoutOverviewPage(IPage page) : base(page)
    {
    }

    public async Task<CheckoutCompletePage> FinishCheckoutAsync()
    {
        await FinishButton.ClickAsync();
        return new CheckoutCompletePage(Page);
    }

}