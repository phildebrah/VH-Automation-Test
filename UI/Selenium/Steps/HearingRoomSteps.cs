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
using System.Linq;
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
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
            ExtensionMethods.FindElementEnabledWithWait(Driver, HearingRoomPage.IncomingFeedJudgeVideo, Config.DefaultElementWait);
            ExtensionMethods.WaitForElementNotVisible(Driver, HearingRoomPage.MicMutedIcon, int.Parse(Config.OneMinuteElementWait));
            foreach (var participant in _hearing.Participant)
            {
                ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantWaitingRoomPage.ParticipantDetails($"{participant.Name.FirstName} {participant.Name.LastName}"), Config.DefaultElementWait).Displayed.Should().BeTrue();
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
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
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
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.IncomingFeedJudgeVideo));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantMicUnlocked));
            ExtensionMethods.WaitForElementNotVisible(Driver, HearingRoomPage.MicMutedIcon, int.Parse(Config.OneMinuteElementWait));
            Driver.FindElement(HearingRoomPage.MuteAndLock).Click();
            ExtensionMethods.WaitForElementVisible(Driver, HearingRoomPage.MicMutedIcon, int.Parse(Config.OneMinuteElementWait));
            foreach (var participant in _hearing.Participant)
            {
                if (!participant.Party.Name.ToLower().Contains("judge"))
                {
                    Driver = GetDriver(participant.Id, _scenarioContext);
                    ExtensionMethods.FindElementEnabledWithWait(Driver, HearingRoomPage.ParticipantMicLocked).Displayed.Should().BeTrue();
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
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
            Driver.FindElement(HearingRoomPage.UnlockMute).Click();
            ExtensionMethods.WaitForElementNotVisible(Driver, HearingRoomPage.MicMutedIcon, int.Parse(Config.OneMinuteElementWait));
            foreach (var participant in _hearing.Participant)
            {
                if (!participant.Party.Name.ToLower().Contains("judge"))
                {
                    Driver = GetDriver($"#{participant.Party.Name}-{participant.Role.Name}", _scenarioContext);
                    wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantMicUnlocked));
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
            ExtensionMethods.WaitForElementNotVisible(Driver, HearingRoomPage.MicMutedIcon, int.Parse(Config.OneMinuteElementWait));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantHandRaised));
        }

        [Then(@"the judge can lower participants hands")]
        public void ThenTheJudgeCanLowerParticipantsHands()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ParticipantHandRaised, _scenarioContext);
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.LowerHands, _scenarioContext).Click();
            ExtensionMethods.WaitForElementNotVisible(Driver, HearingRoomPage.ParticipantHandRaised);
            Assert.IsFalse(ExtensionMethods.IsElementVisible(Driver, HearingRoomPage.ParticipantHandRaised, _scenarioContext));
        }

        [When(@"the participant switches off their camera, the judge can see it on the screen")]
        public void WhenTheParticipantSwitchesOffTheirCameraTheJudgeCanSeeItOnTheScreen()
        {
            Driver = GetDriver(_hearing.Participant[1].Id, _scenarioContext);
            _scenarioContext["driver"] = Driver;
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ParticipantToggleVideo, _scenarioContext).Click();
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(HearingRoomPage.ParticipantCameraOffIcon));
        }

        [When(@"the participant shares their screen, everyone should be able to see the shared screen")]
        public void WhenTheParticipantSharesTheirScreenEveryoneShouldBeAbleToSeeTheSharedScreen()
        {
            var participants = _hearing.Participant.Where(a => !a.Id.ToLower().Contains("judge"));
            // participant shares screen
            foreach(var participant in participants)
            {
                Driver = GetDriver(participant.Id, _scenarioContext);
                _scenarioContext["driver"] = Driver;
                ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ShareScreenButton, _scenarioContext).Click();
                ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ShareDocuments, _scenarioContext).Click();
                ExtensionMethods.WaitForElementVisible(Driver, HearingRoomPage.StopSharingScreen);
                ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.OutgoingScreenShare, _scenarioContext).Displayed.Should().BeTrue();
            }
            // Judge shares screen
            var judge = _hearing.Participant.Where(a => a.Id.ToLower().Contains("judge")).FirstOrDefault();

            Driver = GetDriver(judge.Id, _scenarioContext);
            _scenarioContext["driver"] = Driver;
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ShareScreenButton, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ShareDocuments, _scenarioContext).Click();
            ExtensionMethods.WaitForElementVisible(Driver, HearingRoomPage.StopSharingScreen);
            ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.OutgoingScreenShare, _scenarioContext).Displayed.Should().BeTrue();
            foreach (var user in _hearing.Participant)
            {
                if(user != judge)
                {
                    Driver = GetDriver(user.Id, _scenarioContext);
                    _scenarioContext["driver"] = Driver;
                    ExtensionMethods.WaitForElementNotVisible(Driver, HearingRoomPage.StopSharingScreen);
                    Driver.FindElement(HearingRoomPage.SecondVideoFeed).Displayed.Should().BeTrue();
                    ExtensionMethods.IsElementVisible(Driver, HearingRoomPage.StopSharingScreen, null).Should().BeFalse();
                    ExtensionMethods.IsElementVisible(Driver, HearingRoomPage.SecondVideoFeed, null).Should().BeTrue();
                }
            }
        }

        [When(@"the judge can open and close the participant panel")]
        public void WhenTheJudgeCanOpenAndCloseTheChatPanel()
        {
            var judge = _hearing.Participant.Where(a => a.Id.ToLower().Contains("judge")).FirstOrDefault();
            Driver = GetDriver(judge.Id, _scenarioContext);
            _scenarioContext["driver"] = Driver;
            // open / close participant panel
            for (int i = 0; i < 2; i++)
            {
                if (ExtensionMethods.IsElementVisible(Driver, HearingRoomPage.PanelList, null))
                {
                    ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ParticipantPanelToggel, _scenarioContext).Click();
                    ExtensionMethods.WaitForElementNotVisible(Driver, HearingRoomPage.PanelList, null);
                    ExtensionMethods.IsElementVisible(Driver, HearingRoomPage.PanelList, null).Should().BeFalse();
                }
                else
                {
                    ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.ParticipantPanelToggel, _scenarioContext).Click();
                    ExtensionMethods.FindElementWithWait(Driver, HearingRoomPage.PanelList, _scenarioContext).Displayed.Should().BeTrue();
                }
            }
        }
    }
}