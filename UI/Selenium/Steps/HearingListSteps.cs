using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UI.Model;

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

        }
    }
}
