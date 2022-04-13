using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;

namespace UI.Steps
{
    [Binding]
    public class ConfirmEquipmentSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        ConfirmEquipmentSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I confirm equipment is working")]
        public void ThenIConfirmEquipmentIsWorking()
        {
            ExtensionMethods.FindElementWithWait(Driver, ConfirmEquipmentPage.ContinueBtn, _scenarioContext).Click();
        }
    }
}