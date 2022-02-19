using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
namespace UI.Steps
{
    [Binding]
    public class VideoAccessSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        public VideoAccessSteps(ScenarioContext scenarioContext)
            :base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
    }
}
