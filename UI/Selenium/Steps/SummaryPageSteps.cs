using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UISelenium.Pages;
using OpenQA.Selenium.Support.UI;
using TestFramework;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using FluentAssertions;

namespace UI.Steps
{
    [Binding]
    public class SummaryPageSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        public SummaryPageSteps(ScenarioContext scenarioContext)
            :base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I book the hearing")]
        public void GivenIBookTheHearing()
        {
            //System.Threading.Thread.Sleep(5000);
            ExtensionMethods.FindElementWithWait(Driver, SummaryPage.BookButton).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SummaryPage.DotLoader));
            if (ExtensionMethods.IsElementVisible(Driver, SummaryPage.TryAgainButton))
            {
                ExtensionMethods.FindElementWithWait(Driver, SummaryPage.TryAgainButton).Click();
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SummaryPage.DotLoader));
            }
        }

        [Then(@"A hearing should be created")]
        public void ThenAHearingShouldBeCreated()
        {
            var successTitle = ExtensionMethods.FindElementWithWait(Driver, SummaryPage.SuccessTitle);
            successTitle.Text.Should().Contain("Your hearing booking was successful");
        }

    }
}
