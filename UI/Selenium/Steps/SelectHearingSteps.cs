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
using TestLibrary.Utilities;

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
            if (ExtensionMethods.WaitForPageLoad(Driver, SelectHearingPage.Quicklinks, _scenarioContext))
            {
                ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
                ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Hearingidtoclipboard, _scenarioContext).Click();
                ApplicationData.hearingID = new TextCopy.Clipboard().GetText();
                
            }
        }

        [Then(@"Hearing id should be copied")]
        public void ThenHearingIdShouldBeCopied()
        {
            bool isValid = Guid.TryParse(ApplicationData.hearingID, out Guid guidOutput);
            Assert.IsTrue(isValid);
        }                      

        [When(@"I click on link to join by Quicklink details to clipboard")]
        public void WhenIClickOnLinkToJoinByQuicklinkDetailsToClipboard()
        {
            if (ExtensionMethods.WaitForPageLoad(Driver, SelectHearingPage.Quicklinks, _scenarioContext))
            {
                ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
                ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.QuicklinkCopy, _scenarioContext).Click();
                ApplicationData.hearingListUrl = new TextCopy.Clipboard().GetText();
            }
        }

        [Then(@"I should able to open quicklink on new browser")]
        public void ThenIShouldAbleToOpenQuicklinkOnNewBrowser()
        {            
            Driver.Quit();
            Driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
            _scenarioContext["driver"] = Driver;
            Driver.Navigate().GoToUrl(ApplicationData.hearingListUrl);
        }

        [When(@"I click on copy joining by phone details to clipboard")]
        public void WhenIClickOnCopyJoiningByPhoneDetailsToClipboard()
        {
            if (ExtensionMethods.WaitForPageLoad(Driver, SelectHearingPage.Quicklinks, _scenarioContext))
            {
                ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
                ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Phonetoclipboard, _scenarioContext).Click();
                ApplicationData.hearingPhone = new TextCopy.Clipboard().GetText();
            }
        }

        [Then(@"phone details should be copied")]
        public void ThenPhoneDetailsShouldBeCopied()
        {
            Assert.IsTrue(ApplicationData.hearingPhone.Contains("+448000488500"), "Phone verified");
            
        }       
    }
}