
using UISelenium.Pages;
using System;
using TechTalk.SpecFlow;
using TestFramework.Drivers;
using TestLibrary.Utilities;
using UI.Steps.CommonActions;

namespace SeleniumSpecFlow.Utilities
{
    [Binding]
    public class ObjectFactory
    {
        public LoginPage LoginPage { get; set; }
        public CommonPageActions CommonPageActions { get; set; }
        public EnvironmentConfigSettings Config { get; set; }
        public ObjectFactory(ScenarioContext context)
        {
            LoginPage = new LoginPage((IDriver)context["driver"]);
            CommonPageActions = new CommonPageActions((IDriver)context["driver"]);
            Config = (EnvironmentConfigSettings)context["config"];
        }
        //    public Lazy<Home> Home = new Lazy<Home>(() => new Home(Hooks.Driver));
        //    public Lazy<DriverFactory> DriverFactory = new Lazy<DriverFactory>();


        ////elements
        //    public Lazy<DropdownList> DropdownList = new Lazy<DropdownList>(() => new DropdownList(Hooks.Driver));
        //   // public Lazy<EntryField> EntryField = new Lazy<EntryField>(() => new EntryField(Hooks.Driver));
        //}

    }
}
