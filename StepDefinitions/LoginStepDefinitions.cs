using Reqnroll;
using Microsoft.Playwright;
using saucedemo_playwright_csharp.Config;
using saucedemo_playwright_csharp.Pages;

[Binding]
public class LoginStepDefinitions
{
    private readonly PlaywrightContext _context;

    private LoginPage? loginPage;
    private InventoryPage? inventoryPage;

    public LoginStepDefinitions(PlaywrightContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [Given("I am on the Sauce Demo login page")]
    public async Task GivenIAmOnTheSauceDemoLoginPage()
    {
        if (_context.Page == null)
        {
            throw new InvalidOperationException("Playwright page has not been initialized.");
        }
        loginPage = new LoginPage(_context.Page);            
        await loginPage.NavigateAsync();
    }

    [When("I log in with valid credentials")]
    public async Task WhenILogInWithValidCredentials()
    {
        if (loginPage == null)
        {
            throw new InvalidOperationException("Login page has not been initialized.");
        }
        inventoryPage = await loginPage.LoginAsync(TestConfig.Username, TestConfig.Password);
    }

    [Then("I should see the inventory page")]
    public async Task ThenIShouldSeeTheInventoryPage()
    {
        if (inventoryPage == null)
        {
            throw new InvalidOperationException("Inventory page has not been initialized.");
        }
        await Assertions.Expect(inventoryPage.PageTitle).ToBeVisibleAsync();
    }
}