using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UI.Model;
using UISelenium.Pages;
namespace UI.Steps
{
    [Binding]
    public class WaitingRoomPageSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;
        private Hearing _hearing;
        public WaitingRoomPageSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _hearing = (Hearing)_scenarioContext["Hearing"];
        }

        [Then(@"the judge starts the hearing")]
        public void ThenTheJudgeStartsTheHearing()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait));
            Driver.FindElement(WaitingRoomPage.StartVideoHearingButton).Click();
            Driver.FindElement(WaitingRoomPage.ConfirmStartButton).Click();
        }
    }
}
