using Microsoft.Playwright;
using saucedemo_playwright_csharp.Config;

namespace saucedemo_playwright_csharp.Pages;

public class LoginPage : BasePage
{
    private ILocator UsernameInput => Page.GetByPlaceholder("Username");

    private ILocator PasswordInput => Page.GetByPlaceholder("Password");

    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    public LoginPage(IPage page) : base(page)
    {
    }

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(TestConfig.BaseUrl);
    }

    public async Task<InventoryPage> LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
        return new InventoryPage(Page);
    }

    public ILocator GetUsernameInput()
    {
        return UsernameInput;
    }
}