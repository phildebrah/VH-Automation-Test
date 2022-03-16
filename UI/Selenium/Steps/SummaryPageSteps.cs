using SeleniumSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using UISelenium.Pages;
using OpenQA.Selenium.Support.UI;
using TestFramework;
using SeleniumExtras.WaitHelpers;
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
            _scenarioContext.UpdatePageName("Hearing summary");
            ExtensionMethods.FindElementWithWait(Driver, SummaryPage.BookButton, _scenarioContext).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SummaryPage.DotLoader));
            if (ExtensionMethods.IsElementExists(Driver, SummaryPage.TryAgainButton, _scenarioContext))
            {
                ExtensionMethods.FindElementWithWait(Driver, SummaryPage.TryAgainButton, _scenarioContext).Click();
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SummaryPage.DotLoader));
            }
        }

        [Then(@"A hearing should be created")]
        public void ThenAHearingShouldBeCreated()
        {
            _scenarioContext.UpdatePageName("Hearing booking confirmation");
            var successTitle = ExtensionMethods.FindElementWithWait(Driver, SummaryPage.SuccessTitle, _scenarioContext);
            successTitle.Text.Should().Contain("Your hearing booking was successful");
        }

        [Then(@"optionally I confirm the booking")]
        public void ThenConfirmTheBooking()
        {
            _scenarioContext.UpdatePageName("Hearing summary");
            ExtensionMethods.FindElementWithWait(Driver, SummaryPage.ViewThisBooking, _scenarioContext).Click();

            ExtensionMethods.FindElementEnabledWithWait(Driver, BookingDetailsPage.ConfirmBookingButton);

            if (ExtensionMethods.IsElementExists(Driver, BookingDetailsPage.ConfirmBookingButton, _scenarioContext))
            {
                ExtensionMethods.FindElementWithWait(Driver, BookingDetailsPage.ConfirmBookingButton, _scenarioContext).Click();

                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.OneMinuteElementWait)));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SummaryPage.DotLoader));
            }
        }
    }
}
