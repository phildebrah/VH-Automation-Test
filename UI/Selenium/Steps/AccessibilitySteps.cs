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

namespace UI.Steps
{
    [Binding]
    public class AccessibilitySteps : ObjectFactory
    {
        private ScenarioContext _scenarioContext;
        private Hearing _hearing;
        private string username = "auto_aw.videohearingsofficer_01@hearings.reform.hmcts.net";
        private LoginPageSteps loginSteps;
        private DashboardSteps dashboardSteps;
        private HearingScheduleSteps hearingScheduleSteps;
        private CreateHearingDetails createHearingDetails;
        ParticipantsSteps participantsSteps;
        HearingAssignJudgeSteps hearingAssignJudgeSteps;
        VideoAccessSteps videoAccessSteps;
        OtherInformationSteps otherInformationSteps;
        SummaryPageSteps summaryPageSteps;
        private AxeBuilder axeResult;
        string _pageName;
        public AccessibilitySteps(ScenarioContext scenarioContext)
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
            axeResult = new AxeBuilder(Driver);
        }

        [Given(@"I'm on the ""([^""]*)"" page")]
        public void GivenImOnThePage(string pageName)
        {
            _pageName = pageName;
            loginSteps.GivenILogInAs(username);
        }

        [Then(@"the page should be accessible")]
        public void ThenThePageShouldBeAccessible()
        {
            CheckAccessibilityCompliency(_pageName);
        }

        private void CheckAccessibilityCompliency(string pageName)
        {
            ExtensionMethods.FindElementWithWait(Driver, DashboardPage.BookHearingButton, _scenarioContext);
            axeResult.Analyze().Violations.Should().BeEmpty();
            switch(pageName)
            {
                case "Hearing Details":
                    dashboardSteps.GivenISelectBookAHearing();
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Hearing Schedule":
                    ProceedToPage("Hearing Details");
                    var table = new Table(new string[] { "Case Number", "Case Name", "Case Type", "Hearing Type" });
                    Dictionary<string, string> data = new Dictionary<string, string>()
                    {
                        ["Case Number"] = "AA",
                        ["Case Name"] = "AutomationTestCaseName",
                        ["Case Type"] = "Civil",
                        ["Hearing Type"] = "Enforcement Hearing"
                    };
                    table.AddRow(data);
                    createHearingDetails.GivenIWantToCreateAHearingWithCaseDetails(table);
                    ExtensionMethods.FindElementWithWait(Driver, HearingSchedulePage.HearingDate, _scenarioContext);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "AssignJudge":
                    ProceedToPage("Hearing Schedule");
                    hearingScheduleSteps = new HearingScheduleSteps(_scenarioContext);
                    table = new Table(new string[] { "Duration Hour", "Duration Minute"});
                    data = new Dictionary<string, string>()
                    {
                        ["Duration Hour"] = "0",
                        ["Duration Minute"] = "30"
                    };
                    table.AddRow(data);
                    hearingScheduleSteps.GivenTheHearingHasTheFollowingScheduleDetails(table);
                    ExtensionMethods.FindElementWithWait(Driver, HearingAssignJudgePage.JudgeEmail, _scenarioContext);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Participants":
                    ProceedToPage("AssignJudge");
                    hearingAssignJudgeSteps = new HearingAssignJudgeSteps(_scenarioContext);
                    table = new Table(new string[] { "Judge or Courtroom Account" });
                    data = new Dictionary<string, string>()
                    {
                        ["Judge or Courtroom Account"] = "auto_aw.judge_02@hearings.reform.hmcts.net"
                    };
                    table.AddRow(data);
                    hearingAssignJudgeSteps.GivenIWantToAssignAJudgeWithCourtroomDetails(table);
                    new SelectElement(ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.PartyDropdown, _scenarioContext));
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Video Access Points":
                    ProceedToPage("Participants");
                    table = new Table(new string[] { "Party", "Role", "Id" });
                    data = new Dictionary<string, string>()
                    {
                        ["Party"] = "Claimant",
                        ["Role"] = "Litigant in person",
                        ["Id"] = "auto_vw.individual_05@hearings.reform.hmcts.net",
                    };
                    table.AddRow(data);
                    participantsSteps.GivenIWantToCreateAHearingFor(table);
                    ExtensionMethods.GetSelectElementWithText(Driver, VideoAccessPointsPage.DefenceAdvocate(0), "None", _scenarioContext);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Other Information":
                    ProceedToPage("Video Access Points");
                    table = new Table(new string[] { "Display Name", "Advocate" });
                    data = new Dictionary<string, string>()
                    {
                        ["Display Name"] = "",
                        ["Advocate"] = ""
                    };
                    table.AddRow(data);
                    videoAccessSteps.GivenWithVideoAccessPointsDetails(table);
                    ExtensionMethods.FindElementWithWait(Driver, OtherInformationPage.OtherInfo, _scenarioContext);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;

                case "Summary":
                    ProceedToPage("Other Information");
                    table = new Table(new string[] { "Record Hearing", "Other information"});
                    data = new Dictionary<string, string>()
                    {
                        ["Record Hearing"] = "",
                        ["Other information"] = "accessiblility test"
                    };
                    table.AddRow(data);
                    otherInformationSteps.GivenISetAnyOtherInformation(table);
                    ExtensionMethods.FindElementWithWait(Driver, SummaryPage.BookButton, _scenarioContext);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Booking Confirmation":
                    ProceedToPage("Summary");
                    summaryPageSteps.GivenIBookTheHearing();
                    summaryPageSteps.ThenAHearingShouldBeCreated();
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Booking Details":
                    ProceedToPage("Booking Confirmation");
                    ExtensionMethods.FindElementWithWait(Driver, BookingConfirmationPage .ViewBookingLink, _scenarioContext).Click();
                    ExtensionMethods.FindElementWithWait(Driver, BookingDetailsPage.ConfirmBookingButton, _scenarioContext);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
                case "Booking List":
                    ExtensionMethods.FindElementWithWait(Driver, Header.BookingsList, _scenarioContext).Click();
                    ExtensionMethods.FindElementWithWait(Driver, BookingListPage.VideoHearingsTable, _scenarioContext);
                    axeResult.Analyze().Violations.Should().BeEmpty();
                    break;
            }
        }

        private void ProceedToPage(string pageName)
        {
            CheckAccessibilityCompliency(pageName);
        }
    }
}
