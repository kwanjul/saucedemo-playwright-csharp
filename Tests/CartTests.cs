using Microsoft.Playwright;
using NUnit.Framework;
using saucedemo_playwright_csharp.Pages;

namespace saucedemo_playwright_csharp.Tests;

public class CartTests : BaseTest
{
    [Test]
    public async Task AddSingleProductToCart()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        await Assertions.Expect(inventoryPage.ShoppingCartBadge).ToHaveTextAsync("1");
    }

    [Test]
    public async Task AddMultipleProductsToCart()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        await inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        await inventoryPage.AddProductToCartAsync("Sauce Labs Fleece Jacket");
        await Assertions.Expect(inventoryPage.ShoppingCartBadge).ToHaveTextAsync("3");
    }

    [Test]
    public async Task VerifyShoppingCartContents()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        await inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        CartPage cartPage = await inventoryPage.NavigateToCartAsync();
        await Assertions.Expect(cartPage.PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(2);
        IReadOnlyList<string> actualProductNames = await cartPage.ProductNames.AllTextContentsAsync();
        string[] expectedProductNames = {"Sauce Labs Backpack", "Sauce Labs Bike Light"};
        Assert.That(actualProductNames, Is.EquivalentTo(expectedProductNames));
    }

    [Test]
    public async Task RemoveProductFromInventoryPage()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        await inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        await inventoryPage.RemoveProductFromCartAsync("Sauce Labs Backpack");
        await Assertions.Expect(inventoryPage.ShoppingCartBadge).ToHaveTextAsync("1");
        CartPage cartPage = await inventoryPage.NavigateToCartAsync();
        await Assertions.Expect(cartPage.PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(1);
        IReadOnlyList<string> actualProductNames = await cartPage.ProductNames.AllTextContentsAsync();
        string[] expectedProductNames = {"Sauce Labs Bike Light"};
        Assert.That(actualProductNames, Is.EquivalentTo(expectedProductNames));
    }

    [Test]
    public async Task RemoveProductFromCartPage()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        await inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        await Assertions.Expect(inventoryPage.ShoppingCartBadge).ToHaveTextAsync("2");
        CartPage cartPage = await inventoryPage.NavigateToCartAsync();
        await Assertions.Expect(cartPage.PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(2);
        await cartPage.RemoveProductFromCartAsync("Sauce Labs Backpack");
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(1);
        await Assertions.Expect(cartPage.ShoppingCartBadge).ToHaveTextAsync("1");
        await Assertions.Expect(cartPage.ProductNames).ToHaveTextAsync(new[] { "Sauce Labs Bike Light" });
    }
        
    [Test]
    public async Task RemoveAllProductsFromCart()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await inventoryPage.AddProductToCartAsync("Sauce Labs Backpack");
        await inventoryPage.AddProductToCartAsync("Sauce Labs Bike Light");
        await Assertions.Expect(inventoryPage.ShoppingCartBadge).ToHaveTextAsync("2");
        CartPage cartPage = await inventoryPage.NavigateToCartAsync();
        await Assertions.Expect(cartPage.PageTitle).ToBeVisibleAsync();
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(2);
        await cartPage.RemoveProductFromCartAsync("Sauce Labs Bike Light");
        await cartPage.RemoveProductFromCartAsync("Sauce Labs Backpack");
        await Assertions.Expect(cartPage.CartItems).ToHaveCountAsync(0);
        await Assertions.Expect(cartPage.ShoppingCartBadge).ToBeHiddenAsync();
    }

}