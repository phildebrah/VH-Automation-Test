using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace UISelenium.Pages
{
    public class HearingListPage
    {
        public static By ConferenceList = By.ClassName("conference-list");
        public static By VenueListItem(string hearingId) => By.XPath($"//div[@id='{hearingId}-venue']");
        public static By SummaryListItem(string hearingId) => By.XPath($"//div[@id='{hearingId}-summary']");
        public static By CaseNumberListItem(string hearingId) => By.XPath($"//div[@id='{hearingId}-case-number']");
        public static By CaseNameListItem(string hearingId) => By.XPath($"//div[@id='{hearingId}-case-name']");
        public static By Quicklinks => By.CssSelector("fa-icon");
        public static By WaitingRoomIframe => By.Id("admin-frame");
        public static By WaitingRoomJudgeLink => By.XPath("//table[@id='WaitingRoom']//td[contains(.,'Judge')]");
        public static By WaitingRoomParticipantLink => By.XPath("//td[contains(@id,'-WaitingRoom-menu')][not(text(),'Judge')]");
        public static By WaitingRoomMenu2 => By.XPath("//div[@id='vho-admin-view']");
        public static By PrivateConsultation => By.XPath("//*[contains(.,'Private consultation')]");
        public static By HearingListConsultationRooms = By.Id("ConsultationRooms");
        public static By SelfViewButton => By.Id("selfViewButton");
        public static By CloseButton => By.Id("closeButton");
        public static By MuteButton => By.Id("muteButton");
    }
}
