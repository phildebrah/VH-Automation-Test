using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Model;
using UISelenium.Pages;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace UI.Steps
{
    public class HearingRoomSteps : ObjectFactory
    {
        private ScenarioContext _scenarioContext;
        private Hearing _hearing;

        public HearingRoomSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _hearing = (Hearing)_scenarioContext["Hearing"];
        }

        [Then(@"the judge checks that all participants have joined the hearing room")]
        public void ThenTheJudgeChecksThatAllParticipantsHaveJoinedTheHearingRoom()
        {
            Driver = GetDriver("Judge", _scenarioContext);
            _scenarioContext["driver"] = Driver;

            foreach (var participant in _hearing.Participant)
            {
                Assert.True(Driver.FindElement(HearingRoomPage.ParticipantDisplayName(participant.DisplayName))?.Displayed);
            }
        }
    }
}
