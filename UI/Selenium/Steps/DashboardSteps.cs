using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using TestFramework;
using UI.Model;
using UISelenium.Pages;

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

        [Given(@"I want to create a hearing with case details")]
        public void GivenIWantToCreateAHearingWithCaseDetails(Table table)
        {
            SelectDashboardOption("Book a video hearing");

            CreateCaseModel(table);

            EnterCaseDetails(_hearing.Case);
        }

           
        [Given(@"I want to create a hearing")]
        public void GivenIWantToCreateAHearing(Table table)
        {
           
        }

        public void EnterCaseDetails(Case caseDetails)
        {
            Driver.FindElement(HearingDetailsPage.CaseNumber).SendKeys(caseDetails.CaseNumber);
            Driver.FindElement(HearingDetailsPage.CaseName).SendKeys(caseDetails.CaseName);

            var caseTypeElement = Driver.FindElement(HearingDetailsPage.CaseType);
            var selectElement = new SelectElement(caseTypeElement);
            selectElement.SelectByText(caseDetails.CaseType);

            var heardingTypeElement = Driver.FindElement(HearingDetailsPage.HeardingType);
             selectElement = new SelectElement(heardingTypeElement);
            selectElement.SelectByText(caseDetails.HearingType);

            Driver.FindElement(HearingDetailsPage.NextButton).Click();
        }

        public void SelectDashboardOption(string optionName)
        {
            var isPageLoaded = false;
            var waitPeriod=TimeSpan.FromSeconds(Int32.Parse(Config.DefaultElementWait));
            switch(optionName)
            {
                case "Book a video hearing":
                    Driver.FindElementWithWait(DashboardPage.BookHearingButton, waitPeriod).Click();
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

        private void CreateHeardingModel(Table table)
        {
            if (_scenarioContext.ContainsKey("Hearing"))
            {
                _hearing=_scenarioContext.Get<Hearing>("Hearing");
            }
            else
            {
                _hearing=new Hearing();
                _scenarioContext.Add("Hearing", _hearing);
            }

            var tableRow = table.Rows[0];

            _hearing.Judge=tableRow["Judge"];

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

            _hearing.VHO=tableRow.ContainsKey("VHO") ? tableRow["VHO"] : null;
            _hearing.JOH=tableRow.ContainsKey("Judicial Office Holder") ? tableRow["Judicial Office Holder"] : null;

            _scenarioContext["Hearing"]=_hearing;
        }
        private void CreateCaseModel(Table table)
        {
            if (_scenarioContext.ContainsKey("Hearing"))
            {
                _hearing=_scenarioContext.Get<Hearing>("Hearing");
            }
            else
            {
                _hearing=new Hearing();
                _scenarioContext.Add("Hearing", _hearing);
            }

            var tableRow = table.Rows[0];

            _hearing.Case.CaseNumber=new Random().Next(99, 99999).ToString();
            _hearing.Case.CaseName=$"{tableRow["Case Name"]}-{_hearing.Case.CaseNumber}";
            _hearing.Case.CaseType=tableRow["Case Type"];
            _hearing.Case.HearingType=tableRow["Hearing Type"];

            _scenarioContext["Hearing"]=_hearing;
        }

    }
}
