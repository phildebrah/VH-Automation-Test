using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using TestFramework;
using UI.Model;
using UISelenium.Pages;
using System.Linq;
using System.Collections.Generic;
namespace UI.Steps
{
    [Binding]
    public class DashboardSteps: ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        private Hearing _hearing;
        
        public DashboardSteps(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I want to create a Hearing for")]
        public void GivenIWantToCreateAHearing(Table table)
        {
            SelectDashboardOption("Book a video hearing");
            SetHearingParticipants(table);
        }

        public void SelectDashboardOption(string optionName)
        {
            var isPageLoaded = false;
            var waitPeriod=TimeSpan.FromSeconds(Int32.Parse(Config.DefaultElementWait));
            switch(optionName)
            {
                case "Book a video hearing":
                    ExtensionMethods.FindElementWithWait(Driver, DashboardPage.BookHearingButton).Click();
                    isPageLoaded=IsHeardingDetailsPageLoaded();
                    break;
            }

            isPageLoaded.Should().BeTrue($"cannot load {optionName} page");
        }

        public bool IsHeardingDetailsPageLoaded()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
                wait.Until(ExpectedConditions.ElementIsVisible(HearingDetailsPage.CaseNumber));
               
                return true;
            }
            catch
            {
                //log the exception
                return false;
            }
        }

        private void SetHearingParticipants(Table table)
        {
            if (_scenarioContext.ContainsKey("Hearing"))
            {
                _hearing = _scenarioContext.Get<Hearing>("Hearing");
            }
            else
            {
                _hearing = new Hearing();
                _scenarioContext.Add("Hearing", _hearing);
            }

            var tableRow = table.Rows[0];

            _hearing.Judge = tableRow["Judge"];

            var interpreters = tableRow.ContainsKey("Interpreters") ?
                tableRow["Interpreters"].Split(",") : null;
            foreach (var item in interpreters)
            {
                _hearing.Interpreters.Add(item);
            }

            var participants = tableRow.ContainsKey("Participants") ?
                tableRow["Participants"].Split(",") : null;
            foreach (var item in participants)
            {
                _hearing.Participants.Add(item);
            }

            _hearing.VHO = tableRow.ContainsKey("VHO") ? tableRow["VHO"] : null;
            _hearing.JOH = tableRow.ContainsKey("Judicial Office Holder") ? tableRow["Judicial Office Holder"] : null;

            _scenarioContext["Hearing"] = _hearing;
        }
    }
}
