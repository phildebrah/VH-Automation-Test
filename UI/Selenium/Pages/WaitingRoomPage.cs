using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace UISelenium.Pages
{
    public class WaitingRoomPage
    {
        public static By StartVideoHearingButton => By.XPath("//*[contains(text(),'Start video hearing')]");
        public static By EnterConsultationRoomButton => By.Id("joinPCButton");
        public static By ConfirmStartButton => By.Id("btnConfirmStart");
        public static By CancelStartHearingButton => By.Id("btnCancelStart");
        public static By HearingClosedTitle => By.XPath("//*[contains(text(),'Your video hearing is closed')]");
    }
}
