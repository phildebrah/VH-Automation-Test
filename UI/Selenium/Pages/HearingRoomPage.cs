using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace UISelenium.Pages
{
    public class HearingRoomPage
    {
        public static By LabelStatusAvailable = By.XPath("//label[contains(@class, 'label-status--available')]");
        public static By ParticipantRole = By.XPath("//p[contains(@class, 'hearing-role-participant')]");
        public static By ParticipantType = By.XPath("//label[contains(@class, 'case-type-group-participant')]");
        public static By PanelList = By.Id("panelList");
        public static By EndHearing = By.Id("end-hearing-desktop");
        public static By PauseHearing = By.Id("pause-hearing-desktop");
        public static By MuteAndLock => By.XPath("//*[contains(text(),'Mute & lock')]");
        public static By UnlockMute => By.XPath("//*[contains(text(),'Unlock mute')]");
        public static By LowerHands => By.XPath("//*[contains(text(),'Lower hands')]");
        public static By ParticipantDisplayName(string name) => By.XPath($"//*[contains(text(),'{name}')]");
    }
}
