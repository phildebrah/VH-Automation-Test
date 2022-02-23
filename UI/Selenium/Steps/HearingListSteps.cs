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
                ProceedToWaitingRoom(driver.Key.Split('#').LastOrDefault().Split('-').FirstOrDefault(), _hearing.Case.CaseNumber);
                //if (driver.Key.Contains("Judge"))
                //{
                //    Driver = ((Dictionary<string, IWebDriver>)_scenarioContext["drivers"]).FirstOrDefault(a => a.Key.Contains("Judge")).Value;
                //    var id = Driver.FindElements(JudgeHearingListPage.HealingListRow).Where(a => a.Text.Contains($"{_hearing.Case.CaseNumber}"))?.FirstOrDefault().GetAttribute("id");
                //    id = id.Replace("judges-list-", "start-hearing-btn-");
                //    TestFramework.ExtensionMethods.MoveToElement(Driver, By.Id(id))?.Click();
                //    _hearing.HearingId = id.Split(new string[] { "start-hearing-btn-" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                //    _scenarioContext["Hearing"] = _hearing;
                //    _scenarioContext["driver"] = Driver;
                //    Driver.FindElement(JudgeHearingListPage.SelectButton(_hearing.HearingId)).Click();
                //}
                //else
                //{
                //    Driver.FindElement(ParticipantHearingListPage.SignInButton(_hearing.HearingId)).Click();
                //    _scenarioContext["driver"] = Driver;
                //}
            }
        }

        public void ProceedToWaitingRoom(string participant, string caseNumber)
        {
            Driver = GetDriver(participant);
            var id = Driver.FindElements(JudgeHearingListPage.HealingListRow).Where(a => a.Text.Contains($"{caseNumber}"))?.FirstOrDefault().GetAttribute("id");
            _scenarioContext["driver"] = Driver;
            if (id.Contains("judges-list-"))
            {
                id = id.Replace("judges-list-", "");
                _hearing.HearingId = id;
                _scenarioContext["Hearing"] = _hearing;
                Driver.FindElement(JudgeHearingListPage.SelectButton(id)).Click();
                
            }
            else
            {
                id = id.Replace("sign-into-hearing-btn-", "");
                _hearing.HearingId = id;
                Driver.FindElement(ParticipantHearingListPage.SignInButton(id)).Click();
                _scenarioContext["Hearing"] = _hearing;
            }
        }

        public IWebDriver GetDriver(string participant)
        {
            return ((Dictionary<string, IWebDriver>)_scenarioContext["drivers"]).FirstOrDefault(a => a.Key.Contains(participant)).Value;
        }
    }
}
