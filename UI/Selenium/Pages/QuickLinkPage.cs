using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenium.Pages
{
    public class QuickLinkPage
    {
        
        public static By HearingList => By.CssSelector("input[aria-autocomplete='list']");
        public static By HearingCheckBox => By.CssSelector("input[type='checkbox']");
        public static By ViewHearings => By.CssSelector("#select-venue-allocation-btn");
        public static By Quicklinks => By.CssSelector("fa-icon");
        public static By QuicklinkCopy => By.XPath("//a[text()='Copy join by quick link details to clipboard']");
        public static By FullName => By.Id("full-name");
        public static By QuickLinkParticipant => By.Id("QuickLinkParticipant");
        public static By QuickLinkObserver => By.Id("QuickLinkObserver");
        public static By ContinueButton => By.Id("continue-button");

        public static By CheckEquipmentButton => By.Id("check-equipment-btn");

        public static By ContinueBtn => By.Id("continue-btn");

        public static By SwitchOnButton => By.Id("switch-on-btn");

        public static By WatchVideoButton => By.Id("watch-video-btn");

        public static By EquipmentWorksButton => By.Id("equipment-works-btn");
        public static By EquipmentFaultyButton => By.Id("equipment-faulty-btn");
        //public static By CheckEquipmentButton => By.Id("check-equipment-btn");
        public static By NewMessageBox => By.CssSelector("#new-message-box");
        public static By UnreadMsgBtn => By.XPath("//app-unread-messages/img");
        public static By UnreadMsgPartBtn => By.XPath("//app-unread-messages-participant/img");

        public static By signOut => By.CssSelector(".table-row");
        public static By hereLink => By.LinkText("here");



        public static By SignInToHearingButton => By.XPath("//button[contains(@id,'sign-into-hearing-btn-')]");

        public static By NextButton => By.Id("next");

        public static By CameraYesRadioButton => By.Id("camera-yes");
        public static By CameraNoRadioButton => By.Id("camera-no");

        public static By MicrophoneYesRadioBUtton => By.Id("microphone-yes");
        public static By MicrophoneNoRadioBUtton => By.Id("microphone-no");

        public static By VideoYesRadioButton => By.Id("video-yes");
        public static By VideoNoRadioButton => By.Id("video-no");

        public static By CourtRulesContinueBtn => By.Id("nextButton");

        public static By DeclarationCheckBox => By.Id("declare");
        public static By DeclarationContinueBtn => By.Id("nextButton");

        public static By StartPrivateMeeting => By.Id("openStartPCButton");
        public static By JoinPrivateMeeting => By.Id("openJoinPCButton");













    }
}
