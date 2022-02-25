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
using OpenQA.Selenium.Interactions;
using TestFramework;

namespace SeleniumSpecFlow.Steps
{
    [Binding]
    public class LoginPageSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        private Hearing _hearing;
        private Dictionary<string, IWebDriver> drivers = new Dictionary<string, IWebDriver>();
        public LoginPageSteps(ScenarioContext scenarioContext)
            :base (scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I log in as ""([^""]*)""")]
        public void GivenILogInAs(string userName)
        {
            _scenarioContext.UpdatePageName("Login");
            var result= CommonPageActions.NavigateToPage(Config.URL, "login.microsoftonline.com");
            Login(userName, Config.BambooPassword);
            _scenarioContext.UpdateUserName(userName);
        }
    
        public void Login(string username, string password)
        {
            TestFramework.ExtensionMethods.FindElementWithWait(Driver, LoginPage.UsernameTextfield, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait))).SendKeys(username);
            Driver.FindElement(LoginPage.Next).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
            wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.PasswordField));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.SignIn));
            wait.Until(ExpectedConditions.ElementToBeClickable(LoginPage.BackButton));
            Driver.FindElement(LoginPage.PasswordField).SendKeys(password);
            TestFramework.ExtensionMethods.FindElementWithWait(Driver, LoginPage.SignIn).Click();
        }

        [Given(@"all participants log in to video web")]
        public void GivenAllParticipantsLogInToVideoWeb()
        {
            var participants = new List<Participant>();
            participants.Add(new Participant
            {
                Id = "auto_vw.individual_05@hearings.reform.hmcts.net",
                Party = new Party { Name = "Claimant" },
                Role = new Role { Name = "Litigant in person" }
            });
            participants.Add(new Participant
            {
                Id = "auto_vw.representative_01@hearings.reform.hmcts.net",
                Party = new Party { Name = "Claimant" },
                Role = new Role { Name = "Representative " }
            });
            participants.Add(new Participant
            {
                Id = "auto_vw.individual_06@hearings.reform.hmcts.net",
                Party = new Party { Name = "Defendant" },
                Role = new Role { Name = "Litigant in person " }
            });

            participants.Add(new Participant
            {
                Id = "auto_vw.representative_02@hearings.reform.hmcts.net",
                Party = new Party { Name = "Defendant" },
                Role = new Role { Name = "Solicitor" }
            });

            _hearing = new Hearing
            {
                Case = new Case
                {
                    CaseNumber = "AA98628"
                },
                Judge = new Judge
                {
                    Email = "auto_aw.judge_02@hearings.reform.hmcts.net"
                },
                Participant = participants
            };
            
            var key = string.Empty;
            var username = string.Empty;

            _scenarioContext.Add("Hearing", _hearing);
            foreach (var participant in _hearing.Participant)
            {
                key= $"{participant.Id}#{participant.Party.Name}-{participant.Role.Name}";
                username = participant.Id.Replace("gmail.com", "hearings.reform.hmcts.net");
                SetupBrowsers(key, username, Config.BambooPassword);
            }

            //login for judge
            key = "Judge";
            username = _hearing.Judge.Email;
            SetupBrowsers(key, username, Config.BambooPassword);

            _scenarioContext.Add("drivers", drivers);

            Driver = ((Dictionary<string, IWebDriver>)_scenarioContext["drivers"]).FirstOrDefault(a => a.Key.Contains("Judge")).Value; // Where(a => a.Key.Contains("Judge")).FirstOrDefault().Value
            var el = Driver.FindElements(By.XPath("//tr[@class='govuk-table__row']")).Where(a => a.Text.Contains($"{_hearing.Case.CaseNumber}")).FirstOrDefault();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//tr[contains(.,'AA82653')]//button")));
            var selectButton = Driver.FindElement(By.XPath("//tr[contains(.,'AA82653')]//button"));
            selectButton.Click();
        }


        [Then(@"all participants log in to video web")]
        public void ThenAllParticipantsLogInToVideoWeb()
        {
            Driver?.Close();
   
            _hearing = (Hearing)_scenarioContext["Hearing"];
            foreach (var participant in _hearing.Participant)
            {
                Driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
                _scenarioContext["driver"] = Driver;
                Driver.Navigate().GoToUrl(Config.VideoUrl);
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
                wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.UsernameTextfield));
                drivers.Add($"{participant.Id}#{participant.Party.Name}-{participant.Role.Name}", Driver);
                    
                Login(participant.Id, Config.BambooPassword);
            }
            _scenarioContext.Add("drivers", drivers);
        }

        private void SetupBrowsers(string key,string username,string password)
        {
            var _driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
            _driver.Navigate().GoToUrl(Config.VideoUrl);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
            wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.UsernameTextfield));
            drivers.Add(key, _driver);
            Driver = _driver;
            Login(username, password);
        }
    }
}
