using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using SeleniumSpecFlow.Steps;
using UI.Utilities;
using UI.Model;
using TestFramework;
using UISelenium.Pages;
namespace UI.Steps
{
     internal class BookingListSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        public string username = "auto_aw.videohearingsofficer_02@hearings.reform.hmcts.net";
        private LoginPageSteps loginSteps;
        private DashboardSteps dashboardSteps;
        private HearingScheduleSteps hearingScheduleSteps;
        private CreateHearingDetails createHearingDetails;
        ParticipantsSteps participantsSteps;
        HearingAssignJudgeSteps hearingAssignJudgeSteps;
        VideoAccessSteps videoAccessSteps;
        OtherInformationSteps otherInformationSteps;
        SummaryPageSteps summaryPageSteps;
        Hearing _hearing;

        public BookingListSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            loginSteps = new LoginPageSteps(scenarioContext);
            dashboardSteps = new DashboardSteps(scenarioContext);
            createHearingDetails = new CreateHearingDetails(scenarioContext);
            participantsSteps = new ParticipantsSteps(scenarioContext);
            videoAccessSteps = new VideoAccessSteps(scenarioContext);
            otherInformationSteps = new OtherInformationSteps(scenarioContext);
            summaryPageSteps = new SummaryPageSteps(scenarioContext);
        }

        [Given(@"I have a booked hearing")]
        public void GivenIHaveABookedHearing()
        {

            loginSteps.GivenILogInAs(username);
            dashboardSteps.GivenISelectBookAHearing();
            createHearingDetails.GivenIWantToCreateAHearingWithCaseDetails(StepsHelper.Set.HearingDetailsData());
            _hearing = (Hearing)_scenarioContext["Hearing"];
            hearingScheduleSteps = new HearingScheduleSteps(_scenarioContext);
            hearingScheduleSteps.GivenTheHearingHasTheFollowingScheduleDetails(StepsHelper.Set.HearingScheduleData());
            hearingAssignJudgeSteps = new HearingAssignJudgeSteps(_scenarioContext);
            hearingAssignJudgeSteps.GivenIWantToAssignAJudgeWithCourtroomDetails(StepsHelper.Set.JudgeData());
            participantsSteps.GivenIWantToCreateAHearingFor(StepsHelper.Set.ParticipantsData());
            videoAccessSteps.GivenWithVideoAccessPointsDetails(StepsHelper.Set.VideoAccessPointData());
            otherInformationSteps.GivenISetAnyOtherInformation(StepsHelper.Set.SetOtherInfoData());
            summaryPageSteps.GivenIBookTheHearing();
            summaryPageSteps.ThenAHearingShouldBeCreated();
        }

        [When(@"I navigate to booking list page")]
        public void INavigateToBookingListPage()
        {
            _hearing = (Hearing)_scenarioContext["Hearing"];
            ExtensionMethods.FindElementWithWait(Driver, Header.BookingsList, _scenarioContext).Click();
            ExtensionMethods.FindElementEnabledWithWait(Driver, BookingListPage.HearingDateTitle).Displayed.Should().BeTrue();
            ExtensionMethods.FindElementEnabledWithWait(Driver, BookingListPage.HearingDetailsRow).Displayed.Should().BeTrue();
            ExtensionMethods.FindElementEnabledWithWait(Driver, BookingListPage.HearingDetailsRowSpecific(_hearing.Case.CaseName)).Displayed.Should().BeTrue();
            ExtensionMethods.FindElementEnabledWithWait(Driver, BookingListPage.HearingDetailsRowSpecific(_hearing.Case.CaseNumber)).Displayed.Should().BeTrue();
            ExtensionMethods.FindElementEnabledWithWait(Driver, BookingListPage.HearingDetailsRowSpecific(_hearing.HearingSchedule.HearingVenue)).Displayed.Should().BeTrue();
            Driver.FindElement(BookingListPage.SearchCaseTextBox).SendKeys(_hearing.Case.CaseNumber);
            Driver.FindElement(BookingListPage.SearchButton).Click();
            ExtensionMethods.FindElementWithWait(Driver, BookingDetailsPage.BookingConfirmedStatus, _scenarioContext);
            Driver.FindElements(BookingListPage.HearingDetailsRow).Count.Should().Be(1);
        }

        [Then(@"The booking should contain expected values")]
        public void ThenTheBookingShouldContainExpectedValues()
        {

        }

    }
}
