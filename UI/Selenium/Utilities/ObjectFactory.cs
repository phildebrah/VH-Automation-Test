
using UISelenium.Pages;
using System;
using TechTalk.SpecFlow;
using TestLibrary.Utilities;
using UI.Steps.CommonActions;
using OpenQA.Selenium;
using UI.Pages;

namespace SeleniumSpecFlow.Utilities
{
    [Binding]
    public class ObjectFactory
    {
        public LoginPage LoginPage { get; set; }
        public CommonPageActions CommonPageActions { get; set; }

        public DashboardPage DashboardPage { get; set; }

        public HearingAssignJudgePage HearingAssignJudgePage { get; set; }

        //public ParticipantHearingListPage ParticipantHearingListPage { get; set; }
        public EnvironmentConfigSettings Config { get; set; }
        public IWebDriver Driver { get; set; }
        public ObjectFactory(ScenarioContext context)
        {
            CommonPageActions = new CommonPageActions((IWebDriver)context["driver"]);
            Config = (EnvironmentConfigSettings)context["config"];
            Driver = (IWebDriver) context["driver"];
        }
        //    public Lazy<Home> Home = new Lazy<Home>(() => new Home(Hooks.Driver));
        //    public Lazy<DriverFactory> DriverFactory = new Lazy<DriverFactory>();


        ////elements
        //    public Lazy<DropdownList> DropdownList = new Lazy<DropdownList>(() => new DropdownList(Hooks.Driver));
        //   // public Lazy<EntryField> EntryField = new Lazy<EntryField>(() => new EntryField(Hooks.Driver));
        //}

    }
}
