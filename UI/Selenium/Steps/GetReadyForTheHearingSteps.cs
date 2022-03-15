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
    public class GetReadyForTheHearingSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        GetReadyForTheHearingSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I get ready for the hearing")]
        public void ThenIGetReadyForTheHearing()
        {
            ExtensionMethods.FindElementWithWait(Driver, GetReadyForTheHearingPage.NextButton,_scenarioContext).Click();
               
        }
    }
}