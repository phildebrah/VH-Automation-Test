using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Model
{
    public class Hearing
    {
        public Case Case { get; set; }=new Case();
        public string Judge { get; set; }
        public List<string> Interpreters { get; set; } = new List<string>();
        public List<string> Participants { get; set; } = new List<string>();
        public string VHO { get; set; }
        public string JOH { get; set; }
        public HearingSchedule HearingSchedule { get; set; } = new HearingSchedule();
    }

    public class Case
    {
        public string CaseNumber { get; set; }
        public string CaseName { get; set; }
        public string CaseType { get; set; }
        public string HearingType { get; set; }
    }

    public class HearingSchedule
    {
        public bool IsMultiHeariang { get; set; }
        public List<DateTime> HearingDate { get; set; }
        public DateTime HearingTime { get; set; }
        public string DurationHours { get; set; }
        public string DurationMinutes { get; set; }
        public string HearingVenue  { get; set; }
        public string HearingRoom { get; set; }
        
    }
}
