using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;
using System;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using UI.Model;

namespace UI.Utilities
{
    /// <summary>
    /// Class to initialze new Webdriver
    /// Can create different types of browser drivers
    /// </summary>
    public class DriverFactory
    {
        public IWebDriver WebDriver { get; set; }
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static int ProcessId;
        public IWebDriver InitializeDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Firefox:
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    FirefoxOptions firefoxoptions = new FirefoxOptions();
                    firefoxoptions.AddArguments("start-maximized");
                    firefoxoptions.AddArgument("no-sandbox");
                    firefoxoptions.AddArguments("--use-fake-ui-for-media-stream");
                    firefoxoptions.AddArguments("--use-fake-device-for-media-stream");
                    WebDriver = new FirefoxDriver(firefoxoptions);
                    WebDriver.Manage().Window.Maximize();
                    Logger.Info(" Firefox started in maximized mode");
                    break;
                case BrowserType.Edge:
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    EdgeOptions edgeoptions = new EdgeOptions();
                    edgeoptions.AddArguments("start-maximized");
                    edgeoptions.AddArgument("no-sandbox");
                    edgeoptions.AddArguments("--use-fake-ui-for-media-stream");
                    edgeoptions.AddArguments("--use-fake-device-for-media-stream");
                    edgeoptions.AddArgument("inprivate");
                    WebDriver = new EdgeDriver(edgeoptions);
                    Logger.Info(" Edge started in maximized mode");
                    break;
                case BrowserType.Safari:
                    SafariOptions safarioptions = new SafariOptions();
                    WebDriver = new SafariDriver(safarioptions);
                    WebDriver.Manage().Window.Maximize();
                    Logger.Info(" Safari started in maximized mode");
                    break;
                case BrowserType.Chrome:
                    for (int i = 1; i < 3; i++)
                    {
                        try
                        {
                            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                            var cService = ChromeDriverService.CreateDefaultService();
                            ChromeOptions chromeoptions = new ChromeOptions();
                            chromeoptions.AddArguments("start-maximized");
                            chromeoptions.AddArgument("no-sandbox");
                            chromeoptions.AddArguments("--use-fake-ui-for-media-stream");
                            chromeoptions.AddArguments("--use-fake-device-for-media-stream");
                            WebDriver = new ChromeDriver(cService, chromeoptions);
                            ProcessId = cService.ProcessId;
                            Logger.Info(" Chrome Driver started in maximized mode");
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (i > 1 && ex.Message.Contains("The HTTP request to the remote WebDriver server for URL http://localhost"))
                            {
                                NUnit.Framework.Assert.Fail($"Chrome failed to start after {i} attempts with exception - {ex.Message}");
                            }
                        }
                    }
                    break;
                default:
                    // code block 
                    break;
            }
            return WebDriver;
        }

        public IWebDriver InitializeSauceDriver(SauceLabsOptions sauceLabsOptions, SauceLabsConfiguration config)
        {
            Dictionary<string, object> SauceOptions = new Dictionary<string, object>
                    {
                        {"username",config.SauceUsername },
                        {"accessKey",config.SauceAccessKey },
                        { "name",sauceLabsOptions.Name}
                        ,{"commandTimeout",sauceLabsOptions.CommandTimeoutInSeconds }
                        ,{"idleTimeout",sauceLabsOptions.IdleTimeoutInSeconds }
                        ,{"maxDuration",sauceLabsOptions.MaxDurationInSeconds }
                        ,{"seleniumVersion",sauceLabsOptions.SeleniumVersion }
                        ,{"timeZone",sauceLabsOptions.Timezone }
                    };
            var remoteUrl = new Uri($"http://{config.SauceUsername}:{config.SauceAccessKey}{config.SauceUrl}");

            switch (config.PlatformName)
            {
                case "Android":
                    AppiumOptions options = new AppiumOptions();
                    options.DeviceName = config.DeviceName;
                    options.PlatformName = config.PlatformName;
                    options.BrowserName = config.BrowserName;
                    options.AddAdditionalAppiumOption(MobileCapabilityType.AppiumVersion, config.AppiumVersion);
                    options.AddAdditionalAppiumOption(MobileCapabilityType.Orientation, config.Orientation);
                    options.AddAdditionalAppiumOption("PlatformVersion", config.PlatformVersion);
                    options.AddAdditionalAppiumOption("name", sauceLabsOptions.Name);
                    foreach (var (key, value) in SauceOptions)
                    {
                        options.AddAdditionalAppiumOption(key, value);
                    }
                    WebDriver = new RemoteWebDriver(remoteUrl, options.ToCapabilities());
                    break;
                case "iOS":
                    AppiumOptions iosOptions = new AppiumOptions();
                    iosOptions.DeviceName = config.DeviceName;
                    iosOptions.PlatformName = config.PlatformName;
                    iosOptions.BrowserName = config.BrowserName;
                    iosOptions.AddAdditionalAppiumOption(MobileCapabilityType.AppiumVersion, config.AppiumVersion);
                    iosOptions.AddAdditionalAppiumOption(MobileCapabilityType.Orientation, config.Orientation);
                    iosOptions.AddAdditionalAppiumOption("PlatformVersion", config.PlatformVersion);
                    iosOptions.AddAdditionalAppiumOption("name", sauceLabsOptions.Name);
                    foreach (var (key, value) in SauceOptions)
                    {
                        iosOptions.AddAdditionalAppiumOption(key, value);
                    }
                    WebDriver = new RemoteWebDriver(remoteUrl, iosOptions.ToCapabilities());
                    break;
                case "macOS 12":
                    DriverOptions driverOptions = null;
                    if (config.BrowserName.Equals("chrome"))
                    {
                        driverOptions = new ChromeOptions();
                    }
                    else if (config.BrowserName.Equals("Firefox"))
                    {
                        driverOptions = new FirefoxOptions();
                    }
                    else if (config.BrowserName.Equals("Edge"))
                    {
                        driverOptions = new EdgeOptions();
                    }
                    else if (config.BrowserName.Equals("Safari"))
                    {
                        driverOptions = new SafariOptions();
                    }
                    driverOptions.PlatformName = config.PlatformName;
                    foreach (var (key, value) in SauceOptions)
                    {
                        driverOptions.AddAdditionalOption(key, value);
                    }
                    WebDriver = new RemoteWebDriver(remoteUrl, driverOptions);
                    break;
            }
            return WebDriver;
        }
    }

}

