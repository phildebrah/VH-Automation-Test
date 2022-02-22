using TechTalk.SpecFlow;
using SeleniumSpecFlow.Utilities;
using UISelenium.Pages;
using FluentAssertions;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras;
using OpenQA.Selenium.Support.UI;
using System;
using UI.Model;
using TestLibrary.Utilities;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;

namespace SeleniumSpecFlow.Steps
{
    [Binding]
    public class LoginPageSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        private Hearing _hearing;
        public LoginPageSteps(ScenarioContext scenarioContext)
            :base (scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I log in as ""([^""]*)""")]
        public void GivenILogInAs(string userName)
        {
            var result= CommonPageActions.NavigateToPage(Config.URL, "login.microsoftonline.com");
            Login(userName, Config.BambooPassword);
        }
    
        public void Login(string username, string password)
        {
            TestFramework.ExtensionMethods.FindElementWithWait(Driver, LoginPage.UsernameTextfield, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait))).SendKeys(username);
            Driver.FindElement(LoginPage.Next).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.PasswordField));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.SignIn));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.BackButton));
            Driver.FindElement(LoginPage.PasswordField).SendKeys(password);
            TestFramework.ExtensionMethods.FindElementWithWait(Driver, LoginPage.SignIn).Click();
        }

        [Then(@"all participants log in to video web")]
        public void ThenAllParticipantsLogInToVideoWeb()
        {
            Driver?.Close();
            Dictionary<string, IWebDriver> drivers = new Dictionary<string, IWebDriver>();
            _hearing = (Hearing)_scenarioContext["Hearing"];
            foreach (var participant in _hearing.Participant)
            {
                var _driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
                _driver.Navigate().GoToUrl(Config.VideoUrl);
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
                wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.UsernameTextfield));
                drivers.Add($"{participant.Id}#{participant.Party.Name}-{participant.Role.Name}", _driver);
                Driver = _driver;
                Login(participant.Id, Config.BambooPassword);
            }
            _scenarioContext.Add("drivers", drivers);

            Driver = ((Dictionary<string, IWebDriver>)_scenarioContext["drivers"]).FirstOrDefault(a => a.Key.Contains("Judge")).Value; // Where(a => a.Key.Contains("Judge")).FirstOrDefault().Value
        }
    }
}
