using OpenQA.Selenium;
namespace UI.Pages
{
    public class WaitingRoomPage
    {
        public static By StartVideoHearingButton => By.XPath("//buton[text()[contains(.,'Start video hearing')]]");
    }
}
