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

    [TestCase("standard_user","WrongPassword","Epic sadface: Username and password do not match any user in this service")]
    [TestCase("invalid_user","secret_sauce","Epic sadface: Username and password do not match any user in this service")]
    [TestCase("locked_out_user","secret_sauce","Epic sadface: Sorry, this user has been locked out.")]
    public async Task UnsuccessfulLogin(string username, string password, string expectedErrorMessage)
    {
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync();
        await loginPage.LoginAsync(username, password);
        await Assertions.Expect(loginPage.GetErrorMessage())
            .ToHaveTextAsync(expectedErrorMessage);
    }
}