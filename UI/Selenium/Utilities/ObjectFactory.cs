
using UISelenium.Pages;
using System;
using TechTalk.SpecFlow;
using TestFramework.Drivers;
using TestLibrary.Utilities;

namespace SeleniumSpecFlow.Utilities
{
    [Binding]
    public class ObjectFactory
    {
        public Lazy<Home> Home = new Lazy<Home>(() => new Home(Hooks.Driver));
        public Lazy<DriverFactory> DriverFactory = new Lazy<DriverFactory>();

        ////elements
        //    public Lazy<DropdownList> DropdownList = new Lazy<DropdownList>(() => new DropdownList(Hooks.Driver));
        //   // public Lazy<EntryField> EntryField = new Lazy<EntryField>(() => new EntryField(Hooks.Driver));
        //}

    }
}
