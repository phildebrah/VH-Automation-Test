using OpenQA.Selenium.Support.UI;
using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestFramework;
using UI.Model;
using UISelenium.Pages;
using TestLibrary.Utilities;
using SeleniumExtras.WaitHelpers;

namespace UI.Steps
{
    [Binding]
    public class ParticipantsSteps : ObjectFactory
    {
        private readonly ScenarioContext _scenarioContext;
        private Hearing _hearing;
        public ParticipantsSteps (ScenarioContext context)
            :base(context)
        {
            _scenarioContext = context;
        }

        [Given(@"I want to create a Hearing for")]
        public void GivenIWantToCreateAHearingFor(Table table)
        {
            SetHearingParticipants(table);
            EnterParticipants();
        }


        private void SetHearingParticipants(Table table)
        {
            if (_scenarioContext.ContainsKey("Hearing"))
            {
                _hearing = _scenarioContext.Get<Hearing>("Hearing");
            }
            else
            {
                _hearing = new Hearing();
                _scenarioContext.Add("Hearing", _hearing);
            }
 
            foreach (var row in table.Rows)
            {
                var participant = new Participant
                {
                    Party = new Party
                    {
                        Name = row["Party"]
                    },
                    Role = new Role
                    {
                        Name = row["Role"]
                    }
                };

                _hearing.Participant.Add(participant);
            }

            _scenarioContext["Hearing"] = _hearing;
        }

        public void EnterParticipants()
        {
            foreach (var participant in _hearing.Participant)
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(int.Parse(Config.DefaultElementWait)));
                ExtensionMethods.WaitForDropDownListItems(Driver, ParticipantsPage.PartyDropdown);
                new SelectElement(ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.PartyDropdown)).SelectByText(participant.Party.Name);
                new SelectElement(ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.RoleDropdown)).SelectByText(participant.Role.Name);
                ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.ParticipantEmailTextfield).SendKeys($"{ Util.RandomString(8)}@email.com" );
                ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.FirstNameTextfield).SendKeys($"AutoFirst{Util.RandomAlphabet(4)}");
                ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.LastNameTextfield).SendKeys($"AutoLast{Util.RandomAlphabet(4)}");
                ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.PhoneTextfield).SendKeys("07021234567");
                if (ExtensionMethods.IsElementVisible(Driver, ParticipantsPage.RepresentingTextfield))
                {
                    ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.RepresentingTextfield).SendKeys($"AutoRepresent{Util.RandomAlphabet(4)}");
                    ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.RepOrganisationTextfield).SendKeys($"AutoOrg{Util.RandomAlphabet(4)}");
                }
                ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.DisplayNameTextfield).SendKeys($"AutoDisplay{Util.RandomAlphabet(4)}");
                ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.AddParticipantLink).Click();
            }

            ExtensionMethods.FindElementWithWait(Driver, ParticipantsPage.NextButton).Click();
        }
    }
}
