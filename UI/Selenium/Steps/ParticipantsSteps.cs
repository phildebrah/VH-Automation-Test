using SeleniumSpecFlow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UI.Model;

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

            var tableRow = table.Rows[0];

            var interpreters = tableRow.ContainsKey("Defendant") ?
                tableRow["Defendant"].Split(",") : null;
            foreach (var item in interpreters)
            {
                _hearing.Defendants.Add(item);
            }

            var participants = tableRow.ContainsKey("Claimant") ?
                tableRow["Claimant"].Split(",") : null;
            foreach (var item in participants)
            {
                _hearing.Claimants.Add(item);
            }

            //_hearing.VHO = tableRow.ContainsKey("VHO") ? tableRow["VHO"] : null;
            //_hearing.JOH = tableRow.ContainsKey("Judicial Office Holder") ? tableRow["Judicial Office Holder"] : null;

            _scenarioContext["Hearing"] = _hearing;
        }
    }
}
