﻿using OpenQA.Selenium;
namespace UISelenium.Pages
{
    public class ParticipantWaitingRoomPage
    {
        public static By StartVideoHearingButton => By.XPath("//button[contains(text(),'Start video hearing')]");
        public static By AcceptConsultationButton => By.Id("notification-toastr-invite-accept");
        public static By ConfirmStartButton => By.Id("btnConfirmStart");
        public static By CancelStartHearingButton => By.Id("btnCancelStart");
        public static By HearingClosedTitle => By.XPath("//h1[contains(text(),'This hearing has finished. You may now sign out')]");
        public static By ParticipantDetails(string name) => By.XPath($"//*[contains(text(),'{name}')]");
        public static By JointPrivateMeetingCheckbox(string name) => By.XPath($"//div[contains(.,'{name}')]//input");
        public static By PrivateMeetingModal=>By.XPath("//div[@class='modal-content']");
        public static By ChooseCameraAndMicButton => By.Id("changeCameraButton");

        public static By ContinueJoiningPrivateMeetingButton => By.Id("continue-btn");
        public static By StartPrivateMeetingButton => By.Id("openStartPCButton");
        public static By JoinPrivateMeetingButton => By.Id("openJoinPCButton");
        public static string ParticipantWaitingRoomClosedTitle = "This hearing has finished. You may now sign out";
        public static string ParticipantWaitingRoomPausedTitle = "The judge will restart the hearing when they are ready. Please stay near your screen";
    }
}
