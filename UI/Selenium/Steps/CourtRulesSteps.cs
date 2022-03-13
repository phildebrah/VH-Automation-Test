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
    public class CourtRulesSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        CourtRulesSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I agree to court rules")]
        public void ThenIAgreeToCourtRules()
        {
            ExtensionMethods.FindElementWithWait(Driver, CourtRulesPage.CourtRulesContinueBtn, _scenarioContext).Click();
        }



    }
}

