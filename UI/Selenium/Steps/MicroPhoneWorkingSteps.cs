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
    public class MicroPhoneWorkingSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        MicroPhoneWorkingSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Then(@"Checking was your microphone working")]
        public void ThenCheckingWasYourMicrophoneWorking()
        {
            ExtensionMethods.FindElementWithWait(Driver, MicroPhoneWorkingPage.MicrophoneYesRadioBUtton, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, MicroPhoneWorkingPage.Continue, _scenarioContext).Click();
        }
    }
}