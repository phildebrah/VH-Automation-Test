using OpenQA.Selenium;
namespace UISelenium.Pages
{
    public class ConsultationRoomPage
    {
        public static By InviteParticipant(string name) => By.XPath($"//div[@class='participant-endpoint-row' and contains(.,'{name}')]//app-invite-participant");
        public static By ParticipantTick(string name) => By.XPath($"//div[@class='participant-endpoint-row' and contains(.,'{name}')]//fa-icon[@icon='check']");
    }
}
