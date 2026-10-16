# Sauce Demo — Playwright C# Test Automation Framework

[![Playwright C# Tests](https://github.com/kwanjul/saucedemo-playwright-csharp/actions/workflows/playwright-tests.yml/badge.svg)](https://github.com/kwanjul/saucedemo-playwright-csharp/actions/workflows/playwright-tests.yml)

## Test Reports

View the latest automated test results:

[View latest Playwright public HTML test report](https://kwanjul.github.io/saucedemo-playwright-csharp/)

An end-to-end UI test automation framework for [Sauce Demo](https://www.saucedemo.com/), built with **C#**, **Microsoft Playwright**, **NUnit**, and **.NET 10**. The project demonstrates maintainable Page Object Model design, reusable browser setup, data-driven testing, BDD with Reqnroll, and automated execution through GitHub Actions.

## Technology stack

- **C# / .NET 10** — test implementation and runtime
- **Microsoft Playwright** — Chromium browser automation and auto-retrying assertions
- **NUnit** — test execution and parameterized test cases
- **Reqnroll** — Gherkin feature files and C# step definitions
- **GitHub Actions** — continuous integration and scheduled execution
- **VSTest HTML logger** — downloadable test execution reports

## Test coverage

| Area | Automated checks |
| --- | --- |
| Login | Login page loads; successful login; invalid credentials and locked-out user |
| Inventory | Product count and names; sorting by name and price |
| Shopping cart | Add one or multiple products; verify contents; remove products from inventory or cart |
| Checkout | Complete purchase flow; validation of required customer-information fields |
| BDD | Successful login; unsuccessful login; add product to cart expressed in Gherkin and executed through Reqnroll |

**Current suite:** 19 NUnit test cases plus three Reqnroll scenario (22 total). Both positive and negative paths are covered.

## Project structure

```text
.
├── .github/workflows/playwright-tests.yml  # CI and weekday schedule
├── Config/                                 # JSON settings and configuration loader
├── Features/                               # Reqnroll Gherkin feature files
├── Hooks/                                  # Reqnroll scenario setup and teardown
├── Pages/                                  # Page Object Model classes
├── StepDefinitions/                        # Gherkin-to-C# bindings
├── Support/                                # Shared Playwright scenario context
├── Tests/                                  # NUnit tests and common test setup
└── saucedemo-playwright-csharp.csproj
```

The page objects encapsulate selectors and user interactions, keeping test cases focused on behavior and assertions. Each test or BDD scenario creates an isolated browser context. Sauce Demo's `data-test` attribute is configured as Playwright's test-ID attribute.

## Run locally

**Prerequisites:** .NET 10 SDK and a terminal. The project uses Chromium through Playwright.

```bash
git clone https://github.com/kwanjul/saucedemo-playwright-csharp.git
cd saucedemo-playwright-csharp
dotnet restore
dotnet build
pwsh bin/Debug/net10.0/playwright.ps1 install chromium
dotnet test
```

On macOS, if PowerShell (`pwsh`) is not installed, install it or use another supported way to invoke Playwright's browser installer.

Test settings are stored in `Config/appsettings.json`, including the Sauce Demo base URL, demo credentials, and the `Headless` setting. The repository uses publicly documented Sauce Demo practice credentials; do not put private credentials in this file.

To generate an HTML report locally:

```bash
dotnet test --logger "html;LogFileName=TestReport.html" --results-directory ./TestResults
```

Open `TestResults/TestReport.html` in a browser after execution.

## CI/CD with GitHub Actions

The [Playwright C# Tests workflow](https://github.com/kwanjul/saucedemo-playwright-csharp/actions/workflows/playwright-tests.yml) runs on an Ubuntu 24.04 runner. It restores and builds the .NET project, installs Chromium and its dependencies, executes the suite, and uploads the test results.

It runs:

- On pushes to `main`
- On demand through **Actions → Playwright C# Tests → Run workflow**
- **Monday–Friday at 8:00 AM America/New_York**, using GitHub Actions' timezone-aware schedule

Scheduled runs may begin slightly later than the configured time depending on GitHub Actions availability.

## Reports and failure screenshots

The **[latest HTML test report](https://kwanjul.github.io/saucedemo-playwright-csharp/)** is published to GitHub Pages after workflow runs and is publicly viewable without a GitHub account. The published report includes the run date and time and shows test outcomes.

The workflow uploads a downloadable artifact named **`playwright-test-results`**, containing `TestReport.html`. When a test fails and its browser page is available, teardown also captures a full-page PNG screenshot under `TestResults/Screenshots/`.

Screenshots are generated **only for failures**; a fully passing run normally produces no screenshots. Screenshots are included in the downloadable Actions artifact, not embedded in the public HTML report. The artifact-upload step runs even when tests fail, to preserve diagnostic information.

To inspect results, open a workflow run in GitHub's **Actions** tab and download **`playwright-test-results`** from the **Artifacts** section.

## Notes

This is a demonstration framework built against the public Sauce Demo practice application. The BDD scenario reuses the same page objects as the NUnit tests rather than duplicating browser interactions.
