using Microsoft.Playwright;

namespace saucedemo_playwright_csharp.Pages;

public class LoginPage
{
    private readonly IPage _page;

    private ILocator UsernameInput => _page.GetByPlaceholder("Username");

    private ILocator PasswordInput => _page.GetByPlaceholder("Password");

    private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync(TestConfig.BaseUrl);
    }

    public async Task<InventoryPage> LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
        return new InventoryPage(_page);
    }

    public ILocator GetUsernameInput()
    {
        return UsernameInput;
    }
}