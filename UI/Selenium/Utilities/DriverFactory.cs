﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            
            switch(browser)
            {
                case BrowserType.Chrome:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("start-maximized");
                    WebDriver = new ChromeDriver(options);
                    Logger.Info(" Chrome Driver started in maximized mode");
                    break;
                default:
                    // code block
                    break;

            }
            WebDriver.Url = Hooks.config.URL;
            Logger.Info(" Following Url has entered " + Hooks.config.URL);
            return WebDriver;
        }

       
    }
}
