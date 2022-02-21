
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;
using TestFramework;
using UI.Model;
using UISelenium.Pages;

namespace UI.Steps
{
    [Binding]
    public class LogoffSteps: ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
       
        public LogoffSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I log off")]
        public void ThenILogOff()
        {
            Driver.FindElement(Header.LogOffLink).Click();
        }

    }
}
