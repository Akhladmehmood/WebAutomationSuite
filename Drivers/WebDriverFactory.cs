using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WebAutomationSuite.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(string browser)
        {
            browser = browser?.ToLowerInvariant() ?? "chrome";

            switch (browser)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), version: "MatchingBrowser");
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddExcludedArgument("enable-automation");
                    chromeOptions.AddAdditionalOption("useAutomationExtension", false);
                    return new ChromeDriver(chromeOptions);

                case "firefox":
                case "ff":
                    new DriverManager().SetUpDriver(new FirefoxConfig(), version: "MatchingBrowser");
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--width=1920");
                    firefoxOptions.AddArgument("--height=1080");
                    return new FirefoxDriver(firefoxOptions);

                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig(), version: "MatchingBrowser");
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("start-maximized");
                    return new EdgeDriver(edgeOptions);

                default:
                    throw new ArgumentException($"Browser not supported: {browser}");
            }
        }
    }
}
