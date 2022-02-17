using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using TestFramework;
using UI.Model;
using UISelenium.Pages;
using System.Linq;
namespace UI.Steps
{
    [Binding]
    public class HearingSchedule :ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        private Hearing _hearing;
        
        public HearingSchedule(ScenarioContext scenarioContext)
            :base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _hearing = (Hearing)_scenarioContext["Hearing"];
        }

        [Given(@"the hearing has the following schedule details")]
        public void GivenTheHearingHasTheFollowingScheduleDetails(Table table)
        {
            _hearing = CreateHearingModel(table);
            EnterHearingSchedule(_hearing.HearingSchedule);
        }

        private void EnterHearingSchedule(Model.HearingSchedule hearingSchedule)
        {
            Driver.FindElement(HearingSchedulePage.HearingDate).SendKeys(hearingSchedule.HearingDate.FirstOrDefault().ToString("dd/MM/yyyy"));
            Driver.FindElement(HearingSchedulePage.HearingStartTimeHour).SendKeys(hearingSchedule.HearingDate.FirstOrDefault().ToString("HH"));
            Driver.FindElement(HearingSchedulePage.HearingStartTimeMinute).SendKeys(hearingSchedule.HearingDate.FirstOrDefault().ToString("mm"));
            Driver.FindElement(HearingSchedulePage.HearingDurationHour).SendKeys(hearingSchedule.DurationHours); // remove hardcoded string later
            Driver.FindElement(HearingSchedulePage.HearingDurationMinute).SendKeys(hearingSchedule.DurationMinutes);
            new SelectElement(Driver.FindElement(HearingSchedulePage.CourtVenue)).SelectByText(hearingSchedule.HearingVenue);
            Driver.FindElement(HearingSchedulePage.CourtRoom).SendKeys(hearingSchedule.HearingRoom);
            Driver.FindElement(HearingDetailsPage.NextButton).Click();
        }

        private Hearing CreateHearingModel(Table table)
        {
            var tableRow = table.Rows[0];
            var date = DateTime.Now.AddMinutes(2);
            _hearing.HearingSchedule.HearingDate = new System.Collections.Generic.List<DateTime> { date };
            _hearing.HearingSchedule.HearingTime = date;
            _hearing.HearingSchedule.DurationHours = tableRow["Duration Hour"];
            _hearing.HearingSchedule.DurationMinutes = tableRow["Duration Minute"];
            _hearing.HearingSchedule.HearingVenue = "Birmingham Civil and Family Justice Centre";
            _hearing.HearingSchedule.HearingRoom = new Random().Next(99, 99999).ToString();
            _scenarioContext["Hearing"] = _hearing;
            return _hearing;
        }
    }
}
