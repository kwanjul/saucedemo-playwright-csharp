using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }
}