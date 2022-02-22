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

        [Given(@"all participants log in to video web")]
        public void GivenAllParticipantsLogInToVideoWeb()
        {
            Dictionary<string, IWebDriver> drivers = new Dictionary<string, IWebDriver>();
            var participants = new List<Participant>();
            participants.Add(new Participant
            {
                Id="auto_vw.individual_05@hearings.reform.hmcts.net",
                Party=new Party { Name="Claimant" },
                Role=new Role { Name="Litigant in person" }
            });
            participants.Add(new Participant
            {
                Id="auto_vw.representative_01@hearings.reform.hmcts.net",
                Party=new Party { Name="Claimant" },
                Role=new Role { Name="Representative " }
            });
            participants.Add(new Participant
            {
                Id="auto_vw.individual_06@hearings.reform.hmcts.net",
                Party=new Party { Name="Defendant" },
                Role=new Role { Name="Litigant in person " }
            });

            participants.Add(new Participant
            {
                Id="auto_vw.representative_02@hearings.reform.hmcts.net",
                Party=new Party { Name="Defendant" },
                Role=new Role { Name="Solicitor" }
            });

            var hearing = new Hearing
            {
                Case=new Case
                {
                    CaseNumber="AA98628"
                },
                Judge=new Judge
                {
                    Email="auto_aw.judge_02@hearings.reform.hmcts.net"
                },
                Participant=participants
            };

            _scenarioContext.Add("Hearing", hearing);
            foreach (var participant in _hearing.Participant)
            {
                var _driver = new DriverFactory().InitializeDriver(TestConfigHelper.browser);
                _driver.Navigate().GoToUrl(Config.VideoUrl);
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
                wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.UsernameTextfield));
                drivers.Add($"{participant.Id}#{participant.Party.Name}-{participant.Role.Name}", _driver);
                Driver = _driver;
                Login(participant.Id.Replace("gmail.com", "hearings.reform.hmcts.net"), Config.BambooPassword);
            }
            _scenarioContext.Add("drivers", drivers);

            Driver = ((Dictionary<string, IWebDriver>)_scenarioContext["drivers"]).FirstOrDefault(a => a.Key.Contains("Judge")).Value; // Where(a => a.Key.Contains("Judge")).FirstOrDefault().Value
            var el = Driver.FindElements(By.XPath("//tr[@class='govuk-table__row']")).Where(a => a.Text.Contains($"{_hearing.Case.CaseNumber}")).FirstOrDefault();
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
                Login(participant.Id.Replace("gmail.com", "hearings.reform.hmcts.net"), Config.BambooPassword);
            }
            _scenarioContext.Add("drivers", drivers);

            Driver = ((Dictionary<string, IWebDriver>)_scenarioContext["drivers"]).FirstOrDefault(a => a.Key.Contains("Judge")).Value;
            var el = Driver.FindElements(By.XPath("//tr[@class='govuk-table__row']")).Where(a => a.Text.Contains($"{_hearing.Case.CaseNumber}")).FirstOrDefault();
            
            var btnID = el.GetAttribute("id");
            btnID = btnID.Replace("judges-list-", "start-hearing-btn-");
            var btn = el.FindElement(By.Id(btnID));
            btn.Click();
        }
    }
}
