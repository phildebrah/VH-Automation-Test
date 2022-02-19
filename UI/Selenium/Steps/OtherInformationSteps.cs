using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
namespace UI.Steps
{
    public class OtherInformationSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        public OtherInformationSteps(ScenarioContext scenarioContext)
            :base (scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
    }
}
