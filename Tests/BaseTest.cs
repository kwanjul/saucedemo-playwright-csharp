using Microsoft.Playwright;
using NUnit.Framework;
using saucedemo_playwright_csharp.Config;
using saucedemo_playwright_csharp.Pages;

namespace saucedemo_playwright_csharp.Tests;

public class BaseTest
{
    protected IPlaywright Playwright = null!;
    protected IBrowser Browser = null!;
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;

    [SetUp]
    public async Task SetUp()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Playwright.Selectors.SetTestIdAttribute("data-test");

        Browser = await Playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = TestConfig.Headless
            });

        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        if (Context != null)
            await Context.CloseAsync();

        if (Browser != null)
            await Browser.CloseAsync();

        Playwright?.Dispose();
    }

    protected async Task<InventoryPage> LoginSuccessfullyAsync()
    {
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync();
        InventoryPage inventoryPage = await loginPage.LoginAsync(TestConfig.Username, TestConfig.Password);
        await Assertions.Expect(inventoryPage.PageTitle).ToBeVisibleAsync();
        return inventoryPage;
    }

}