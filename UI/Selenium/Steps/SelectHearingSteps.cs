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
    public class SelectHearingSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        SelectHearingSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
          
        [STAThread]
       
        [When(@"I click on copy hearing id to clipboard")]
        public void WhenIClickOnCopyHearingIdToClipboard()
        {
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Hearingidtoclipboard, _scenarioContext).Click();
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.filters, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgPartBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).SendKeys(Keys.Control + "v");

        }

        [Then(@"Hearing id should be copied")]
        public void ThenHearingIdShouldBeCopied()
        {
            String hearingID = ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).GetAttribute("value");
            Assert.IsTrue(hearingID.Length > 0, "ID verified");
        }                      

        [When(@"I click on link to join by Quicklink details to clipboard")]
        public void WhenIClickOnLinkToJoinByQuicklinkDetailsToClipboard()
        {
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.QuicklinkCopy, _scenarioContext).Click();
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.filters, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgPartBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).Clear();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).SendKeys(Keys.Control + "v");

        }

        [Then(@"I should able to open quicklink on new browser")]
        public void ThenIShouldAbleToOpenQuicklinkOnNewBrowser()
        {
            ApplicationData.hearingListUrl = ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).GetAttribute("value");

        }
        
        [When(@"I click on copy joining by phone details to clipboard")]
        public void WhenIClickOnCopyJoiningByPhoneDetailsToClipboard()
        {
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Phonetoclipboard, _scenarioContext).Click();
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.filters, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgPartBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).SendKeys(Keys.Control + "v");

        }

        [Then(@"phone details should be copied")]
        public void ThenPhoneDetailsShouldBeCopied()
        {
            String hearingID = ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).GetAttribute("value");
            Assert.IsTrue(hearingID.Contains("+448000488500"), "Phone verified");
        }

    }
}

