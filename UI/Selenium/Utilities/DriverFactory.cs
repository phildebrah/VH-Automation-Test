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
using TestLibrary.Utilities;
using OpenQA.Selenium.Appium.Enums;
using UI.Model;
using UI.Utilities;

namespace SeleniumSpecFlow.Utilities
{
    public class DriverFactory
    {
        public IWebDriver WebDriver { get; set; }
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
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
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions chromeoptions = new ChromeOptions();
                    chromeoptions.AddArguments("start-maximized");
                    chromeoptions.AddArgument("no-sandbox");
                    chromeoptions.AddArguments("--use-fake-ui-for-media-stream");
                    chromeoptions.AddArguments("--use-fake-device-for-media-stream");
                    WebDriver = new ChromeDriver(chromeoptions);
                    Logger.Info(" Chrome Driver started in maximized mode");
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
                        options.AddAdditionalOption(key, value);
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
                        iosOptions.AddAdditionalOption(key, value);
                    }
                    WebDriver = new RemoteWebDriver(remoteUrl, iosOptions.ToCapabilities());
                    break;

                case "macOS 12":
                    if (config.BrowserName.Equals("chrome"))
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.PlatformName = config.PlatformName;
                        foreach (var (key, value) in SauceOptions)
                        {
                            chromeOptions.AddAdditionalOption(key, value);
                        }
                        WebDriver = new RemoteWebDriver(remoteUrl, chromeOptions);
                        break;
                    }
                    else if (config.BrowserName.Equals("Firefox")) {
                        FirefoxOptions ffOptions = new FirefoxOptions();
                        ffOptions.PlatformName = config.PlatformName;
                        foreach (var (key, value) in SauceOptions)
                        {
                            ffOptions.AddAdditionalOption(key, value);
                        }
                        WebDriver = new RemoteWebDriver(remoteUrl, ffOptions);
                        break;
                    }
                    else if (config.BrowserName.Equals("Edge"))
                    {
                        EdgeOptions edgeOptions = new EdgeOptions();
                        edgeOptions.PlatformName = config.PlatformName;
                        foreach (var (key, value) in SauceOptions)
                        {
                            edgeOptions.AddAdditionalOption(key, value);
                        }
                        WebDriver = new RemoteWebDriver(remoteUrl, edgeOptions);
                        break;
                    }
                    else if (config.BrowserName.Equals("Safari"))
                    {
                        SafariOptions safariOptions = new SafariOptions();
                        safariOptions.PlatformName = config.PlatformName;
                        foreach (var (key, value) in SauceOptions)
                        {
                            safariOptions.AddAdditionalOption(key, value);
                        }
                        WebDriver = new RemoteWebDriver(remoteUrl, safariOptions);
                        break;
                    }
                    break;
            }
            return WebDriver;
        }
    }

}

