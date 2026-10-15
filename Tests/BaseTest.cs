using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Text.RegularExpressions;
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

    private static string GetProjectRoot()
    {
        DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory != null)
        {
            string projectFile = Path.Combine(
                directory.FullName, "saucedemo-playwright-csharp.csproj");
            if (File.Exists(projectFile))
            {
                return directory.FullName;
            }
            directory = directory.Parent;
        }
        throw new DirectoryNotFoundException("Could not locate the project root directory.");
    }

    [TearDown]
    public async Task TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && Page != null)
            {
                string path = Path.Combine(GetProjectRoot(), "TestResults", "Screenshots");
                Directory.CreateDirectory(path);
                string testName = TestContext.CurrentContext.Test.Name;
                string safeTestName = Regex.Replace(testName, @"[^a-zA-Z0-9_-]", "_");
                string screenshotPath = Path.Combine(path, safeTestName + ".png");
                await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath, FullPage = true
                });
            }            
        } finally
        {
            if (Context != null)
                await Context.CloseAsync();

            if (Browser != null)
                await Browser.CloseAsync();

            Playwright?.Dispose();            
        }
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