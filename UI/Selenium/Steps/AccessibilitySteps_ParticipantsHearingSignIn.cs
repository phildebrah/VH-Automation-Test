using SeleniumSpecFlow.Utilities;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using UI.Model;
using SeleniumSpecFlow.Steps;
using Selenium.Axe;
using FluentAssertions;
using TestFramework;
using UISelenium.Pages;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
namespace UI.Steps
{
    [Binding]
    public class AccessibilitySteps_ParticipantsHearingSignIn : ObjectFactory
    {
        private ScenarioContext _scenarioContext;
        private LoginPageSteps loginSteps;
        private DashboardSteps dashboardSteps;
        VideoAccessSteps videoAccessSteps;
        private AxeBuilder axeResult;
        AccessibilitySteps_VideoBooking accessibilitySteps;
        public string username = "auto_vw.individual_05@hearings.reform.hmcts.net";
        string _pageName;
        Hearing _hearing;
        public AccessibilitySteps_ParticipantsHearingSignIn(ScenarioContext scenarioContext)
           : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            loginSteps = new LoginPageSteps(scenarioContext);
            dashboardSteps = new DashboardSteps(scenarioContext);
            videoAccessSteps = new VideoAccessSteps(scenarioContext);
            accessibilitySteps = new AccessibilitySteps_VideoBooking(scenarioContext);
            axeResult = new AxeBuilder(Driver);
        }

        [Given(@"an individual on the ""([^""]*)"" page")]
        public void GivenAnIndividualOnThePage(string pageName)
        {
            _pageName = pageName;
            accessibilitySteps.GivenImOnThePage("Booking Details");
            accessibilitySteps.ThenThePageShouldBeAccessible();
            RetryBookingConfirmationIfFails();
            _hearing = (Hearing)_scenarioContext["Hearing"];
            loginSteps.LoginUrl = Config.VideoUrl;
        }

        [Then(@"assert page should be accessible")]
        public void ThenAssertPageShouldBeAccessible()
        {
            LoginAsParticipant();
            CheckAccessibility(_pageName);
        }

        public void CheckAccessibility(string pageName)
        {
            switch (pageName)
            {
                case "Hearing List":
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.CheckEquipment);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Introduction":
                    ProceedToPage("Hearing List");
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.SelectButton(_hearing.Case.CaseNumber)).Click();
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Equipment-Check":
                    ProceedToPage("Introduction");
                    Driver.FindElement(ParticipantHearingListPage.ButtonNext).Click();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.ContinueButton);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Switch-On-Camera-Microphone":
                    ProceedToPage("Equipment-Check");
                    Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.SwitchOnButton);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Camera-Working":
                    ProceedToPage("Switch-On-Camera-Microphone");
                    var url = Driver.Url;
                    url = url.Replace("switch-on-camera-microphone", "camera-working");
                    Driver.Navigate().GoToUrl(url);
                    Driver.SwitchTo().Alert().Accept();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.CameraWorkingYes);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Microphone-Working":
                    ProceedToPage("Camera-Working");
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.CameraWorkingYes).Click();
                    Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.MicrophoneWorkingYes);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "See-And-Hear-Video":
                    ProceedToPage("Microphone-Working");
                    Driver.FindElement(ParticipantHearingListPage.MicrophoneWorkingYes).Click();
                    Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.VideoWorkingYes);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Hearing-Rules":
                    ProceedToPage("See-And-Hear-Video");
                    Driver.FindElement(ParticipantHearingListPage.VideoWorkingYes).Click();
                    Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.NextButton);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Declaration":
                    ProceedToPage("Hearing-Rules");
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.NextButton).Click();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.DeclareCheckbox);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Waiting Room":
                    ProceedToPage("Declaration");
                    Driver.FindElement(ParticipantHearingListPage.DeclareCheckbox).Click();
                    Driver.FindElement(ParticipantHearingListPage.NextButton).Click();
                    ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantWaitingRoomPage.ChooseCameraAndMicButton);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
            }
        }

        private void ProceedToPage(string pageName)
        {
            CheckAccessibility(pageName);
        }

        private void LoginAsParticipant()
        {
            this.Driver = StartNewDriver();
            _scenarioContext["driver"] = this.Driver;
            loginSteps = new LoginPageSteps(_scenarioContext);
            axeResult = new AxeBuilder(Driver);
            dashboardSteps = new DashboardSteps(_scenarioContext);
            loginSteps.LoginUrl = Config.VideoUrl;
            loginSteps.GivenILogInAs(username);
        }

        public void RetryBookingConfirmationIfFails()
        {
            if(ExtensionMethods.IsElementVisible(Driver, BookingDetailsPage.CloseBookingFailureWindowButton, _scenarioContext))
            {
                ExtensionMethods.FindElementEnabledWithWait(Driver, BookingDetailsPage.CloseBookingFailureWindowButton).Click();
            }
        }
    }
}
