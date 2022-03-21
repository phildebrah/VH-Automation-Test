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
        public Guid hearingId { get; set; }
        public DateTime Scheduled_date_time { get; set; }
        public string Hearing_venue_name { get; set; }
        public string CaseType { get; set; }
        public string HearingTypeName { get; set; }
        public int ScheduledDuration { get; set; }
        public IList<Case> Hearing_cases { get; set; }
        public IList<Participant> Hearing_participants { get; set; } 

        public Hearing setAudioRecordingRequired(bool _setAudioReqired)
        { Audio_recording_required = _setAudioReqired; return this; }

        public Hearing setId (Guid _hearingId)
        { hearingId = _hearingId; return this; }

        public Hearing setScheduledDateTime(DateTime _setScheduledDateTime)
        { Scheduled_date_time = _setScheduledDateTime; return this; }

        public Hearing setCaseType (string _caseType)
        { CaseType = _caseType; return this; }

        public Hearing setHearingTYpeName(string _hearingTypeName)
        { HearingTypeName = _hearingTypeName; return this; }

        public Hearing setDuration(int _duration)
        { ScheduledDuration = _duration; return this; }

        public Hearing addCase(Case _case)
        {
            if (Hearing_cases is null) { Hearing_cases = new List<Case>(); }
            Hearing_cases.Add(_case);
            return this;
        }

        public Hearing addParticipant(Participant _participant)
        { 
            if (Hearing_participants is null)
                { Hearing_participants = new List<Participant>(); }
                Hearing_participants.Add(_participant); return this; }
    }
}
