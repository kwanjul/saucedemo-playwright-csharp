using Microsoft.Playwright;
using NUnit.Framework;
using saucedemo_playwright_csharp.Pages;

namespace saucedemo_playwright_csharp.Tests;

public class InventoryTests : BaseTest
{
    [Test]
    public async Task InventoryPageDisplaysSixProducts()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await Assertions.Expect(inventoryPage.InventoryItems).ToHaveCountAsync(6);
    }

    [Test]
    public async Task InventoryPageDisplaysExpectedProducts()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await Assertions.Expect(inventoryPage.InventoryItems).ToHaveCountAsync(6);
        IReadOnlyList<string> actualProductNames = await inventoryPage.ProductNames.AllTextContentsAsync();
        string[] expectedProductNames = {"Sauce Labs Backpack", "Sauce Labs Bike Light", "Sauce Labs Bolt T-Shirt",
            "Sauce Labs Fleece Jacket", "Sauce Labs Onesie", "Test.allTheThings() T-Shirt (Red)"};
        Assert.That(actualProductNames, Is.EquivalentTo(expectedProductNames));
    }

    [Test]
    public async Task SortProductsByName()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await Assertions.Expect(inventoryPage.InventoryItems).ToHaveCountAsync(6);
        
        //Sort products by name from Z to A
        await inventoryPage.SortProductsAsync("za");
        await Assertions.Expect(inventoryPage.ProductNames.First).ToHaveTextAsync("Test.allTheThings() T-Shirt (Red)");
        IReadOnlyList<string> actualProductNames = await inventoryPage.ProductNames.AllTextContentsAsync();
        Assert.That(actualProductNames, Is.Ordered.Descending);

        //Sort products by name from A to Z
        await inventoryPage.SortProductsAsync("az");
        await Assertions.Expect(inventoryPage.ProductNames.First).ToHaveTextAsync("Sauce Labs Backpack");
        actualProductNames = await inventoryPage.ProductNames.AllTextContentsAsync();
        Assert.That(actualProductNames, Is.Ordered);
    }

    [Test]
    public async Task SortProductsByPrice()
    {
        InventoryPage inventoryPage = await LoginSuccessfullyAsync();
        await Assertions.Expect(inventoryPage.InventoryItems).ToHaveCountAsync(6);

        //Sort products by price from high to low
        await inventoryPage.SortProductsAsync("hilo");
        await Assertions.Expect(inventoryPage.ProductPrices.First).ToHaveTextAsync("$49.99");
        IReadOnlyList<string> actualProductPrices = await inventoryPage.ProductPrices.AllTextContentsAsync();
        decimal[] pricesDecimal = actualProductPrices.Select(price => decimal.Parse(price.Replace("$",""))).ToArray();
        Assert.That(pricesDecimal, Is.Ordered.Descending);

        //Sort products by price from low to high
        await inventoryPage.SortProductsAsync("lohi");
        await Assertions.Expect(inventoryPage.ProductPrices.First).ToHaveTextAsync("$7.99");
        actualProductPrices = await inventoryPage.ProductPrices.AllTextContentsAsync();
        pricesDecimal = actualProductPrices.Select(price => decimal.Parse(price.Replace("$",""))).ToArray();
        Assert.That(pricesDecimal, Is.Ordered);
    }
}