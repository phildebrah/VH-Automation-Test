using TechTalk.SpecFlow;
using SeleniumSpecFlow.Utilities;
using UISelenium.Pages;
using FluentAssertions;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras;
using OpenQA.Selenium.Support.UI;
using System;

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

        [Given(@"I log in as ""([^""]*)""")]
        public void GivenILogInAs(string userName)
        {
            var result= CommonPageActions.NavigateToPage(Config.URL, "login.microsoftonline.com");
            result.Should().BeTrue("Cannot navigate to login.microsoftonline.com" + "" );
            
            Login(userName, Config.BambooPassword);
           
            
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
           // throw new PendingStepException();
        }

        [When(@"I start a hearing")]
        public void WhenIStartAHearing()
        {
            //throw new PendingStepException();
        }

        [Then(@"all the attendees will be seen")]
        public void ThenAllTheAttendeesWillBeSeen()
        {
            //throw new PendingStepException();
        }

        public void Login(string username, string password)
        {
            Driver.FindElement(LoginPage.UsernameTextfield).SendKeys(username);
            Driver.FindElement(LoginPage.Next).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.PasswordField));
            Driver.FindElement(LoginPage.PasswordField).SendKeys(password);
            Driver.FindElement(LoginPage.SignIn).Click();

        }

    }
}
