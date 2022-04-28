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
    }
}
