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
                ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.HearingList, _scenarioContext).SendKeys(row[0]);
                ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.HearingCheckBox, _scenarioContext).Click();
            }

            
        }


        [When(@"I click on view hearings")]
        public void WhenIClickOnViewHearings()
        {
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.ViewHearings, _scenarioContext).Click();
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
            CommonPageActions.MouseMoveToElement(QuickLinkPage.Quicklinks);
            ExtensionMethods.MoveToElement(Driver,QuickLinkPage.Quicklinks, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, QuickLinkPage.QuicklinkCopy, _scenarioContext).Click();
            ExtensionMethods.OpenNewPage(Driver);

        }

        [Then(@"And I click on hearing link to clipboard it should able to copy hearing link")]
        public void ThenAndIClickOnHearingLinkToClipboardItShouldAbleToCopyHearingLink()
        {
        }



    }
}
