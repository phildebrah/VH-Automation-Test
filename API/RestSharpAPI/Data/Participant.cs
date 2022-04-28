using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpApi.Data
{
    internal class Participant
    {
        public string UserName { get; set; }
        public string CaseRoleName { get; set; }
        public string HearingRoleName { get; set; }
        public string ContactEmail { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DisplayName { get; set; }
    }
}
