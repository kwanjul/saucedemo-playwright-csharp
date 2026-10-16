using Reqnroll;
using Microsoft.Playwright;
using saucedemo_playwright_csharp.Config;
using saucedemo_playwright_csharp.Pages;

[Binding]
public class CartStepDefinitions
{
    private readonly PlaywrightContext _context;

    private LoginPage? loginPage;
    private InventoryPage? inventoryPage;
    private CartPage? cartPage;

    public CartStepDefinitions(PlaywrightContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [Given("I am logged into Sauce Demo")]
    public async Task GivenIAmLoggedIntoSauceDemo()
    {
        if (_context.Page == null)
        {
            throw new InvalidOperationException("Playwright page has not been initialized.");
        }
        loginPage = new LoginPage(_context.Page);
        await loginPage.NavigateAsync();
        inventoryPage = await loginPage.LoginAsync(TestConfig.Username, TestConfig.Password);
        await Assertions.Expect(inventoryPage.PageTitle).ToBeVisibleAsync();
    }

    [When("I add Sauce Labs Backpack to the shopping cart")]
    public async Task WhenIAddSauceLabsBackpackToTheShoppingCart()
    {
        if (inventoryPage == null)
        {
            throw new InvalidOperationException("Inventory page has not been initialized.");
        }
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
    }

    [Then("the shopping cart should contain Sauce Labs Backpack")]
    public async Task ThenTheShoppingCartShouldContainSauceLabsBackpack()
    {
        if (inventoryPage == null)
        {
            throw new InvalidOperationException("Inventory page has not been initialized.");
        }
        cartPage = await inventoryPage.NavigateToCartAsync();
        await Assertions.Expect(cartPage.PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(1);
        await Assertions.Expect(cartPage.ProductNames).ToHaveTextAsync(new string[] {"Sauce Labs Backpack"});
    }
}