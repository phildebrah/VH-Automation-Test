using TechTalk.SpecFlow;
using SeleniumSpecFlow.Utilities;
using UISelenium.Pages;
using FluentAssertions;

namespace SeleniumSpecFlow.Steps
{
    [Binding]
    public class LoginPageSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        public LoginPageSteps(ScenarioContext scenarioContext)
            :base (scenarioContext)
        {
            _scenarioContext = scenarioContext;
            
        }

        [Given(@"I log in as VHO ""([^""]*)""")]
        public void GivenILogInAsVHO(string userName)
        {
            var result= CommonPageActions.NavigateToPage(Config.URL, "login.microsoftonline.com");
            result.Should().BeTrue("Cannot nagive to login.microsoft.com");
            
            LoginPage.Login(userName, Config.BambooPassword);
           
            
            //Home.Value.ClickDropDown();

            //  Handle multiple browsers in tests.
            //  _browsers[_c.CurrentUser].Click(AccountTypeSelectionPage.DoNotStayLoggedInButton);



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
