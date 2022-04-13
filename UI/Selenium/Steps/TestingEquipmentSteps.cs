using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;
namespace UI.Steps
{
    [Binding]
    public class TestingEquipmentSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        TestingEquipmentSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"Testing your equipment")]
        public void ThenIConfirmEquipmentIsWorking()
        {
            ExtensionMethods.FindElementWithWait(Driver, TestingEquipmentPage.ContinueBtn, _scenarioContext).Click();
                       
        }      
    }
}