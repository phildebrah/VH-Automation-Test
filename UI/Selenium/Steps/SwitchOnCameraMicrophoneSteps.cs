using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;

namespace UI.Steps
{
    [Binding]
    public class SwitchOnCameraMicrophoneSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        SwitchOnCameraMicrophoneSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I make sure camera and microphone switched on")]
        public void ThenIMakeSureCameraAndMicrophoneSwitchedOn()
        {
            ExtensionMethods.FindElementWithWait(Driver, SwitchOnCameraMicrophonePage.SwitchOnButton, _scenarioContext).Click();
        }
    }
}