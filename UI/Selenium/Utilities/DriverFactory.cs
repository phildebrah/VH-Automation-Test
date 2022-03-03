using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using WebDriverManager.DriverConfigs.Impl;

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

       
    }
}
