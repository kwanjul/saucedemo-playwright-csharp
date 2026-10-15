using Microsoft.Playwright;
using NUnit.Framework;
using saucedemo_playwright_csharp.Pages;

namespace saucedemo_playwright_csharp.Tests;

public class CheckoutTests : BaseTest
{
    [Test]
    public async Task SuccessfulCheckout()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        await inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        CartPage cartPage = await inventoryPage.NavigateToCartAsync();
        await Assertions.Expect(cartPage.PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(2);
        CheckoutInformationPage checkoutInformationPage = await cartPage.ProceedToCheckoutAsync();
        await Assertions.Expect(checkoutInformationPage.PageTitle).ToBeVisibleAsync();
        CheckoutOverviewPage checkoutOverviewPage = await checkoutInformationPage.EnterCheckoutInformationAsync("John", 
            "Smith", "07601");
        await Assertions.Expect(checkoutOverviewPage.PageTitle).ToBeVisibleAsync();
        IReadOnlyList<string> actualProductNames = await checkoutOverviewPage.ProductNames.AllTextContentsAsync();
        string[] expectedProductNames = {"Sauce Labs Backpack", "Sauce Labs Bike Light"};
        Assert.That(actualProductNames, Is.EquivalentTo(expectedProductNames));
        CheckoutCompletePage checkoutCompletePage = await checkoutOverviewPage.FinishCheckoutAsync();
        await Assertions.Expect(checkoutCompletePage.PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(checkoutCompletePage.ConfirmationMessage).ToBeVisibleAsync();        
    }

    [TestCase("", "Smith", "07601", "Error: First Name is required")]
    [TestCase("John", "", "07601", "Error: Last Name is required")]
    [TestCase("John", "Smith", "", "Error: Postal Code is required")]
    public async Task CheckoutRequiresMandatoryFields(string firstName, string lastName,
        string postalCode, string expectedErrorMessage)
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        CartPage cartPage = await inventoryPage.NavigateToCartAsync();
        CheckoutInformationPage checkoutInformationPage = await cartPage.ProceedToCheckoutAsync();
        await checkoutInformationPage.SubmitCheckoutInformationAsync(firstName, lastName, postalCode);
        await Assertions.Expect(checkoutInformationPage.ErrorMessage).ToHaveTextAsync(expectedErrorMessage);
        await Assertions.Expect(checkoutInformationPage.PageTitle).ToBeVisibleAsync();
    }

}