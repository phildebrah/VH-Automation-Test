using OpenQA.Selenium;
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
    public class HearingListSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;
        private Hearing _hearing;
        HearingListSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)    
        {
            _scenarioContext = scenarioContext;
            _hearing = (Hearing)_scenarioContext["Hearing"];
        }
        [Then(@"all participants have joined the hearing waiting room")]
        public void ThenAllParticipantsHaveJoinedTheHearingWaitingRoom()
        {
            SignAllParticipantsIn();
        }

        public void SignAllParticipantsIn()
        {
            foreach(var driver in (Dictionary<string, IWebDriver>)_scenarioContext["drivers"])
            {
                Driver = driver.Value;
                if (driver.Key.Contains("Judge"))
                {
                    Driver.FindElement(JudgeHearingListPage.SelectButton(_hearing.HearingId)).Click();
                }
                else
                {
                    Driver.FindElement(ParticipantHearingListPage.SignInButton(_hearing.HearingId)).Click();
                }
            }
        }
    }
}
