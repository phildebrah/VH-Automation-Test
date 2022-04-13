using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;

namespace UI.Steps
{
    [Binding]
    public class WatchtheVideoSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        WatchtheVideoSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I continue to watch the video")]
        public void ThenIContinueToWatchTheVideo()
        {
            ExtensionMethods.FindElementWithWait(Driver, WatchtheVideoPage.WatchVideoButton, _scenarioContext).Click();
        }        
    }
}