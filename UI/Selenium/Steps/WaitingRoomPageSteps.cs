using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using UI.Model;
using UISelenium.Pages;
using FluentAssertions;
using TestFramework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UI.Steps
{
    [Binding]
    public class WaitingRoomPageSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;
        private Hearing _hearing;
        public WaitingRoomPageSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _hearing = (Hearing)_scenarioContext["Hearing"];
        }

        [Then(@"the judge starts the hearing")]
        public void ThenTheJudgeStartsTheHearing()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(ParticipantWaitingRoomPage.StartVideoHearingButton));
            ExtensionMethods.FindElementWithWait(Driver, ParticipantWaitingRoomPage.StartVideoHearingButton, _scenarioContext).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(ParticipantWaitingRoomPage.ConfirmStartButton));
            ExtensionMethods.FindElementWithWait(Driver, ParticipantWaitingRoomPage.ConfirmStartButton, _scenarioContext).Click();
            _scenarioContext.UpdatePageName("Judge Waiting Room");
        }

        [Then(@"all participants are redirected to the hearing room when the judge resumes the video hearing")]
        public void ThenAllParticipantsAreRedirectedToTheHearingRoomWhenTheJudgeResumesTheVideoHearing()
        {
            ResumeVideoHearing();
            new HearingRoomSteps(_scenarioContext).ThenTheJudgeChecksThatAllParticipantsHaveJoinedTheHearingRoom();
        }

        public void ResumeVideoHearing()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(JudgeWaitingRoomPage.ResumeVideoHearing));
            ExtensionMethods.FindElementWithWait(Driver, JudgeWaitingRoomPage.ResumeVideoHearing, _scenarioContext).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(ParticipantWaitingRoomPage.ConfirmStartButton));
            ExtensionMethods.FindElementWithWait(Driver, ParticipantWaitingRoomPage.ConfirmStartButton, _scenarioContext).Click();
        }

        public void CheckParticipantsAreInWaitingRoom()
        {
            foreach (var participant in _hearing.Participant)
            {
                if (!participant.Party.Name.ToLower().Contains("judge"))
                {
                    Driver = GetDriver($"#{participant.Party.Name}-{participant.Role.Name}", _scenarioContext);
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.DefaultElementWait));
                    wait.Until(ExpectedConditions.ElementToBeClickable(ParticipantWaitingRoomPage.JoinPrivateMeetingButton));
                    wait.Until(ExpectedConditions.ElementToBeClickable(ParticipantWaitingRoomPage.ChooseCameraAndMicButton));
                    var waitingRoomPage = Driver.PageSource;
                    bool isPaused = waitingRoomPage.Contains("Your video hearing is paused") ? true : false;
                    var role = participant.Role.Name == "Solicitor" ? "Representative" : participant.Role.Name;
                    _scenarioContext["driver"] = Driver;
                    if(isPaused)
                    {
                        waitingRoomPage.Should().Contain(ParticipantWaitingRoomPage.ParticipantWaitingRoomPausedTitle);
                    }
                    else
                    {
                        waitingRoomPage.Should().Contain(ParticipantWaitingRoomPage.ParticipantWaitingRoomClosedTitle);
                    }
                    Driver.FindElement(ParticipantWaitingRoomPage.ParticipantDetails($"{participant.Name.FirstName} {participant.Name.LastName}")).Displayed.Should().BeTrue();
                    Driver.FindElement(ParticipantWaitingRoomPage.ParticipantDetails(participant.Party.Name)).Displayed.Should().BeTrue();
                    Driver.FindElement(ParticipantWaitingRoomPage.ParticipantDetails(_hearing.Case.CaseNumber)).Displayed.Should().BeTrue();
                    Driver.FindElement(ParticipantWaitingRoomPage.ChooseCameraAndMicButton).Displayed.Should().BeTrue();
                    Driver.FindElement(ParticipantWaitingRoomPage.JoinPrivateMeetingButton).Displayed.Should().BeTrue();
                }
            }
        }
    }
}
