using SeleniumSpecFlow.Utilities;
using System;
using UI.Model;
using UISelenium.Pages;
using TechTalk.SpecFlow;
using NUnit.Framework;
using FluentAssertions;
using TestFramework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UI.Steps
{
    public class HearingRoomSteps : ObjectFactory
    {
        private ScenarioContext _scenarioContext;
        private Hearing _hearing;

        public HearingRoomSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _hearing = (Hearing)_scenarioContext["Hearing"];
        }

        [Then(@"the judge checks that all participants have joined the hearing room")]
        public void ThenTheJudgeChecksThatAllParticipantsHaveJoinedTheHearingRoom()
        {
            _scenarioContext.UpdatePageName("Judge Waiting Room");
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.CloseHearingButton));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
            ExtensionMethods.FindElementEnabledWithWait(Driver, HearingRoomPage.IncomingFeedJudgeVideo, int.Parse(Config.DefaultElementWait));
            foreach (var participant in _hearing.Participant)
            {
                ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantWaitingRoomPage.ParticipantDetails($"{participant.Name.FirstName} {participant.Name.LastName}"), int.Parse(Config.DefaultElementWait)).Displayed.Should().BeTrue();
            }
        }

        [Then(@"the judge closes the hearing")]
        public void ThenTheJudgeClosesTheHearing()
        {
            _scenarioContext.UpdatePageName("Judge Waiting Room");
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.CloseHearingButton));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
            Driver.FindElement(HearingRoomPage.CloseHearingButton).Click();
            Driver.FindElement(HearingRoomPage.ConfirmCloseHearingButton).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(JudgeWaitingRoomPage.EnterPrivateConsultationButton));
            Assert.IsTrue(Driver.FindElement(JudgeWaitingRoomPage.EnterPrivateConsultationButton).Displayed);
            // Assert participants are redirected back to the participants waiting room
            new WaitingRoomPageSteps(_scenarioContext).CheckParticipantsAreInWaitingRoom();
        }

        [Then(@"the the participants microphone are all muted and locked when the judge mutes them")]
        public void ThenTheJudgeMutesTheMicForAllParticipants()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.CloseHearingButton));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.MuteAndLock));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.IncomingFeedJudgeVideo));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantMicUnlocked));
            wait.Until(ExpectedConditions.ElementExists(HearingRoomPage.JudgeYellow));
            //auto_aw.judge_02

            Driver.FindElement(HearingRoomPage.MuteAndLock).Click();
            foreach (var participant in _hearing.Participant)
            {
                if (!participant.Party.Name.ToLower().Contains("judge"))
                {
                    Driver = GetDriver($"#{participant.Party.Name}-{participant.Role.Name}", _scenarioContext);
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
                    Driver.FindElement(HearingRoomPage.ParticipantMicLocked).Displayed.Should().BeTrue();
                }
            }
        }

        [Then(@"the participants microphones are unmuted when the judge unmutes them")]
        public void ThenTheJudgeUnmutesTheMicForTheParticipants()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.CloseHearingButton));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.UnlockMute));
            wait.Until(ExpectedConditions.ElementExists(HearingRoomPage.JudgeYellow));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
            Driver.FindElement(HearingRoomPage.UnlockMute).Click();
            foreach (var participant in _hearing.Participant)
            {
                if (!participant.Party.Name.ToLower().Contains("judge"))
                {
                    Driver = GetDriver($"#{participant.Party.Name}-{participant.Role.Name}", _scenarioContext);
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
                    Driver.FindElement(HearingRoomPage.ParticipantMicUnlocked).Displayed.Should().BeTrue();
                }
            }
        }

        [Then(@"all participants are redirected to the waiting room when the judge pauses the hearing")]
        public void ThenAllParticipantsAreRedirectedToTheWaitingRoomWhenTheJudgePausesTheHearing()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.PauseHearing, _scenarioContext).Click();
            new WaitingRoomPageSteps(_scenarioContext).CheckParticipantsAreInWaitingRoom();
        }

        [Then(@"when a participant raises their hand, it shows on the judge's screen")]
        public void ThenWhenAParticipantRaisesTheirHandItShowsOnTheJudgesScreen()
        {
            Driver = GetDriver(_hearing.Participant[1].Id, _scenarioContext);
            _scenarioContext["driver"] = Driver;
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ParticipantToggleRaiseHand, _scenarioContext).Click();
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementExists(HearingRoomPage.JudgeYellow));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantHandRaised));
        }

        [Then(@"the judge can lower participants hands")]
        public void ThenTheJudgeCanLowerParticipantsHands()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantHandRaised));
            wait.Until(ExpectedConditions.ElementExists(HearingRoomPage.JudgeYellow));
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.LowerHands, _scenarioContext).Click();
            System.Threading.Thread.Sleep(300);
            Assert.IsFalse(ExtensionMethods.IsElementVisible(Driver, HearingRoomPage.ParticipantHandRaised, _scenarioContext));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
        }

        [Then(@"when the participant switches off their camera, the judge can see it on the screen")]
        public void ThenWhenTheParticipantSwitchesOffTheirCameraTheJudgeCanSeeItOnTheScreen()
        {
            Driver = GetDriver(_hearing.Participant[1].Id, _scenarioContext);
            _scenarioContext["driver"] = Driver;
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ParticipantToggleVideo, _scenarioContext).Click();
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementExists(HearingRoomPage.JudgeYellow));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantCameraOffIcon));
        }
    }
}