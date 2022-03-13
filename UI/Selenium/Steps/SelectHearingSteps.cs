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
        [Then(@"I click on copy hearing id to clipboard it should able to copy")]
        public void ThenIClickOnCopyHearingIdToClipboardItShouldAbleToCopy()
        {           
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Hearingidtoclipboard, _scenarioContext).Click();
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.filters, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgPartBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).SendKeys(Keys.Control + "v");
            String hearingID = ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).GetAttribute("value");

            Assert.IsTrue(hearingID.Length > 0, "ID verified");

        }

        [Then(@"I click on link to join by quick link details to clipboard it should able to open on new browser")]
        public void ThenIClickOnLinkToJoinByQuickLinkDetailsToClipboardItShouldAbleToOpenOnNewBrowser()
        {
            ExtensionMethods.MoveToElement(Driver,SelectHearingPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.QuicklinkCopy, _scenarioContext).Click();
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.filters, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgPartBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).Clear();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).SendKeys(Keys.Control+"v");
            ApplicationData.hearingListUrl = ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).GetAttribute("value");



            //ExtensionMethods.OpenNewPage(Driver, url);
            //ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.signOut, _scenarioContext).Click();
            //ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.hereLink, _scenarioContext).Click();
            //ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.signOut, _scenarioContext).Click();
            //ExtensionMethods.FindElementWithWait(Driver, LoginPage.PasswordField, _scenarioContext).SendKeys("_6qc2;b=s4m:NRK[");
            //ExtensionMethods.FindElementWithWait(Driver, LoginPage.SignIn, _scenarioContext).Click();

           // ExtensionMethods.CloseAndOpenBrowser(Driver, url);
        }

        
        [Then(@"And I click on copy joining by phone details to clipboard it should able to copy")]
        public void ThenAndIClickOnCopyJoiningByPhoneDetailsToClipboardItShouldAbleToCopy()
        {
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.Phonetoclipboard, _scenarioContext).Click();
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.filters, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgPartBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).SendKeys(Keys.Control + "v");
            String hearingID = ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).GetAttribute("value");

            Assert.IsTrue(hearingID.Contains("+448000488500"),"Phone verified");
        }



    }
}

