using OpenQA.Selenium;
namespace UISelenium.Pages
{
    public class ConsultationRoomPage
    {
        public static By InviteParticipant(string name) => By.XPath($"//div[@class='participant-endpoint-row' and contains(.,'{name}')]//app-invite-participant");
        public static By InviteParticipants => By.ClassName("phone");
        public static By ParticipantsTick => By.CssSelector(".member-group+.member-group .yellow fa-icon");
        public static By ParticipantTick(string name) => By.XPath($"//div[@class='participant-endpoint-row' and contains(.,'{name}')]//fa-icon[@icon='check']");
        public static By ConfirmLeaveButton => By.Id("consultation-leave-button");
        public static By CloseButton => By.Id("closeButton");
    }
}
