using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class ParticipantHearingListPage
    {
        public static By CheckEquipment = By.Id("check-equipment-btn");
        public static By HearingListPageTitle = By.XPath("//*[contains(text(), 'Video hearings for') or contains(text(),'Your video hearing') or contains(text(),'Your video hearings')]");
        //public static By NoHearingsWarningMessage = CommonLocators.ElementContainingText("You do not have a video hearing today");
        public static By CaseName(Guid conferenceId) => By.Id($"participant-case-name-{conferenceId:D}");
        public static By CaseNumber(Guid conferenceId) => By.Id($"participant-case-number-{conferenceId:D}");
        public static By HearingDate(Guid conferenceId) => By.Id($"participant-scheduled-date-{conferenceId:D}");
        public static By HearingTime(Guid conferenceId) => By.Id($"participant-scheduled-time-{conferenceId:D}");
        public static By SignInDate(Guid conferenceId) => By.Id($"participant-sign-in-date-{conferenceId:D}");
        public static By SignInTime(string conferenceId) => By.Id($"participant-sign-in-time-{conferenceId}");
        public static By SignInButton(string conferenceId) => By.Id($"sign-into-hearing-btn-{conferenceId}");
        public static By StartHearingButton(Guid conferenceId) => By.Id($"start-hearing-btn-{conferenceId:D}");
        public static By HealingListRow => By.XPath("//tr[@class='govuk-table__row']");
    }
}
