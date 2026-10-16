using Microsoft.Playwright;

public class PlaywrightContext
{
    public IPlaywright? Playwright { get; set; }
    public IBrowser? Browser { get; set; }
    public IBrowserContext? BrowserContext { get; set; }
    public IPage? Page { get; set; }
}