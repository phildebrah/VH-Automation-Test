using NUnit.Framework;
using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;
using TestLibrary.Utilities;
using UI.Model;

namespace UI.Steps
{
    [Binding]
    ///<summary>
    /// Steps class for Select Hearing page
    ///</summary>
    public class SelectHearingSteps : ObjectFactory
    {
        private ScenarioContext _scenarioContext;
        private HearingList _hearingList;

        SelectHearingSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _hearingList = (HearingList)_scenarioContext["HearingList"];
        }
          
        [STAThread]
       
        [When(@"I click on copy hearing id to clipboard")]
        public void WhenIClickOnCopyHearingIdToClipboard()
        {
            if (ExtensionMethods.WaitForPageLoad(Driver, SelectHearingPage.Quicklinks, _scenarioContext))
            {
                ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
                ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Hearingidtoclipboard, _scenarioContext).Click();
                _hearingList.HearingListID = new TextCopy.Clipboard().GetText();
                
            }
        }

        [Then(@"Hearing id should be copied")]
        public void ThenHearingIdShouldBeCopied()
        {
            bool isValid = Guid.TryParse(_hearingList.HearingListID, out Guid guidOutput);
            Assert.IsTrue(isValid);
        }                      

        [When(@"I click on link to join by Quicklink details to clipboard")]
        public void WhenIClickOnLinkToJoinByQuicklinkDetailsToClipboard()
        {
            if (ExtensionMethods.WaitForPageLoad(Driver, SelectHearingPage.Quicklinks, _scenarioContext))
            {
                ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
                ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.QuicklinkCopy, _scenarioContext).Click();
                var url = new TextCopy.Clipboard().GetText();
                _hearingList.HearingListURL = url;
            }
        }

        [Then(@"I should able to open quicklink on new browser")]
        public void ThenIShouldAbleToOpenQuicklinkOnNewBrowser()
        {
            Driver.Close();
            Driver.Quit();
            Driver.Dispose();
            Driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
            _scenarioContext["driver"] = Driver;
            Driver.Navigate().GoToUrl(_hearingList.HearingListURL);
        }

        [When(@"I click on copy joining by phone details to clipboard")]
        public void WhenIClickOnCopyJoiningByPhoneDetailsToClipboard()
        {
            if (ExtensionMethods.WaitForPageLoad(Driver, SelectHearingPage.Quicklinks, _scenarioContext))
            {
                ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
                ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Phonetoclipboard, _scenarioContext).Click();
                _hearingList.HearingListPhone = new TextCopy.Clipboard().GetText();
            }
        }

        [Then(@"phone details should be copied")]
        public void ThenPhoneDetailsShouldBeCopied()
        {
            Assert.IsTrue(_hearingList.HearingListPhone.Contains("448000488500"), "Phone verified");
            
        }       
    }
}