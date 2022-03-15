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
    public class SelectYourHearingListSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        SelectYourHearingListSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I choose from hearing lists")]
        public void GivenIChooseFromHearingLists(Table table)
        {
            _scenarioContext.UpdatePageName("View hearing venue list");
            foreach (var row in table.Rows)
            {
                ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.HearingList, _scenarioContext).SendKeys(row[0]);
                ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.HearingCheckBox, _scenarioContext).Click();
            }

            
        }


        [When(@"I click on view hearings")]
        public void WhenIClickOnViewHearings()
        {
            ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.ViewHearings, _scenarioContext).Click();
        }

      
    }
}

