using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using SeleniumSpecFlow.Steps;
using UI.Utilities;
using UI.Model;
namespace UI.Steps
{
    [Binding]
    internal class DeleteBookingSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        public string username = "auto_aw.videohearingsofficer_01@hearings.reform.hmcts.net";
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

        public DeleteBookingSteps(ScenarioContext scenarioContext)
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
            username = RandomizeEmail(username);
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

        [When(@"I delete the booking")]
        public void WhenIDeleteTheBooking()
        {

        }

        [Then(@"The booking should be deleted")]
        public void ThenTheBookingShouldBeDeleted()
        {

        }

    }
}
