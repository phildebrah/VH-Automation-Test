using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpApi.Data
{
    internal class Hearing
    {
        public bool Audio_recording_required { get; set; }
        public Guid HearingId { get; set; }
        public DateTime Scheduled_date_time { get; set; }
        public string Hearing_venue_name { get; set; }
        public string CaseType { get; set; }
        public string HearingTypeName { get; set; }
        public int ScheduledDuration { get; set; }
        public IList<Case> Hearing_cases { get; set; }
        public IList<Participant> Hearing_participants { get; set; } 

        public Hearing AddCase(Case _case)
        {
            if (Hearing_cases is null) { Hearing_cases = new List<Case>(); }
            Hearing_cases.Add(_case);
            return this;
        }

        public Hearing AddParticipant(Participant _participant)
        { 
            if (Hearing_participants is null)
                { Hearing_participants = new List<Participant>(); }
                Hearing_participants.Add(_participant); return this; }
    }
}
