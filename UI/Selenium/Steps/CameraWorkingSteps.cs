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
    public class CameraWorkingSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        CameraWorkingSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"Checking was your camera working")]
        public void ThenCheckingWasYourCameraWorking()
        {

            if(ExtensionMethods.WaitForPageLoad(Driver, CameraWorkingPage.CameraYesRadioButton, _scenarioContext))
            {
                ExtensionMethods.FindElementWithWait(Driver, CameraWorkingPage.CameraYesRadioButton, _scenarioContext).Click();

                ExtensionMethods.FindElementWithWait(Driver, CameraWorkingPage.Continue, _scenarioContext).Click();
            }
        }
    }
}