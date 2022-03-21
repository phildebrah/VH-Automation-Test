using System;
using DataHelpers;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace TestFramework.Hooks
{
    [Binding]
    public class GlobalHooks
    {
        private Dictionary<UserDto, UserBrowser> _browsers;
        //private Dictionary<UserDto, UserBrowser> _browsers;
        //private static readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        //  No beforefeature binding if there is no common code
        //  No afterfeature binding if there is no common code
        //  No beforescenario binding if there is no common code
        //  No afterscenario binding if there is no common code
        //  No beforestep binding if there is no common code
        //  No afterstep binding if there is no common code

        [BeforeScenario]
        public static void BeforeSeleniumScenario()
        {
            Console.WriteLine("Selenium Test Started in Chrome");
            //_specFlowOutputHelper.WriteLine("Selenium Test Started in Chrome");
        }
        [AfterScenario]
        public static void AfterSeleniumScenario()
        {
            Console.WriteLine("Selenium Test Finished - Drivers Closed");
            //_specFlowOutputHelper.WriteLine("Selenium Test Finished - Drivers Closed");
        }

        [BeforeScenario]
        public static void BeforeRestSharpScenario()
        {
        }

        [AfterScenario]
        public static void AfterRestSharpScenario()
        {
        }

        [BeforeTestRun]
        public static void BeforeTest() { }


        [AfterTestRun]
        public static void AfterTest() { }


        [BeforeFeature]
        public static void BeforeFeature() { }

        [AfterFeature]
        public static void AfterFeature() { }

        [BeforeScenarioBlock]
        public static void BeforeScenarioBlock() { }

        [AfterScenarioBlock]
        public static void AfterScenarioBlock() { }

        [BeforeStep]
        public static void BeforeStep() { }

        [AfterStep]
        public static void AfterStep() { }
    }
}
