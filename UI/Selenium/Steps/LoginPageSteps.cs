using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SeleniumSpecFlow.Utilities;
namespace UI.Steps
{
    [Binding]
    internal class LoginPageSteps : ObjectFactory
    {
        ScenarioContext context;
        LoginPageSteps(ScenarioContext scenarioContext)
        {
            context = scenarioContext;
        }

        [Given(@"I log in as VHO ""([^""]*)""")]
        public void GivenILogInAsVHO(string p0)
        {
            Home.Value.ClickDropDown();
            //SeleniumSpecFlow.Hooks.Driver.Navigate(SeleniumSpecFlow.Hooks.config.URL.);
            //SeleniumSpecFlow.Hooks.config.URL = 
            //throw new PendingStepException();
        }

        [Given(@"I want to creat a new hearing with Judge, (.*) Interpreter, (.*) complainant, (.*) respondant, (.*) VHO, (.*) representative")]
        public void GivenIWantToCreatANewHearingWithJudgeInterpreterComplainantRespondantVHORepresentative(int p0, int p1, int p2, int p3, int p4)
        {
            throw new PendingStepException();
        }

        [When(@"I start a hearing")]
        public void WhenIStartAHearing()
        {
            throw new PendingStepException();
        }

        [Then(@"all the attendees will be seen")]
        public void ThenAllTheAttendeesWillBeSeen()
        {
            throw new PendingStepException();
        }

    }
}
