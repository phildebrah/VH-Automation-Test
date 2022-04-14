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
using UI.Model;
using TestLibrary.Utilities;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using FluentAssertions;
using System.Collections.Generic;

namespace UI.Steps
{
    [Binding]
    public class CheckParticipantStatusToCommandCentreSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        private Hearing _hearing;

        CheckParticipantStatusToCommandCentreSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"participants have joined the hearing waiting room")]
        public void ThenParticipantsHaveJoinedTheHearingWaitingRoom()
        {
             int participantNum = 0;
            _hearing = (Hearing)_scenarioContext["Hearing"];        
            foreach (var participant in _hearing.Participant)
            {   
                Driver = GetDriver(participant.Id, _scenarioContext);
                _scenarioContext["driver"] = Driver;
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
                ExtensionMethods.FindElementWithWait(Driver, ParticipantHearingListPage.SelectButton(_hearing.Case.CaseNumber), _scenarioContext, TimeSpan.FromSeconds(Config.DefaultElementWait)).Click();
                if (!(participant.Party.Name.ToLower().Contains("judge") || participant.Party.Name.ToLower().Contains("panel")))
                {
                    if (participantNum == 1)
                    {
                        ProceedToWaitingRoom(_hearing.Case.CaseNumber);
                        _hearing.HearingId = Driver.Url.Split('/').LastOrDefault();
                        _scenarioContext["Hearing"] = _hearing;
                    }                    
                }
                participantNum++;
            }
        }

        [Then(@"participants have joined the hearing waiting room without Judge")]
        public void ThenParticipantsHaveJoinedTheHearingWaitingRoomWithoutJudge()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];
            foreach (var participant in _hearing.Participant)
            {
                Driver = GetDriver(participant.Id, _scenarioContext);
                _scenarioContext["driver"] = Driver;
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
                if (!(participant.Party.Name.ToLower().Contains("judge")))
                {
                    if (participant.Party.Name.ToLower().Contains("panel"))
                    {
                        ExtensionMethods.FindElementWithWait(Driver, ParticipantHearingListPage.SelectButton(_hearing.Case.CaseNumber), _scenarioContext, TimeSpan.FromSeconds(Config.DefaultElementWait)).Click();
                    }
                    else
                    {
                        ExtensionMethods.FindElementWithWait(Driver, ParticipantHearingListPage.SelectButton(_hearing.Case.CaseNumber), _scenarioContext, TimeSpan.FromSeconds(Config.DefaultElementWait)).Click();
                        ProceedToWaitingRoom(_hearing.Case.CaseNumber);
                        _hearing.HearingId = Driver.Url.Split('/').LastOrDefault();
                        _scenarioContext["Hearing"] = _hearing;
                    }
                }
            }
        }

        public void ProceedToWaitingRoom(string caseNumber)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
            ExtensionMethods.WaitForElementVisible(Driver, ParticipantHearingListPage.ButtonNext);
            Driver.FindElement(ParticipantHearingListPage.ButtonNext).Click();
            ExtensionMethods.WaitForElementVisible(Driver, ParticipantHearingListPage.ContinueButton);
            Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
            ExtensionMethods.WaitForElementVisible(Driver, ParticipantHearingListPage.SwitchOnButton);
            Driver.FindElement(ParticipantHearingListPage.SwitchOnButton).Click();
            ExtensionMethods.WaitForElementVisible(Driver, ParticipantHearingListPage.WatchVideoButton);
            Driver.FindElement(ParticipantHearingListPage.WatchVideoButton).Click();
            // Assert video is playing
            Driver.RetryClick(ParticipantHearingListPage.ContinueButton, _scenarioContext, TimeSpan.FromSeconds(180));
            if (SkipPracticeVideoHearingDemo)
            {
                string cameraUrl = Driver.Url.Replace("practice-video-hearing", "camera-working");
                Driver.Navigate().GoToUrl(cameraUrl);
                Driver.SwitchTo().Alert().Accept();
            }
            else
            {
                ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.ContinueButton, 180).Click();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
            Driver.FindElement(ParticipantHearingListPage.CameraWorkingYes)?.Click();
            Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
            Driver.FindElement(ParticipantHearingListPage.MicrophoneWorkingYes).Click();
            Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
            Driver.FindElement(ParticipantHearingListPage.VideoWorkingYes).Click();
            Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
            Driver.FindElement(ParticipantHearingListPage.NextButton).Click();
            Driver.FindElement(ParticipantHearingListPage.DeclareCheckbox).Click();
            Driver.FindElement(ParticipantHearingListPage.NextButton).Click();
        }

        [Then(@"the Video Hearings Officer should able to see the status")]
        public void ThenTheVideoHearingsOfficerShouldAbleToSeeTheStatus()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];
            foreach (var driver in (Dictionary<string, IWebDriver>)_scenarioContext["drivers"])
            {
                Driver = driver.Value;
                string participant = driver.Key.Split('#').FirstOrDefault();
                Driver = GetDriver(participant, _scenarioContext);
                if (participant.Equals("VHO"))
                {
                    ExtensionMethods.WaitForElementVisible(Driver, VHOHearingListPage.ParticipantName);
                    Driver.FindElement(VHOHearingListPage.ParticipantStatusInHearing).Displayed.Should().BeTrue();
                    Driver.FindElement(VHOHearingListPage.ParticipantStatusJoining).Displayed.Should().BeTrue();
                    Driver.FindElement(VHOHearingListPage.ParticipantStatusNotSignedIn).Displayed.Should().BeTrue();
                }               
            }
        }

        [Then(@"the Video Hearings Officer should able to view the status")]
        public void ThenTheVideoHearingsOfficerShouldAbleToViewTheStatus()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];
            foreach (var driver in (Dictionary<string, IWebDriver>)_scenarioContext["drivers"])
            {
                Driver = driver.Value;
                string participant = driver.Key.Split('#').FirstOrDefault();
                Driver = GetDriver(participant, _scenarioContext);
                if (participant.Equals("VHO"))
                {
                    ExtensionMethods.WaitForElementVisible(Driver, VHOHearingListPage.ParticipantName);
                    Driver.FindElement(VHOHearingListPage.ParticipantStatusAvailable).Displayed.Should().BeTrue();
                    Driver.FindElement(VHOHearingListPage.ParticipantStatusUnavailable).Displayed.Should().BeTrue();
                    Driver.FindElement(VHOHearingListPage.ParticipantStatusInConsultation).Displayed.Should().BeTrue();                    
                }
            }
        }
    }
}