using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class CheckoutInformationPage : BasePage
{
    public ILocator PageTitle => Page.GetByText("Checkout: Your Information", new() { Exact = true });

    private ILocator FirstNameInput => Page.GetByPlaceholder("First Name");

    private ILocator LastNameInput => Page.GetByPlaceholder("Last Name");

    private ILocator PostalCodeInput => Page.GetByPlaceholder("Zip/Postal Code");

    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true});

    public ILocator ErrorMessage => Page.GetByTestId("error");

    public CheckoutInformationPage(IPage page) : base(page)
    {
    }

    public async Task<CheckoutOverviewPage> EnterCheckoutInformationAsync(string firstName, 
        string lastName, string postalCode)
    {
        await SubmitCheckoutInformationAsync(firstName, lastName, postalCode);
        return new CheckoutOverviewPage(Page);
    }

    public async Task SubmitCheckoutInformationAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await PostalCodeInput.FillAsync(postalCode);
        await ContinueButton.ClickAsync();
    }

}