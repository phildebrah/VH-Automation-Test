using FluentAssertions;
using SeleniumSpecFlow.Utilities;
using TechTalk.SpecFlow;


namespace SeleniumSpecFlow.Steps
{
    [Binding]
    public class WebTestSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;

        public WebTestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }

        [When(@"I click on ""(.*)""")]
        public void WhenIClickOn(string option)
        {
            Home.Value.ClickDropDown();
        }

        [When(@"I select ""(.*)"" from dropdown list")]
        public void WhenISelectFromDropdownList(string option)
        {
            DropdownList.Value.SelectDropdownValue(option);
        }

        [Then(@"I validate ""(.*)"" is selected")]
        public void ThenIValidateIsSelected(string value)
        {
            DropdownList.Value.SelectedDropDown.Text.Should().Match(d => (d.ToString() == value));
        }

        
    }
}
