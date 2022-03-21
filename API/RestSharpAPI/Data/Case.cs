using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpApi.Data
{
    internal class Case
    {
        public bool Is_lead_case { get; set; } = true;
        public string Name { get; set; }
        public String Number { get; set; }

        public Case() { }

        public Case setIsLeadCase(bool _isLeadCase)
        { Is_lead_case = _isLeadCase;
            return this;
        }

        public Case setName(string _name)
        { Name = _name; return this; }

        public Case setNumber(String _number)
            { Number = _number; return this; }

    }
}
