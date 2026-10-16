using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Reqnroll;
using saucedemo_playwright_csharp.Config;

[Binding]
public class PlaywrightHooks
{
    private readonly PlaywrightContext _context;

    private readonly ScenarioContext _scenarioContext;

    public PlaywrightHooks(PlaywrightContext context, ScenarioContext scenarioContext)
    {
        _context = context;
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _context.Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        _context.Playwright.Selectors.SetTestIdAttribute("data-test");

        _context.Browser = await _context.Playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions { Headless = TestConfig.Headless });

        _context.BrowserContext = await _context.Browser.NewContextAsync();

        _context.Page = await _context.BrowserContext.NewPageAsync();
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        try
        {
            if (_scenarioContext.TestError != null && _context.Page != null)
            {
                string path = Path.Combine( GetProjectRoot(), "TestResults", "Screenshots");
                Directory.CreateDirectory(path);
                string testName = TestContext.CurrentContext.Test.Name;
                string safeTestName = Regex.Replace(testName, @"[^a-zA-Z0-9_-]", "_");
                string screenshotPath = Path.Combine(path, safeTestName + ".png");
                await _context.Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath, FullPage = true
                });
            }            
        } finally
        {
            if (_context.BrowserContext != null)
                await _context.BrowserContext.CloseAsync();

            if (_context.Browser != null)
                await _context.Browser.CloseAsync();

            _context.Playwright?.Dispose();
        }
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

}