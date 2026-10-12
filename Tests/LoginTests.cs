using Microsoft.Playwright;
using NUnit.Framework;
using saucedemo_playwright_csharp.Config;
using saucedemo_playwright_csharp.Pages;

namespace saucedemo_playwright_csharp.Tests;

public class LoginTests : BaseTest
{
    [Test]
    public async Task LoginPageLoadsSuccessfully()
    {
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync();
        await Assertions.Expect(loginPage.GetUsernameInput()).ToBeVisibleAsync();
    }

    [Test]
    public async Task SuccessfulLogin()
    {
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync();
        InventoryPage inventoryPage = await loginPage.LoginAsync(TestConfig.Username, TestConfig.Password);
        await Assertions.Expect(inventoryPage.PageTitle).ToBeVisibleAsync();
    }
}