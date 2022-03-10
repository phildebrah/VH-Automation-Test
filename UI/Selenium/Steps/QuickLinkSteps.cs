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

namespace UI.Steps
{
    [Binding]
    public class QuickLinkSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        QuickLinkSteps(ScenarioContext scenarioContext)
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

        [Then(@"I should naviagte to Hearing list page")]
        public void ThenIShouldNaviagteToHearingListPage()
        {
            //Assert.IsTrue(ExtensionMethods.VerifyPageUrl(Driver, "hearings-list"));
        }
        [STAThread]
        [Then(@"I click on link copy id to clipboard it should able to copy")]
        public void ThenIClickOnLinkCopyIdToClipboardItShouldAbleToCopy()
        {
            //Clipboard.GetText();
            //Clip
            


        }

        [Then(@"I click on link to join by quick link details to clipboard it should able to open on new browser")]
        public void ThenIClickOnLinkToJoinByQuickLinkDetailsToClipboardItShouldAbleToOpenOnNewBrowser()
        {
            ExtensionMethods.MoveToElement(Driver,SelectHearingPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.QuicklinkCopy, _scenarioContext).Click();
            ExtensionMethods.MoveToElement(Driver, SelectHearingPage.filters, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.UnreadMsgPartBtn, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).SendKeys(Keys.Control+"v");
            String url = ExtensionMethods.FindElementWithWait(Driver, SelectHearingPage.NewMessageBox, _scenarioContext).GetAttribute("value");
            ExtensionMethods.OpenNewPage(Driver, url);
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.signOut, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.hereLink, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.signOut, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, LoginPage.PasswordField, _scenarioContext).SendKeys("_6qc2;b=s4m:NRK[");
            ExtensionMethods.FindElementWithWait(Driver, LoginPage.SignIn, _scenarioContext).Click();
            ExtensionMethods.NavigateUrl(Driver, url);
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
                ExtensionMethods.FindElementWithWait(Driver, JoinYourHearingPage.ContinueButton, _scenarioContext).Click();
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




        [Then(@"And I click on hearing link to clipboard it should able to copy hearing link")]
        public void ThenAndIClickOnHearingLinkToClipboardItShouldAbleToCopyHearingLink()
        {
        }



    }
}
