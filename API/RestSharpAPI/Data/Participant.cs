using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpApi.Data
{
    internal class Participant
    {
        public string userName { get; set; }
        public string caseRoleName { get; set; }
        public string hearingRoleName { get; set; }
        public string contactEmail { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string displayName { get; set; }

        public Participant setUserName (string _userName)
        {
            userName = _userName;
            return this;
        }

        public Participant setCaseRoleName(string _caseRoleName)
        {
            caseRoleName = _caseRoleName;
            return this;
        }

        public Participant setHearingRoleName (string _hearingRoleName)
        {
            hearingRoleName = _hearingRoleName;
            return this;
        }

        public Participant setContactEmail(string _contactEmail)
        { contactEmail = _contactEmail; return this; } 

        public Participant setLastName(string _lastName)
        { lastName = _lastName; return this; }

        public Participant setFirstName(string _firstName)
        { firstName = _firstName; return this; }

        public Participant setDisplayName(string _displayName)
        { displayName = _displayName; return this; }

    }
}
