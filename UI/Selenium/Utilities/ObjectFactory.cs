
using UISelenium.Pages;
using System;

namespace SeleniumSpecFlow.Utilities
{
    public class ObjectFactory
    {
        public Lazy<Home> Home = new Lazy<Home>(() => new Home(Hooks.Driver));
        public Lazy<DriverFactory> DriverFactory = new Lazy<DriverFactory>();


    //elements
        public Lazy<DropdownList> DropdownList = new Lazy<DropdownList>(() => new DropdownList(Hooks.Driver));
        public Lazy<EntryField> EntryField = new Lazy<EntryField>(() => new EntryField(Hooks.Driver));
    }

}
