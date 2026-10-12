using System;
using System.IO;
using System.Text.Json;

public static class TestConfig
{
    public static string BaseUrl { get; private set; } = "";
    public static bool Headless { get; private set; }
    public static string Username { get; private set; } = "";
    public static string Password { get; private set; } = "";

    static TestConfig()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
        string json = File.ReadAllText(filePath);

        using JsonDocument document = JsonDocument.Parse(json);
        JsonElement root = document.RootElement;
        BaseUrl = root.GetProperty("BaseUrl").GetString()
            ?? throw new InvalidOperationException("BaseUrl cannot be null.");
        Headless = root.GetProperty("Headless").GetBoolean();
        Username = root.GetProperty("Username").GetString()
            ?? throw new InvalidOperationException("Username cannot be null.");
        Password = root.GetProperty("Password").GetString()
            ?? throw new InvalidOperationException("Password cannot be null.");
    }
}