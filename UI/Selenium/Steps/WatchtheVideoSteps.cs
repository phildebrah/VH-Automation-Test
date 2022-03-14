using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumSpecFlow.Utilities;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;
using OpenQA.Selenium.Interactions;
using UI.Utilities;

namespace UI.Steps
{
    [Binding]
    public class WatchtheVideoSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        WatchtheVideoSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I continue to watch the video")]
        public void ThenIContinueToWatchTheVideo()
        {
            ExtensionMethods.FindElementWithWait(Driver, WatchtheVideoPage.WatchVideoButton, _scenarioContext).Click();
        }
        
    }
}

