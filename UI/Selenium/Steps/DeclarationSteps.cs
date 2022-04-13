using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;
using TestFramework;
using UISelenium.Pages;

namespace UI.Steps
{
    [Binding]
    public class DeclarationSteps : ObjectFactory
    {
        ScenarioContext _scenarioContext;

        DeclarationSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I confirm declaration")]
        public void ThenIConfirmDeclaration()
        {
            ExtensionMethods.FindElementWithWait(Driver, DeclarationPage.DeclarationCheckBox, _scenarioContext).Click();
            ExtensionMethods.FindElementWithWait(Driver, DeclarationPage.DeclarationContinueBtn, _scenarioContext).Click();
        }
    }
}