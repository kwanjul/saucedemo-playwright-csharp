using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class CheckoutCompletePage : BasePage
{
    public ILocator PageTitle => Page.GetByText("Checkout: Complete!", new() { Exact = true });

    public ILocator ConfirmationMessage => Page.GetByText("Thank you for your order!", new() { Exact = true });

    public CheckoutCompletePage(IPage page) : base(page)
    {
    }

}