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
    public class TestingEquipmentSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        TestingEquipmentSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"Testing your equipment")]
        public void ThenIConfirmEquipmentIsWorking()
        {
            ExtensionMethods.FindElementWithWait(Driver, TestingEquipmentPage.ContinueBtn, _scenarioContext).Click();
                       
        }      
    }
}