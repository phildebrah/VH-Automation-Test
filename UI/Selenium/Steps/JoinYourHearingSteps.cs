using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumSpecFlow.Utilities;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;
using System.Windows.Forms;
using com.sun.media.sound;
using OpenQA.Selenium.Interactions;
using UI.Utilities;

namespace UI.Steps
{
    [Binding]
    public class JoinYourHearingSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        JoinYourHearingSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        


        [Then(@"I want to join hearing with details")]
        public void ThenIWantToJoinHearingWithDetails(Table table)
        {
            foreach (var row in table.Rows)
            {
                ExtensionMethods.FindElementWithWait(Driver, JoinYourHearingPage.FullName, _scenarioContext).SendKeys(row[0]);
            }
            if(ExtensionMethods.IsElementExists(Driver, JoinYourHearingPage.QuickLinkParticipant, _scenarioContext))
            {
                ExtensionMethods.FindElementWithWait(Driver, JoinYourHearingPage.QuickLinkParticipant, _scenarioContext).Click();
      
            }
            if (ExtensionMethods.IsElementExists(Driver, JoinYourHearingPage.ContinueButton, _scenarioContext))
            {
                ExtensionMethods.FindElementWithWait(Driver, JoinYourHearingPage.ContinueButton, _scenarioContext).Submit();
            }
        }

        [Then(@"I click on signintoHearing")]
        public void ThenIClickOnSignintoHearing()
        {
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.SignInToHearingButton, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.NextButton, _scenarioContext).Click();
        }

        [Then(@"I confirm equipment is working")]
        public void ThenIConfirmEquipmentIsWorking()
        {
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.ContinueBtn, _scenarioContext).Click();
        }






    }
}

