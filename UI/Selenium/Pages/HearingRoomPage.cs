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
    }
}
