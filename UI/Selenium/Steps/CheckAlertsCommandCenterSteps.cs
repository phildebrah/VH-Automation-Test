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
using System.Collections.Generic;
using FluentAssertions;

namespace UI.Steps
{
    [Binding]
    public class CheckAlertsCommandCenterSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;
        private Hearing _hearing;

        CheckAlertsCommandCenterSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I login to VHO in video url as ""([^""]*)"" for existing hearing")]
        public void GivenILoginToVHOInVideoUrlAsForExistingHearing(string userName)
        {

            Driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
            _scenarioContext["driver"] = Driver;
            Driver.Navigate().GoToUrl(Config.VideoUrl);
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.DefaultElementWait));
            wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.UsernameTextfield));
            _scenarioContext.UpdatePageName("Video Web Login");
            Login(userName, Config.UserPassword);
            //_scenarioContext.UpdatePageName("Your Video Hearings");
            //_scenarioContext.Add("drivers", drivers);
        }

        public void Login(string username, string password)
        {
            ExtensionMethods.FindElementWithWait(Driver, LoginPage.UsernameTextfield, _scenarioContext, TimeSpan.FromSeconds(Config.DefaultElementWait)).SendKeys(username);

            Driver.FindElement(LoginPage.Next).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.DefaultElementWait));
            wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.PasswordField));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.SignIn));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.BackButton));
            Driver.FindElement(LoginPage.PasswordField).SendKeys(password);
            ExtensionMethods.FindElementWithWait(Driver, LoginPage.SignIn, _scenarioContext).Click();
        }

        //[Then(@"I log off and close application")]
        //public void ThenILogOffAndCloseApplication()
        //{
        //    _scenarioContext.UpdatePageName("logout");
        //    Driver = (IWebDriver)_scenarioContext["driver"];
        //    if (ExtensionMethods.IsElementVisible(Driver, Header.LinkSignOut, null))
        //    {
        //        Driver.FindElement(Header.LinkSignOut).Click();
        //    }
        //    else
        //    {
        //        Driver.FindElement(Header.SignOut).Click();
        //    }            
        //}

        [Then(@"participant has joined and progressed to waiting room without completing self test")]
        public void ThenParticipantHasJoinedAndProgressedToWaitingRoomWithoutCompletingSelfTest()
        {

            _hearing = (Hearing)_scenarioContext["Hearing"];
            foreach (var driver in (Dictionary<string, IWebDriver>)_scenarioContext["drivers"])
            {
                Driver = driver.Value;
                string participant = driver.Key.Split('#').FirstOrDefault();

                Driver = GetDriver(participant, _scenarioContext);
                _scenarioContext["driver"] = Driver;
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
                ExtensionMethods.FindElementWithWait(Driver, ParticipantHearingListPage.SelectButton(_hearing.Case.CaseNumber), _scenarioContext, TimeSpan.FromSeconds(Config.DefaultElementWait)).Click();
                if (!(participant.ToLower().Contains("judge") || participant.ToLower().Contains("panel")))
                {
                    Driver.FindElement(ParticipantHearingListPage.ButtonNext).Click();
                    Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
                    Driver.FindElement(ParticipantHearingListPage.SwitchOnButton).Click();
                    Driver.FindElement(ParticipantHearingListPage.WatchVideoButton).Click();
                    // Assert video is playing
                    Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();
                    if (SkipPracticeVideoHearingDemo)
                    {
                        string cameraUrl = Driver.Url.Replace("practice-video-hearing", "camera-working");
                        Driver.Navigate().GoToUrl(cameraUrl);
                        Driver.SwitchTo().Alert().Accept();
                    }
                    else
                    {
                        TestFramework.ExtensionMethods.FindElementEnabledWithWait(Driver, ParticipantHearingListPage.ContinueButton, 180).Click();
                        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultElementWait);
                    }
                    Driver.FindElement(ParticipantHearingListPage.CameraWorkingNo)?.Click();
                    Driver.FindElement(ParticipantHearingListPage.ContinueButton).Click();

                }
            }
        }

        [When(@"the Video Hearings Officer check alerts for this hearing")]
        public void WhenTheVideoHearingsOfficerCheckAlertsForThisHearing()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];
            ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.ViewHearings, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.SelectCaseNumber(_hearing.Case.CaseNumber), _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.HearingBtn, _scenarioContext).Click();

        }

        [Then(@"the the Video Hearings Officer see the alert Failed self-test \(No to Camera\) participant F & L name")]
        public void ThenTheTheVideoHearingsOfficerSeeTheAlertFailedSelf_TestNoToCameraParticipantFLName()
        {         
            _hearing = (Hearing)_scenarioContext["Hearing"];

            if (ExtensionMethods.IsElementExists(Driver, SelectYourHearingListPage.FailedAlert, _scenarioContext))
            {
                var alerts = Driver.FindElements(SelectYourHearingListPage.FailedAlert);

                for (int i = 1; i <= alerts.Count; i++)
                {

                    string alertMsg = ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.AlertMsg(i.ToString()), _scenarioContext).Text;
                    string firstLastName = ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.FirstLastName(i.ToString()), _scenarioContext).Text;

                    Assert.AreEqual(alertMsg, "Failed self-test (Camera)");
                    Assert.True(firstLastName.Contains(_hearing.Participant[1].Name.FirstName));
                    Assert.True(firstLastName.Contains(_hearing.Participant[1].Name.LastName));
                }

            }

        }

        [Then(@"the the Video Hearings Officer see the alert Failed self-test \(No to Microphone\) participant F & L name")]
        public void ThenTheTheVideoHearingsOfficerSeeTheAlertFailedSelf_TestNoToMicrophoneParticipantFLName()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];

            if (ExtensionMethods.IsElementExists(Driver, SelectYourHearingListPage.FailedAlert, _scenarioContext))
            {
                var alerts = Driver.FindElements(SelectYourHearingListPage.FailedAlert);

                for (int i = 1; i <= alerts.Count; i++)
                {

                    string alertMsg = ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.AlertMsg(i.ToString()), _scenarioContext).Text;
                    string firstLastName = ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.FirstLastName(i.ToString()), _scenarioContext).Text;

                    Assert.AreEqual(alertMsg, "Failed self-test (Microphone)");
                    Assert.True(firstLastName.Contains(_hearing.Participant[1].Name.FirstName));
                    Assert.True(firstLastName.Contains(_hearing.Participant[1].Name.LastName));
                }

            }

        }

        [Then(@"the the Video Hearings Officer see the alert Failed self-test \(No to Video\) participant F & L name")]
        public void ThenTheTheVideoHearingsOfficerSeeTheAlertFailedSelf_TestNoToVideoParticipantFLName()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];

            if (ExtensionMethods.IsElementExists(Driver, SelectYourHearingListPage.FailedAlert, _scenarioContext))
            {
                var alerts = Driver.FindElements(SelectYourHearingListPage.FailedAlert);

                for (int i = 1; i <= alerts.Count; i++)
                {

                    string alertMsg = ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.AlertMsg(i.ToString()), _scenarioContext).Text;
                    string firstLastName = ExtensionMethods.FindElementWithWait(Driver, SelectYourHearingListPage.FirstLastName(i.ToString()), _scenarioContext).Text;

                    Assert.AreEqual(alertMsg, "Failed self-test (Video)");
                    Assert.True(firstLastName.Contains(_hearing.Participant[1].Name.FirstName));
                    Assert.True(firstLastName.Contains(_hearing.Participant[1].Name.LastName));
                }

            }

        }

    }
}