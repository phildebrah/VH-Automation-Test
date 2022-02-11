
using UISelenium.Pages;
using System;
using TestLibrary.Utilities;

namespace SeleniumSpecFlow.Utilities
{
    public class ObjectFactory
    {
        public Lazy<Home> Home = new Lazy<Home>(() => new Home(Hooks.Driver));
        public Lazy<DropdownList> DropdownList = new Lazy<DropdownList>(() => new DropdownList(Hooks.Driver));      
        public Lazy<DriverFactory> DriverFactory = new Lazy<DriverFactory>();
       
    }

}
