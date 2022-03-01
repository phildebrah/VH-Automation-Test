
using UISelenium.Pages;
using System;
using TechTalk.SpecFlow;
using TestLibrary.Utilities;
using UI.Steps.CommonActions;
using OpenQA.Selenium;
using UI.Pages;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumSpecFlow.Utilities
{
    [Binding]
    public class ObjectFactory
    {
        public LoginPage LoginPage { get; set; }
        public CommonPageActions CommonPageActions { get; set; }
        public DashboardPage DashboardPage { get; set; }
        public Dictionary<string, IWebDriver> drivers = new Dictionary<string, IWebDriver>();
        public HearingAssignJudgePage HearingAssignJudgePage { get; set; }
        public EnvironmentConfigSettings Config { get; set; }
        public IWebDriver Driver { get; set; }
        public bool SkipPracticeVideoHearingDemo = true;
        public ObjectFactory(ScenarioContext context)
        {
            CommonPageActions = new CommonPageActions((IWebDriver)context["driver"]);
            Config = (EnvironmentConfigSettings)context["config"];
            Driver = (IWebDriver) context["driver"];
        }
        public IWebDriver GetDriver(string participant, ScenarioContext _scenarioContext)
        {
            return ((Dictionary<string, IWebDriver>)_scenarioContext["drivers"]).Where(a => a.Key.ToLower().Contains(participant.ToLower()))?.FirstOrDefault().Value;
        }
    }
}
